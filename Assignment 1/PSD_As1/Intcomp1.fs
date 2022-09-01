module Intcomp1

type expr = 
  | CstI of int
  | Var of string
  | Let of (string * expr) list * expr // 2.1
  | Prim of string * expr * expr

(* Some closed expressions: *)

//let e1 = Let([("z", CstI 17)], Prim("+", Var "z", Var "z"))

//let e2 = Let([("z", CstI 17)], 
//             Prim("+", Let([("z", CstI 22)], Prim("*", CstI 100, Var "z")),
//                       Var "z"))

//let e3 = Let([("z", Prim("-", CstI 5, CstI 4))], 
//             Prim("*", CstI 100, Var "z"))

//let e4 = Prim("+", Prim("+", CstI 20, Let([("z", CstI 17)], 
//                                          Prim("+", Var "z", CstI 2))),
//                   CstI 30)

//let e5 = Prim("*", CstI 2, Let([("x", CstI 3)], Prim("+", Var "x", CstI 4)))

//let e6 = Let([("z", Var "x")], Prim("+", Var "z", Var "x"))
//let e7 = Let([("z", CstI 3)], Let([("y", Prim("+", Var "z", CstI 1))], Prim("+", Var "z", Var "y")))
//let e8 = Let([("z", Let([("x", CstI 4)], Prim("+", Var "x", CstI 5)))], Prim("*", Var "z", CstI 2))
//let e9 = Let([("z", CstI 3)], Let([("y", Prim("+", Var "z", CstI 1))], Prim("+", Var "x", Var "y")))
//let e10 = Let([("z", Prim("+", Let([("x", CstI 4)], Prim("+", Var "x", CstI 5)), Var "x"))], Prim("*", Var "z", CstI 2))

(* ---------------------------------------------------------------------- *)

(* Evaluation of expressions with variables and bindings *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Let(l, ebody) -> //2.1
        List.fold (fun acc (s, e) -> (s, eval e env) :: acc ) env l |> 
        eval ebody
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim _            -> failwith "unknown primitive"

//let run e = eval e []
//let res = List.map run [e1;e2;e3;e4;e5;e7]  (* e6 has free variables *)


(* ---------------------------------------------------------------------- *)

(* Closedness *)

// let mem x vs = List.exists (fun y -> x=y) vs

let rec mem x vs = 
    match vs with
    | []      -> false
    | v :: vr -> x=v || mem x vr


(* ---------------------------------------------------------------------- *)

(* Free variables *)

(* Operations on sets, represented as lists.  Simple but inefficient;
   one could use binary trees, hashtables or splaytrees for
   efficiency.  *)

(* union(xs, ys) is the set of all elements in xs or ys, without duplicates *)

let rec union (xs, ys) = 
    match xs with 
    | []    -> ys
    | x::xr -> if mem x ys then union(xr, ys)
               else x :: union(xr, ys)

(* minus xs ys  is the set of all elements in xs but not in ys *)

let rec minus (xs, ys) = 
    match xs with 
    | []    -> []
    | x::xr -> if mem x ys then minus(xr, ys)
               else x :: minus (xr, ys)

(* Find all variables that occur free in expression e *)

let rec freevars e : string list =
    match e with
    | CstI i -> []
    | Var x  -> [x]
    | Let(l, ebody) -> 
        List.fold (fun acc (s, e) -> union (freevars e, minus (freevars ebody, [s])) @ acc) [] l
    | Prim(ope, e1, e2) -> union (freevars e1, freevars e2)


(* ---------------------------------------------------------------------- *)

(* Compilation to target expressions with numerical indexes instead of
   symbolic variable names.  *)

type texpr =                            (* target expressions *)
  | TCstI of int
  | TVar of int                         (* index into runtime environment *)
  | TLet of texpr * texpr               (* erhs and ebody                 *)
  | TPrim of string * texpr * texpr


(* Map variable name to variable index at compile-time *)

let rec getindex vs x = 
    match vs with 
    | []    -> failwith "Variable not found"
    | y::yr -> if x=y then 0 else 1 + getindex yr x

(* Compiling from expr to texpr *)
// 2.3
let rec tcomp (e : expr) (cenv : string list) : texpr =
    match e with
    | CstI i -> TCstI i
    | Var x  -> TVar (getindex cenv x)
    | Let ((s, exp) :: xs, ebody) -> 
        let cenv1 = s :: cenv 
        TLet (tcomp (Let (xs, exp)) cenv, tcomp (Let (xs, ebody)) cenv1)
    | Prim (ope, e1, e2) -> TPrim(ope, tcomp e1 cenv, tcomp e2 cenv)


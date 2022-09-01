(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)]

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x

let cvalue = lookup env "c"


(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr //1.1.IV

let e1 = CstI 17

let e2 = Prim("+", CstI 3, Var "a")

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a")

let e4 = Prim("max", CstI 3, CstI 4) //1.1.II


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim("==", e1, e2) -> if eval e1 env = eval e2 env then 1 else 0 //1.1.I
    | Prim("max", e1, e2) -> //1.1.I
        let e1 = eval e1 env
        let e2 = eval e2 env
        if e1>e2 then e1 else e2
    | Prim("min", e1, e2) -> //1.1.I
        let e1 = eval e1 env
        let e2 = eval e2 env
        if e1>e2 then e2 else e1
    | Prim _ -> failwith "unknown primitive"

let rec eval2 e (env : (string * int) list) : int = //1.1.III
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | If (e1, e2, e3) -> if eval2 e1 env = 1 then eval2 e2 env else eval2 e3 env
    | Prim(op, e1, e2) -> 
        let r1 = eval e1 env
        let r2 = eval e2 env
        match op with
        | "+" -> r1 + r2
        | "*" -> r1 * r2
        | "-" -> r1 - r2
        | "==" -> if e1 = e2 then 1 else 0
        | "max" -> if r1>r2 then r1 else r2
        | "min" -> if r1<r2 then r1 else r2
    | Prim _ -> failwith "unknown primitive"

let e1v  = eval e1 env
let e2v1 = eval e2 env
let e2v2 = eval e2 [("a", 314)]
let e3v  = eval e3 env
let e4v = eval e4 env //1.1.II


//1.2.I
type aexpr = 
    | CstI of int
    | Var of string
    | Add of aexpr * aexpr
    | Mul of aexpr * aexpr
    | Sub of aexpr * aexpr

//1.2.II
let a1 = Sub (Var "v", Add (Var "w", Var "z"))
let a2 = Mul (CstI 2, Sub (Var "v", Add (Var "w", Var "z")))
let a3 = Add (Var "x", Add (Var "y", Add (Var "z", Var "v")))

//1.2.III
let rec fmt a : string =
    match a with
    | CstI i -> string i
    | Var s -> s
    | Add (e1, e2) -> $"({fmt e1} + {fmt e2})"
    | Mul (e1, e2) -> $"({fmt e1} * {fmt e2})"
    | Sub (e1, e2) -> $"({fmt e1} - {fmt e2})"

//fmt a1
//fmt a2
//fmt a3

//1.2.IV
let rec simplify a =
    match a with
    | Add (a1,a2) -> 
        let r1 = simplify a1
        let r2 = simplify a2
        match r1, r2 with
        | CstI 0, r2 -> r2
        | r1, CstI 0 -> r1
        | _ -> a
    | Sub (a1,a2) -> 
        let r1 = simplify a1
        let r2 = simplify a2
        match r1, r2 with
        | r1, CstI 0 -> r1
        | r1, r2 when r1 = r2 -> CstI 0
        | _ -> a
    | Mul (a1, a2) ->
        let r1 = simplify a1
        let r2 = simplify a2
        match r1, r2 with
        | CstI 0, r2 -> CstI 0
        | r1, CstI 0 -> CstI 0
        | CstI 1, r2 -> r2
        | r1, CstI 1 -> r1
        | _ -> a
    | _ -> a

//let a4 = Mul (Add (CstI 1, CstI 0), Add (Var "x", CstI 0))

//simplify a4

//exersise 1.2.V
let rec diff a var = 
    match a with
    | CstI _ -> CstI 0
    | Var v when v = var -> CstI 1
    | Add (a1, a2) -> Add (diff a1 var, diff a2 var)
    | Sub (a1, a2) -> Sub (diff a1 var, diff a2 var)
    | Mul (a1, a2) -> Add (Mul (diff a1 var, a2), Mul (a1, diff a2 var))


let rec aeval a (env : (string * int) list) =
    match a with
    | CstI i -> i
    | Var s -> lookup env s
    | Add (a1, a2) -> aeval a1 env + aeval a2 env
    | Mul (a1, a2) -> aeval a1 env * aeval a2 env
    | Sub (a1, a2) -> aeval a1 env - aeval a2 env

//aeval a4 [("x", 3)]

// exersise 1.4 can be found in Ex14.cs and the examples are in Program.fs
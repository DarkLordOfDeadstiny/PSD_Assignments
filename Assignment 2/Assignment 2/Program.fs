// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//open Intcomp1

type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr

type sinstr =
  | SCstI of int                        (* push integer           *)
  | SVar of int                         (* push variable from env *)
  | SAdd                                (* pop args, push sum     *)
  | SSub                                (* pop args, push diff.   *)
  | SMul                                (* pop args, push product *)
  | SPop                                (* pop value/unbind var   *)
  | SSwap                             (* exchange top and next  *)

let e1 = Let("z", CstI 17, Prim("+", Var "z", Var "z"));

type rtvalue =
  | Bound of string                     (* A bound variable       *)
  | Intrm                               (* An intermediate result *)


(* Compilation to a list of instructions for a unified-stack machine *)
let rec getindex env x = 
    match env with 
    | [] -> raise (Failure "Variable not found")
    | y::yr -> if x=y then 0 else 1 + getindex yr x;
    
let rec scomp e (cenv : rtvalue list) : sinstr list =
    match e with
      | CstI i -> [SCstI i]
      | Var x  -> [SVar (getindex cenv (Bound x))]
      | Let(x, erhs, ebody) -> 
            scomp erhs cenv @ scomp ebody (Bound x :: cenv) @ [SSwap; SPop]
      | Prim("+", e1, e2) -> 
            scomp e1 cenv @ scomp e2 (Intrm :: cenv) @ [SAdd] 
      | Prim("-", e1, e2) -> 
            scomp e1 cenv @ scomp e2 (Intrm :: cenv) @ [SSub] 
      | Prim("*", e1, e2) -> 
            scomp e1 cenv @ scomp e2 (Intrm :: cenv) @ [SMul] 
      | Prim _ -> raise (Failure "scomp: unknown operator")

let s1 = scomp e1 []
 

let rec seval (inss : sinstr list) (stack : int list) =
    match (inss, stack) with
    | ([], v :: _) -> v
    | ([], [])     -> failwith "seval: no result on stack"
    | (SCstI i :: insr,          stk) -> seval insr (i :: stk) 
    | (SVar i  :: insr,          stk) -> seval insr (List.item i stk :: stk) 
    | (SAdd    :: insr, i2::i1::stkr) -> seval insr (i1+i2 :: stkr)
    | (SSub    :: insr, i2::i1::stkr) -> seval insr (i1-i2 :: stkr)
    | (SMul    :: insr, i2::i1::stkr) -> seval insr (i1*i2 :: stkr)
    | (SPop    :: insr,    _ :: stkr) -> seval insr stkr
    | (SSwap   :: insr, i2::i1::stkr) -> seval insr (i1::i2::stkr)
    | _ -> failwith "seval: too few operands on stack"

let assemble list =
    let mapper s =
        match s with
        | SCstI i -> [0; i]
        | SVar i -> [1; i]
        | SAdd -> [2]
        | SSub -> [3]
        | SMul -> [4]
        | SPop -> [5]
        | SSwap -> [6]
        | _ -> failwith "Unknown instruction"
        
    List.fold (fun acc x -> acc @ (mapper x)) [] list

let intsToFile (inss : int list) (fname : string) = 
    let text = String.concat " " (List.map string inss)
    System.IO.File.WriteAllText(fname, text);;


let ints = assemble (scomp e1 [])

intsToFile ints "is1.txt"

//BCD
//2.1
//a : /\b0*42/
//b : /^(?!0*42\b)\d+/
//c : /^(0*[5-9]\d|(0+)?[1-9][0-9]{2,}|0*4[3-9])$/

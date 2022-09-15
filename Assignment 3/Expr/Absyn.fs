(* File Expr/Absyn.fs
   Abstract syntax for the simple expression language 
 *)

module Absyn

//If added for exercise 3.7

type expr = 
  | CstI of int
  | Var of string
  | Let of string * expr * expr
  | Prim of string * expr * expr
  | If of expr * expr * expr 

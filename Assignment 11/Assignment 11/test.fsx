#r "nuget: FsLexYacc.Runtime"
#load "Absyn.fs" 
#load "CPar.fs"
#load "CLex.fs" 
#load "Parse.fs"
#load "Machine.fs"
#load "ContComp.fs"
//load the resst of your files here

//remember to open a module if you need it
//open ParseAndComp

open Contcomp

contCompileToFile (Parse.fromFile "ex16.c") "ex16"

contCompileToFile (Parse.fromFile "ex12_2.c") "ex12_2"


//[LDARGS; CALL (1, "L1"); STOP; Label "L1"; GETBP; LDI; IFZERO "L3";
//   GOTO "L2"; Label "L3"; CSTI 1111; PRINTI; INCSP -1; Label "L2"; CSTI 2222;
//   PRINTI; RET 1]
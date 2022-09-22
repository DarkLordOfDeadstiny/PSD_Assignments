#r "nuget: FsLexYacc.Runtime"


#load "Absyn.fs"
#load "ExprPar.fs"
#load "ExprLex.fs"
#load "Parse.fs"
#load "Expr.fs"

open Expr

compString "1 + 2 * 3"
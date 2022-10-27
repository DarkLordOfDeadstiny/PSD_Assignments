#r "nuget: FsLexYacc.Runtime"
#load "Absyn.fs"
#load "CPar.fs"
#load "CLex.fs"
#load "Parse.fs"
#load "Interp.fs"
#load "ParseAndRun.fs"

open ParseAndRun

fromFile "ex1.c";;
// run (fromFile "ex1.c") [17];;
//run (fromFile "ex5.c") [2];;
// run (fromFile "ex11.c") [8];;
run (fromFile "7_2.c") [15];;
run (fromFile "7_3.c") [15];;

run (fromFile "7_5.c") [15];;
#r "nuget: FsLexYacc.Runtime"
#load "Absyn.fs" 
#load "CPar.fs"
#load "CLex.fs" 
#load "Parse.fs"
#load "Machine.fs" 
#load "Comp.fs"
#load "ParseAndComp.fs"


//fsharpi -r ~/fsharp/FsLexYacc.Runtime.dll Absyn.fs CPar.fs CLex.fs Parse.fs Machine.fs Comp.fs ParseAndComp.fs   

open ParseAndComp;;
compileToFile (fromFile "examples/ex11.c") "examples/ex11.out";;
compile "examples/ex11";;

compileToFile (fromFile "examples/ex3.c") "examples/ex3.out";;
compile "examples/ex3";;

compileToFile (fromFile "examples/ex5.c") "examples/ex5.out";;
compile "examples/ex5";;

compileToFile (fromFile "examples/ex5.c") "examples/ex5.out";;
compile "examples/ex5";;

compileToFile (fromFile "examples/7_5.c") "examples/7_5.out";;
compile "examples/7_5";;

compile "examples/8_5";;


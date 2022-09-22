#r "nuget: FsLexYacc.Runtime"
#load "Absyn.fs"
#load "Fun.fs"
#load "FunPar.fs"
#load "FunLex.fs"
#load "Parse.fs"
#load "ParseAndRun.fs"

open Absyn;;
open Fun;;
let res = run (Prim("+", CstI 5, CstI 7));;


open Parse;;
let e1 = fromString "5+7"
let e2 = fromString "let y = 7 in y + 2 end"
let e3 = fromString "let f x = x + 7 in f 2 end"

open ParseAndRun
run (fromString "5+7")
run (fromString "let y = 7 in y + 2 end")
run (fromString "let f x = x + 7 in f 2 end")


run (fromString "let sum x = x + 7 in sum 1000 end")

fromString "let sum x = if 0 < x then x + sum (x-1) else 0 in sum 1000 end" |> run

fromString "let mult3 x = if 0 < x then 3 * mult3 (x-1) else 1 in mult3 8 end" |> run

fromString "
    let mult3 x =
        if 
            0 < x 
        then
            3 * mult3 (x-1) 
        else
            1 
        in let sum y =
             if -1 < y then (mult3 y) + sum (y-1) else 0 in sum 11 end
    end" |> run


fromString ""
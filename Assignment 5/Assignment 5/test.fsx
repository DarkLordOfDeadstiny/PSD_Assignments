#r "nuget: FsLexYacc.Runtime"
#load "Absyn.fs"
//#load "Fun.fs"
#load "FunPar.fs"
#load "FunLex.fs"
#load "Parse.fs"
#load "HigherFun.fs"
#load "ParseAndRunHigher.fs"
#load "TypeInference.fs"

open TypeInference
open ParseAndRunHigher

// 5.1

let merge (l1, l2) =
    let rec merge' l1 l2 acc =
        match l1, l2 with
        | [], [] -> acc
        | [], l2 -> acc @ l2
        | l1, [] -> acc @ l1
        | h1::t1, h2::t2 ->
            if h1 < h2 then merge' t1 l2 (acc @ [h1])
            else merge' l1 t2 (acc @ [h2])
    merge' l1 l2 []

merge ([3;5;12], [2;3;4;7])


// 5.7




let parseAndTypeCheck (s : string) =
    let e = Parse.fromString s
    let t = inferType e
    printfn "%A" t


parseAndTypeCheck "let x = [1;2;3;4;5] in x + 4"
parseAndTypeCheck "let x = 3 in x + t"


run (fromString @"let twice f = let g x = f(f(x)) in g end 
                    in let mul3 z = z*3 in twice mul3 2 end end")

"let add x = let f y = x+y in f end
in add 2 5 end" |> fromString |> run


"let add x = let f y = x+y in f end
in let addtwo = add 2
  in addtwo 5 end
end" |> fromString |> run


"let add x = let f y = x+y in f end
in let addtwo = add 2
  in let x = 77 in addtwo 5 end
  end
end" |> fromString |> run


"let add x = let f y = x+y in f end
in add 2 end" |> fromString |> run

"fun x -> 2*x" |> fromString |> run
"let y = 22 in fun z -> z+y end" |> fromString |> run

"let add x = fun y -> x+y in add 2 5 end" |> fromString |> run

"let add = fun x -> fun y -> x+y in add 2 5 end" |> fromString |> run
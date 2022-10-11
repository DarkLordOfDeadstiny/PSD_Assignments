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

let parseAndTypeCheck (s : string) =
    let e = Parse.fromString s
    let t = inferType e
    printfn "%A" t



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

// 6.5 -----------------------

// a --------------

"let f x = 1 in f f end" |> parseAndTypeCheck

"let f g = g g in f end" |> parseAndTypeCheck

"let f x =
  let g y = y
  in g false end
in f 42 end" |> parseAndTypeCheck

"let f x =
  let g y = if true then y else x
  in g false end
in f 42 end" |> parseAndTypeCheck


"let f x =
  let g y = if true then y else x
  in g false end
in f true end" |> parseAndTypeCheck

// b --------------

//bool -> bool
"let f x = true = x in f end" |> parseAndTypeCheck

// int -> int
"let f x = 1 + x in f end" |> parseAndTypeCheck
 
//(int -> (int -> int))
"let f x =
  let g y = if true then y+1 else x+1
  in g end
in f end" |> parseAndTypeCheck

//('h -> ('g -> 'h))
"let f x =
  let g y = x
  in g end
in f end" |> parseAndTypeCheck

//('g -> ('h -> 'h))
"let f x =
  let g y = y
  in g end
in f end" |> parseAndTypeCheck

// (’a -> ’b) -> (’b -> ’c) -> (’a -> ’c) 
// should be correct, but fun z -> y (x z) is not typed correctly
"let g x =
  let h y = fun z -> y (x z)
  in  h end
in g end" |> parseAndTypeCheck

let f (x: 'a -> 'b) (y: 'b -> 'c): ('a -> 'c) = 
    fun z -> y (x z)


// ('j -> 'k)
"let g x =
  let h y = y x
  in fun z -> h z end
in g end" |> parseAndTypeCheck

// 'b
// i guess this is wrong?
"fun x -> x" |> parseAndTypeCheck
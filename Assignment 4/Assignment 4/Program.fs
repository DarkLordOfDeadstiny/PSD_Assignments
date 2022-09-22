// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open Absyn;;
open Fun;;
let res = run (Prim("+", CstI 5, CstI 7)) |> printf "%A\n"


open Parse;;
let e1 = fromString "5+7" |> printf "%A\n"
let e2 = fromString "let y = 7 in y + 2 end" |> printf "%A\n"
let e3 = fromString "let f x = x + 7 in f 2 end" |> printf "%A\n"

open ParseAndRun;;
run (fromString "5+7") |> printf "%A\n"
run (fromString "let y = 7 in y + 2 end") |> printf "%A\n"
run (fromString "let f x = x + 7 in f 2 end") |> printf "%A\n"

//exercise 4.2
//Compute the sum of the numbers from 1000 down to 1. Do this by defining a function sum n that computes the sum n+(n−1) + ···+2+1. (Use straightforward summation, no clever tricks.)
let sum n =
    let rec sum' n acc =
        if n = 0 then acc
        else sum' (n-1) (acc+n)
    sum' n 0

sum 1000 |> printf "%A\n"

fromString "let sum x = if 0 < x then x + sum (x-1) else 0 in sum 1000 end" |> run |> printf "%A\n"

//Compute the number 3^8, that is, 3 raised to the power 8. Again, use a recursive function.

fromString "let mult3 x = if 0 < x then 3 * mult3 (x-1) else 1 in mult3 8 end" |> run |> printf "%A\n"

//Compute 3^0 + 3^1 + ···+3^10 + 3^11, using a recursive function (or two, if you prefer).

fromString "
    let mult3 x =
        if 0 < x 
        then 3 * mult3 (x-1) 
        else 1
        in 
        let sum y =
             if -1 < y
             then (mult3 y) + sum (y-1) 
             else 0 
             in 
             sum 11 
        end
    end" |> run |> printf "%A\n"

let rec test x y =
    if 0 <= y then (pown x y) + test x (y-1)
    else 0

//test 3 11 |> printf "%A\n"


//Compute 1^8 +2^8 + ··· + 10^8, again using a recursive function (or two).

fromString "
    let mult3 x =
        let aux z =
            if 0 < z
            then x * aux (z-1) 
            else 1
            in
            aux 8
            end
        in 
        let sum y =
             if 0 < y
             then (mult3 y) + sum (y-1) 
             else 0 
             in 
             sum 10
        end
    end" |> run |> printf "%A\n"



// exercise 4.4
//"let pow x n = if n=0 then 1 else x * pow x (n-1) in pow 3 8 end
//let max2 a b = if a<b then b else a 
//in let max3 a b c = max2 a (max2 b c) 
//   in max3 25 6 62 end
//end"
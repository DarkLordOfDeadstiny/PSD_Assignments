#load "Icon.fs" 
//load the resst of your files here

//remember to open a module if you need it
//open ParseAndComp

let rec len xs =
    match xs with 
    | [] -> 0 
    | x::xr -> 1 + len xr


//rewrite len using a continuation function
let rec lenc xs k = 
    match xs with
    | [] -> k 0
    | x::xr -> lenc xr (fun x -> k (1+x))

lenc [2; 5; 7] id

lenc [2; 5; 7] (printf "The answer is ’%d’\n")

lenc [2; 5; 7] (fun v -> 2*v)

let rec leni xs acc =
    match xs with
    | [] -> acc
    | x::xs -> leni xs (acc+1)

leni [2; 5; 7] 0

let rec rev xs =
    match xs with
    | [] -> []
    | x::xr -> rev xr @ [x]

//rev using continuation function
let rec revc xs k =
    match xs with
    | [] -> k []
    | x::xr -> revc xr (fun v -> k (v @ [x]))

revc [2; 5; 7] id

revc [2; 5; 7] (fun v -> v @ v)

//rev using tailrecursion
let rec revi xs acc =
    match xs with
    | [] -> acc
    | x::xr -> revi xr (x::acc)

revi [2; 5; 7] []


let rec prod xs =
    match xs with
    | [] -> 1
    | x::xr -> x * prod xr

//prod using continuation function
let rec prodc xs k =
    match xs with
    | [] -> k 1
    | x::xr -> prodc xr (fun v -> k (x*v))
   
prodc [2; 5; 7] id

let rec prodc' xs k =
    match xs with
    | [] -> k 1
    | 0::_ -> printfn "hej jonas, you like this niels?"; 0
    | x::xr -> prodc' xr (fun v -> k (x*v))

prodc' [2; 0; 5; 7] id

//prod' using accumulator
let rec prodi xs acc =
    match xs with
    | [] -> acc
    | 0::_ -> 0
    | x::xr -> prodi xr (x*acc)


open Icon
run (Every (Write (Prim ("*", CstI 2, FromTo (1, 4)))))

run (Every (Write (Prim ("+", Prim ("*", CstI 2, FromTo (1, 2)), CstI 1))))

run (Every (Write (Prim ("+", Prim ("*", CstI 10, FromTo (2, 4)), FromTo (1, 2)))))

run (Write (Prim ("<", CstI 50, Prim ("*", CstI 7, FromTo (1, 10)))))

run (Every (Write (Prim1 ("sqr", FromTo (3, 6)))))

run (Every (Write (Prim1 ("even", FromTo (1, 7)))))

run (Every (Write (Prim1 ("mult", Write(FromTo (1, 7))))))
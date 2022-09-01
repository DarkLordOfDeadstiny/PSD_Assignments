open Ex14

//1.4.II
let e2 = Mul (Add (CstI 5, CstI 0), Add (Var "x", CstI 2))
let e3 = Mul (CstI 2, Sub (Var "v", Add (Var "w", Var "z")))
let e4 = Add (Var "x", Add (Var "y", Add (Var "z", Var "v")))
printf "%O\n" e2
printf "%O\n" e3
printf "%O\n" e4

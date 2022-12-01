// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open Contcomp

contCompileToFile (Parse.fromFile "ex16.c") "ex16.c"
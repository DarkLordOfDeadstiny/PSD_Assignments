// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

open ParseAndComp;;
compileToFile (fromFile "ex11.c") "ex11.out" |> ignore
compile "ex11" |> ignore


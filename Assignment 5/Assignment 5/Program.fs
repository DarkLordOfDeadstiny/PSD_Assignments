// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

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
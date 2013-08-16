let mutable tests = []
let test f = tests <- List.append tests [f]
let mutable before = fun () -> ()
let mutable after = fun () -> ()
let run _ = tests |> List.map (fun f -> before(); f(); after())
let (==) value1 value2 = printfn "%b expected: %A got: %A" (value1 = value2) value2 value1

before <- (fun _ -> printfn "before each test")
after <- (fun _ -> printfn "after each test")

test (fun _ -> 1 == 1)
test (fun _ -> 1 == 2)
test (fun _ -> "string" == "string")

run ()

System.Console.ReadKey()
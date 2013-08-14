//repl

//Full type inference

//first class functions
let hello name = printfn "Hello %s" name
hello "Chris"

let people = ["Chris"; "Marcos"; "Andy"]

people |> List.iter hello 

//Immutable by default, opt in mutability
let a = 1
a = 1
//a <- 1

let mutable b = 1
b = 1
b <- 2

//New data types: Discriminated Unions, Record Types, Tuples
//Discriminated Union
type role =    
    | Developer
    | QA
    | Manager of int

let about person =
    match person with
    | (name, Developer) -> printfn "%s in Developer!" name
    | (name, QA) -> printfn "%s is in QA!" name
    | (name, Manager(reports)) -> printfn "%s is a Manger with %i reports!" name reports
    //| _ -> failwith "wtf?"

//Tuples
let peopleAndRoles = [("Chris", QA); ("Marcos", Manager(3)); ("Andy", Developer)]

peopleAndRoles |> List.iter about


//Records
type PersonAndRole = { Name : string; Role : role}
let peopleAndRolesRecord = [{ Name = "Chris"; Role = QA }; { Name = "Marcos"; Role = Manager(3) }; { Name = "Andy"; Role = Developer}]

let about2 person =
    match person with
    | { Role = Developer } as p -> printfn "%s in Developer!" p.Name
    | { Role = QA} as p -> printfn "%s is in QA!" p.Name
    | { Role = Manager(reports) } as p -> printfn "%s is a Manger with %i reports!" p.Name reports
    
peopleAndRolesRecord |> List.iter about2

peopleAndRolesRecord 
|> List.map (fun p -> (p.Name, p.Role))
|> List.iter about

System.Console.ReadKey()
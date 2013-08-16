//repl

//Full type inference

//first class functions
let hello name = printfn "Hello %s" name
hello "Chris"

let people = ["Chris"; "Marcos"; "Andy"]

List.iter hello people
List.iter hello (List.map (fun (person:string) -> person.ToUpper()) (List.sort people))

//pipe forward makes your life easier
people |> List.iter hello
people 
|> List.map (fun person -> person.ToUpper())
|> List.sort
|> List.iter hello















//Immutable by default, opt in mutability
let a = 1
a = 1
//a <- 1

let mutable b = 1
b = 1
b <- 2













//New data types: Discriminated Unions, Record Types, Tuples


//Tuples
let peopleAndAges = [("Chris", 31); ("Marcos", 50); ("Andy", 40)]

peopleAndAges
|> List.iter (fun (name, age) -> printfn "%s is %i years old" name age)














//Discriminated Union
type role =    
    | Developer
    | QA
    | Manager of int

let peopleAndRoles = [("Chris", QA); ("Marcos", Manager(3)); ("Andy", Developer)]

let about person =
    match person with
    | (name, Developer) -> printfn "%s in Developer!" name
    | (name, QA) -> printfn "%s is in QA!" name
    | (name, Manager(reports)) -> printfn "%s is a Manger with %i reports!" name reports
    //| _ -> failwith "wtf?"

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
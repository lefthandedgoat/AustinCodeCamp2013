open System

let read = System.IO.File.ReadAllText
let files = System.IO.Directory.GetFiles(@"c:\books\")

files 
|> Array.map read
|> Array.fold(fun accumulatedBook book -> accumulatedBook + book) String.Empty
|> fun combined -> combined.Split(' ')
|> Seq.countBy id
|> Seq.sortBy (fun (word, count) -> count)
|> List.ofSeq
|> List.rev
|> Seq.take 10
|> Seq.iter (fun (word, count) -> printfn "%s %i" word count)

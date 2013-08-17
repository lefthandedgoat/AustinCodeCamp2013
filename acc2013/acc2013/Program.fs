open System
open System.Security.Cryptography

let read = System.IO.File.ReadAllText
let files = System.IO.Directory.GetFiles(@"c:\books\")

let hash (word:string) =    
    let md5 = new MD5CryptoServiceProvider();
    let sha1 = new SHA1CryptoServiceProvider();
    let sha256 = new SHA256CryptoServiceProvider();
    let sha384 = new SHA384CryptoServiceProvider();
    let sha512 = new SHA512CryptoServiceProvider();    
    
    //sha512.ComputeHash(sha384.ComputeHash(sha256.ComputeHash(sha1.ComputeHash(md5.ComputeHash(Text.Encoding.ASCII.GetBytes(word))))))

    word 
    |> Text.Encoding.ASCII.GetBytes
    |> md5.ComputeHash
    |> sha1.ComputeHash
    |> sha256.ComputeHash
    |> sha256.ComputeHash
    |> sha384.ComputeHash
    |> sha512.ComputeHash

let sw = Diagnostics.Stopwatch.StartNew()

files 
|> Array.map read
|> Array.fold(fun accumulatedBook book -> accumulatedBook + book) String.Empty
|> fun combined -> combined.Split(' ')
|> Seq.countBy id
|> Seq.sortBy (fun (word, count) -> count)
|> Array.ofSeq
|> Array.rev
|> Array.map (fun (word, count) -> (word, count, (hash word)))
|> Seq.take 10
|> Seq.iter (fun (word, count, hash) -> printfn "%s %i" word count)

sw.Stop()
printfn "%i" sw.ElapsedMilliseconds
System.Console.ReadKey()
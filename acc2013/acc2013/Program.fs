open System.IO
let numImages = 1000
let size = 512
let numPixels = size * size

let makeImageFiles () =
    printfn "making %d %dx%d images... " numImages size size
    let pixels = Array.init numPixels (fun i -> byte i)
    for i = 1 to numImages  do
        System.IO.File.WriteAllBytes(sprintf "Image%d.tmp" i, pixels)
    printfn "done."

let processImageRepeats = 20

let transformImage (pixels, imageNum) =
    printfn "transformImage %d" imageNum;
    // Perform a CPU-intensive operation on the image.
    for i in 1 .. processImageRepeats do 
        pixels |> Array.map (fun b -> b + 1uy) |> ignore
    pixels |> Array.map (fun b -> b + 1uy)

let processImageSync i =
    use inStream =  File.OpenRead(sprintf "Image%d.tmp" i)
    let pixels = Array.zeroCreate numPixels
    let nPixels = inStream.Read(pixels, 0, numPixels);
    let pixels' = transformImage(pixels, i)
    use outStream =  File.OpenWrite(sprintf "Image%d.done" i)
    outStream.Write(pixels', 0, numPixels)

let processImagesSync () =
    printfn "processImagesSync...";
    for i in 1 .. numImages do
        processImageSync(i)

let processImagesSyncParallel () =
    printfn "processImagesSync...";
    [| 1 .. numImages |]
    |> Array.Parallel.iter processImageSync

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__;;
System.Environment.CurrentDirectory <- @"C:\temp\junk\";;
makeImageFiles();;

let sw = System.Diagnostics.Stopwatch.StartNew();;
processImagesSync ()
sw.Stop();;
printfn "%i" sw.ElapsedMilliseconds;;

let sw2 = System.Diagnostics.Stopwatch.StartNew();;
processImagesSyncParallel ()
sw2.Stop();;
printfn "%i" sw2.ElapsedMilliseconds;;


//////////////////////////////////////

let processImageAsync i =
    async {use inStream = File.OpenRead(sprintf "Image%d.tmp" i)
           let! pixels = inStream.AsyncRead(numPixels)
           let  pixels' = transformImage(pixels, i)
           use outStream = File.OpenWrite(sprintf "Image%d.done" i)
           do! outStream.AsyncWrite(pixels')}

let processImagesAsync() =
    printfn "processImagesAsync...";
    let tasks = [for i in 1 .. numImages -> processImageAsync(i)]
    Async.RunSynchronously (Async.Parallel tasks) |> ignore
    printfn "processImagesAsync finished!"

let sw3 = System.Diagnostics.Stopwatch.StartNew();;
processImagesAsync();;
sw3.Stop();;
printfn "%i" sw3.ElapsedMilliseconds;;

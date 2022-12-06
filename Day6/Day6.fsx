let inputData =
    (System.IO.File.ReadAllText "Day6/input.txt")
        .Split "\n"

let partA (lines: string []) =
    lines
    |> Array.map (fun line ->
        line
        |> Array.ofSeq
        |> Array.windowed 4
        |> Array.map (fun window -> (window |> Set.ofArray |> Set.count))
        |> Array.findIndex (fun uniqueCount -> uniqueCount = 4)
        |> (fun index -> index + 4))

let partB (lines: string []) =
    lines
    |> Array.map (fun line ->
        line
        |> Array.ofSeq
        |> Array.windowed 14
        |> Array.map (fun window -> (window |> Set.ofArray |> Set.count))
        |> Array.findIndex (fun uniqueCount -> uniqueCount = 14)
        |> (fun index -> index + 14))

printfn $"%A{partA inputData}"
printfn $"%A{partB inputData}"

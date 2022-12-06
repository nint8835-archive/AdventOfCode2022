let inputData =
    (System.IO.File.ReadAllText "Day6/input.txt")
        .Split "\n"

let findMarker (size: int) (input: string []) : int [] =
    input
    |> Array.map (fun line ->
        line
        |> Array.ofSeq
        |> Array.windowed size
        |> Array.map (fun window -> (window |> Set.ofArray |> Set.count))
        |> Array.findIndex (fun uniqueCount -> uniqueCount = size)
        |> (fun index -> index + size))

printfn $"%A{findMarker 4 inputData}"
printfn $"%A{findMarker 14 inputData}"

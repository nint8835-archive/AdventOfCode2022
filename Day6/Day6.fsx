let inputData =
    (System.IO.File.ReadAllText "Day6/input.txt")

let findMarker (size: int) (input: string) : int =
    input
    |> Array.ofSeq
    |> Array.windowed size
    |> Array.map (fun window -> (window |> Set.ofArray |> Set.count))
    |> Array.findIndex (fun uniqueCount -> uniqueCount = size)
    |> (+) size

printfn $"%A{findMarker 4 inputData}"
printfn $"%A{findMarker 14 inputData}"

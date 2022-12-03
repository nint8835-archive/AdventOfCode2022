let inputData =
    (System.IO.File.ReadAllText "Day3/input.txt")
        .Split "\n"


let getPriority (item: char) : int =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        .IndexOf item
    + 1

let getCommonItems (bag: string * string) : char [] =
    Set.intersect (Set.ofSeq (fst bag)) (Set.ofSeq (snd bag))
    |> Set.toArray

let partA (input: string []) : int =
    input
    |> Array.map (fun line -> (line[.. line.Length / 2 - 1], line[line.Length / 2 ..]))
    |> Array.map getCommonItems
    |> Array.map (fun common -> common |> Array.map getPriority |> Array.sum)
    |> Array.sum

let partB (input: string []) : int =
    input
    |> Array.chunkBySize 3
    |> Array.map (fun group ->
        group
        |> Array.fold (fun acc bag -> Set.intersect acc (Set.ofSeq bag)) (Set.ofSeq group[0])
        |> Set.toArray
        |> (fun arr -> arr[0]))
    |> Array.map getPriority
    |> Array.sum


printfn $"%A{partA inputData}"
printfn $"%A{partB inputData}"

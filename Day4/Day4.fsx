let inputData =
    (System.IO.File.ReadAllText "Day4/input.txt")
        .Split "\n"
    |> Array.map (fun line ->
        line.Split ","
        |> Array.map (fun elf -> elf.Split "-" |> Array.map int))

let partA (pair: int [] []) : bool =
    ((pair[0][0] <= pair[1][0])
     && (pair[0][1] >= pair[1][1]))
    || ((pair[1][0] <= pair[0][0])
        && (pair[1][1] >= pair[0][1]))

let partB (pair: int [] []) : bool =
    (pair[0][0] <= pair[1][0]
     && pair[1][0] <= pair[0][1])
    || (pair[1][0] <= pair[0][0]
        && pair[0][0] <= pair[1][1])

let partAResult =
    inputData |> Array.filter partA |> Array.length

let partBResult =
    inputData |> Array.filter partB |> Array.length

printfn $"%A{partAResult}"
printfn $"%A{partBResult}"

let inputData =
    (System.IO.File.ReadAllText "Day4/input.txt")
        .Split "\n"
    |> Array.map (fun line ->
        line.Split ","
        |> Array.map (fun elf -> elf.Split "-" |> Array.map int))

let tryBothOrders (func: int [] -> int [] -> bool) (pair: int [] []) : bool =
    func pair[0] pair[1] || func pair[1] pair[0]

let partA (a: int []) (b: int []) : bool = a[0] <= b[0] && a[1] >= b[1]

let partB (a: int []) (b: int []) : bool = a[0] <= b[0] && b[0] <= a[1]

let partAResult =
    inputData
    |> Array.filter (tryBothOrders partA)
    |> Array.length

let partBResult =
    inputData
    |> Array.filter (tryBothOrders partB)
    |> Array.length

printfn $"%A{partAResult}"
printfn $"%A{partBResult}"

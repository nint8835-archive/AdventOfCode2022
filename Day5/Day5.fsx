let inputData =
    (System.IO.File.ReadAllText "Day5/input.txt")
        .Split "\n\n"

type Instruction =
    { count: int
      from_col: int
      to_col: int }

let gridLines = inputData[ 0 ].Split "\n"

let stackCount =
    Array.last gridLines
    |> (fun line -> line.Split " ")
    |> Array.filter (fun index -> index.Length <> 0)
    |> Array.map int
    |> Array.last

let grid: char [] [] =
    [| 1..stackCount |]
    |> Array.map (fun columnNum ->
        gridLines[.. gridLines.Length - 2]
        |> Array.map (fun line -> line[4 * (columnNum - 1) + 1])
        |> Array.filter (fun entry -> entry <> ' '))

printfn $"%A{grid}"

let instructions =
    inputData[ 1 ].Split "\n"
    |> Array.map (fun instruction -> instruction.Split " ")
    |> Array.map (fun instruction ->
        { count = int instruction[1]
          from_col = int instruction[3]
          to_col = int instruction[5] })

printfn $"%A{instructions}"
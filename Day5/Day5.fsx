let inputData =
    (System.IO.File.ReadAllText "Day5/input.txt")
        .Split "\n\n"

type Instruction =
    { count: int
      from_col: int
      to_col: int }

let applyMovement (reverse: bool) (grid: char [] []) (instruction: Instruction) : char [] [] =
    grid
    |> Array.copy
    |> Array.updateAt (instruction.from_col - 1) grid[instruction.from_col - 1].[instruction.count ..]
    |> Array.updateAt
        (instruction.to_col - 1)
        (Array.concat [| grid[instruction.from_col - 1].[0 .. instruction.count - 1]
                         |> (if reverse then Array.rev else id)
                         grid[instruction.to_col - 1] |])

let getTopString (reverse: bool) (instructions: Instruction []) (grid: char [] []) : string =
    instructions
    |> Array.fold (applyMovement reverse) grid
    |> Array.map (fun col -> col[0])
    |> Array.map string
    |> String.concat ""

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

let instructions =
    inputData[ 1 ].Split "\n"
    |> Array.map (fun instruction -> instruction.Split " ")
    |> Array.map (fun instruction ->
        { count = int instruction[1]
          from_col = int instruction[3]
          to_col = int instruction[5] })

let partAStr =
    getTopString true instructions grid

let partBStr =
    getTopString false instructions grid

printfn $"{partAStr}"
printfn $"{partBStr}"

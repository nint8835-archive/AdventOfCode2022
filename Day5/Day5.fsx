let inputData =
    (System.IO.File.ReadAllText "Day5/input.txt")
        .Split "\n\n"

type Instruction =
    { count: int
      from_col: int
      to_col: int }

let applyMovement (reverse: bool) (grid: char [] []) (instruction: Instruction) : char [] [] =
    let newGrid = Array.copy grid

    let movedChars =
        grid[instruction.from_col - 1][0 .. instruction.count - 1]

    newGrid[instruction.from_col - 1] <- newGrid[instruction.from_col - 1][instruction.count ..]

    newGrid[instruction.to_col - 1] <- Array.concat [| if reverse then
                                                           movedChars |> Array.rev
                                                       else
                                                           movedChars
                                                       newGrid[instruction.to_col - 1] |]

    newGrid

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

let partAGrid =
    instructions
    |> Array.fold (applyMovement true) grid

let partBGrid =
    instructions
    |> Array.fold (applyMovement false) grid

let partAChars =
    partAGrid
    |> Array.map (fun col -> col[0])
    |> Array.map string
    |> String.concat ""

let partBChars =
    partBGrid
    |> Array.map (fun col -> col[0])
    |> Array.map string
    |> String.concat ""

printfn $"%A{partAChars}"
printfn $"%A{partBChars}"

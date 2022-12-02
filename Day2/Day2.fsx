let inputData =
    (System.IO.File.ReadAllText "Day2/input.txt")
        .Split "\n"
    |> Array.map (fun line -> line.Split " ")

type WinStatus =
    | Win
    | Loss
    | Draw

let winMap =
    Map [ ("A",
           Map [ ("X", Draw)
                 ("Y", Win)
                 ("Z", Loss) ])
          ("B",
           Map [ ("X", Loss)
                 ("Y", Draw)
                 ("Z", Win) ])
          ("C",
           Map [ ("X", Win)
                 ("Y", Loss)
                 ("Z", Draw) ]) ]

let moveMap =
    Map [ ("A",
           Map [ ("X", "Z")
                 ("Y", "X")
                 ("Z", "Y") ])
          ("B",
           Map [ ("X", "X")
                 ("Y", "Y")
                 ("Z", "Z") ])
          ("C",
           Map [ ("X", "Y")
                 ("Y", "Z")
                 ("Z", "X") ]) ]


let scoreMap =
    Map [ ("X", 1); ("Y", 2); ("Z", 3) ]

let winScoreMap =
    Map [ (Win, 6); (Draw, 3); (Loss, 0) ]

let useDirectly (_: string) (ourMove: string) : string = ourMove
let desiredWinStatus (theirMove: string) (ourMove: string) : string = moveMap[theirMove][ourMove]

let getScore (line: string []) (scoreGetter: string -> string -> string) : int =
    let theirMove = line[0]
    let ourMove = line[1]

    let ourFinalMove =
        scoreGetter theirMove ourMove

    winScoreMap[winMap[theirMove][ourFinalMove]]
    + scoreMap[ourFinalMove]

let partAScore =
    inputData
    |> Array.map (fun input -> getScore input useDirectly)
    |> Array.sum

let partBScore =
    inputData
    |> Array.map (fun input -> getScore input desiredWinStatus)
    |> Array.sum

printfn $"Part A: {partAScore}"
printfn $"Part B: {partBScore}"

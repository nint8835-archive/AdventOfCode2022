/// Euclidean remainder, the proper modulo operation
/// Taken from https://stackoverflow.com/a/35848799
let inline (%!) a b = (a % b + b) % b

let getCharCode (char: string) : int = (("ABCXYZ".IndexOf char) %! 3) + 1


let inputData =
    (System.IO.File.ReadAllText "Day2/input.txt")
        .Split "\n"
    |> Array.map (fun line -> line.Split " " |> Array.map getCharCode)
    |> Array.map (fun line -> (line[0], line[1]))

let partA (m: int) (n: int) : int = (3 * ((1 + n - m) %! 3)) + n

let partB (m: int) (n: int) : int = (3 * (n - 1)) + ((m + n) %! 3) + 1

let partAScore =
    inputData
    |> Array.map (fun line -> line ||> partA)
    |> Array.sum

let partBScore =
    inputData
    |> Array.map (fun line -> line ||> partB)
    |> Array.sum

printfn $"Part A: {partAScore}"
printfn $"Part B: {partBScore}"

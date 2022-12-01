let inputData =
    (System.IO.File.ReadAllText "Day1/input.txt")
        .Split "\n\n"
        |> Array.map
               (fun elf ->
                    elf.Split("\n")
                    |> Array.map int)

let maxNElves (elves: int[][]) (n: int) : int =
    elves
    |> Array.map Array.sum
    |> Array.sortDescending
    |> Array.take n
    |> Array.sum

printfn $"Part A: {maxNElves inputData 1}"
printfn $"Part B: {maxNElves inputData 3}"

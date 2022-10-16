open Hw5.MaybeBuilder
open Hw5.Calculator
open Hw5.Parser

[<EntryPoint>]
let main args =
    let result = maybe {
        let! (arg1, operation, arg2) = parseCalcArguments [|"1"; "+"; "2"|]
        return calculate arg1 operation arg2
    }

    match result with
    | Ok result -> printfn $"{result}"
    | Error message -> printfn $"{message}"
    
    0
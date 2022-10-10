module Hw4.Parser

open System
open Hw4.Calculator

type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation    
} 

let isArgLengthSupported (args : string[]) = args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> ArgumentException() |> raise
    
let parseCalcArguments(args : string[]) =
    let isArg0Parsed, value1 = Double.TryParse(args[0])
    let isArg2Parsed, value2 = Double.TryParse(args[2])
    if (not (isArgLengthSupported args) || not isArg0Parsed || not isArg2Parsed) then
        ArgumentException() |> raise
    
    { arg1 = value1; arg2 = value2; operation = parseOperation(args[1])}
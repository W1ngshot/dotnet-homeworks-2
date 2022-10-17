module Hw5.Parser

open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length with
    | 3 -> Ok args
    | _ -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
        | "+" -> Ok (arg1, CalculatorOperation.Plus, arg2)
        | "-" -> Ok (arg1, CalculatorOperation.Minus, arg2)
        | "*" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
        | "/" -> Ok (arg1, CalculatorOperation.Divide, arg2)
        | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<('a * string * 'b), Message> =
     try
        Ok (args[0] |> decimal, args[1], args[2] |> decimal)
        with | _ -> Error Message.WrongArgFormat
    
        
    

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match (operation, arg2.ToString()) with
    | (CalculatorOperation.Divide, "0") -> Error Message.DivideByZero
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe {
        let! supportedLengthArgs = isArgLengthSupported args
        let! parsedArgs = parseArgs supportedLengthArgs
        let! argsWithOperation = isOperationSupported parsedArgs
        let! parsedArgsWithoutZeroDivide = isDividingByZero argsWithOperation
        return parsedArgsWithoutZeroDivide
    }
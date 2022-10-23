module Hw6.Calculator

module Calculator = 
    let calculate (arg1:decimal, operation:string, arg2:decimal) : Result<string, string> =
        match operation, arg2 with
        | "Plus", _-> Ok $"{arg1 + arg2}"
        | "Minus", _ -> Ok $"{arg1 - arg2}"
        | "Multiply", _ -> Ok $"{arg1 * arg2}"
        | "Divide", 0m -> Ok "DivideByZero"
        | "Divide", _ -> Ok $"{arg1 / arg2}"
        | _ -> Error $"Could not parse value '{operation}'"
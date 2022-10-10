open Hw4

let a = System.Console.ReadLine()
let b = System.Console.ReadLine()
let c = System.Console.ReadLine()

let parsedValues = Parser.parseCalcArguments([|a;b;c|])
let result = Calculator.calculate(parsedValues.arg1) (parsedValues.operation) (parsedValues.arg2)
System.Console.WriteLine(result)
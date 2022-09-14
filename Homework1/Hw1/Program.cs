var arg1 = args[0];
var operation = args[1];
var arg2 = args[2];

Hw1.Parser.ParseCalcArguments(new [] {arg1, operation, arg2 }, out var val1, out var op, out var val2);
var result = Hw1.Calculator.Calculate(val1, op, val2);
Console.WriteLine(result);
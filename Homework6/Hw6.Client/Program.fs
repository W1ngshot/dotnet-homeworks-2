open System
open System.Net
open System.Net.Http
open System.Threading.Tasks
open Hw6.Client
open MaybeBuilder

let getInput() =
    Console.WriteLine("Введите первое число")
    let value1 = Console.ReadLine()
    Console.WriteLine("Введите название операции")
    let operation = Console.ReadLine()
    Console.WriteLine("Введите второе число")
    let value2 = Console.ReadLine()
    value1, operation, value2
    
let validateInput(arg1, operation, arg2) =
    match operation with
    | "Plus" | "Minus" | "Multiply" | "Divide" -> Ok (arg1, operation, arg2)
    | _ -> Error "Wrong operation, try Plus/Minus/Multiply/Divide"

let parseArgs(arg1:string, operation, arg2:string) =
    let isArg1Parsed, value1 = Decimal.TryParse(arg1)
    let isArg2Parsed, value2 = Decimal.TryParse(arg2)
    
    if (not isArg1Parsed) then
        Error $"{arg1} не является числом"
    elif (not isArg2Parsed) then
        Error $"{arg2} не является числом"
    else
        Ok (value1, operation, value2)

let buildQueryString(arg1:decimal, operation:string, arg2:decimal) =
    Uri($"https://localhost:5001/calculate?value1={arg1}&operation={operation}&value2={arg2}")

let Calculate (client:WebClient, uri:Uri) =
    async {
        try
            return! client.AsyncDownloadString(uri)
        with
        | ex -> return ex.Message
    }

[<EntryPoint>]
let main _ =
    let client = new WebClient()
    while true do
        let parsedArgs = maybe {
            let inputData = getInput()
            let! parsedOp = validateInput inputData
            let! parsed = parseArgs parsedOp
            return parsed
        }
        match parsedArgs with
        | Error message -> Console.WriteLine(message)
        | Ok args ->
            let uri = buildQueryString args
            let result = Calculate (client, uri)
            printfn $"{Async.RunSynchronously result}"
    0
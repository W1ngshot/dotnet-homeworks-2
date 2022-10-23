module Hw6.App

open Hw6.MaybeBuilder
open Hw6.Arguments
open Hw6.Calculator
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = maybe {
            let! args = ctx.TryBindQueryString<Arguments>()
            let! result = Calculator.calculate (args.value1, args.operation, args.value2)
            return result
        }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=>
        choose [ route "/" >=> text "Use /calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"]
        choose [ route "/calculate" >=> calculatorHandler ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0
module DependencyInjection.FSharp.Tests.Helpers.DependencyInjectionHelper

open System
open System.Collections.Generic
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging;
open Application.Api

let kvp key value =
    new KeyValuePair<string, string>(key, value)

let private createServiceCollection (startup:Startup) =
    let services = new ServiceCollection()
    services.AddLogging(fun logger -> logger.AddConsole() |> ignore) |> ignore;
    startup.ConfigureServices(services)
    services
    
let registerServices configValues =
    let config =
        (new ConfigurationBuilder())
            .AddInMemoryCollection(configValues)
            .Build()
    let startup = new Startup(config)
    createServiceCollection startup

let addController serviceType (services: IServiceCollection) =
    services.AddTransient(serviceType)

let buildServiceProvider (services: IServiceCollection) =
    services.BuildServiceProvider()

let getController serviceType (serviceProvider: IServiceProvider) =
    serviceProvider.GetRequiredService(serviceType)

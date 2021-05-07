module DependencyInjection.FSharp.Tests.Tests.DependencyInjectionTests

open System
open System.Collections.Generic
open Application.Api
open FsUnit.Xunit
open DependencyInjection.FSharp.Tests.Helpers
open DependencyInjection.FSharp.Tests.Helpers.DependencyInjectionHelper
open Xunit

module Config =
    let basicConfig =
        [
            kvp "Config:Key" "null"
        ]

open Config

let controllersToTest: IEnumerable<Object []> =
        typeof<Controllers.BaseController>
        |> ControllerFinder.findControllers
        |> Seq.map (fun t -> t :> Object)
        |> Seq.map Array.singleton

[<Theory>]
[<MemberData("controllersToTest")>]
let ``Successfully create the controllers with their dependencies`` controllerType =
    List.concat [basicConfig]
    |> registerServices
    |> addController controllerType
    |> buildServiceProvider
    |> getController controllerType
    |> should not' (be Null)

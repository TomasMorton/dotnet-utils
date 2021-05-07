module DependencyInjection.FSharp.Tests.Helpers.ControllerFinder

open System
open System.Reflection

let private getTypes (assembly: Assembly) =
    assembly.GetTypes()

let private canBeInstantiated (typeToCheck: Type) =
    typeToCheck.IsClass && not typeToCheck.IsAbstract

let private inheritsInterface (baseInterface: Type) typeToCheck =
    baseInterface.IsAssignableFrom(typeToCheck)
        && (canBeInstantiated typeToCheck)

let findControllers (baseInterface: Type) =
    AppDomain.CurrentDomain.GetAssemblies()
    |> Array.collect getTypes
    |> Array.filter (inheritsInterface baseInterface)

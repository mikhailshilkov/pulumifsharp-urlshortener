open System
open Pulumi

open Components
open Common
open Cosmos
open FunctionApp

module Program =

    let app () =
        
        let functions = resourceGroup {
            name "dotnet-rg"
            region WestEurope
        }

        let zipStorage = storageAccount {
            name "dotnetsa"
            resourceGroup functions
        }

        let cosmos = cosmosdb {
            name "fsharp-cosmos"
            resourceGroup functions
            database "mydb"
            collection "myitems"
        }       
        
        functionApp {
            name "fsharpfa"
            package "..\\Functions\\bin\\Debug\\netcoreapp2.1\\Functions.zip"
            version V2
            runtime DotNet
            resourceGroup functions
            storageAccount zipStorage
            cosmosdb cosmos
        }

    [<EntryPoint>]
    let Main (_: string[]) =
        Runtime.Run(Action (app >> ignore))
        0
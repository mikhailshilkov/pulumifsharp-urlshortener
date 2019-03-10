namespace UrlShortener

open System.Net.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.Http

module AzureFunctions = 

  type Item = { url: string }

  [<FunctionName("Navigate")>]
  let Navigate
     (     
      [<HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{route}")>] 
      req: HttpRequestMessage,
      
      [<CosmosDB("mydb", "myitems", ConnectionStringSetting = "CosmosDBConnection", Id = "{route}")>] 
      doc: Item) =

      if box doc <> null then RedirectResult doc.url :> IActionResult 
      else NotFoundResult () :> IActionResult
open System
open System.ServiceModel
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders
open FSharp.Data

type TerraService = WsdlService<"http://msrmaps.com/TerraService2.asmx?WSDL">

let terraClient = TerraService.GetTerraServiceSoap ()

type Organization = JsonProvider<"""{"organization": {
  "name": "organizationName",
  "people": {
    "it": [
      {"name": "personName", "role": "personRole"}
    ]
  }
}}""">

let organization = Organization.Parse("""{"organization": {
  "name": "Defi",
  "people": {
    "it": [
      {"name": "chris", "role": "QA"},
      {"name": "andy", "role": "Dev"},
      {"name": "marcos", "role": "Manager"}
    ]
  }
}}""")

organization.Organization.People.It
|> Array.iter (fun person -> printfn "%s's role is %s" person.Name person.Role)


System.Console.ReadKey()
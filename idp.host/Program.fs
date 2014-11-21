module idp.host

open System

open Thinktecture.IdentityServer.Core.Logging

open Microsoft.Owin
open Microsoft.Owin.Hosting

open OwinConsole

[<EntryPoint>]
let main argv = 
  Console.Title <- "Intelliplan IdP PoC"
  LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider())

  let _ = WebApp.Start<IntelliplanPoC>("http://localhost:1234/core")
  let _ = System.Console.ReadLine ()
  0

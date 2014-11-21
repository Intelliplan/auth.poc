module idp.host

open System

open Thinktecture.IdentityServer.Core.Logging

open Owin
open Microsoft.Owin
open Microsoft.Owin.Hosting

open OwinConsole

[<EntryPoint>]
let main argv = 
  Console.Title <- "Intelliplan IdP PoC"
  LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider())

  let start : string -> (IAppBuilder -> unit) -> IDisposable =
    fun url f_builder ->
      let options = StartOptions url
      options.AppStartup <- typeof<IntelliplanPoC>.AssemblyQualifiedName
      WebApp.Start(options, Action<_>(f_builder))
  use app = start "https://localhost:9443/core" <| fun builder -> ()
  let _ = System.Console.ReadLine ()
  0

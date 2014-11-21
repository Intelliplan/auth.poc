open Microsoft.Owin

[<EntryPoint>]
let main argv = 
    let _ = WebApp.Start
    let _ = System.Console.ReadLine ()
    0

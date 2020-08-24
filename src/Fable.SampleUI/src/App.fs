module App

open Feliz
open Fable.React

let app = React.functionComponent(fun () ->
    let routes = {| 
        ``/`` = fun _ -> MessageInbox.page()
    |}

    let content = 
        HookRouter.useRoutes routes 
        |> Option.defaultValue (h1 [][str "Not Found"])

    div [] [
        div [] [

        ]
        div [] [
            content
        ]
    ]
)

module App

open Feliz
open Fable.React
open Fable.React.Props
open Css

let app = React.functionComponent(fun () ->
    let routes = {| 
        ``/`` = fun _ -> InboxPage.page()
        ``/Inbox`` = fun _ -> InboxPage.page()
        ``/Users`` = fun _ -> UsersPage.page()
    |}

    let content = 
        HookRouter.useRoutes routes 
        |> Option.defaultValue (h1 [] [str "Not Found"])

    div [Class B.container] [
        div [Class B.row] [
            div [Class B.col] [
                content
            ]
        ]
    ]
)

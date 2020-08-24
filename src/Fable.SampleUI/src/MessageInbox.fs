module MessageInbox

open Fable
open Fable.React
open Fable.React.Props
open Feliz
open GroupingPanel
open System
open Css

type Email = {
    From: string
    Subject: string
    Received: DateTime
}

let email from subject rcvd = 
    { From = from; Subject = subject; Received = rcvd }

let today = DateTime.Today
let day (days: int) = DateTime.Today.AddDays(float days)

let getEmails() =
    [ email "Jackson Blevins" "whuut?!" (day -2)
      email "Jason Stafford" "Training Deadline" (day -2)
      email "Cameron Clay" "RE: App registrations..." (day -1)
      email "Jackson Blevin" "Incoming memes!" (day -1)
      email "Jackson Blevin" "Funny cat stuff" (day -1)
      email "Lamar Shaw" "Meeting Request" today
      email "Xander Cruz" "RE: Next Set of Features" today ]
    |> List.sortBy (fun e -> e.Received)


let page = React.functionComponent(fun () ->     
    div [Class B.container] [
        div [Class B.row] [
            div [Class B.``col-3``; Style [Background "#ececec"]] [
                table [Class B.table] [
                    tbody [] [
                        groupingPanel {
                            for email in getEmails() do
                            groupBy (
                                match email.Received with
                                | r when r = today  -> "Today"
                                | r when r >= (day -1) -> "Yesterday"
                                | _ -> "Older"
                            )
                            groupSortByDescending (email.Received)
                            groupHeader (fun header ->
                                tr [OnClick header.ToggleOnClick] [
                                    td [Style [Width "10px"]] [header.Chevron]
                                    td [] [str header.GroupKey]
                                ]
                            )
                            groupCollapsedIf (email.Received < today)
                            select (
                                tr [] [
                                    td [] []
                                    td [] [
                                        div [] [str email.From]
                                        div [Style [FontWeight "bold"]] [str email.Subject]
                                        div [] [str (email.Received.ToShortDateString())]
                                    ]
                                ]
                            )
                        }
                    ]
                ]
            ]
            div [classes [B.``col-9``; B.``p-2``]; Style [Background "whitesmoke"]] [
                b [] [str "Message Body..."]
            ]
        ]        
    ]
)

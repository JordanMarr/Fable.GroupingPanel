module UsersPage

open Fable
open Fable.React
open Fable.React.Props
open Feliz
open GroupingPanel
open System
open Css

type User = {
    Username: string
    Email: string
    IsEnabled: bool
}

let usr nm eml enbld =
    { Username = nm; Email = eml; IsEnabled = enbld }

let getFilteredUsers() =
    [ usr "Peter Gibbons" "pgibbons@initech.com" true
      usr "Bill Lumbergh" "blumbergh@initech.com" true
      usr "Michael Scott" "mscott@dmifflin.com" true
      usr "Dwight Schrute" "dschrute@dmifflin.com" true
      usr "Angela Martin" "amartin@dmifflin.com" false
      usr "Pam Beesly" "pbeesly@dmifflin.com" false ]
    |> List.sortBy (fun u -> u.Username)

let getCompany user =
    match user.Email with
    | email when email.EndsWith("initech.com") -> "Initech"
    | email when email.EndsWith("dmifflin.com") -> "Dunder Mifflin"
    | _ -> "Other"

let page = React.functionComponent(fun () -> 
    div [Class B.container] [
        div [Class B.row] [
            div [Class B.col] [
                table [classes [B.table; B.``mt-4``]] [
                    tbody [] [
                        groupingPanel {
                            for user in getFilteredUsers() do
                            groupBy (if user.IsEnabled then "Active Users" else "Disabled Users")
                            groupHeader (fun header ->
                                tr [Style [Background "#ececec"]] [
                                    td [OnClick header.ToggleOnClick; Style [Width "40px"]] [
                                        header.Chevron
                                    ]
                                    td [ColSpan 3] [
                                        span [] [str (sprintf "%s (%i)" header.GroupKey header.Group.Length)]
                                    ]
                                ]
                            )
                            groupCollapsedIf (not user.IsEnabled)
                            groupBy (sprintf "%s" (getCompany user))
                            groupHeader (fun header ->
                                tr [Style [Background "whitesmoke"]] [
                                    td [OnClick header.ToggleOnClick] [
                                        header.Chevron
                                    ]
                                    td [ColSpan 3] [
                                        span [] [str (sprintf "%s (%i)" header.GroupKey header.Group.Length)]
                                    ]
                                ]
                            )
                            select (
                                tr [Key ("usr_" + user.Email)] [
                                    td [] []
                                    td[Style[LineHeight "30px"]] [
                                        str user.Email
                                    ]
                                    td [] [
                                        str user.Username
                                    ]
                                    td [] [
                                        input [
                                            Props.Type "checkbox"
                                            Style [Width "20px"; Height "32px"]
                                            Class B.``form-control`` 
                                            Checked (user.IsEnabled)
                                        ]
                                    ]
                                ]
                            )
                        } 
                    ]
                ]
            ]
        ]
    ]
)
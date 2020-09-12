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
    let users, setUsers = React.useState([])

    React.useEffectOnce(fun () ->
        getFilteredUsers() |> setUsers
    )

    div [Class B.container] [
        div [Class B.row] [
            div [Class B.col] [
                table [classes [B.table; B.``mt-4``]] [
                    tbody [] [

                        let headerTemplate header = 
                            tr [Style [Background "#ececec"]; OnClick header.ToggleOnClick] [
                                td [ColSpan 4] [
                                    header.Chevron
                                    span [] [str (sprintf "%s (%i)" header.GroupKey header.Group.Length)]
                                ]                                    
                            ]

                        groupingPanel {
                            for user in users do
                            groupBy (if user.IsEnabled then "Active Users" else "Inactive Users")
                            groupHeader headerTemplate
                            groupCollapsedIf (not user.IsEnabled)
                            groupBy (getCompany user)
                            groupHeader headerTemplate
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
                                            OnChange (fun e -> 
                                                users |> List.map (fun u -> 
                                                    if u.Email = user.Email
                                                    then { u with IsEnabled = not u.IsEnabled }
                                                    else u
                                                ) |> setUsers
                                            )
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
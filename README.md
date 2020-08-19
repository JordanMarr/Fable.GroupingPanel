# Fable.GroupingPanel
The `groupingPanel` computation expression helps you to easily group UI data in Fable into one or more collapsible groups.

## Message Inbox Example

Imagine you are creating a messaging app that lists messages in the left pane.  
You might start out with this:

```F#
let page = React.functionComponent(fun () -> 
    container [
        div [Class "row"] [
            div [Class "col-3"; Style [Background "#ececec"]] [
                table [Class B.table] [
                    tbody [] [
                        for email in getEmails() do
                            tr [] [
                                td [] []
                                td [] [
                                    div [] [str email.From]
                                    div [Style [FontWeight "bold"]] [str email.Subject]
                                    div [] [str (email.Received.ToShortDateString())]
                                ]
                            ]
                    ]
                ]
            ]
            div [Class "col-9 p-2"; Style [Background "whitesmoke"]] [
                // Display message here...
            ]
        ]        
    ]
)
```   
Which yields:
![Sample Message App](documentation/imgs/MessageApp_Before.png)

Now let's adding some collapsable groups using the `groupingPanel` computation expression:
```F#
let page = React.functionComponent(fun () ->     
    container [
        div [Class "row"] [
            div [Class "col-3"; Style [Background "#ececec"]] [
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
                                tr [] [
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
            div [Class "col-9 p-2"; Style [Background "whitesmoke"]] [
                b [] [str "Message Body..."]
            ]
        ]        
    ]
)
```

Which yields:
![Sample Message App with Grouping](documentation/imgs/MessageApp_After.png)


## Minimum Configuration
`groupingPanel` requires, at a minimum, the following:
Operation | Description | Required
--------- | ----------- | --------
`for` ___ `in` ___ `do` | Initializes the items | Yes
`groupBy` | This adds a grouping | Yes (one or more)
`select` | This defines the item template | Yes

## Full List of Configuration Options
Operation | Description | Required
--------- | ----------- | --------
`for` ___ `in` ___ `do` | Initializes the items | Yes
`localStorageKey` | Persists your collapsed state in local storage if a key is specified | No
`groupBy` | This adds a grouping | Yes (one or more)
`groupHeader` | Allows the user to specify a group header template | No
`groupFooter` | Allows the user to specify a group footer template | No
`groupCollapsed` | A bool that will determine whether the group will be collapsed by default | No
`groupCollapsedIf` | An expression based on the `for` item that will determine whether a group will be collapsed | No
`groupSortBy` | An expression based on the `for` item that will sort a group in ascending order | No
`groupSortBy` | An expression based on the `for` item that will sort a group in descending order | No
`groupColor` | Overrides the bg color - only applies when using the default group header template | No
`select` | This defines the item template | Yes

module internal Fable.GroupingPanel.Hooks

open Fable.SimpleJson

type SetState<'T> = 'T -> unit  

let useState<'T> (t: unit -> 'T) : ('T * SetState<'T>) = 
    Fable.Core.JsInterop.import "useState" "react"

let inline useLocalStorage<'T>(key, initialValue: 'T) =
    let storedValue, setStoredValue = 
        useState(fun () ->
            try
                let item = Browser.Dom.window.localStorage.getItem(key)
                if item <> null then Json.parseAs<'T>(item) 
                else initialValue
            with ex ->
                Browser.Dom.console.error(ex.Message)
                initialValue
        )

    let setValue value = 
        try
            setStoredValue value
            Browser.Dom.window.localStorage.setItem(key, Json.stringify(value))
        with ex ->
            Browser.Dom.console.error(ex.Message)

    storedValue, setValue

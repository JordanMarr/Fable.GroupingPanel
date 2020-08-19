module internal Hooks

open Fable.SimpleJson
open Fable.React

let useState<'T> (initialState: 'T) : ('T * ('T -> unit)) = 
    let state = HookBindings.Hooks.useState<'T>(initialState)
    state.current, state.update

let inline useLocalStorage<'T>(key, initialValue: 'T) =
    let storedValue, setStoredValue = 
        useState(
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

module Main

open Fable.Core.JsInterop
open Browser.Dom
open Fable.Core.JsInterop
open Feliz

//importAll "../styles/main.scss"
importAll "../styles/bootstrap.min.css"

// App
ReactDOM.render(App.app, document.getElementById "feliz-app")
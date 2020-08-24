module HookRouter
// https://pastebin.com/dRsnyx4U

open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open System.Text.RegularExpressions

type InterceptedPath = string option
type ConfirmNavigation = unit -> unit
type ResetPath = unit -> unit
type StopInterception = unit -> unit
 
type PreviousPath = string
type NextPath = string
type InterceptorPath = string
type InterceptorFn = PreviousPath -> NextPath -> InterceptorPath
 
let inline A (props: IHTMLProp list) (elems: ReactElement seq) : ReactElement =
   ofImport "A" "hookrouter" (keyValueList CaseRules.LowerFirst props) elems

let AType: ReactElementType = import "A" "hookrouter"

let setLinkProps: props: obj -> obj = import "setLinkProps" "hookrouter"
let confirmNavigation: unit -> unit = import "confirmNavigation" "hookrouter"
let resetPath: unit -> unit = import "resetPath" "hookrouter"
let stopInterception: unit -> unit = import "stopInterception" "hookrouter"
let useControlledInterceptor: unit -> InterceptedPath * ConfirmNavigation * ResetPath * StopInterception = import "useControlledInterceptor" "hookrouter"
let useInterceptor: InterceptorFn -> unit = import "useInterceptor" "hookrouter"
let interceptRoute: previousRoute: string * nextRoute: string -> ResizeArray<string> = import "interceptRoute" "hookrouter"
let get: componentId: float -> obj option = import "get" "hookrouter"
let remove: componentId: float -> unit = import "remove" "hookrouter"
let setobj: inObj: obj * ?replace: bool -> unit = import "setobj" "hookrouter"
let getobj: unit -> obj = import "getobj" "hookrouter"
let queryStringToObject: inStr: string -> obj = import "queryStringToObject" "hookrouter"
let objectToQueryString: inObj: obj -> string = import "objectToQueryString" "hookrouter"
let useobj: unit -> obj * obj = import "useobj" "hookrouter"
let useRedirect (fromURL: string) (toURL: string) (queryParams: obj option) (replace: bool option) : unit = import "useRedirect" "hookrouter"
let setBasepath: inBasepath: string -> unit = import "setBasepath" "hookrouter"
let getBasepath: unit -> string = import "getBasepath" "hookrouter"
let resolvePath: inPath: string -> string = import "resolvePath" "hookrouter"
let prepareRoute: inRoute: string -> Regex * ResizeArray<string> = import "prepareRoute" "hookrouter"
let navigate: url: string -> unit = import "navigate" "hookrouter"
let setPath: inPath: string -> unit = import "setPath" "hookrouter"
let getPath: unit -> string = import "getPath" "hookrouter"
let usePath: unit -> string = import "usePath" "hookrouter"
let updatePathHooks: unit -> unit = import "updatePathHooks" "hookrouter"
let getWorkingPath: parentRouterId: string -> string = import "getWorkingPath" "hookrouter"
let useRoutes: routeObj: obj -> ReactElement option = import "useRoutes" "hookrouter"
let useTitle: inString: string -> unit = import "useTitle" "hookrouter"
let getTitle: unit -> string = import "getTitle" "hookrouter"

module Css

open Zanaptak.TypedCssClasses
open Fable.React.Props

/// Bootstrap css classes
type B = CssClasses<"../styles/bootstrap.min.css">

/// Joins multiple classes into a single space delimited string.
let classes (classes: string seq) = 
    Class (System.String.Join(" ", classes))

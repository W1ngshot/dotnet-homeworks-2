module Hw6.MaybeBuilder

type MaybeBuilder() =
    member builder.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok okValue -> f okValue
        | Error errorMessage  -> Error errorMessage
    member builder.Return x: Result<'a,'b> =
        Ok x
let maybe = MaybeBuilder()
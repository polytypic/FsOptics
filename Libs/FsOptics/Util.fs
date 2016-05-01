namespace FsOptics

[<AutoOpen>]
module internal Util =

  let inline (^) f = f
  let inline flip f y x = f x y
  let inline constant x _ = x

  module Option =
    let inline getOr x = function None -> x | Some x -> x

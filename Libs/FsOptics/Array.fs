﻿namespace FsOptics

module Array =
  let valuesT U = sequenceI Array.length Array.iter id U

  let inline insertL op U (xs: 'a []) =
    U (None: option<'a>) </> fun xO -> op xs ^ Option.toArray xO
  let  appendL U = insertL       Array.append  U
  let prependL U = insertL (flip Array.append) U

  let indexL i U xs =
    if i < 0 then failwithf "Invalid index: %d" i
    let n = Array.length xs
    U ^ if i < n then Some xs.[i] else None
    </> fun xO ->
          if i <= n then
            [| Array.sub xs 0 i
               Option.toArray xO
               Array.sub xs <| i+1 <| n-(i+1) |] |> Array.concat
          else
            xs

  let inline findL p =
       Array.tryFindIndex p
    >> Option.map indexL
    >> Option.getOr appendL
    |> choose

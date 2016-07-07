namespace FsOptics

module List =
  let valuesT U = sequenceI List.length List.iter List.ofArray U

  let atL i U = List.revSplitAt i >> function
    | (ys, x::xs) -> U x </> fun x -> List.revAppend ys (x::xs)
    | (_, _) -> failwithf "Invalid index: %d" i

  let inline insertL op U (xs: list<'a>) =
    U (None: option<'a>) </> fun xO -> op xs ^ Option.toList xO
  let  appendL U = insertL       (@)  U
  let prependL U = insertL (flip (@)) U

  let indexL i U = List.revSplitAt i >> function
    | (ys, [])    -> U   None   </> fun xO -> List.revAppend ys (Option.toList xO)
    | (ys, x::xs) -> U ^ Some x </> fun xO -> List.revAppend ys (Option.toList xO @ xs)

  let inline findL p =
       List.tryFindIndex p
    >> Option.map indexL
    >> Option.getOr appendL
    |> choose

namespace FsOptics

module List =
  let valuesT U = sequenceI List.length List.iter List.ofArray U

  let inline insertL op U (xs: list<'a>) =
    U (None: option<'a>) </> fun xO -> op xs ^ Option.toList xO
  let  appendL U = insertL       (@)  U
  let prependL U = insertL (flip (@)) U

  let indexL i U xs =
    if i < 0 then failwithf "Invalid index: %d" i
    let n = List.length xs
    U ^ if i < n then Some xs.[i] else None
    </> fun xO ->
          if i <= n then
            [| List.take i xs
               Option.toList xO
               List.skip <| i+1 <| xs |] |> List.concat
          else
            xs

  let inline findL p =
       List.tryFindIndex p
    >> Option.map indexL
    >> Option.getOr appendL
    |> choose

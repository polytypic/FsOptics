namespace FsOptics

module Array =
  let valuesT U = sequenceI Array.length Array.iter id U

  let atL i U xs = U ^ Array.get xs i </> Array.replace xs i

  let inline insertL op U (xs: 'a []) =
    U (None: option<'a>) </> fun xO -> op xs ^ Option.toArray xO
  let  appendL U = insertL       Array.append  U
  let prependL U = insertL (flip Array.append) U

  let indexL i U xs =
    let n = Array.length xs
    if i < 0 || n < i then failwithf "Invalid index: %d" i
    if i = n
    then U None </> (Option.toArray >> Array.append xs)
    else U ^ Some xs.[i] </> fun xO ->
         [| Array.sub xs 0 i
            Option.toArray xO
            Array.sub xs <| i+1 <| n-(i+1) |] |> Array.concat

  let inline findL p =
       Array.tryFindIndex p
    >> Option.map indexL
    >> Option.getOr appendL
    |> choose

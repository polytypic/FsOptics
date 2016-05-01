namespace FsOptics

type Update<'a> = Over of 'a | View

[<AutoOpen>]
module Update =
  let result x = Over x
  let (<&>) a2b = function
    | Over a -> Over ^ a2b a
    | View -> View
  let inline (</>) aU a2b = a2b <&> aU
  let (<*>) a2bA aA =
    match a2bA with
     | Over a2b -> a2b <&> aA
     | View -> View
  // XXX: It seems that by making update into a monad, it becomes easy to
  // compose traversals.  It is not yet clear whether this is really a good
  // idea.
  let (>>=) aA a2bA =
    match aA with
     | Over a -> a2bA a
     | View -> View
  let (>=>) a2bA b2cA a = a2bA a >>= b2cA
  let sequenceI length iter ofArray x2yF xs =
    let ys = Array.zeroCreate ^ length xs
    let mutable i = 0
    xs
    |> iter (x2yF >> function
        | Over y ->
          match ys with
           | null -> ()
           | ys ->
             let j = i
             if 0 <= j then
               ys.[j] <- y
               i <- j + 1
        | View -> i <- -1)
    if i = ys.Length then Over ^ ofArray ys else View

type Optic<'s, 't, 'a, 'b> = ('a -> Update<'b>) -> 's -> Update<'t>
type Optic<'s, 'a> = Optic<'s, 's, 'a, 'a>

[<AutoOpen>]
module Optic =
  // XXX: This is not currently type safe, because the given optic is not
  // guaranteed to have exactly one focus.  Will need to experiment with various
  // ways to encode that in the types.
  let view (l: Optic<'s, 't, 'a, 'b>) s =
    let r = ref Unchecked.defaultof<_>
    s
    |> l ^ fun a -> r := a; View
    |> ignore
    !r
  let foldOf (o: Optic<_, _, _, _>) f a s =
    let r = ref a
    s
    |> o ^ fun a -> r := f !r a; View
    |> ignore
    !r
  let over (l: Optic<'s, 't, 'a, 'b>) a2b s =
    match l (a2b >> Over) s with
     | Over t -> t
     | _ -> failwith "Impossible"
  let inline set l b s = over l <| constant b <| s
  let inline remove l s = set l None s

  let inline choose (s2l: 's -> Optic<'s, 't, 'a, 'b>) U s = s2l s U s
  let inline normalize x2x U s = U ^ x2x s </> x2x

  let rep inn out x = if x = inn then out else x
  let replace inn out U s = U ^ rep inn out s </> rep out inn

  let (<^>) sa sb U s =
    U (view sa s, view sb s) </> fun (a, b) -> s |> set sa a |> set sb b

  let (<=>) (a: Optic<_,_,_,_>) b U = a U >=> b U

  let some a : Optic<_, _, option<_>, _> = fun U s -> U ^ Some a </> constant s
  let none   : Optic<_, _, option<_>, _> = fun U s -> U   None   </> constant s

  let (<|>) aP bP =
    choose ^ fun s -> if Option.isSome ^ view aP s then aP else bP

  let defaults out = replace None ^ Some out

  let ofPrism b2tO l U = function
    | None -> U None </> b2tO
    | Some s -> s |> flip ^ set l >> Some <&> U ^ view l s

  let ofPrism' l = ofPrism <| constant None <| l

  let ofTotal s2tO b2t l U = function
    | None -> U ^ None </> Option.map b2t
    | Some s -> U ^ Some ^ view l s
            </> function None -> s2tO s | Some b -> set l b s |> Some

namespace FsOptics

type Update = O | V
type Update<'a> = Over of 'a | View

type Optic<'s,'a,'b,'t> = (Update -> 'a -> Update<'b>) -> (Update -> 's -> Update<'t>)
type Optic<'s, 'a> = Optic<'s, 'a, 'a, 's>

[<AutoOpen; CompilationRepresentation (CompilationRepresentationFlags.ModuleSuffix)>]
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
  let sequenceI length iter ofArray u2x2yF (u: Update) xs =
    let ys = Array.zeroCreate ^ length xs
    let i = ref 0
    xs
    |> iter (u2x2yF u >> function
        | Over y ->
          match ys with
           | null -> ()
           | ys ->
             let j = !i
             if 0 <= j then
               ys.[j] <- y
               i := j + 1
        | View -> ())
    match u with
     | O -> Over ^ ofArray ys
     | V -> View

[<AutoOpen>]
module Optic =
  // XXX: This is not currently type safe, because the given optic is not
  // guaranteed to have exactly one focus.  Will need to experiment with various
  // ways to encode that in the types.
  let view (l: Optic<'s, 'a, 'b, 't>) s =
    let r = ref Unchecked.defaultof<_>
    s
    |> l (fun _ a -> r := a; View) V
    |> ignore
    !r
  let foldOf (o: Optic<_, _, _, _>) f a s =
    let r = ref a
    s
    |> o (fun _ a -> r := f !r a; View) V
    |> ignore
    !r
  let over (l: Optic<'s, 'a, 'b, 't>) a2b s =
    match l (fun _ a -> Over ^ a2b a) O s with
     | Over t -> t
     | _ -> failwith "Impossible"
  let inline set l b s = over l <| constant b <| s
  let inline remove l s = set l None s

  let inline choose (s2l: 's -> Optic<'s, 'a, 'b, 't>) U u s = s2l s U u s
  let inline normalize x2x U (u: Update) s = U u ^ x2x s </> x2x

  let rep inn out x = if x = inn then out else x
  let replace inn out U (u: Update) s = U u ^ rep inn out s </> rep out inn

  let (<^>) sa sb U (u: Update) s =
    U u (view sa s, view sb s) </> fun (a, b) -> s |> set sa a |> set sb b

  let (<=>) (a: Optic<_,_,_,_>) b U u = a U u >=> b U u

  let inline some Uu C a = Uu ^ Some a </> C
  let inline none Uu C   = Uu ^ None   </> C

  let (<|>) aP bP =
    choose ^ fun s -> if Option.isSome ^ view aP s then aP else bP

  let defaults out = replace None ^ Some out

  let ofPrism' b2tO l U (u: Update) = function
    | None -> U u None </> b2tO
    | Some s -> s |> flip ^ set l >> Some <&> U u ^ view l s

  let ofPrism l = ofPrism' <| constant None <| l

  let ofTotal s2tO b2t l U (u: Update) = function
    | None -> U u ^ None </> Option.map b2t
    | Some s -> U u ^ Some ^ view l s
            </> function None -> s2tO s | Some b -> set l b s |> Some

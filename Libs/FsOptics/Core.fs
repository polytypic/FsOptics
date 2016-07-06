namespace FsOptics

type Internal = O | V
type Internal<'a> = Over of 'a | View

[<CompilationRepresentation (CompilationRepresentationFlags.ModuleSuffix)>]
module Internal =
  let (<&>) a2b = function
    | Over a -> Over ^ a2b a
    | View   -> View
  let (<*>) a2bA aA =
    match a2bA with
     | Over a2b -> a2b <&> aA
     | View     -> View
  // XXX: It seems that by making update into a monad, it becomes easy to
  // compose traversals.  It is not yet clear whether this is really a good
  // idea.
  let (>>=) aA a2bA =
    match aA with
     | Over a -> a2bA a
     | View   -> View

type Optic<'s,'a,'b,'t> = ('a -> Internal -> Internal<'b>) -> ('s -> Internal -> Internal<'t>)
type Optic<'s, 'a> = Optic<'s, 'a, 'a, 's>

[<AutoOpen>]
module Optic =
  open Internal

  type Update<'x> = Internal -> Internal<'x>

  let result x = function O -> Over x | V -> View
  let inline (</>) u2aU a2b (u: Internal) = a2b <&> u2aU u
  let inline (<&>) a2b u2aU (u: Internal) = a2b <&> u2aU u
  let inline (<*>) u2a2bU u2aU (u: Internal) = u2a2bU u <*> u2aU u
  let inline (>>=) u2aU a2u2bU (u: Internal) = u2aU u >>= flip a2u2bU u
  let inline (>=>) a2bA b2cA a = a2bA a >>= b2cA

  let sequenceI length iter ofArray x2u2yF xs = function
    | V ->
      xs
      |> iter (flip x2u2yF V >> ignore)
      View
    | O ->
      let ys = Array.zeroCreate ^ length xs
      let i = ref 0
      xs
      |> iter ^ fun x ->
           match x2u2yF x O with
            | Over y ->
              let j = !i
              ys.[j] <- y
              i := j + 1
            | View ->
              failwith "Bug"
      Over ^ ofArray ys

  // XXX: This is not currently type safe, because the given optic is not
  // guaranteed to have exactly one focus.  Will need to experiment with various
  // ways to encode that in the types.
  let view (l: Optic<'s, 'a, 'b, 't>) (s: 's) =
    let r = ref Unchecked.defaultof<_>
    l (fun a _ -> r := a; View) s V
    |> ignore
    !r
  let foldOf (o: Optic<_, _, _, _>) f a s =
    let r = ref a
    o (fun a _ -> r := f !r a; View) s V
    |> ignore
    !r
  let over (l: Optic<'s, 'a, 'b, 't>) a2b s =
    match l (fun a _ -> Over ^ a2b a) s O with
     | Over t -> t
     | _ -> failwith "Bug"
  let inline set l b s = over l <| constant b <| s
  let inline remove l s = set l None s

  let inline choose (s2l: 's -> Optic<'s, 'a, 'b, 't>) U s = s2l s U s
  let inline normalize x2x U s = U ^ x2x s </> x2x

  let inline rep inn out x = if x = inn then out else x
  let replace inn out U s = U ^ rep inn out s </> rep out inn

  let (<^>) sa sb U s =
    U (view sa s, view sb s) </> fun (a, b) -> s |> set sa a |> set sb b

  let (<=>) (a: Optic<_,_,_,_>) b U = a U >=> b U

  let inline none U C   = U   None   </> C
  let inline some U C a = U ^ Some a </> C

  let (<|>) aP bP =
    choose ^ fun s -> if Option.isSome ^ view aP s then aP else bP

  let defaults out = replace None ^ Some out

  let ofPrism' b2tO l U = function
    | None   -> U   None     </> b2tO
    | Some s -> U ^ view l s </> (s |> flip ^ set l >> Some)

  let ofPrism l = ofPrism' <| constant None <| l

  let ofTotal s2tO b2t l U = function
    | None   -> U   None            </> Option.map b2t
    | Some s -> U ^ Some ^ view l s </> function None   -> s2tO s
                                               | Some b -> set l b s |> Some

namespace FsOptics

type Update<'a>

[<AutoOpen>]
module Update =
  val result: 'a -> Update<'a>
  val inline (</>): Update<'a> -> ('a -> 'b) -> Update<'b>
  val (<&>):       ('a -> 'b)  -> Update<'a>  -> Update<'b>
  val (<*>): Update<'a -> 'b>  -> Update<'a>  -> Update<'b>
  val (>>=): Update<'a> -> ('a -> Update<'b>) -> Update<'b>
  val (>=>): ('a -> Update<'b>) -> ('b -> Update<'c>) ->  'a -> Update<'c>
  val sequenceI: length: ('xs -> int)
              -> iter: (('x -> unit) -> 'xs -> unit)
              -> ofArray: (array<'y> -> 'ys)
              -> ('x -> Update<'y>)
              -> 'xs
              -> Update<'ys>

type Optic<'s, 't, 'a, 'b> = ('a -> Update<'b>) -> 's -> Update<'t>
type Optic<'s,     'a    > = Optic<'s, 's, 'a, 'a>

[<AutoOpen>]
module Optic =
  val          view: Optic<'s, 't, 'a,        'b > ->               's -> 'a
  val          over: Optic<'s, 't, 'a,        'b > -> ('a -> 'b) -> 's -> 't
  val inline    set: Optic<'s, 't, 'a,        'b > ->        'b  -> 's -> 't
  val inline remove: Optic<'s, 't, 'a, option<'b>> ->               's -> 't

  val foldOf: Optic<'s, 't, 'a, 'b> -> ('x -> 'a -> 'x) -> 'x -> 's -> 'x

  val inline choose: ('s -> Optic<'s, 't, 'a, 'b>) -> Optic<'s, 't, 'a, 'b>

  val inline normalize: ('s -> 't) -> Optic<'s, 't, 't, 's>

  val replace: inn: 'a -> out: 'a -> Optic<'a, 'a> when 'a: equality

  val (<^>): Optic<'s, 's, 'a,      'b     >
          -> Optic<'s, 't,      'c,      'd>
          -> Optic<'s, 't, 'a * 'c, 'b * 'd>

  val (<=>): Optic<'s, 'u, 'a, 'b>
          -> Optic<'u, 't, 'a, 'b>
          -> Optic<'s, 't, 'a, 'b>

  val some: 'a -> Optic<'s, 's, option<'a>, 'b>
  val none:       Optic<'s, 's, option<'a>, 'b>

  val (<|>): Optic<'s, 't, option<'a>, 'b>
          -> Optic<'s, 't, option<'a>, 'b>
          -> Optic<'s, 't, option<'a>, 'b>

  val defaults: 's -> Optic<option<'s>, option<'s>> when 's: equality

  val ofPrism: insert:      ('b -> option<'t>)
            -> prism: Optic<       's,         't,  option<'a>, 'b>
            ->        Optic<option<'s>, option<'t>, option<'a>, 'b>

  val ofPrism': prism: Optic<       's,         't,  option<'a>, 'b>
             ->        Optic<option<'s>, option<'t>, option<'a>, 'b>

  val ofTotal: remove:          ('s -> option<'t>)
            -> insert:                 ('b -> 't)
            -> lens: Optic<       's,         't,         'a,         'b>
            ->       Optic<option<'s>, option<'t>, option<'a>, option<'b>>

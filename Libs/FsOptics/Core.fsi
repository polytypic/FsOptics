namespace FsOptics

////////////////////////////////////////////////////////////////////////////////

type Internal
type Internal<'a>

[<CompilationRepresentation (CompilationRepresentationFlags.ModuleSuffix)>]
module Internal =
  val (<&>):         ('a -> 'b)  -> Internal<'a>  -> Internal<'b>
  val (<*>): Internal<'a -> 'b>  -> Internal<'a>  -> Internal<'b>
  val (>>=): Internal<'a> -> ('a -> Internal<'b>) -> Internal<'b>

////////////////////////////////////////////////////////////////////////////////

type Optic<'s,'a,'b,'t> = ('a -> Internal -> Internal<'b>) -> ('s -> Internal -> Internal<'t>)
type Optic<'s, 'a> = Optic<'s, 'a, 'a, 's>

[<AutoOpen>]
module Optic =
  //# Update Monad

  type Update<'x> = Internal -> Internal<'x>

  val result: 'a -> Update<'a>
  val inline (<&>):       ('a -> 'b)  -> Update<'a>  -> Update<'b>
  val inline (<*>): Update<'a -> 'b>  -> Update<'a>  -> Update<'b>
  val inline (>>=): Update<'a> -> ('a -> Update<'b>) -> Update<'b>
  val inline (>=>): ('a -> Update<'b>) -> ('b -> Update<'c>) -> 'a -> Update<'c>
  val inline (</>): Update<'a> -> ('a -> 'b) -> Update<'b>

  val sequenceI: length: ('xs -> int)
              -> iter: (('x -> unit) -> 'xs -> unit)
              -> ofArray: (array<'y> -> 'ys)
              -> Optic<'xs, 'x, 'y, 'ys>

  //# Operations on Optics

  val   view: Optic<'s, 'a,         _,  _> ->               's -> 'a
  val   over: Optic<'s, 'a,        'b, 't> -> ('a -> 'b) -> 's -> 't
  val    inline set: Optic<'s,  _,        'b, 't> ->        'b  -> 's -> 't
  val inline remove: Optic<'s,  _, option<_>, 't> ->               's -> 't

  val foldOf: Optic<'s, 'a, _, _> -> ('x -> 'a -> 'x) -> 'x -> 's -> 'x

  //# Search combinators

  val inline choose: ('s -> Optic<'s, 'a, 'b, 't>) -> Optic<'s, 'a, 'b, 't>

  val (<|>): Optic<'s, option<'a>, 'b, 't>
          -> Optic<'s, option<'a>, 'b, 't>
          -> Optic<'s, option<'a>, 'b, 't>

  //# Picking combinators

  val (<^>): Optic<'s, 'a,      'b     , 's>
          -> Optic<'s,      'c,      'd, 't>
          -> Optic<'s, 'a * 'c, 'b * 'd, 't>

  //# Traversal composition

  val (<=>): Optic<'s, 'a, 'b, 'u>
          -> Optic<'u, 'a, 'b, 't>
          -> Optic<'s, 'a, 'b, 't>

  //# Union type helpers

  val inline none: (option<'a> -> Update<'b>) -> ('b -> 't)       -> Update<'t>
  val inline some: (option<'a> -> Update<'b>) -> ('b -> 't) -> 'a -> Update<'t>

  //# Optic combinators

  val inline normalize: ('s -> 't) -> Optic<'s, 't, 's, 't>

  val replace: inn: 'a -> out: 'a -> Optic<'a, 'a> when 'a: equality

  val defaults: 's -> Optic<option<'s>, option<'s>> when 's: equality

  //# Partial lens intros

  val ofPrism': insert:                                     ('b -> option<'t>)
             ->  prism: Optic<       's,  option<'a>,        'b,          't>
             ->         Optic<option<'s>, option<'a>,        'b,   option<'t>>

  val  ofPrism:  prism: Optic<       's,  option<'a>,        'b,          't>
             ->         Optic<option<'s>, option<'a>,        'b,   option<'t>>

  val  ofTotal: remove:             ('s                         -> option<'t>)
             -> insert:                                     ('b ->        't)
             ->   lens: Optic<       's,         'a,         'b,          't>
             ->         Optic<option<'s>, option<'a>, option<'b>,  option<'t>>

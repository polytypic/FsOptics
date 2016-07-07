namespace FsOptics

module List =
  val valuesT: Optic<list<'a>, 'a, 'b, list<'b>>

  val atL: int -> Optic<list<'a>, 'a>

  val  appendL:                 Optic<list<'a>, option<'a>>
  val prependL:                 Optic<list<'a>, option<'a>>
  val   indexL: int          -> Optic<list<'a>, option<'a>>
  val    inline findL: ('a -> bool) -> Optic<list<'a>, option<'a>>

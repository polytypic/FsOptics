namespace FsOptics

module Array =
  val valuesT: Optic<array<'a>, 'a, 'b, array<'b>>

  val  appendL:                 Optic<array<'a>, option<'a>>
  val prependL:                 Optic<array<'a>, option<'a>>
  val   indexL: int          -> Optic<array<'a>, option<'a>>
  val    inline findL: ('a -> bool) -> Optic<array<'a>, option<'a>>

namespace FsOptics

module Array =
  val elemsT: Optic<array<'a>, array<'b>, 'a, 'b>

  val      appendL:                 Optic<array<'a>, option<'a>>
  val       indexL: int          -> Optic<array<'a>, option<'a>>
  val inline findL: ('a -> bool) -> Optic<array<'a>, option<'a>>

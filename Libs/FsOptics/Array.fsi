namespace FsOptics

module Array =
  val elemsT: Optic<array<'a>, array<'b>, 'a, 'b>

  val      appendO:                 Optic<array<'a>, option<'a>>
  val       indexO: int          -> Optic<array<'a>, option<'a>>
  val inline findO: ('a -> bool) -> Optic<array<'a>, option<'a>>

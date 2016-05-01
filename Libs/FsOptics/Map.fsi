namespace FsOptics

module Map =
  val arrayM: Optic<Map<'ka, 'va>, Map<'kb, 'vb>, array<'ka * 'va>, array<'kb * 'vb>>

  val valuesT: Optic<Map<'k, 'a>, Map<'k, 'b>, 'a, 'b>

  val valueL: 'k -> Optic<Map<'k, 'v>, option<'v>>

namespace FsOptics

module Map =
  val arrayM: Optic<Map<'ka, 'va>, array<'ka * 'va>, array<'kb * 'vb>, Map<'kb, 'vb>>

  val valuesT: Optic<Map<'k, 'a>, 'a, 'b, Map<'k, 'b>>

  val indexL: 'k -> Optic<Map<'k, 'v>, option<'v>>

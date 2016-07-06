namespace FsOptics

module Option =
  val valuesT: Optic<option<'a>, 'a, 'b, option<'b>>
  val valueL: Optic<option<'a>, option<'a>, 'b, option<'b>>

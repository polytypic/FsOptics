namespace FsOptics

module List =
  let elemsT U = sequenceI List.length List.iter List.ofArray U

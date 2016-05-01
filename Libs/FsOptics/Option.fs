namespace FsOptics

module Option =
  let valueL U = function
    | None -> U None </> constant None
    | Some v -> U ^ Some v </> Some

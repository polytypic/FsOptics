namespace FsOptics

module Option =
  let valueT U = function Some x -> U x </> Some
                        | None   -> result None
  let valueL U = function Some v -> some U Some v
                        | None   -> none U Some

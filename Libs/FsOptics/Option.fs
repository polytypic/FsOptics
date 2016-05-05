namespace FsOptics

module Option =
  let valueL U = function Some v -> some U Some v
                        | None   -> none U Some

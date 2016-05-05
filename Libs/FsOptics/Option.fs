namespace FsOptics

module Option =
  let valueL U = function None   -> none Some   U
                        | Some v -> some Some v U

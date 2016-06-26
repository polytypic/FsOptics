namespace FsOptics

module Option =
  let valueL U (u: Update) = function Some v -> some (U u) Some v
                                    | None   -> none (U u) Some

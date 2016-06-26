namespace FsOptics

module Map =
  let arrayM U (u: Update) s = U u ^ Map.toArray s </> Map.ofArray

  let valuesT : Optic<Map<'k, 'a>, 'a, 'b, Map<'k, 'b>> = fun U u ->
    (arrayM << Array.elemsT << item2) U u

  let valueL k U (u: Update) s =
    U u ^ Map.tryFind k s
    </> function None -> Map.remove k s
               | Some v -> Map.add k v s

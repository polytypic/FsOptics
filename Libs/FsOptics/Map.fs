namespace FsOptics

module Map =
  let arrayM U s = U ^ Map.toArray s </> Map.ofArray

  let valuesT : Optic<Map<'k, 'a>, 'a, 'b, Map<'k, 'b>> = fun U ->
    arrayM << Array.valuesT << item2 <| U

  let indexL k U s =
    U ^ Map.tryFind k s
    </> function None -> Map.remove k s
               | Some v -> Map.add k v s

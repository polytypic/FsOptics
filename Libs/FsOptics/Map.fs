namespace FsOptics

module Map =
  let arrayM U s = U ^ Map.toArray s </> Map.ofArray

  let valuesT : Optic<Map<'k, 'a>, Map<'k, 'b>, 'a, 'b> = fun U ->
    arrayM << Array.elemsT << item2 <| U

  let valueL k U s =
    U ^ Map.tryFind k s
    </> function None -> Map.remove k s
               | Some v -> Map.add k v s
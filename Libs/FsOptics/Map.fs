namespace FsOptics

module Map =
  let arrayI U s = U ^ Map.toArray s </> Map.ofArray

  let valuesT : Optic<Map<'k, 'a>, Map<'k, 'b>, 'a, 'b> = fun U ->
    arrayI << Array.elemsT << _2 <| U

  let valueO k U s =
    U ^ Map.tryFind k s
    </> function None -> Map.remove k s
               | Some v -> Map.add k v s

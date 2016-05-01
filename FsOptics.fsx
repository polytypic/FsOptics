#I "Libs/FsOptics/bin/Debug"

#r "FsOptics.dll"

let inline constant x _ = x
let inline ( ^ ) f = f
let inline ( *< ) f = f

open FsOptics

module Example1 =
  type Text =
    {language: string; text: string}
    static member languageL U r = U r.language </> fun x -> {r with language=x}
    static member textL     U r = U r.text     </> fun x -> {r with     text=x}

  type Data =
    {contents: array<Text>}
    static member contentsL U r = U r.contents </> fun x -> {r with contents=x}

  let textIn language =
       Data.contentsL
    << normalize ^ Array.sortBy ^ view Text.languageL
    << Array.findL (view Text.languageL >> (=) language)
    << ofTotal *< constant None
               *< fun text -> {language = language; text = text}
               *< Text.textL

  let data = {contents = [|{language = "en"; text = "Title" }
                           {language = "sv"; text = "Rubrik"}|]}

  let run () =
    data |> view ^ textIn "sv" |> printfn "%A"
    data |> view ^ textIn "en" |> printfn "%A"
    data |> view ^ textIn "fi" |> printfn "%A"

    set <| textIn "en"
        <| Some "The title"
        <| data
    |> printfn "%A"
    set <| textIn "fi"
        <| Some "Otsikko"
        <| data
    |> printfn "%A"

    data
    |> remove ^ textIn "sv"
    |> printfn "%A"

    data
    |> remove ^ textIn "en"
    |> remove ^ textIn "sv"
    |> printfn "%A"

module Example2 =
  type Node<'k, 'v> =
    {k: 'k
     v: 'v
     lt: BST<'k, 'v>
     gt: BST<'k, 'v>}
  and BST<'k, 'v> = option<Node<'k, 'v>>

  module Node =
    let kL  U n = U n.k  </> fun x -> {n with  k=x}
    let vL  U n = U n.v  </> fun x -> {n with  v=x}
    let ltL U n = U n.lt </> fun x -> {n with lt=x}
    let gtL U n = U n.gt </> fun x -> {n with gt=x}

  module BST =
    let rec vT x2yF = function
      | None -> result None
      | Some n ->
        fun v lt gt -> Some {k=n.k; v=v; lt=lt; gt=gt}
        <&> x2yF n.v
        <*> vT x2yF n.lt
        <*> vT x2yF n.gt

    let rec searchL k =
      choose ^ function
       | Some n when k <> n.k ->
         ofPrism' (if k < n.k then Node.ltL else Node.gtL) << searchL k
       | _ -> id

    let valueOfL k =
         searchL k
      << ofTotal *< function {lt = None; gt = n} | {lt = n; gt = None} -> n
                           | {lt = Some lt; gt = Some gt} ->
                             set <| searchL lt.k <| Some lt <| Some gt
                 *< fun v -> {k=k; v=v; lt=None; gt=None}
                 *< Node.vL

  let tree =
    [3,"a"; 1,"b"; 4,"c"; 1,"d"; 5,"e"; 9,"f"; 2,"g"]
    |> List.fold *< fun t (k, v) -> set <| BST.valueOfL k <| Some v <| t
                 *< None

  let run () =
    printfn "%A" tree

    tree
    |> remove ^ BST.valueOfL 1
    |> printfn "%A"

    tree
    |> over BST.vT ^ fun v -> v + v
    |> printfn "%A"

    tree
    |> foldOf BST.vT (+) ""
    |> printfn "%A"

module Example3 =
  let run () =
    (((1, "a", false), (2, "b", true)), [(3, 1.0); (15, 2.0)])
    |> set ((item1 << (item1 <=> item2) << item1) <=>
            (item2 << List.elemsT << item1)) "lol"
    |> printfn "%A"

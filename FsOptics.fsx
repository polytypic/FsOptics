#I "Libs/FsOptics/bin/Debug"

#r "FsOptics.dll"

let inline constant x _ = x
let inline ( ^ ) f = f
let inline ( *< ) f = f

open FsOptics

module Example1 =
  type Text =
    {language: string; text: string}
    static member languageO U r = U r.language </> fun x -> {r with language=x}
    static member textO     U r = U r.text     </> fun x -> {r with     text=x}

  type Data =
    {contents: array<Text>}
    static member contentsO U r = U r.contents </> fun x -> {r with contents=x}

  let textIn language =
       Data.contentsO
    << normalize ^ Array.sortBy ^ view Text.languageO
    << Array.findO (view Text.languageO >> (=) language)
    << ofTotal *< constant None
               *< fun text -> {language = language; text = text}
               *< Text.textO

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
    let kO  U n = U n.k  </> fun x -> {n with  k=x}
    let vO  U n = U n.v  </> fun x -> {n with  v=x}
    let ltO U n = U n.lt </> fun x -> {n with lt=x}
    let gtO U n = U n.gt </> fun x -> {n with gt=x}

  module BST =
    let rec vT x2yF = function
      | None -> result None
      | Some n ->
        fun v lt gt -> Some {k=n.k; v=v; lt=lt; gt=gt}
        <&> x2yF n.v
        <*> vT x2yF n.lt
        <*> vT x2yF n.gt

    let rec searchO k =
      choose ^ function
       | Some n when k <> n.k ->
         ofPrism' (if k < n.k then Node.ltO else Node.gtO) << searchO k
       | _ -> id

    let valueOfO k =
         searchO k
      << ofTotal *< function {lt = None; gt = n} | {lt = n; gt = None} -> n
                           | {lt = Some lt; gt = Some gt} ->
                             set <| searchO lt.k <| Some lt <| Some gt
                 *< fun v -> {k=k; v=v; lt=None; gt=None}
                 *< Node.vO

  let tree =
    [3,"a"; 1,"b"; 4,"c"; 1,"d"; 5,"e"; 9,"f"; 2,"g"]
    |> List.fold *< fun t (k, v) -> set <| BST.valueOfO k <| Some v <| t
                 *< None

  let run () =
    printfn "%A" tree

    tree
    |> remove ^ BST.valueOfO 1
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
    |> set ((_1 << (_1 <=> _2) << _1) <=> (_2 << List.elemsT << _1)) "lol"
    |> printfn "%A"

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
    {key: 'k
     value: 'v
     smaller: BST<'k, 'v>
     greater: BST<'k, 'v>}
  and BST<'k, 'v> = option<Node<'k, 'v>>

  module Node =
    let keyL     U r = U r.key     </> fun x -> {r with     key=x}
    let valueL   U r = U r.value   </> fun x -> {r with   value=x}
    let smallerL U r = U r.smaller </> fun x -> {r with smaller=x}
    let greaterL U r = U r.greater </> fun x -> {r with greater=x}

  module BST =
    let rec valuesT U = function
      | None -> result None
      | Some node ->
            fun value smaller greater ->
              Some {key=node.key; value=value; smaller=smaller; greater=greater}
        <&> U node.value
        <*> valuesT U node.smaller
        <*> valuesT U node.greater

    let rec nodeL key =
      choose ^ function
       | Some node when key <> node.key ->
            ofPrism' ^ if key < node.key then Node.smallerL else Node.greaterL
         << nodeL key
       | _ -> id

    let valueL key =
         nodeL key
      << ofTotal *< function {smaller = None; greater = node}
                           | {smaller = node; greater = None} ->
                             node
                           | node ->
                             set (nodeL key) node.smaller node.greater
                 *< fun value ->
                      {key=key; value=value; smaller=None; greater=None}
                 *< Node.valueL

  let tree =
    [3,"a"; 1,"b"; 4,"c"; 1,"d"; 5,"e"; 9,"f"; 2,"g"]
    |> List.fold *< fun t (k, v) -> set <| BST.valueL k <| Some v <| t
                 *< None

  let run () =
    printfn "%A" tree

    tree
    |> remove ^ BST.valueL 1
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

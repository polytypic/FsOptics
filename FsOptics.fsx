#I "Libs/FsOptics/bin/Debug"

#r "FsOptics.dll"

let inline constant x _ = x
let inline ( ^ ) f = f
let inline ( *< ) f = f

open FsOptics

module Example1 =
  type Text =
    {language: string; text: string}
    static member languageL U u r = U u r.language </> fun x -> {r with language=x}
    static member     textL U u r = U u r.text     </> fun x -> {r with     text=x}

  type Data =
    {contents: array<Text>}
    static member contentsL U u r = U u r.contents </> fun x -> {r with contents=x}

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
    let     keyL U u r = U u r.key     </> fun x -> {r with     key=x}
    let   valueL U u r = U u r.value   </> fun x -> {r with   value=x}
    let smallerL U u r = U u r.smaller </> fun x -> {r with smaller=x}
    let greaterL U u r = U u r.greater </> fun x -> {r with greater=x}

  module BST =
    let rec valuesT U u = function
      | None -> result None
      | Some node ->
            fun value smaller greater ->
              Some {key=node.key; value=value; smaller=smaller; greater=greater}
        <&> U u node.value
        <*> valuesT U u node.smaller
        <*> valuesT U u node.greater

    let rec nodeL key =
      choose ^ function
       | Some node when key <> node.key ->
            ofPrism ^ if key < node.key then Node.smallerL else Node.greaterL
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
    |> over BST.valuesT ^ fun v -> v + v
    |> printfn "%A"

    tree
    |> foldOf BST.valuesT (+) ""
    |> printfn "%A"

module Example22 =
  type BST<'k, 'v> =
    | Branch of key: 'k * value: 'v * smaller: BST<'k, 'v> * greater: BST<'k, 'v>
    | Empty

  module BST =
    let rec valuesT U u = function
      | Empty -> result Empty
      | Branch (key, value, smaller, greater) ->
            fun value smaller greater ->
              Branch (key, value, smaller, greater)
        <&> U u value
        <*> valuesT U u smaller
        <*> valuesT U u greater

    let toOption = function
      | Branch (k, v, s, g) -> Some (k, v, s, g)
      | Empty               -> None
    let ofOption = function
      | Some (k, v, s, g) -> Branch (k, v, s, g)
      | None              -> Empty

    let toOptionM U u x = U u ^ toOption x </> ofOption
    let ofOptionM U u x = U u ^ ofOption x </> toOption

    let rec nodeL key =
      choose ^ function
       | Branch (key', _, _, _) when key <> key' ->
            toOptionM
         << ofPrism ^ ((if key < key' then item3 else item4) << toOptionM)
         << ofOptionM
         << nodeL key
       | _ ->
         id

    let valueL key =
         nodeL key
      << toOptionM
      << ofTotal *< (toOption << function
                      | (_, _, Empty, node) | (_, _, node, Empty) ->
                        node
                      | (_, _, smaller, greater) ->
                        set (nodeL key) smaller greater)
                 *< fun value ->
                      (key, value, Empty, Empty)
                 *< item2

  let tree =
    [3,"a"; 1,"b"; 4,"c"; 1,"d"; 5,"e"; 9,"f"; 2,"g"]
    |> List.fold *< fun t (k, v) -> set <| BST.valueL k <| Some v <| t
                 *< Empty

  let run () =
    printfn "%A" tree

    tree
    |> remove ^ BST.valueL 1
    |> printfn "%A"

    tree
    |> over BST.valuesT ^ fun v -> v + v
    |> printfn "%A"

    tree
    |> foldOf BST.valuesT (+) ""
    |> printfn "%A"

module Example3 =
  let run () =
    (((1, "a", false), (2, "b", true)), [(3, 1.0); (15, 2.0)])
    |> set ((item1 << (item1 <=> item2) << item1) <=>
            (item2 << List.elemsT << item1)) "lol"
    |> printfn "%A"

    Choice2Of3 [|Some "a"|]
    |> view (choice2
          << ofPrism ^ Array.indexL 0
          << ofPrism ^ Option.valueL)
    |> printfn "%A"

module Example4 =
  type Foo<'x> =
    | Bar of int * string
    | Baz of 'x

  let BarL U u = function Bar (x, y) -> some (U u) Bar (x, y) | _ -> none (U u) Bar
  let BazL U u = function Baz x      -> some (U u) Baz x      | _ -> none (U u) Baz

  let run () =
    Baz 10
    |> view BarL
    |> printfn "%A"

    Baz "10"
    |> view BazL
    |> printfn "%A"

    Baz 10
    |> set BarL (1, "2")
    |> printfn "%A"

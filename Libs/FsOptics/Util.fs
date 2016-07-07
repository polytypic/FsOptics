namespace FsOptics

[<AutoOpen>]
module internal Util =

  let inline (^) f = f
  let inline flip f y x = f x y
  let inline constant x _ = x

  module List =
    let revSplitAt i xs =
      if i < 0 then failwith "Negative index"
      let rec lp i ys xs =
        if i <= 0 then
          (ys, xs)
        else
          match xs with
           | [] -> failwith "Index too high"
           | y::xs -> lp (i-1) (y::ys) xs
      lp i [] xs

    let revAppend ys xs = List.fold (fun xs y -> y::xs) xs ys

  module Option =
    let inline getOr x = function None -> x | Some x -> x

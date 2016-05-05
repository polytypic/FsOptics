namespace FsOptics

type C1 = C1 with
  static member (+) (C1,c) = match c with Choice1Of2 c -> some Choice1Of2 c | _ -> none Choice1Of2
  static member (+) (C1,c) = match c with Choice1Of3 c -> some Choice1Of3 c | _ -> none Choice1Of3
  static member (+) (C1,c) = match c with Choice1Of4 c -> some Choice1Of4 c | _ -> none Choice1Of4
  static member (+) (C1,c) = match c with Choice1Of5 c -> some Choice1Of5 c | _ -> none Choice1Of5
  static member (+) (C1,c) = match c with Choice1Of6 c -> some Choice1Of6 c | _ -> none Choice1Of6
  static member (+) (C1,c) = match c with Choice1Of7 c -> some Choice1Of7 c | _ -> none Choice1Of7

type C2 = C2 with
  static member (+) (C2,c) = match c with Choice2Of2 c -> some Choice2Of2 c | _ -> none Choice2Of2
  static member (+) (C2,c) = match c with Choice2Of3 c -> some Choice2Of3 c | _ -> none Choice2Of3
  static member (+) (C2,c) = match c with Choice2Of4 c -> some Choice2Of4 c | _ -> none Choice2Of4
  static member (+) (C2,c) = match c with Choice2Of5 c -> some Choice2Of5 c | _ -> none Choice2Of5
  static member (+) (C2,c) = match c with Choice2Of6 c -> some Choice2Of6 c | _ -> none Choice2Of6
  static member (+) (C2,c) = match c with Choice2Of7 c -> some Choice2Of7 c | _ -> none Choice2Of7

type C3 = C3 with
  static member (+) (C3,c) = match c with Choice3Of3 c -> some Choice3Of3 c | _ -> none Choice3Of3
  static member (+) (C3,c) = match c with Choice3Of4 c -> some Choice3Of4 c | _ -> none Choice3Of4
  static member (+) (C3,c) = match c with Choice3Of5 c -> some Choice3Of5 c | _ -> none Choice3Of5
  static member (+) (C3,c) = match c with Choice3Of6 c -> some Choice3Of6 c | _ -> none Choice3Of6
  static member (+) (C3,c) = match c with Choice3Of7 c -> some Choice3Of7 c | _ -> none Choice3Of7

type C4 = C4 with
  static member (+) (C4,c) = match c with Choice4Of4 c -> some Choice4Of4 c | _ -> none Choice4Of4
  static member (+) (C4,c) = match c with Choice4Of5 c -> some Choice4Of5 c | _ -> none Choice4Of5
  static member (+) (C4,c) = match c with Choice4Of6 c -> some Choice4Of6 c | _ -> none Choice4Of6
  static member (+) (C4,c) = match c with Choice4Of7 c -> some Choice4Of7 c | _ -> none Choice4Of7

type C5 = C5 with
  static member (+) (C5,c) = match c with Choice5Of5 c -> some Choice5Of5 c | _ -> none Choice5Of5
  static member (+) (C5,c) = match c with Choice5Of6 c -> some Choice5Of6 c | _ -> none Choice5Of6
  static member (+) (C5,c) = match c with Choice5Of7 c -> some Choice5Of7 c | _ -> none Choice5Of7

type C6 = C6 with
  static member (+) (C6,c) = match c with Choice6Of6 c -> some Choice6Of6 c | _ -> none Choice6Of6
  static member (+) (C6,c) = match c with Choice6Of7 c -> some Choice6Of7 c | _ -> none Choice6Of7

type C7 = C7 with
  static member (+) (C7,c) = match c with Choice7Of7 c -> some Choice7Of7 c | _ -> none Choice7Of7

[<AutoOpen>]
module Choice =
  let inline choice1 U s = (C1 + s) U
  let inline choice2 U s = (C2 + s) U
  let inline choice3 U s = (C3 + s) U
  let inline choice4 U s = (C4 + s) U
  let inline choice5 U s = (C5 + s) U
  let inline choice6 U s = (C6 + s) U
  let inline choice7 U s = (C7 + s) U

namespace FsOptics

type C1 = C1 with
  static member (?<-) (U,C1,c) = match c with Choice1Of2 c -> some U Choice1Of2 c | _ -> none U Choice1Of2
  static member (?<-) (U,C1,c) = match c with Choice1Of3 c -> some U Choice1Of3 c | _ -> none U Choice1Of3
  static member (?<-) (U,C1,c) = match c with Choice1Of4 c -> some U Choice1Of4 c | _ -> none U Choice1Of4
  static member (?<-) (U,C1,c) = match c with Choice1Of5 c -> some U Choice1Of5 c | _ -> none U Choice1Of5
  static member (?<-) (U,C1,c) = match c with Choice1Of6 c -> some U Choice1Of6 c | _ -> none U Choice1Of6
  static member (?<-) (U,C1,c) = match c with Choice1Of7 c -> some U Choice1Of7 c | _ -> none U Choice1Of7

type C2 = C2 with
  static member (?<-) (U,C2,c) = match c with Choice2Of2 c -> some U Choice2Of2 c | _ -> none U Choice2Of2
  static member (?<-) (U,C2,c) = match c with Choice2Of3 c -> some U Choice2Of3 c | _ -> none U Choice2Of3
  static member (?<-) (U,C2,c) = match c with Choice2Of4 c -> some U Choice2Of4 c | _ -> none U Choice2Of4
  static member (?<-) (U,C2,c) = match c with Choice2Of5 c -> some U Choice2Of5 c | _ -> none U Choice2Of5
  static member (?<-) (U,C2,c) = match c with Choice2Of6 c -> some U Choice2Of6 c | _ -> none U Choice2Of6
  static member (?<-) (U,C2,c) = match c with Choice2Of7 c -> some U Choice2Of7 c | _ -> none U Choice2Of7

type C3 = C3 with
  static member (?<-) (U,C3,c) = match c with Choice3Of3 c -> some U Choice3Of3 c | _ -> none U Choice3Of3
  static member (?<-) (U,C3,c) = match c with Choice3Of4 c -> some U Choice3Of4 c | _ -> none U Choice3Of4
  static member (?<-) (U,C3,c) = match c with Choice3Of5 c -> some U Choice3Of5 c | _ -> none U Choice3Of5
  static member (?<-) (U,C3,c) = match c with Choice3Of6 c -> some U Choice3Of6 c | _ -> none U Choice3Of6
  static member (?<-) (U,C3,c) = match c with Choice3Of7 c -> some U Choice3Of7 c | _ -> none U Choice3Of7

type C4 = C4 with
  static member (?<-) (U,C4,c) = match c with Choice4Of4 c -> some U Choice4Of4 c | _ -> none U Choice4Of4
  static member (?<-) (U,C4,c) = match c with Choice4Of5 c -> some U Choice4Of5 c | _ -> none U Choice4Of5
  static member (?<-) (U,C4,c) = match c with Choice4Of6 c -> some U Choice4Of6 c | _ -> none U Choice4Of6
  static member (?<-) (U,C4,c) = match c with Choice4Of7 c -> some U Choice4Of7 c | _ -> none U Choice4Of7

type C5 = C5 with
  static member (?<-) (U,C5,c) = match c with Choice5Of5 c -> some U Choice5Of5 c | _ -> none U Choice5Of5
  static member (?<-) (U,C5,c) = match c with Choice5Of6 c -> some U Choice5Of6 c | _ -> none U Choice5Of6
  static member (?<-) (U,C5,c) = match c with Choice5Of7 c -> some U Choice5Of7 c | _ -> none U Choice5Of7

type C6 = C6 with
  static member (?<-) (U,C6,c) = match c with Choice6Of6 c -> some U Choice6Of6 c | _ -> none U Choice6Of6
  static member (?<-) (U,C6,c) = match c with Choice6Of7 c -> some U Choice6Of7 c | _ -> none U Choice6Of7

type C7 = C7 with
  static member (?<-) (U,C7,c) = match c with Choice7Of7 c -> some U Choice7Of7 c | _ -> none U Choice7Of7

[<AutoOpen>]
module Choice =
  let inline choice1 U s = U ? (C1) <- s
  let inline choice2 U s = U ? (C2) <- s
  let inline choice3 U s = U ? (C3) <- s
  let inline choice4 U s = U ? (C4) <- s
  let inline choice5 U s = U ? (C5) <- s
  let inline choice6 U s = U ? (C6) <- s
  let inline choice7 U s = U ? (C7) <- s

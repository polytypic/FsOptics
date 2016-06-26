namespace FsOptics

type C1 = C1 with
  static member (?<-) (W,C1,c) = match c with Choice1Of2 c -> some W Choice1Of2 c | _ -> none W Choice1Of2
  static member (?<-) (W,C1,c) = match c with Choice1Of3 c -> some W Choice1Of3 c | _ -> none W Choice1Of3
  static member (?<-) (W,C1,c) = match c with Choice1Of4 c -> some W Choice1Of4 c | _ -> none W Choice1Of4
  static member (?<-) (W,C1,c) = match c with Choice1Of5 c -> some W Choice1Of5 c | _ -> none W Choice1Of5
  static member (?<-) (W,C1,c) = match c with Choice1Of6 c -> some W Choice1Of6 c | _ -> none W Choice1Of6
  static member (?<-) (W,C1,c) = match c with Choice1Of7 c -> some W Choice1Of7 c | _ -> none W Choice1Of7

type C2 = C2 with
  static member (?<-) (W,C2,c) = match c with Choice2Of2 c -> some W Choice2Of2 c | _ -> none W Choice2Of2
  static member (?<-) (W,C2,c) = match c with Choice2Of3 c -> some W Choice2Of3 c | _ -> none W Choice2Of3
  static member (?<-) (W,C2,c) = match c with Choice2Of4 c -> some W Choice2Of4 c | _ -> none W Choice2Of4
  static member (?<-) (W,C2,c) = match c with Choice2Of5 c -> some W Choice2Of5 c | _ -> none W Choice2Of5
  static member (?<-) (W,C2,c) = match c with Choice2Of6 c -> some W Choice2Of6 c | _ -> none W Choice2Of6
  static member (?<-) (W,C2,c) = match c with Choice2Of7 c -> some W Choice2Of7 c | _ -> none W Choice2Of7

type C3 = C3 with
  static member (?<-) (W,C3,c) = match c with Choice3Of3 c -> some W Choice3Of3 c | _ -> none W Choice3Of3
  static member (?<-) (W,C3,c) = match c with Choice3Of4 c -> some W Choice3Of4 c | _ -> none W Choice3Of4
  static member (?<-) (W,C3,c) = match c with Choice3Of5 c -> some W Choice3Of5 c | _ -> none W Choice3Of5
  static member (?<-) (W,C3,c) = match c with Choice3Of6 c -> some W Choice3Of6 c | _ -> none W Choice3Of6
  static member (?<-) (W,C3,c) = match c with Choice3Of7 c -> some W Choice3Of7 c | _ -> none W Choice3Of7

type C4 = C4 with
  static member (?<-) (W,C4,c) = match c with Choice4Of4 c -> some W Choice4Of4 c | _ -> none W Choice4Of4
  static member (?<-) (W,C4,c) = match c with Choice4Of5 c -> some W Choice4Of5 c | _ -> none W Choice4Of5
  static member (?<-) (W,C4,c) = match c with Choice4Of6 c -> some W Choice4Of6 c | _ -> none W Choice4Of6
  static member (?<-) (W,C4,c) = match c with Choice4Of7 c -> some W Choice4Of7 c | _ -> none W Choice4Of7

type C5 = C5 with
  static member (?<-) (W,C5,c) = match c with Choice5Of5 c -> some W Choice5Of5 c | _ -> none W Choice5Of5
  static member (?<-) (W,C5,c) = match c with Choice5Of6 c -> some W Choice5Of6 c | _ -> none W Choice5Of6
  static member (?<-) (W,C5,c) = match c with Choice5Of7 c -> some W Choice5Of7 c | _ -> none W Choice5Of7

type C6 = C6 with
  static member (?<-) (W,C6,c) = match c with Choice6Of6 c -> some W Choice6Of6 c | _ -> none W Choice6Of6
  static member (?<-) (W,C6,c) = match c with Choice6Of7 c -> some W Choice6Of7 c | _ -> none W Choice6Of7

type C7 = C7 with
  static member (?<-) (W,C7,c) = match c with Choice7Of7 c -> some W Choice7Of7 c | _ -> none W Choice7Of7

[<AutoOpen>]
module Choice =
  let inline choice1 U (u: Update) s = (U u) ? (C1) <- s
  let inline choice2 U (u: Update) s = (U u) ? (C2) <- s
  let inline choice3 U (u: Update) s = (U u) ? (C3) <- s
  let inline choice4 U (u: Update) s = (U u) ? (C4) <- s
  let inline choice5 U (u: Update) s = (U u) ? (C5) <- s
  let inline choice6 U (u: Update) s = (U u) ? (C6) <- s
  let inline choice7 U (u: Update) s = (U u) ? (C7) <- s

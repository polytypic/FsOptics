namespace FsOptics

[<AutoOpen>]
module internal C =
  let inline s U C c = U ^ Some c </> C
  let inline n U C c = U ^ None </> constant ^ C c

type C1 = C1 with
  static member (?<-) (U,C1,c) = match c with Choice1Of2 c -> s U Choice1Of2 c | Choice2Of2 c -> n U Choice2Of2 c
  static member (?<-) (U,C1,c) = match c with Choice1Of3 c -> s U Choice1Of3 c | Choice2Of3 c -> n U Choice2Of3 c | Choice3Of3 c -> n U Choice3Of3 c
  static member (?<-) (U,C1,c) = match c with Choice1Of4 c -> s U Choice1Of4 c | Choice2Of4 c -> n U Choice2Of4 c | Choice3Of4 c -> n U Choice3Of4 c | Choice4Of4 c -> n U Choice4Of4 c
  static member (?<-) (U,C1,c) = match c with Choice1Of5 c -> s U Choice1Of5 c | Choice2Of5 c -> n U Choice2Of5 c | Choice3Of5 c -> n U Choice3Of5 c | Choice4Of5 c -> n U Choice4Of5 c | Choice5Of5 c -> n U Choice5Of5 c
  static member (?<-) (U,C1,c) = match c with Choice1Of6 c -> s U Choice1Of6 c | Choice2Of6 c -> n U Choice2Of6 c | Choice3Of6 c -> n U Choice3Of6 c | Choice4Of6 c -> n U Choice4Of6 c | Choice5Of6 c -> n U Choice5Of6 c | Choice6Of6 c -> n U Choice6Of6 c
//  static member (?<-) (U,C1,c) = match c with Choice1Of7 c -> s U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

type C2 = C2 with
  static member (?<-) (U,C2,c) = match c with Choice1Of2 c -> n U Choice1Of2 c | Choice2Of2 c -> s U Choice2Of2 c
  static member (?<-) (U,C2,c) = match c with Choice1Of3 c -> n U Choice1Of3 c | Choice2Of3 c -> s U Choice2Of3 c | Choice3Of3 c -> n U Choice3Of3 c
  static member (?<-) (U,C2,c) = match c with Choice1Of4 c -> n U Choice1Of4 c | Choice2Of4 c -> s U Choice2Of4 c | Choice3Of4 c -> n U Choice3Of4 c | Choice4Of4 c -> n U Choice4Of4 c
  static member (?<-) (U,C2,c) = match c with Choice1Of5 c -> n U Choice1Of5 c | Choice2Of5 c -> s U Choice2Of5 c | Choice3Of5 c -> n U Choice3Of5 c | Choice4Of5 c -> n U Choice4Of5 c | Choice5Of5 c -> n U Choice5Of5 c
  static member (?<-) (U,C2,c) = match c with Choice1Of6 c -> n U Choice1Of6 c | Choice2Of6 c -> s U Choice2Of6 c | Choice3Of6 c -> n U Choice3Of6 c | Choice4Of6 c -> n U Choice4Of6 c | Choice5Of6 c -> n U Choice5Of6 c | Choice6Of6 c -> n U Choice6Of6 c
//  static member (?<-) (U,C2,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> s U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

type C3 = C3 with
  static member (?<-) (U,C3,c) = match c with Choice1Of3 c -> n U Choice1Of3 c | Choice2Of3 c -> n U Choice2Of3 c | Choice3Of3 c -> s U Choice3Of3 c
  static member (?<-) (U,C3,c) = match c with Choice1Of4 c -> n U Choice1Of4 c | Choice2Of4 c -> n U Choice2Of4 c | Choice3Of4 c -> s U Choice3Of4 c | Choice4Of4 c -> n U Choice4Of4 c
  static member (?<-) (U,C3,c) = match c with Choice1Of5 c -> n U Choice1Of5 c | Choice2Of5 c -> n U Choice2Of5 c | Choice3Of5 c -> s U Choice3Of5 c | Choice4Of5 c -> n U Choice4Of5 c | Choice5Of5 c -> n U Choice5Of5 c
  static member (?<-) (U,C3,c) = match c with Choice1Of6 c -> n U Choice1Of6 c | Choice2Of6 c -> n U Choice2Of6 c | Choice3Of6 c -> s U Choice3Of6 c | Choice4Of6 c -> n U Choice4Of6 c | Choice5Of6 c -> n U Choice5Of6 c | Choice6Of6 c -> n U Choice6Of6 c
//  static member (?<-) (U,C3,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> s U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

type C4 = C4 with
  static member (?<-) (U,C4,c) = match c with Choice1Of4 c -> n U Choice1Of4 c | Choice2Of4 c -> n U Choice2Of4 c | Choice3Of4 c -> n U Choice3Of4 c | Choice4Of4 c -> s U Choice4Of4 c
  static member (?<-) (U,C4,c) = match c with Choice1Of5 c -> n U Choice1Of5 c | Choice2Of5 c -> n U Choice2Of5 c | Choice3Of5 c -> n U Choice3Of5 c | Choice4Of5 c -> s U Choice4Of5 c | Choice5Of5 c -> n U Choice5Of5 c
  static member (?<-) (U,C4,c) = match c with Choice1Of6 c -> n U Choice1Of6 c | Choice2Of6 c -> n U Choice2Of6 c | Choice3Of6 c -> n U Choice3Of6 c | Choice4Of6 c -> s U Choice4Of6 c | Choice5Of6 c -> n U Choice5Of6 c | Choice6Of6 c -> n U Choice6Of6 c
//  static member (?<-) (U,C4,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> s U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

type C5 = C5 with
  static member (?<-) (U,C5,c) = match c with Choice1Of5 c -> n U Choice1Of5 c | Choice2Of5 c -> n U Choice2Of5 c | Choice3Of5 c -> n U Choice3Of5 c | Choice4Of5 c -> n U Choice4Of5 c | Choice5Of5 c -> s U Choice5Of5 c
  static member (?<-) (U,C5,c) = match c with Choice1Of6 c -> n U Choice1Of6 c | Choice2Of6 c -> n U Choice2Of6 c | Choice3Of6 c -> n U Choice3Of6 c | Choice4Of6 c -> n U Choice4Of6 c | Choice5Of6 c -> s U Choice5Of6 c | Choice6Of6 c -> n U Choice6Of6 c
//  static member (?<-) (U,C5,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> s U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

type C6 = C6 with
  static member (?<-) (U,C6,c) = match c with Choice1Of6 c -> n U Choice1Of6 c | Choice2Of6 c -> n U Choice2Of6 c | Choice3Of6 c -> n U Choice3Of6 c | Choice4Of6 c -> n U Choice4Of6 c | Choice5Of6 c -> n U Choice5Of6 c | Choice6Of6 c -> s U Choice6Of6 c
//  static member (?<-) (U,C6,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> s U Choice6Of7 c | Choice7Of7 c -> n U Choice7Of7 c

//type C7 = C7 with
//  static member (?<-) (U,C7,c) = match c with Choice1Of7 c -> n U Choice1Of7 c | Choice2Of7 c -> n U Choice2Of7 c | Choice3Of7 c -> n U Choice3Of7 c | Choice4Of7 c -> n U Choice4Of7 c | Choice5Of7 c -> n U Choice5Of7 c | Choice6Of7 c -> n U Choice6Of7 c | Choice7Of7 c -> s U Choice7Of7 c

[<AutoOpen>]
module Choice =
  let inline choice1 U s = U ? (C1) <- s
  let inline choice2 U s = U ? (C2) <- s
  let inline choice3 U s = U ? (C3) <- s
  let inline choice4 U s = U ? (C4) <- s
  let inline choice5 U s = U ? (C5) <- s
  let inline choice6 U s = U ? (C6) <- s
//  let inline choice7 U s = U ? (C7) <- s

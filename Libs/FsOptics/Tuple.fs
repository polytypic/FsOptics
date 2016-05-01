namespace FsOptics

type I1 = I1 with
  static member (?<-) (U,I1,(a,b))           = U a </> fun a -> (a,b)
  static member (?<-) (U,I1,(a,b,c))         = U a </> fun a -> (a,b,c)
  static member (?<-) (U,I1,(a,b,c,d))       = U a </> fun a -> (a,b,c,d)
  static member (?<-) (U,I1,(a,b,c,d,e))     = U a </> fun a -> (a,b,c,d,e)
  static member (?<-) (U,I1,(a,b,c,d,e,f))   = U a </> fun a -> (a,b,c,d,e,f)
  static member (?<-) (U,I1,(a,b,c,d,e,f,g)) = U a </> fun a -> (a,b,c,d,e,f,g)

type I2 = I2 with
  static member (?<-) (U,I2,(a,b))           = U b </> fun b -> (a,b)
  static member (?<-) (U,I2,(a,b,c))         = U b </> fun b -> (a,b,c)
  static member (?<-) (U,I2,(a,b,c,d))       = U b </> fun b -> (a,b,c,d)
  static member (?<-) (U,I2,(a,b,c,d,e))     = U b </> fun b -> (a,b,c,d,e)
  static member (?<-) (U,I2,(a,b,c,d,e,f))   = U b </> fun b -> (a,b,c,d,e,f)
  static member (?<-) (U,I2,(a,b,c,d,e,f,g)) = U b </> fun b -> (a,b,c,d,e,f,g)

type I3 = I3 with
  static member (?<-) (U,I3,(a,b,c))         = U c </> fun c -> (a,b,c)
  static member (?<-) (U,I3,(a,b,c,d))       = U c </> fun c -> (a,b,c,d)
  static member (?<-) (U,I3,(a,b,c,d,e))     = U c </> fun c -> (a,b,c,d,e)
  static member (?<-) (U,I3,(a,b,c,d,e,f))   = U c </> fun c -> (a,b,c,d,e,f)
  static member (?<-) (U,I3,(a,b,c,d,e,f,g)) = U c </> fun c -> (a,b,c,d,e,f,g)

type I4 = I4 with
  static member (?<-) (U,I4,(a,b,c,d))       = U d </> fun d -> (a,b,c,d)
  static member (?<-) (U,I4,(a,b,c,d,e))     = U d </> fun d -> (a,b,c,d,e)
  static member (?<-) (U,I4,(a,b,c,d,e,f))   = U d </> fun d -> (a,b,c,d,e,f)
  static member (?<-) (U,I4,(a,b,c,d,e,f,g)) = U d </> fun d -> (a,b,c,d,e,f,g)

type I5 = I5 with
  static member (?<-) (U,I5,(a,b,c,d,e))     = U e </> fun e -> (a,b,c,d,e)
  static member (?<-) (U,I5,(a,b,c,d,e,f))   = U e </> fun e -> (a,b,c,d,e,f)
  static member (?<-) (U,I5,(a,b,c,d,e,f,g)) = U e </> fun e -> (a,b,c,d,e,f,g)

type I6 = I6 with
  static member (?<-) (U,I6,(a,b,c,d,e,f))   = U f </> fun f -> (a,b,c,d,e,f)
  static member (?<-) (U,I6,(a,b,c,d,e,f,g)) = U f </> fun f -> (a,b,c,d,e,f,g)

type I7 = I7 with
  static member (?<-) (U,I7,(a,b,c,d,e,f,g)) = U g </> fun g -> (a,b,c,d,e,f,g)

[<AutoOpen>]
module Tuple =
  let inline item1 U S = U ? (I1) <- S
  let inline item2 U S = U ? (I2) <- S
  let inline item3 U S = U ? (I3) <- S
  let inline item4 U S = U ? (I4) <- S
  let inline item5 U S = U ? (I5) <- S
  let inline item6 U S = U ? (I6) <- S
  let inline item7 U S = U ? (I7) <- S

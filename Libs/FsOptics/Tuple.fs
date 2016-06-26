namespace FsOptics

type I1 = I1 with
  static member (?<-) (W,I1,(a,b))           = W a </> fun a -> (a,b)
  static member (?<-) (W,I1,(a,b,c))         = W a </> fun a -> (a,b,c)
  static member (?<-) (W,I1,(a,b,c,d))       = W a </> fun a -> (a,b,c,d)
  static member (?<-) (W,I1,(a,b,c,d,e))     = W a </> fun a -> (a,b,c,d,e)
  static member (?<-) (W,I1,(a,b,c,d,e,f))   = W a </> fun a -> (a,b,c,d,e,f)
  static member (?<-) (W,I1,(a,b,c,d,e,f,g)) = W a </> fun a -> (a,b,c,d,e,f,g)

type I2 = I2 with
  static member (?<-) (W,I2,(a,b))           = W b </> fun b -> (a,b)
  static member (?<-) (W,I2,(a,b,c))         = W b </> fun b -> (a,b,c)
  static member (?<-) (W,I2,(a,b,c,d))       = W b </> fun b -> (a,b,c,d)
  static member (?<-) (W,I2,(a,b,c,d,e))     = W b </> fun b -> (a,b,c,d,e)
  static member (?<-) (W,I2,(a,b,c,d,e,f))   = W b </> fun b -> (a,b,c,d,e,f)
  static member (?<-) (W,I2,(a,b,c,d,e,f,g)) = W b </> fun b -> (a,b,c,d,e,f,g)

type I3 = I3 with
  static member (?<-) (W,I3,(a,b,c))         = W c </> fun c -> (a,b,c)
  static member (?<-) (W,I3,(a,b,c,d))       = W c </> fun c -> (a,b,c,d)
  static member (?<-) (W,I3,(a,b,c,d,e))     = W c </> fun c -> (a,b,c,d,e)
  static member (?<-) (W,I3,(a,b,c,d,e,f))   = W c </> fun c -> (a,b,c,d,e,f)
  static member (?<-) (W,I3,(a,b,c,d,e,f,g)) = W c </> fun c -> (a,b,c,d,e,f,g)

type I4 = I4 with
  static member (?<-) (W,I4,(a,b,c,d))       = W d </> fun d -> (a,b,c,d)
  static member (?<-) (W,I4,(a,b,c,d,e))     = W d </> fun d -> (a,b,c,d,e)
  static member (?<-) (W,I4,(a,b,c,d,e,f))   = W d </> fun d -> (a,b,c,d,e,f)
  static member (?<-) (W,I4,(a,b,c,d,e,f,g)) = W d </> fun d -> (a,b,c,d,e,f,g)

type I5 = I5 with
  static member (?<-) (W,I5,(a,b,c,d,e))     = W e </> fun e -> (a,b,c,d,e)
  static member (?<-) (W,I5,(a,b,c,d,e,f))   = W e </> fun e -> (a,b,c,d,e,f)
  static member (?<-) (W,I5,(a,b,c,d,e,f,g)) = W e </> fun e -> (a,b,c,d,e,f,g)

type I6 = I6 with
  static member (?<-) (W,I6,(a,b,c,d,e,f))   = W f </> fun f -> (a,b,c,d,e,f)
  static member (?<-) (W,I6,(a,b,c,d,e,f,g)) = W f </> fun f -> (a,b,c,d,e,f,g)

type I7 = I7 with
  static member (?<-) (W,I7,(a,b,c,d,e,f,g)) = W g </> fun g -> (a,b,c,d,e,f,g)

[<AutoOpen>]
module Tuple =
  let inline item1 U (u: Update) s = (U u) ? (I1) <- s
  let inline item2 U (u: Update) s = (U u) ? (I2) <- s
  let inline item3 U (u: Update) s = (U u) ? (I3) <- s
  let inline item4 U (u: Update) s = (U u) ? (I4) <- s
  let inline item5 U (u: Update) s = (U u) ? (I5) <- s
  let inline item6 U (u: Update) s = (U u) ? (I6) <- s
  let inline item7 U (u: Update) s = (U u) ? (I7) <- s

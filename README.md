# DoubleDoubleComplex
 Double-Double Complex and Quaternion Implements 

## Requirement
.NET 8.0  
[DoubleDouble](https://github.com/tk-yoshimura/DoubleDouble)

## Install

[Download DLL](https://github.com/tk-yoshimura/DoubleDoubleComplex/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.doubledouble.complex/)  

## Functions

| function | note |
| ---- | ---- |
| Complex.Sqrt(z) | |
| Complex.Cbrt(z) | |
| Complex.RootN(z, n) | |
| Complex.Log2(z) | |
| Complex.Log(z) | |
| Complex.Log(z, b) | |
| Complex.Log10(z) | |
| Complex.Log1p(z) | log(1+z) |
| Complex.Pow2(z) | |
| Complex.Pow(z, p) | |
| Complex.Exp(z) | |
| Complex.Sin(z) | |
| Complex.Cos(z) | |
| Complex.Tan(z) | |
| Complex.SinPI(z) | sin(&pi;z) |
| Complex.CosPI(z) | cos(&pi;z) |
| Complex.TanPI(z) | tan(&pi;z) |
| Complex.Sinh(z) | |
| Complex.Cosh(z) | |
| Complex.Tanh(z) | |
| Complex.Asin(z) | Accuracy deteriorates near z=-1,1. |
| Complex.Acos(z) | Accuracy deteriorates near z=-1,1. |
| Complex.Atan(z) | |
| Complex.Arsinh(z) | |
| Complex.Arcosh(z) | |
| Complex.Artanh(z) | Accuracy deteriorates near z=-1,1. |
| Complex.Gamma(z) | Accuracy deteriorates near non-positive intergers. <br/> If z is Natual number lass than 35, an exact integer value is returned. |
| Complex.LogGamma(z) | |
| Complex.Digamma(z) | Near the positive root, polynomial interpolation is used. |
| Complex.Erf(z) | |
| Complex.Erfc(z) | |
| Complex.Erfcx(z) | |
| Complex.FresnelC(z) | |
| Complex.FresnelS(z) | |
| Complex.BesselJ(nu, z) | Accuracy deteriorates near root.<br/>abs(nu) &leq; 256 |
| Complex.BesselY(nu, z) | Accuracy deteriorates near root.<br/>abs(nu) &leq; 256 |
| Complex.BesselI(nu, z) | Accuracy deteriorates near root.<br/>abs(nu) &leq; 256 |
| Complex.BesselK(nu, z) | abs(nu) &leq; 256 |
| Complex.HankelH1(nu, z) | Accuracy deteriorates near root.<br/>abs(nu) &leq; 256 |
| Complex.HankelH2(nu, z) | Accuracy deteriorates near root.<br/>abs(nu) &leq; 256 |
| Complex.AiryAi(z) | Accuracy deteriorates near root. |
| Complex.AiryBi(z) | Accuracy deteriorates near root. |


## Usage

```csharp
Complex z = "1+16i"; // z = (1, 16), new Complex(1, 16);
Complex c = Complex.Gamma(z);

Console.WriteLine(c);
```

## Licence
[MIT](https://github.com/tk-yoshimura/DoubleDoubleComplex/blob/main/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)

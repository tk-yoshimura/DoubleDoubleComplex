﻿using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexFunctionTests {
        [TestMethod()]
        public void InverseTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Inverse(z);

                ComplexAssert.AreEqual(1, c * z, 1e-7);
            }
        }

        [TestMethod()]
        public void SinTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Sin(z);
                NComplex nc = NComplex.Sin((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void SinPITest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.SinPI(z);
                NComplex nc = NComplex.Sin((NComplex)(z * ddouble.PI));

                ComplexAssert.AreEqual(nc, c, 1e-2);
            }
        }

        [TestMethod()]
        public void CosTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Cos(z);
                NComplex nc = NComplex.Cos((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void CosPITest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.CosPI(z);
                NComplex nc = NComplex.Cos((NComplex)(z * ddouble.PI));

                ComplexAssert.AreEqual(nc, c, 1e-2);
            }
        }

        [TestMethod()]
        public void TanTest() {
            foreach (Complex z in new[] { (0, 0), (0, 0.25), (1, 2), (2, 5), (6, -3), (7, -4), (-6, -3), (-7, -4),
                (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1),
                (-2, 73), (-2, -73), (-2, 74), (-2, -74) }) {
                Complex c = Complex.Tan(z);
                NComplex nc = NComplex.Tan((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void TanNearPoleTest() {
            for (ddouble eps = "0.1"; eps >= "1e-8"; eps /= 10) {
                foreach (ddouble x in new[] { -4.5, -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5, 4.5 }) {
                    foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {

                        Complex c = Complex.Tan((z + x) * ddouble.PI);
                        NComplex nc = -1 / NComplex.Tan((NComplex)(z * ddouble.PI));

                        Console.WriteLine(z);
                        Console.WriteLine(c);
                        Console.WriteLine(nc);

                        Assert.AreEqual(nc.Real, (double)c.R, double.Abs(nc.Real) * 1e-7 + 1e-15);
                        Assert.AreEqual(nc.Imaginary, (double)c.I, double.Abs(nc.Imaginary) * 1e-7 + 1e-15);
                    }
                }
            }

            for (ddouble eps = 1d / 128; eps >= 1d / 65536; eps /= 2) {
                foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {
                    Complex z_pi = z * ddouble.PI, z_pi2 = z_pi * z_pi;

                    Complex expected = -638512875 / (z_pi * (638512875 + z_pi2 * (212837625 + z_pi2 * (85135050 +
                        z_pi2 * (34459425 + z_pi2 * (13963950 + z_pi2 * (5659290 + z_pi2 * (2293620 + z_pi2 * 929569))))))));

                    Complex actual = Complex.Tan(z_pi + 0.5 * ddouble.PI);

                    Console.WriteLine(z);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(ddouble.Abs(expected.R - actual.R) < ddouble.Abs(expected.R) * 1e-25 + 1e-20);
                    Assert.IsTrue(ddouble.Abs(expected.I - actual.I) < ddouble.Abs(expected.I) * 1e-25 + 1e-20);
                }
            }
        }

        [TestMethod()]
        public void TanPITest() {
            foreach (Complex z in new[] { (0, 0), (0, 0.25), (1, 2), (2, 5), (6, -3), (7, -4), (-6, -3), (-7, -4),
                (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1),
                (-2, 23), (-2, -23), (-2, 24), (-2, -24), (-2, 1024), (-2, -1024) }) {
                Complex c = Complex.TanPI(z);
                NComplex nc = NComplex.Tan((NComplex)(z * ddouble.PI));

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }


        [TestMethod()]
        public void TanPINearPoleTest() {
            for (ddouble eps = "0.1"; eps >= "1e-8"; eps /= 10) {
                foreach (ddouble x in new[] { -4.5, -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5, 4.5 }) {
                    foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {

                        Complex c = Complex.TanPI(z + x);
                        NComplex nc = -1 / NComplex.Tan((NComplex)(z * ddouble.PI));

                        Console.WriteLine(z);
                        Console.WriteLine(c);
                        Console.WriteLine(nc);

                        Assert.AreEqual(nc.Real, (double)c.R, double.Abs(nc.Real) * 1e-7 + 1e-280);
                        Assert.AreEqual(nc.Imaginary, (double)c.I, double.Abs(nc.Imaginary) * 1e-7 + 1e-280);
                    }
                }
            }

            for (ddouble eps = 1d / 128; eps >= 1d / 65536; eps /= 2) {
                foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {
                    Complex z_pi = z * ddouble.PI, z_pi2 = z_pi * z_pi;

                    Complex expected = -638512875 / (z_pi * (638512875 + z_pi2 * (212837625 + z_pi2 * (85135050 +
                        z_pi2 * (34459425 + z_pi2 * (13963950 + z_pi2 * (5659290 + z_pi2 * (2293620 + z_pi2 * 929569))))))));

                    Complex actual = Complex.TanPI(z + 0.5);

                    Console.WriteLine(z);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(ddouble.Abs(expected.R - actual.R) < ddouble.Abs(expected.R) * 1e-25 + 1e-20);
                    Assert.IsTrue(ddouble.Abs(expected.I - actual.I) < ddouble.Abs(expected.I) * 1e-25 + 1e-20);
                }
            }
        }

        [TestMethod()]
        public void SinhTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Sinh(z);
                NComplex nc = NComplex.Sinh((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void CoshTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Cosh(z);
                NComplex nc = NComplex.Cosh((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void TanhTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Tanh(z);
                NComplex nc = NComplex.Tanh((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void AsinTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Asin(z);
                NComplex nc = NComplex.Asin((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void AcosTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Acos(z);
                NComplex nc = NComplex.Acos((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void AtanTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Atan(z);
                NComplex nc = NComplex.Atan((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void LogTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Log(z);
                NComplex nc = NComplex.Log((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void Log2Test() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Log2(z);
                NComplex nc = NComplex.Log((NComplex)z, 2);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void Log10Test() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Log10(z);
                NComplex nc = NComplex.Log10((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void LogNTest() {
            foreach (int n in new[] { 3, 5, 6 }) {
                foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = Complex.Log(z, n);
                    NComplex nc = NComplex.Log((NComplex)z, n);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void ExpTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Exp(z);
                NComplex nc = NComplex.Exp((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void PowTest() {
            foreach (Complex p in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = Complex.Pow(z, p);
                    NComplex nc = NComplex.Pow((NComplex)z, (NComplex)p);

                    ComplexAssert.AreEqual(nc, c, nc.Magnitude * 1e-7);
                }
            }

            foreach (Complex p in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (ddouble x in new[] { 1, 1.5, 2, 2.5, 3, 6, 7, 10 }) {
                    Complex c = Complex.Pow(x, p);
                    NComplex nc = NComplex.Pow((double)x, (NComplex)p);

                    ComplexAssert.AreEqual(nc, c, nc.Magnitude * 1e-7);
                }
            }

            foreach (ddouble p in new[] { 1, 1.5, 2, 2.5, 3, 6, 7, 10 }) {
                foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = Complex.Pow(z, p);
                    NComplex nc = NComplex.Pow((NComplex)z, (double)p);

                    ComplexAssert.AreEqual(nc, c, nc.Magnitude * 1e-7);
                }
            }
        }

        [TestMethod()]
        public void Pow2Test() {
            foreach (Complex p in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Pow2(p);
                NComplex nc = NComplex.Pow(2, (NComplex)p);

                ComplexAssert.AreEqual(nc, c, nc.Magnitude * 1e-7);
            }
        }

        [TestMethod()]
        public void PowNTest() {
            foreach (int n in new[] { 3, 5, 6 }) {
                foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = Complex.Pow(z, n);
                    NComplex nc = NComplex.Pow((NComplex)z, n);

                    ComplexAssert.AreEqual(nc, c, nc.Magnitude * 1e-7);
                }
            }
        }

        [TestMethod()]
        public void SqrtTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Sqrt(z);
                NComplex nc = NComplex.Sqrt((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void CbrtTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Cbrt(z);
                NComplex nc = NComplex.Pow((NComplex)z, 1d / 3d);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void RootNTest() {
            foreach (int n in new[] { 1, 2, 3, 4, 5, 6, 7, 8 }) {
                foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = Complex.RootN(z, n);
                    NComplex nc = NComplex.Pow((NComplex)z, 1d / n);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }
    }
}
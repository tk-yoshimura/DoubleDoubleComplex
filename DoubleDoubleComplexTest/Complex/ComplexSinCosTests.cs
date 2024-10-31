using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexSinCosTests {

        [TestMethod()]
        public void SinTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Sin(z);
                NComplex nc = NComplex.Sin((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }

        [TestMethod()]
        public void SinPiTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.SinPi(z);
                NComplex nc = NComplex.Sin((NComplex)(z * ddouble.Pi));

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
        public void CosPiTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.CosPi(z);
                NComplex nc = NComplex.Cos((NComplex)(z * ddouble.Pi));

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

                        Complex c = Complex.Tan((z + x) * ddouble.Pi);
                        NComplex nc = -1 / NComplex.Tan((NComplex)(z * ddouble.Pi));

                        Console.WriteLine(z);
                        Console.WriteLine(c);
                        Console.WriteLine(nc);

                        Assert.AreEqual(nc.Real, (double)c.R, double.Abs(nc.Real) * 1e-7 + 1e-15);
                        Assert.AreEqual(nc.Imaginary, (double)c.I, double.Abs(nc.Imaginary) * 1e-7 + 1e-15);
                    }
                }
            }

            for (ddouble eps = 1d / 4; eps >= 1d / 262144; eps /= 2) {
                foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {
                    Complex expected = -Complex.CosPi(z) / Complex.SinPi(z);

                    Complex actual = Complex.Tan((z + 0.5) * ddouble.Pi);

                    Console.WriteLine(z);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(ddouble.Abs(expected.R - actual.R) < ddouble.Abs(expected.R) * 1e-30 + 1e-20);
                    Assert.IsTrue(ddouble.Abs(expected.I - actual.I) < ddouble.Abs(expected.I) * 1e-30 + 1e-20);
                }
            }
        }

        [TestMethod()]
        public void TanPiTest() {
            foreach (Complex z in new[] { (0, 0), (0, 0.25), (1, 2), (2, 5), (6, -3), (7, -4), (-6, -3), (-7, -4),
                (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1),
                (-2, 23), (-2, -23), (-2, 24), (-2, -24), (-2, 1024), (-2, -1024) }) {
                Complex c = Complex.TanPi(z);
                NComplex nc = NComplex.Tan((NComplex)(z * ddouble.Pi));

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }


        [TestMethod()]
        public void TanPiNearPoleTest() {
            for (ddouble eps = "0.1"; eps >= "1e-8"; eps /= 10) {
                foreach (ddouble x in new[] { -4.5, -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5, 4.5 }) {
                    foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {

                        Complex c = Complex.TanPi(z + x);
                        NComplex nc = -1 / NComplex.Tan((NComplex)(z * ddouble.Pi));

                        Console.WriteLine(z);
                        Console.WriteLine(c);
                        Console.WriteLine(nc);

                        Assert.AreEqual(nc.Real, (double)c.R, double.Abs(nc.Real) * 1e-7 + 1e-280);
                        Assert.AreEqual(nc.Imaginary, (double)c.I, double.Abs(nc.Imaginary) * 1e-7 + 1e-280);
                    }
                }
            }

            for (ddouble eps = 1d / 4; eps >= 1d / 262144; eps /= 2) {
                foreach (Complex z in new[] { (eps, eps), (eps, 0), (eps, -eps), (0, eps), (0, -eps), (-eps, eps), (-eps, 0), (-eps, -eps) }) {
                    Complex expected = -Complex.CosPi(z) / Complex.SinPi(z);

                    Complex actual = Complex.TanPi(z + 0.5);

                    Console.WriteLine(z);
                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(ddouble.Abs(expected.R - actual.R) < ddouble.Abs(expected.R) * 1e-30 + 1e-20);
                    Assert.IsTrue(ddouble.Abs(expected.I - actual.I) < ddouble.Abs(expected.I) * 1e-30 + 1e-20);
                }
            }
        }
    }
}
using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexPowTests {

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
    }
}
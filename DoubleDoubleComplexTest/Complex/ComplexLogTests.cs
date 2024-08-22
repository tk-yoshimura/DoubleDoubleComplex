using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexLogTests {

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
    }
}
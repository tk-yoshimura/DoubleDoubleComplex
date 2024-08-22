using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexRootTests {

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
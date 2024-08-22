using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexSinhCoshTests {

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
    }
}
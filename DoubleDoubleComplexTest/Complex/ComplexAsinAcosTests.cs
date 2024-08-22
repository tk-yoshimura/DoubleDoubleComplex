using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexAsinAcosTests {

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
    }
}
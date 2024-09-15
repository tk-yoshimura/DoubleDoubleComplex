using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexArsinhArcoshTests {

        [TestMethod()]
        public void ArsinhTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Arsinh(z);
                Complex d = Complex.Sinh(c);

                ComplexAssert.AreEqual(z, d, 1e-28);
            }
        }

        [TestMethod()]
        public void ArcoshTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Arcosh(z);
                Complex d = Complex.Cosh(c);

                ComplexAssert.AreEqual(z, d, 1e-28);
            }
        }

        [TestMethod()]
        public void ArtanhTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Artanh(z);
                Complex d = Complex.Tanh(c);

                ComplexAssert.AreEqual(z, d, 1e-28);
            }
        }
    }
}
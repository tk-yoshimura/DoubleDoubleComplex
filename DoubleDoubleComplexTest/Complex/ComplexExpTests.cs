using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexExpTests {

        [TestMethod()]
        public void ExpTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                Complex c = Complex.Exp(z);
                NComplex nc = NComplex.Exp((NComplex)z);

                ComplexAssert.AreEqual(nc, c, 1e-7);
            }
        }
    }
}
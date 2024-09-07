using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexConjugateTests {
        [TestMethod()]
        public void ComplexConjugateTest() {
            Complex c = (2, 3);

            Assert.AreEqual((2, -3), Complex.Conjugate(c));
            Assert.AreEqual((2, -3), c.Conj);
        }
    }
}
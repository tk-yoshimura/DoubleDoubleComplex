using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionConjugateTests {
        [TestMethod()]
        public void QuaternionConjugateTest() {
            Quaternion c = (2, 3, 4, 5);

            Assert.AreEqual((2, -3, -4, -5), Quaternion.Conjugate(c));
            Assert.AreEqual((2, -3, -4, -5), c.Conj);
        }
    }
}
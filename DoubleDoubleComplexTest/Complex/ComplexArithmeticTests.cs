using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexArithmeticTests {
        [TestMethod()]
        public void AddTest() {
            Complex a = (1, 2);
            Complex b = (4, 5);

            Assert.AreEqual((5, 7), a + b);

            Assert.AreEqual(new Complex(1, 2) + new Complex(4, 0), new Complex(1, 2) + 4);
            Assert.AreEqual(new Complex(1, 0) + new Complex(4, 5), 1 + new Complex(4, 5));
        }

        [TestMethod()]
        public void SubTest() {
            Complex a = (1, 2);
            Complex b = (4, -5);

            Assert.AreEqual((-3, 7), a - b);

            Assert.AreEqual(new Complex(1, 2) - new Complex(4, 0), new Complex(1, 2) - 4);
            Assert.AreEqual(new Complex(1, 0) - new Complex(4, 5), 1 - new Complex(4, 5));
        }

        [TestMethod()]
        public void MulTest() {
            Complex a = (2, 3);
            Complex b = (4, -5);

            Assert.AreEqual((23, 2), a * b);

            Assert.AreEqual(new Complex(3, 2) * new Complex(4, 0), new Complex(3, 2) * 4);
            Assert.AreEqual(new Complex(3, 0) * new Complex(4, 5), 3 * new Complex(4, 5));
        }

        [TestMethod()]
        public void DivTest() {
            Complex a = (2, 3);
            Complex b = (4, -5);

            Complex c = a / b;

            Assert.AreEqual((double)(-7 / (ddouble)41), (double)c.R);
            Assert.AreEqual((double)(22 / (ddouble)41), (double)c.I);

            ComplexAssert.AreEqual(new Complex(3, 2) / new Complex(4, 0), new Complex(3, 2) / 4, 1e-32);
            ComplexAssert.AreEqual(new Complex(3, 0) / new Complex(4, 5), 3 / new Complex(4, 5), 1e-32);
        }
    }
}

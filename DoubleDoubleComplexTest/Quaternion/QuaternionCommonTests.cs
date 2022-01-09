using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionCommonTests {
        [TestMethod()]
        public void QuaternionTest() {
            Quaternion c1 = new(2, 3, -1, 6);
            Quaternion c2 = (4, 5, 7, -2);
            Quaternion c3 = 6;

            Assert.AreEqual(2, c1.R);
            Assert.AreEqual(3, c1.I);
            Assert.AreEqual(-1, c1.J);
            Assert.AreEqual(6, c1.K);

            Assert.AreEqual(4, c2.R);
            Assert.AreEqual(5, c2.I);
            Assert.AreEqual(7, c2.J);
            Assert.AreEqual(-2, c2.K);
            Assert.AreEqual(4 * 4 + 5 * 5 + 7 * 7 + 2 * 2, c2.Norm);
            Assert.AreEqual(ddouble.Sqrt(4 * 4 + 5 * 5 + 7 * 7 + 2 * 2), c2.Magnitude);

            Assert.AreEqual(6, c3.R);
            Assert.AreEqual(0, c3.I);
            Assert.AreEqual(0, c3.J);
            Assert.AreEqual(0, c3.K);
        }

        [TestMethod()]
        public void DeconstructTest() {
            Quaternion c1 = new(2, 3, -1, 6);

            (ddouble r, ddouble i, ddouble j, ddouble k) = c1;

            Assert.AreEqual(2, r);
            Assert.AreEqual(3, i);
            Assert.AreEqual(-1, j);
            Assert.AreEqual(6, k);
        }

        [TestMethod()]
        public void ToStringTest() {
            Quaternion c1 = (2, 3, 0, 1);
            Quaternion c2 = (2, 0, 1, 0);
            Quaternion c3 = (2, -3, 0, 0);

            Quaternion c4 = (0, 3, 1, 1);
            Quaternion c5 = (0, 0, 0, 0);
            Quaternion c6 = (0, -3, 0, 1);

            Quaternion c7 = (-2, 3, 0, -1);
            Quaternion c8 = (-2, 0, 0, 0);
            Quaternion c9 = (-2, -3, -1, -1);

            Quaternion c10 = (ddouble.NaN, ddouble.NaN, ddouble.NaN, ddouble.NaN);

            Assert.AreEqual("2+3i+1k", c1.ToString());
            Assert.AreEqual("2+1j", c2.ToString());
            Assert.AreEqual("2-3i", c3.ToString());

            Assert.AreEqual("3i+1j+1k", c4.ToString());
            Assert.AreEqual("0", c5.ToString());
            Assert.AreEqual("-3i+1k", c6.ToString());

            Assert.AreEqual("-2+3i-1k", c7.ToString());
            Assert.AreEqual("-2", c8.ToString());
            Assert.AreEqual("-2-3i-1j-1k", c9.ToString());

            Assert.AreEqual(double.NaN.ToString(), c10.ToString());
        }
    }
}
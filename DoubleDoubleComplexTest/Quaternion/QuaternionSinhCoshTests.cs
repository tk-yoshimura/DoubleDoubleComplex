using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionSinhCoshTests {

        [TestMethod()]
        public void SinhCoshTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion s = Quaternion.Sinh(q), c = Quaternion.Cosh(q);

                QuaternionAssert.AreEqual(ddouble.Sinh(q.R), s.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Cosh(q.R), c.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Sinh(q), c = Quaternion.Cosh(q);

                QuaternionAssert.AreEqual(Quaternion.One, c * c - s * s, 1e-23);
            }
        }

        [TestMethod()]
        public void TanhTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion y = Quaternion.Tanh(q);

                QuaternionAssert.AreEqual(ddouble.Tanh(q.R), y.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Sinh(q), c = Quaternion.Cosh(q);
                Quaternion t = Quaternion.Tanh(q);

                QuaternionAssert.AreEqual(s / c, t, 1e-25);
            }
        }
    }
}
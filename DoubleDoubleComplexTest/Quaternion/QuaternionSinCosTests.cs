using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionSinCosTests {

        [TestMethod()]
        public void SinCosTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion s = Quaternion.Sin(q), c = Quaternion.Cos(q);

                QuaternionAssert.AreEqual(ddouble.Sin(q.R), s.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Cos(q.R), c.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Sin(q), c = Quaternion.Cos(q);

                QuaternionAssert.AreEqual(Quaternion.One, s * s + c * c, 1e-23);
            }
        }

        [TestMethod()]
        public void TanTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion y = Quaternion.Tan(q);

                QuaternionAssert.AreEqual(ddouble.Tan(q.R), y.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Sin(q), c = Quaternion.Cos(q);
                Quaternion t = Quaternion.Tan(q);

                QuaternionAssert.AreEqual(s / c, t, 1e-25);
            }
        }
    }
}
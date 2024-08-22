using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionPowTests {

        [TestMethod()]
        public void PowTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion pown1 = Quaternion.Pow(q, -1);
                Quaternion pow2 = Quaternion.Pow(q, 2);
                Quaternion pow3 = Quaternion.Pow(q, 3);

                QuaternionAssert.AreEqual(1 / q.R, pown1.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Pow(q.R, 2), pow2.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Pow(q.R, 3), pow3.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1), (-2, 0, 0, 0), (-8, 0, 0, 0), (-8, 4, 0, 0) }) {
                Quaternion pown1 = Quaternion.Pow(q, -1);
                Quaternion pow2 = Quaternion.Pow(q, 2);
                Quaternion pow3 = Quaternion.Pow(q, 3);

                QuaternionAssert.AreEqual(1 / q, pown1, 1e-25);
                QuaternionAssert.AreEqual(q * q, pow2, 1e-25);
                QuaternionAssert.AreEqual(q * q * q, pow3, 1e-25);
            }
        }
    }
}
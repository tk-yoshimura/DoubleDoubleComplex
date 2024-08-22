using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionExpTests {

        [TestMethod()]
        public void ExpTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion exp = Quaternion.Exp(q);
                Quaternion log = Quaternion.Log(q);

                QuaternionAssert.AreEqual(ddouble.Exp(q.R), exp.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Log(q.R), log.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion t = Quaternion.Exp(Quaternion.Log(q));

                QuaternionAssert.AreEqual(q, t, 1e-25);
            }
        }
    }
}
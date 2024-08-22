using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionRootTests {

        [TestMethod()]
        public void SqrtTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion sqrt = Quaternion.Sqrt(q);

                QuaternionAssert.AreEqual(ddouble.Sqrt(q.R), sqrt.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1), (-2, 0, 0, 0), (-8, 0, 0, 0), (-8, 4, 0, 0) }) {
                Quaternion sqrt = Quaternion.Sqrt(q);

                QuaternionAssert.AreEqual(q, sqrt * sqrt, 1e-25);
            }
        }

        [TestMethod()]
        public void CbrtTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion cbrt = Quaternion.Cbrt(q);

                QuaternionAssert.AreEqual(ddouble.Cbrt(q.R), cbrt.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1), (-2, 0, 0, 0), (-8, 0, 0, 0), (-8, 4, 0, 0) }) {
                Quaternion cbrt = Quaternion.Cbrt(q);

                QuaternionAssert.AreEqual(q, cbrt * cbrt * cbrt, 1e-25);
            }
        }

        [TestMethod()]
        public void RootNTest() {
            foreach (int n in new[] { 1, 2, 3, 4, 5, 6, 7, 8 }) {
                foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                    Quaternion root_n = Quaternion.RootN(q, n);

                    QuaternionAssert.AreEqual(ddouble.RootN(q.R, n), root_n.R, 1e-25);
                }

                foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1), (-2, 0, 0, 0), (-8, 0, 0, 0), (-8, 4, 0, 0) }) {
                    Quaternion root_n = Quaternion.RootN(q, n);

                    QuaternionAssert.AreEqual(q, Quaternion.Pow(root_n, n), 1e-25);
                }
            }
        }
    }
}
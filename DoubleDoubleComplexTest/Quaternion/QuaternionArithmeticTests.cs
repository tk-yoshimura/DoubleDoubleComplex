using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NQuaternion = System.Numerics.Quaternion;

    [TestClass()]
    public class QuaternionArithmeticTests {
        [TestMethod()]
        public void AddTest() {
            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a + b;
                    NQuaternion nc = (NQuaternion)a + (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (ddouble b in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                    Quaternion c = a + b;
                    NQuaternion nc = (NQuaternion)a + new NQuaternion(0, 0, 0, (float)(double)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a + b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)(double)a) + (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void SubTest() {
            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a - b;
                    NQuaternion nc = (NQuaternion)a - (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (ddouble b in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                    Quaternion c = a - b;
                    NQuaternion nc = (NQuaternion)a - new NQuaternion(0, 0, 0, (float)(double)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a - b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)(double)a) - (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void MulTest() {
            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a * b;
                    NQuaternion nc = (NQuaternion)a * (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (ddouble b in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                    Quaternion c = a * b;
                    NQuaternion nc = (NQuaternion)a * new NQuaternion(0, 0, 0, (float)(double)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a * b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)(double)a) * (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void DivTest() {
            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a / b;
                    NQuaternion nc = (NQuaternion)a / (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                    QuaternionAssert.AreEqual(a, c * b, 1e-30);
                }
            }

            foreach (Quaternion a in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                foreach (ddouble b in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                    Quaternion c = a / b;
                    NQuaternion nc = (NQuaternion)a / new NQuaternion(0, 0, 0, (float)(double)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a / b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)(double)a) / (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                }
            }
        }
    }
}

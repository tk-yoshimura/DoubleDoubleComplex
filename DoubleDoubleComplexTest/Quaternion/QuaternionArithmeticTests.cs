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
                    NQuaternion nc = (NQuaternion)a + new NQuaternion(0, 0, 0, (float)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a + b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)a) + (NQuaternion)b;

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
                    NQuaternion nc = (NQuaternion)a - new NQuaternion(0, 0, 0, (float)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a - b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)a) - (NQuaternion)b;

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
                    NQuaternion nc = (NQuaternion)a * new NQuaternion(0, 0, 0, (float)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a * b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)a) * (NQuaternion)b;

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
                    NQuaternion nc = (NQuaternion)a / new NQuaternion(0, 0, 0, (float)b);

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                }
            }

            foreach (ddouble a in new[] { 1, 2, 3, 4, -1, -3, -5 }) {
                foreach (Quaternion b in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                    Quaternion c = a / b;
                    NQuaternion nc = new NQuaternion(0, 0, 0, (float)a) / (NQuaternion)b;

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                }
            }
        }

        [TestMethod()]
        public void FromAxisAngleTest() {
            foreach ((double x, double y, double z) in new[] { (1, 2, 3), (2, -4, 5), (4, 2, 1) }) {
                foreach (double theta in new[] { 0.25, 0.5, 0.75 }) {

                    Quaternion c = Quaternion.FromAxisAngle((x, y, z), theta);
                    NQuaternion nc = NQuaternion.CreateFromAxisAngle(new System.Numerics.Vector3((float)x, (float)y, (float)z), (float)theta);

                    QuaternionAssert.AreEqual(nc, c, 1e-6);
                }
            }
        }

        [TestMethod()]
        public void FromYawPitchRollTest() {
            foreach ((double yaw, double pitch, double roll) in new[] { (1, 2, 3), (2, -4, 5), (4, 2, 1) }) {
                Quaternion c = Quaternion.FromYawPitchRoll(yaw, pitch, roll);
                NQuaternion nc = NQuaternion.CreateFromYawPitchRoll((float)yaw, (float)pitch, (float)roll);

                QuaternionAssert.AreEqual(nc, c, 1e-6);
            }
        }


        [TestMethod()]
        public void InverseTest() {
            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Inverse(q);

                QuaternionAssert.AreEqual(1, s * q, 1e-24);
            }
        }
    }
}

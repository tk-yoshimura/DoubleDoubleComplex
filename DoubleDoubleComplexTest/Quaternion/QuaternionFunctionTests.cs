using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoubleDouble;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionFunctionTests {
        [TestMethod()]
        public void SinCosTest() {
            foreach (Quaternion q in new[] { 1, 2, 4, 5 }) {
                Quaternion s = Quaternion.Sin(q), c = Quaternion.Cos(q);

                QuaternionAssert.AreEqual(ddouble.Sin(q.R), s.R, 1e-25);
                QuaternionAssert.AreEqual(ddouble.Cos(q.R), c.R, 1e-25);
            }

            foreach (Quaternion q in new[] { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -4, 5, 1), (3, -9, 2, 4), (7, 1, -3, 2), (-3, 5, 2, -1) }) {
                Quaternion s = Quaternion.Sin(q), c = Quaternion.Cos(q);

                QuaternionAssert.AreEqual(Quaternion.One, s * s + c * c, 1e-24);
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
    }
}
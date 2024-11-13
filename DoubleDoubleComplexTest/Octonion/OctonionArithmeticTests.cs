using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class OctonionArithmeticTests {
        [TestMethod()]
        public void AddTest() {
            Octonion o1 = (2, 3, 5, 7, 11, 13, 17, 19);
            Octonion o2 = (1, 4, 9, 16, 25, 36, 49, 64);
            Octonion o3 = (1, 8, 27, 64, 125, 216, 343, 512);
            Octonion o4 = (2, -3, 5, -7, 11, -13, 17, -19);

            OctonionAssert.AreEqual((3, 7, 14, 23, 36, 49, 66, 83), o1 + o2, 1e-30);
            OctonionAssert.AreEqual((3, 11, 32, 71, 136, 229, 360, 531), o1 + o3, 1e-30);
            OctonionAssert.AreEqual((4, 0, 10, 0, 22, 0, 34, 0), o1 + o4, 1e-30);
            OctonionAssert.AreEqual(o1, +o1, 1e-30);

            OctonionAssert.AreEqual(o1 + (2, 0, 0, 0, 0, 0, 0, 0), o1 + 2, 1e-30);
            OctonionAssert.AreEqual(o1 + (2, 0, 0, 0, 0, 0, 0, 0), o1 + (ddouble)2, 1e-30);
            OctonionAssert.AreEqual(o1 + (2, -3, 0, 0, 0, 0, 0, 0), o1 + (2, -3), 1e-30);
            OctonionAssert.AreEqual(o1 + (2, -3, 5, -7, 0, 0, 0, 0), o1 + (2, -3, 5, -7), 1e-30);

            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) + o1, 2 + o1, 1e-30);
            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) + o1, (ddouble)2 + o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 0, 0, 0, 0, 0, 0) + o1, (2, -3) + o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 5, -7, 0, 0, 0, 0) + o1, (2, -3, 5, -7) + o1, 1e-30);
        }

        [TestMethod()]
        public void SubTest() {
            Octonion o1 = (2, 3, 5, 7, 11, 13, 17, 19);
            Octonion o2 = (1, 4, 9, 16, 25, 36, 49, 64);
            Octonion o3 = (1, 8, 27, 64, 125, 216, 343, 512);
            Octonion o4 = (2, -3, 5, -7, 11, -13, 17, -19);

            OctonionAssert.AreEqual((1, -1, -4, -9, -14, -23, -32, -45), o1 - o2, 1e-30);
            OctonionAssert.AreEqual((1, -5, -22, -57, -114, -203, -326, -493), o1 - o3, 1e-30);
            OctonionAssert.AreEqual((0, 6, 0, 14, 0, 26, 0, 38), o1 - o4, 1e-30);
            OctonionAssert.AreEqual(Octonion.Zero - o1, -o1, 1e-30);

            OctonionAssert.AreEqual(o1 - (2, 0, 0, 0, 0, 0, 0, 0), o1 - 2, 1e-30);
            OctonionAssert.AreEqual(o1 - (2, 0, 0, 0, 0, 0, 0, 0), o1 - (ddouble)2, 1e-30);
            OctonionAssert.AreEqual(o1 - (2, -3, 0, 0, 0, 0, 0, 0), o1 - (2, -3), 1e-30);
            OctonionAssert.AreEqual(o1 - (2, -3, 5, -7, 0, 0, 0, 0), o1 - (2, -3, 5, -7), 1e-30);

            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) - o1, 2 - o1, 1e-30);
            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) - o1, (ddouble)2 - o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 0, 0, 0, 0, 0, 0) - o1, (2, -3) - o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 5, -7, 0, 0, 0, 0) - o1, (2, -3, 5, -7) - o1, 1e-30);
        }

        [TestMethod()]
        public void MulTest() {
            Octonion o1 = (2, 3, 5, 7, 11, 13, 17, 19);
            Octonion o2 = (1, 4, 9, 16, 25, 36, 49, 64);
            Octonion o3 = (1, 8, 27, 64, 125, 216, 343, 512);
            Octonion o4 = (2, -3, 5, -7, 11, -13, 17, -19);

            OctonionAssert.AreEqual((-2959, -58, 265, 250, -231, 38, 213, 130), o1 * o2, 1e-30);
            OctonionAssert.AreEqual((-20347, -1286, 4123, 2646, -3907, -2, 1735, 1050), o1 * o3, 1e-30);
            OctonionAssert.AreEqual((157, 290, 20, -830, 44, 494, 68, -78), o1 * o4, 1e-30);

            OctonionAssert.AreEqual(o1 * (2, 0, 0, 0, 0, 0, 0, 0), o1 * 2, 1e-30);
            OctonionAssert.AreEqual(o1 * (2, 0, 0, 0, 0, 0, 0, 0), o1 * (ddouble)2, 1e-30);
            OctonionAssert.AreEqual(o1 * (2, -3, 0, 0, 0, 0, 0, 0), o1 * (2, -3), 1e-30);
            OctonionAssert.AreEqual(o1 * (2, -3, 5, -7, 0, 0, 0, 0), o1 * (2, -3, 5, -7), 1e-30);

            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) * o1, 2 * o1, 1e-30);
            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) * o1, (ddouble)2 * o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 0, 0, 0, 0, 0, 0) * o1, (2, -3) * o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 5, -7, 0, 0, 0, 0) * o1, (2, -3, 5, -7) * o1, 1e-30);
        }

        [TestMethod()]
        public void DivTest() {
            Octonion o1 = (2, 3, 5, 7, 11, 13, 17, 19);
            Octonion o2 = (1, 4, 9, 16, 25, 36, 49, 64);
            Octonion o3 = (1, 8, 27, 64, 125, 216, 343, 512);
            Octonion o4 = (2, -3, 5, -7, 11, -13, 17, -19);
            Octonion o5 = (2e250, -3e250, 5e250, -7e250, 11e250, -13e250, 17e250, -19e250);
            Octonion o6 = (2e-250, -3e-250, 5e-250, -7e-250, 11e-250, -13e-250, 17e-250, -19e-250);
            Octonion o7 = (2e250, -3e250, 5e250, -7e250, 11e-250, -13e-250, 17e-250, -19e-250);

            OctonionAssert.AreEqual(o1, (o1 / o2) * o2, 1e-30);
            OctonionAssert.AreEqual(o1, (o1 / o3) * o3, 1e-30);
            OctonionAssert.AreEqual(o1, (o1 / o4) * o4, 1e-30);
            OctonionAssert.AreEqual(1d, o5 / o5, 1e-30);
            OctonionAssert.AreEqual(1d, o6 / o6, 1e-30);
            OctonionAssert.AreEqual(1d, o7 / o7, 1e-30);

            OctonionAssert.AreEqual(o1 / (2, 0, 0, 0, 0, 0, 0, 0), o1 / 2, 1e-30);
            OctonionAssert.AreEqual(o1 / (2, 0, 0, 0, 0, 0, 0, 0), o1 / (ddouble)2, 1e-30);
            OctonionAssert.AreEqual(o1 / (2, -3, 0, 0, 0, 0, 0, 0), o1 / (2, -3), 1e-30);
            OctonionAssert.AreEqual(o1 / (2, -3, 5, -7, 0, 0, 0, 0), o1 / (2, -3, 5, -7), 1e-30);

            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) / o1, 2 / o1, 1e-30);
            OctonionAssert.AreEqual((2, 0, 0, 0, 0, 0, 0, 0) / o1, (ddouble)2 / o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 0, 0, 0, 0, 0, 0) / o1, (2, -3) / o1, 1e-30);
            OctonionAssert.AreEqual((2, -3, 5, -7, 0, 0, 0, 0) / o1, (2, -3, 5, -7) / o1, 1e-30);
        }


        [TestMethod()]
        public void InverseTest() {
            Octonion o1 = (2, 3, 5, 7, 11, 13, 17, 19);
            Octonion o2 = (1, 4, 9, 16, 25, 36, 49, 64);
            Octonion o3 = (1, 8, 27, 64, 125, 216, 343, 512);
            Octonion o4 = (2, -3, 5, -7, 11, -13, 17, -19);
            Octonion o5 = (2e250, -3e250, 5e250, -7e250, 11e250, -13e250, 17e250, -19e250);
            Octonion o6 = (2e-250, -3e-250, 5e-250, -7e-250, 11e-250, -13e-250, 17e-250, -19e-250);
            Octonion o7 = (2e250, -3e250, 5e250, -7e250, 11e-250, -13e-250, 17e-250, -19e-250);

            OctonionAssert.AreEqual(1d, Octonion.Inverse(o2) * o2, 1e-30);
            OctonionAssert.AreEqual(1d, Octonion.Inverse(o3) * o3, 1e-30);
            OctonionAssert.AreEqual(1d, Octonion.Inverse(o4) * o4, 1e-30);
            OctonionAssert.AreEqual(1d, Octonion.Inverse(o5) * o5, 1e-30);
            OctonionAssert.AreEqual(1d, Octonion.Inverse(o6) * o6, 1e-30);
            OctonionAssert.AreEqual(1d, Octonion.Inverse(o7) * o7, 1e-30);

            OctonionAssert.AreEqual(1d, o2 * Octonion.Inverse(o2), 1e-30);
            OctonionAssert.AreEqual(1d, o3 * Octonion.Inverse(o3), 1e-30);
            OctonionAssert.AreEqual(1d, o4 * Octonion.Inverse(o4), 1e-30);
            OctonionAssert.AreEqual(1d, o5 * Octonion.Inverse(o5), 1e-30);
            OctonionAssert.AreEqual(1d, o6 * Octonion.Inverse(o6), 1e-30);
            OctonionAssert.AreEqual(1d, o7 * Octonion.Inverse(o7), 1e-30);
        }
    }
}

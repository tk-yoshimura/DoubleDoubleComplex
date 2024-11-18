using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class OctonionEnumTests {
        [TestMethod()]
        public void OctonionSumTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            Assert.AreEqual((16, 1, 7, 13, 8, 6, 17, 27), qs.Sum());
        }

        [TestMethod()]
        public void OctonionAverageTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            Assert.AreEqual((4, 0.25, 1.75, 3.25, 2, 1.5, 4.25, 6.75), qs.Average());
        }

        [TestMethod()]
        public void OctonionRTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 1, 2, 6, 7 }, qs.R().ToArray());
        }

        [TestMethod()]
        public void OctonionITest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 2, 5, -3, -3 }, qs.I().ToArray());
        }

        [TestMethod()]
        public void OctonionJTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 3, -2, 1, 5 }, qs.J().ToArray());
        }

        [TestMethod()]
        public void OctonionKTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 4, 6, 2, 1 }, qs.K().ToArray());
        }

        [TestMethod()]
        public void OctonionWTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 2, 3, 7, -4 }, qs.W().ToArray());
        }

        [TestMethod()]
        public void OctonionXTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 5, 2, 1, -2 }, qs.X().ToArray());
        }

        [TestMethod()]
        public void OctonionYTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 7, 4, 5, 1 }, qs.Y().ToArray());
        }

        [TestMethod()]
        public void OctonionZTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { 8, 6, 6, 7 }, qs.Z().ToArray());
        }

        [TestMethod()]
        public void OctonionNormTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { qs[0].Norm, qs[1].Norm, qs[2].Norm, qs[3].Norm }, qs.Norm().ToArray());
        }

        [TestMethod()]
        public void OctonionMagnitudeTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new ddouble[] { qs[0].Norm, qs[1].Norm, qs[2].Norm, qs[3].Norm }, qs.Magnitude().ToArray());
        }

        [TestMethod()]
        public void OctonionConjugateTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new Octonion[] { qs[0].Conj, qs[1].Conj, qs[2].Conj, qs[3].Conj }, qs.Conjugate().ToArray());
        }

        [TestMethod()]
        public void OctonionNormalTest() {
            Octonion[] qs = { (1, 2, 3, 4, 2, 5, 7, 8), (2, 5, -2, 6, 3, 2, 4, 6), (6, -3, 1, 2, 7, 1, 5, 6), (7, -3, 5, 1, -4, -2, 1, 7) };

            CollectionAssert.AreEqual(new Octonion[] { qs[0].Normal, qs[1].Normal, qs[2].Normal, qs[3].Normal }, qs.Normal().ToArray());
        }
    }
}
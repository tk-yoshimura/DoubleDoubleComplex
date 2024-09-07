using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DoubleDoubleQuaternionTests {
    [TestClass()]
    public class QuaternionEnumTests {
        [TestMethod()]
        public void QuaternionSumTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            Assert.AreEqual((16, 1, 7, 13), qs.Sum());
        }

        [TestMethod()]
        public void QuaternionAverageTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            Assert.AreEqual((4, 0.25, 1.75, 3.25), qs.Average());
        }

        [TestMethod()]
        public void QuaternionRTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ 1, 2, 6, 7 }, qs.R().ToArray());
        }

        [TestMethod()]
        public void QuaternionITest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ 2, 5, -3, -3 }, qs.I().ToArray());
        }

        [TestMethod()]
        public void QuaternionJTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ 3, -2, 1, 5 }, qs.J().ToArray());
        }

        [TestMethod()]
        public void QuaternionKTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ 4, 6, 2, 1 }, qs.K().ToArray());
        }

        [TestMethod()]
        public void QuaternionNormTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ qs[0].Norm, qs[1].Norm, qs[2].Norm, qs[3].Norm }, qs.Norm().ToArray());
        }

        [TestMethod()]
        public void QuaternionMagnitudeTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new ddouble[]{ qs[0].Magnitude, qs[1].Magnitude, qs[2].Magnitude, qs[3].Magnitude }, qs.Magnitude().ToArray());
        }

        [TestMethod()]
        public void QuaternionConjugateTest() {
            Quaternion[] qs = { (1, 2, 3, 4), (2, 5, -2, 6), (6, -3, 1, 2), (7, -3, 5, 1) };

            CollectionAssert.AreEqual(new Quaternion[]{ qs[0].Conj, qs[1].Conj, qs[2].Conj, qs[3].Conj }, qs.Conjugate().ToArray());
        }
    }
}
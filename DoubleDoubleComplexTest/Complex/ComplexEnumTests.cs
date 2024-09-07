using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexEnumTests {
        [TestMethod()]
        public void ComplexSumTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            Assert.AreEqual((16, 1), cs.Sum());
        }

        [TestMethod()]
        public void ComplexAverageTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            Assert.AreEqual((4, 0.25), cs.Average());
        }

        [TestMethod()]
        public void ComplexRTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new ddouble[] { 1, 2, 6, 7 }, cs.R().ToArray());
        }

        [TestMethod()]
        public void ComplexITest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new ddouble[] { 2, 5, -3, -3 }, cs.I().ToArray());
        }

        [TestMethod()]
        public void ComplexNormTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new ddouble[] { cs[0].Norm, cs[1].Norm, cs[2].Norm, cs[3].Norm }, cs.Norm().ToArray());
        }

        [TestMethod()]
        public void ComplexMagnitudeTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new ddouble[] { cs[0].Magnitude, cs[1].Magnitude, cs[2].Magnitude, cs[3].Magnitude }, cs.Magnitude().ToArray());
        }

        [TestMethod()]
        public void ComplexPhaseTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new ddouble[] { cs[0].Phase, cs[1].Phase, cs[2].Phase, cs[3].Phase }, cs.Phase().ToArray());
        }

        [TestMethod()]
        public void ComplexConjugateTest() {
            Complex[] cs = { (1, 2), (2, 5), (6, -3), (7, -3) };

            CollectionAssert.AreEqual(new Complex[] { cs[0].Conj, cs[1].Conj, cs[2].Conj, cs[3].Conj }, cs.Conjugate().ToArray());
        }
    }
}
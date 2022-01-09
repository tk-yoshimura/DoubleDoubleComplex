using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    public static class QuaternionAssert {

        public static void AreEqual(Quaternion expected, Quaternion actual, ddouble delta) {
            AreEqual(expected, actual, delta, string.Empty);
        }

        public static void AreEqual(Quaternion expected, Quaternion actual, ddouble delta, string message) {
            Assert.IsTrue(expected.R - delta < actual.R && expected.R + delta > actual.R &&
                          expected.I - delta < actual.I && expected.I + delta > actual.I &&
                          expected.J - delta < actual.J && expected.J + delta > actual.J &&
                          expected.K - delta < actual.K && expected.K + delta > actual.K,
                          message + $"\n{nameof(expected)} : {expected}\n{nameof(actual)}   : {actual}");
        }
    }
}

using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    public static class OctonionAssert {

        public static void AreEqual(Octonion expected, Octonion actual, ddouble delta) {
            AreEqual(expected, actual, delta, string.Empty);
        }

        public static void AreEqual(Octonion expected, Octonion actual, ddouble delta, string message) {
            Assert.IsTrue(expected.R - delta < actual.R && expected.R + delta > actual.R &&
                          expected.S - delta < actual.S && expected.S + delta > actual.S &&
                          expected.T - delta < actual.T && expected.T + delta > actual.T &&
                          expected.U - delta < actual.U && expected.U + delta > actual.U &&
                          expected.W - delta < actual.W && expected.W + delta > actual.W &&
                          expected.X - delta < actual.X && expected.X + delta > actual.X &&
                          expected.Y - delta < actual.Y && expected.Y + delta > actual.Y &&
                          expected.Z - delta < actual.Z && expected.Z + delta > actual.Z,
                          message + $"\n{nameof(expected)} : {expected}\n{nameof(actual)}   : {actual}");
        }
    }
}

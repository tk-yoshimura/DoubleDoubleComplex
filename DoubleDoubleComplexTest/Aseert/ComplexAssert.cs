using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    public static class ComplexAssert {

        public static void AreEqual(Complex expected, Complex actual, ddouble delta) {
            AreEqual(expected, actual, delta, string.Empty);
        }

        public static void AreEqual(Complex expected, Complex actual, ddouble delta, string message) {
            Assert.IsTrue(expected.R - delta < actual.R && expected.R + delta > actual.R &&
                          expected.I - delta < actual.I && expected.I + delta > actual.I, 
                          message + $"\n{nameof(expected)} : {expected}\n{nameof(actual)}   : {actual}");
        }
    }
}

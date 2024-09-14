using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {

    [TestClass()]
    public class ComplexFresnelTests {

        [TestMethod()]
        public void FresnelSTest() {
            ComplexAssert.AreEqual(
                "0.4382591473903547660767566966251526374937865724524165673344073262",
                Complex.FresnelS((1, 0)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "-2.061888219194840468080716536685708600816+2.061888219194840468080716536685708600816i",
                Complex.FresnelS((1, 1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "-2.061888219194840468080716536685708600816-2.061888219194840468080716536685708600816i",
                Complex.FresnelS((1, -1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "2.061888219194840468080716536685708600816+2.061888219194840468080716536685708600816i",
                Complex.FresnelS((-1, 1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "2.061888219194840468080716536685708600816-2.061888219194840468080716536685708600816i",
                Complex.FresnelS((-1, -1)),
                1e-28
            );
        }

        [TestMethod()]
        public void FresnelCTest() {
            ComplexAssert.AreEqual(
                "0.7798934003768228294742064136526901366306257081363209601031335831",
                Complex.FresnelC((1, 0)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "2.555793778102439024634522388352195842157+2.555793778102439024634522388352195842157i",
                Complex.FresnelC((1, 1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "2.555793778102439024634522388352195842157-2.555793778102439024634522388352195842157i",
                Complex.FresnelC((1, -1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "-2.555793778102439024634522388352195842157+2.555793778102439024634522388352195842157i",
                Complex.FresnelC((-1, 1)),
                1e-28
            );

            ComplexAssert.AreEqual(
                "-2.555793778102439024634522388352195842157-2.555793778102439024634522388352195842157i",
                Complex.FresnelC((-1, -1)),
                1e-28
            );
        }
    }
}
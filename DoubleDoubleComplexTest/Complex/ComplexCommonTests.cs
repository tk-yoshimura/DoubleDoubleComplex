using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexCommonTests {
        [TestMethod()]
        public void ComplexTest() {
            Complex c1 = new(2, 3);
            Complex c2 = (4, 5);
            Complex c3 = 6;

            Assert.AreEqual(2, c1.R);
            Assert.AreEqual((2, 3), +c1);
            Assert.AreEqual((-2, -3), -c1);
            Assert.AreEqual(3, c1.I);
            Assert.AreEqual((double)0.98279, (double)c1.Phase, 1e-4);

            Assert.AreEqual(4, c2.R);
            Assert.AreEqual(5, c2.I);
            Assert.AreEqual(4 * 4 + 5 * 5, c2.Norm);
            Assert.AreEqual(ddouble.Sqrt(4 * 4 + 5 * 5), c2.Magnitude);

            Assert.AreEqual(6, c3.R);
            Assert.AreEqual(0, c3.I);
        }

        [TestMethod()]
        public void DeconstructTest() {
            Complex c1 = new(2, 3);

            (ddouble r, ddouble i) = c1;

            Assert.AreEqual(2, r);
            Assert.AreEqual(3, i);
        }

        [TestMethod()]
        public void ILogBTest() {
            Complex c1 = new(2, 6);
            Complex c2 = new(9, 1);

            Assert.AreEqual(2, Complex.ILogB(c1));
            Assert.AreEqual(3, Complex.ILogB(c2));
        }

        [TestMethod()]
        public void LdexpTest() {
            Complex c1 = new(2, 6);

            Assert.AreEqual((8, 24), Complex.Ldexp(c1, 2));
        }

        [TestMethod()]
        public void ToStringTest() {
            Complex c1 = (2, 3);
            Complex c2 = (2, 0);
            Complex c3 = (2, -3);

            Complex c4 = (0, 3);
            Complex c5 = (0, 0);
            Complex c6 = (0, -3);

            Complex c7 = (-2, 3);
            Complex c8 = (-2, 0);
            Complex c9 = (-2, -3);

            Complex c10 = (ddouble.Rcp(3), ddouble.Rcp(6));
            Complex c11 = (0, ddouble.Rcp(6));
            Complex c12 = (ddouble.Rcp(3), 0);
            Complex c13 = (0, 0);

            Complex c14 = (ddouble.Rcp(3), -ddouble.Rcp(6));
            Complex c15 = (-ddouble.Rcp(3), ddouble.Rcp(6));

            Assert.AreEqual("2+3i", c1.ToString());
            Assert.AreEqual("2", c2.ToString());
            Assert.AreEqual("2-3i", c3.ToString());

            Assert.AreEqual("3i", c4.ToString());
            Assert.AreEqual("0", c5.ToString());
            Assert.AreEqual("-3i", c6.ToString());

            Assert.AreEqual("-2+3i", c7.ToString());
            Assert.AreEqual("-2", c8.ToString());
            Assert.AreEqual("-2-3i", c9.ToString());

            Assert.AreEqual("0", Complex.Zero.ToString());
            Assert.AreEqual(double.NaN.ToString(), Complex.NaN.ToString());

            Assert.AreEqual("3.333e-1+1.667e-1i", $"{c10:e3}");
            Assert.AreEqual("1.667e-1i", $"{c11:e3}");
            Assert.AreEqual("3.333e-1", $"{c12:e3}");
            Assert.AreEqual("0", $"{c13:e3}");

            Assert.AreEqual("3.333e-1-1.667e-1i", $"{c14:e3}");
            Assert.AreEqual("-3.333e-1+1.667e-1i", $"{c15:e3}");
        }
    }
}
using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionCommonTests {
        [TestMethod()]
        public void QuaternionTest() {
            Quaternion c1 = new(2, 3, -1, 6);
            Quaternion c2 = (4, 5, 7, -2);
            Quaternion c3 = 6;

            Assert.AreEqual((2, 3, -1, 6), +c1);
            Assert.AreEqual((-2, -3, 1, -6), -c1);

            Assert.AreEqual(2, c1.R);
            Assert.AreEqual(3, c1.I);
            Assert.AreEqual(-1, c1.J);
            Assert.AreEqual(6, c1.K);

            Assert.AreEqual(4, c2.R);
            Assert.AreEqual(5, c2.I);
            Assert.AreEqual(7, c2.J);
            Assert.AreEqual(-2, c2.K);
            Assert.AreEqual(4 * 4 + 5 * 5 + 7 * 7 + 2 * 2, c2.SquareNorm);
            Assert.AreEqual(ddouble.Sqrt(4 * 4 + 5 * 5 + 7 * 7 + 2 * 2), c2.Norm);

            Assert.AreEqual(6, c3.R);
            Assert.AreEqual(0, c3.I);
            Assert.AreEqual(0, c3.J);
            Assert.AreEqual(0, c3.K);

            Assert.AreEqual((3, -1, 6), c1.Vector);
        }

        [TestMethod()]
        public void DeconstructTest() {
            Quaternion c1 = new(2, 3, -1, 6);

            (ddouble r, ddouble i, ddouble j, ddouble k) = c1;

            Assert.AreEqual(2, r);
            Assert.AreEqual(3, i);
            Assert.AreEqual(-1, j);
            Assert.AreEqual(6, k);
        }

        [TestMethod()]
        public void ILogBTest() {
            Quaternion c1 = new(2, 6, 0, 1);
            Quaternion c2 = new(9, 1, 2, 3);
            Quaternion c3 = new(2, 1, 16, 3);
            Quaternion c4 = new(2, 1, 2, 34);

            Assert.AreEqual(2, Quaternion.ILogB(c1));
            Assert.AreEqual(3, Quaternion.ILogB(c2));
            Assert.AreEqual(4, Quaternion.ILogB(c3));
            Assert.AreEqual(5, Quaternion.ILogB(c4));
        }

        [TestMethod()]
        public void LdexpTest() {
            Quaternion c1 = new(2, 6, 3, 1);

            Assert.AreEqual((8, 24, 12, 4), Quaternion.Ldexp(c1, 2));
        }

        [TestMethod()]
        public void ToStringTest() {
            Quaternion c1 = (2, 3, 0, 1);
            Quaternion c2 = (2, 0, 1, 0);
            Quaternion c3 = (2, -3, 0, 0);

            Quaternion c4 = (0, 3, 1, 1);
            Quaternion c5 = (0, 0, 0, 0);
            Quaternion c6 = (0, -3, 0, 1);

            Quaternion c7 = (-2, 3, 0, -1);
            Quaternion c8 = (-2, 0, 0, 0);
            Quaternion c9 = (-2, -3, -1, -1);

            Quaternion c10 = (ddouble.Rcp(3), ddouble.Rcp(6), 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c11 = (0, ddouble.Rcp(6), 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c12 = (0, 0, 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c13 = (0, 0, 0, 1 + ddouble.Rcp(6));
            Quaternion c14 = (0, 0, 0, 0);
            Quaternion c15 = (ddouble.Rcp(3), 0, 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c16 = (ddouble.Rcp(3), 0, 0, 1 + ddouble.Rcp(6));
            Quaternion c17 = (ddouble.Rcp(3), 0, 0, 0);
            Quaternion c18 = (ddouble.Rcp(3), ddouble.Rcp(6), 0, 1 + ddouble.Rcp(6));
            Quaternion c19 = (ddouble.Rcp(3), ddouble.Rcp(6), 0, 0);
            Quaternion c20 = (ddouble.Rcp(3), ddouble.Rcp(6), 1 + ddouble.Rcp(3), 0);
            Quaternion c21 = (-ddouble.Rcp(3), ddouble.Rcp(6), 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c22 = (ddouble.Rcp(3), -ddouble.Rcp(6), 1 + ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c23 = (ddouble.Rcp(3), ddouble.Rcp(6), -1 - ddouble.Rcp(3), 1 + ddouble.Rcp(6));
            Quaternion c24 = (ddouble.Rcp(3), ddouble.Rcp(6), 1 + ddouble.Rcp(3), -1 - ddouble.Rcp(6));

            Assert.AreEqual("2+3i+1k", c1.ToString());
            Assert.AreEqual("2+1j", c2.ToString());
            Assert.AreEqual("2-3i", c3.ToString());

            Assert.AreEqual("3i+1j+1k", c4.ToString());
            Assert.AreEqual("0", c5.ToString());
            Assert.AreEqual("-3i+1k", c6.ToString());

            Assert.AreEqual("-2+3i-1k", c7.ToString());
            Assert.AreEqual("-2", c8.ToString());
            Assert.AreEqual("-2-3i-1j-1k", c9.ToString());

            Assert.AreEqual("0", Quaternion.Zero.ToString());
            Assert.AreEqual(double.NaN.ToString(), Quaternion.NaN.ToString());

            Assert.AreEqual("3.333e-1+1.667e-1i+1.333e0j+1.167e0k", $"{c10:e3}");
            Assert.AreEqual("1.667e-1i+1.333e0j+1.167e0k", $"{c11:e3}");
            Assert.AreEqual("1.333e0j+1.167e0k", $"{c12:e3}");
            Assert.AreEqual("1.167e0k", $"{c13:e3}");
            Assert.AreEqual("0", $"{c14:e3}");
            Assert.AreEqual("3.333e-1+1.333e0j+1.167e0k", $"{c15:e3}");
            Assert.AreEqual("3.333e-1+1.167e0k", $"{c16:e3}");
            Assert.AreEqual("3.333e-1", $"{c17:e3}");
            Assert.AreEqual("3.333e-1+1.667e-1i+1.167e0k", $"{c18:e3}");
            Assert.AreEqual("3.333e-1+1.667e-1i", $"{c19:e3}");
            Assert.AreEqual("3.333e-1+1.667e-1i+1.333e0j", $"{c20:e3}");
            Assert.AreEqual("-3.333e-1+1.667e-1i+1.333e0j+1.167e0k", $"{c21:e3}");
            Assert.AreEqual("3.333e-1-1.667e-1i+1.333e0j+1.167e0k", $"{c22:e3}");
            Assert.AreEqual("3.333e-1+1.667e-1i-1.333e0j+1.167e0k", $"{c23:e3}");
            Assert.AreEqual("3.333e-1+1.667e-1i+1.333e0j-1.167e0k", $"{c24:e3}");
        }
    }
}
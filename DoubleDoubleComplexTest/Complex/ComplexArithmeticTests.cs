using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoubleDoubleComplexTests {
    using NComplex = System.Numerics.Complex;

    [TestClass()]
    public class ComplexArithmeticTests {
        [TestMethod()]
        public void AddTest() {
            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a + b;
                    NComplex nc = (NComplex)a + (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (ddouble b in new[] { 1, 3, 4, -1, 2, 7 }) {
                    Complex c = a + b;
                    NComplex nc = (NComplex)a + new NComplex((double)b, 0);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 3, 4, -1, 2, 7 }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a + b;
                    NComplex nc = new NComplex((double)a, 0) + (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void SubTest() {
            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a - b;
                    NComplex nc = (NComplex)a - (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (ddouble b in new[] { 1, 3, 4, -1, 2, 7 }) {
                    Complex c = a - b;
                    NComplex nc = (NComplex)a - new NComplex((double)b, 0);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 3, 4, -1, 2, 7 }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a - b;
                    NComplex nc = new NComplex((double)a, 0) - (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void MulTest() {
            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a * b;
                    NComplex nc = (NComplex)a * (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (ddouble b in new[] { 1, 3, 4, -1, 2, 7 }) {
                    Complex c = a * b;
                    NComplex nc = (NComplex)a * new NComplex((double)b, 0);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 3, 4, -1, 2, 7 }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a * b;
                    NComplex nc = new NComplex((double)a, 0) * (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }
        }

        [TestMethod()]
        public void DivTest() {
            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a / b;
                    NComplex nc = (NComplex)a / (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                    ComplexAssert.AreEqual(a, c * b, 1e-30);
                }
            }

            foreach (Complex a in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                foreach (ddouble b in new[] { 1, 3, 4, -1, 2, 7 }) {
                    Complex c = a / b;
                    NComplex nc = (NComplex)a / new NComplex((double)b, 0);

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            foreach (ddouble a in new[] { 1, 3, 4, -1, 2, 7 }) {
                foreach (Complex b in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1) }) {
                    Complex c = a / b;
                    NComplex nc = new NComplex((double)a, 0) / (NComplex)b;

                    ComplexAssert.AreEqual(nc, c, 1e-7);
                }
            }

            ComplexAssert.AreEqual(new Complex(1, 2) / new Complex(5, 6), new Complex(1e200, 2e200) / new Complex(5e200, 6e200), 1e-7);
            ComplexAssert.AreEqual(new Complex(1, 2) / new Complex(5, 6), new Complex(1e-200, 2e-200) / new Complex(5e-200, 6e-200), 1e-7);
            ComplexAssert.AreEqual(3 / new Complex(5, 6), 3e200 / new Complex(5e200, 6e200), 1e-7);
            ComplexAssert.AreEqual(3 / new Complex(5, 6), 3e-200 / new Complex(5e-200, 6e-200), 1e-7);
        }


        [TestMethod()]
        public void InverseTest() {
            foreach (Complex z in new[] { (1, 2), (2, 5), (6, -3), (7, -4), (3, -9), (7, 1), (-3, -4), (-1, -9), (-2, 1), (1e-200, 2e-200), (1e200, 2e200) }) {
                Complex c = Complex.Inverse(z);

                ComplexAssert.AreEqual(1, c * z, 1e-7);
            }
        }
    }
}

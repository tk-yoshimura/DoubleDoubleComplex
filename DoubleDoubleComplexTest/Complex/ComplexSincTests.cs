using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiPrecision;
using MultiPrecisionComplex;
using System;

namespace DoubleDoubleComplexTests {

    [TestClass()]
    public class ComplexSincTests {

        [TestMethod()]
        public void SincTest() {
            for (int exp = -25; exp >= -80; exp--) {
                double x = double.ScaleB(1, exp);

                foreach ((double r, double i) z in new (double r, double i)[] {
                    (0, x), (x, x), (x, 0), (-x, x), (-x, 0), (-x, -x), (0, -x), (x, -x) }) {

                    Complex actual = Complex.Sinc(z, normalized: false);
                    Complex expected = (Complex<Pow2.N8>.Sin(z) / z).ToString();

                    ddouble err = (expected - actual).Magnitude / expected.Magnitude;

                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(err < 1e-30);
                }
            }
        }

        [TestMethod()]
        public void SincPiTest() {
            for (int exp = -25; exp >= -80; exp--) {
                double x = double.ScaleB(1, exp);

                foreach ((double r, double i) z in new (double r, double i)[] {
                    (0, x), (x, x), (x, 0), (-x, x), (-x, 0), (-x, -x), (0, -x), (x, -x) }) {

                    Complex actual = Complex.Sinc(z, normalized: true);
                    Complex expected = (Complex<Pow2.N8>.Sin((Complex<Pow2.N8>)z * MultiPrecision<Pow2.N8>.Pi) /
                        ((Complex<Pow2.N8>)z * MultiPrecision<Pow2.N8>.Pi)).ToString();

                    ddouble err = (expected - actual).Magnitude / expected.Magnitude;

                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(err < 1e-30);
                }
            }
        }

        [TestMethod()]
        public void SinhcTest() {
            for (int exp = -25; exp >= -80; exp--) {
                double x = double.ScaleB(1, exp);

                foreach ((double r, double i) z in new (double r, double i)[] {
                    (0, x), (x, x), (x, 0), (-x, x), (-x, 0), (-x, -x), (0, -x), (x, -x) }) {

                    Complex actual = Complex.Sinhc(z);
                    Complex expected = (Complex<Pow2.N8>.Sinh(z) / z).ToString();

                    ddouble err = (expected - actual).Magnitude / expected.Magnitude;

                    Console.WriteLine(expected);
                    Console.WriteLine(actual);

                    Assert.IsTrue(err < 1e-30);
                }
            }
        }
    }
}
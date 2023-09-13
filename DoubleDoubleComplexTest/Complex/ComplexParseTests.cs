using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexParseTests {
        [TestMethod()]
        public void NormalTest() {
            Complex c1 = "1+2i";
            Complex c2 = "-1+2i";
            Complex c3 = "+1+2i";
            Complex c4 = "1-2i";
            Complex c5 = "-1-2i";
            Complex c6 = "+1-2i";
            Complex c7 = "1";
            Complex c8 = "-1";
            Complex c9 = "+1";
            Complex c10 = "2i";
            Complex c11 = "+2i";
            Complex c12 = "-2i";
            Complex c13 = "-2i+1";

            Assert.AreEqual(new Complex(1, 2), c1);
            Assert.AreEqual(new Complex(-1, 2), c2);
            Assert.AreEqual(new Complex(1, 2), c3);
            Assert.AreEqual(new Complex(1, -2), c4);
            Assert.AreEqual(new Complex(-1, -2), c5);
            Assert.AreEqual(new Complex(1, -2), c6);
            Assert.AreEqual(new Complex(1, 0), c7);
            Assert.AreEqual(new Complex(-1, 0), c8);
            Assert.AreEqual(new Complex(1, 0), c9);
            Assert.AreEqual(new Complex(0, 2), c10);
            Assert.AreEqual(new Complex(0, 2), c11);
            Assert.AreEqual(new Complex(0, -2), c12);
            Assert.AreEqual(new Complex(1, -2), c13);
        }

        [TestMethod()]
        public void BadParseTest() {
            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "++";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "--";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "-+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "-1+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+1-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+1+i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+1++i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "+1+-i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "1+i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "1i+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "1i+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "1i+1i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "2+2";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "2+2i-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "2+2i-2i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "2+2i-2";
            });

            Assert.ThrowsException<FormatException>(() => {
                Complex _ = "2+2p";
            });
        }
    }
}
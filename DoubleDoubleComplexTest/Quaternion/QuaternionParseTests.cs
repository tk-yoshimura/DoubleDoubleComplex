using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class QuaternionParseTests {
        [TestMethod()]
        public void NormalTest() {
            foreach ((ddouble v, string s) r in new[] { (0, ""), (1, "1"), (-1, "-1"), (+1, "+1") }) {
                foreach ((ddouble v, string s) i in new[] { (0, ""), (2, "2i"), (-2, "-2i"), (+2, "+2i") }) {
                    if (r.s != "" && i.s == "2i") {
                        continue;
                    }

                    foreach ((ddouble v, string s) j in new[] { (0, ""), (3, "3j"), (-3, "-3j"), (+3, "+3j") }) {
                        if ((r.s != "" || i.s != "") && j.s == "3j") {
                            continue;
                        }

                        foreach ((ddouble v, string s) k in new[] { (0, ""), (4, "4k"), (-4, "-4k"), (+4, "+4k") }) {
                            if ((r.s != "" || i.s != "" || j.s != "") && k.s == "4k") {
                                continue;
                            }

                            string s = $"{r.s}{i.s}{j.s}{k.s}";

                            if (string.IsNullOrEmpty(s)) {
                                continue;
                            }

                            Console.WriteLine(s);

                            Quaternion q = s;
                            Assert.AreEqual(new Quaternion(r.v, i.v, j.v, k.v), q, s);
                        }
                    }
                }
            }

            Quaternion q1 = "+4k+1+3j+2i";
            Assert.AreEqual(new Quaternion(1, 2, 3, 4), q1);

            Quaternion q2 = "+4k+3j+2i";
            Assert.AreEqual(new Quaternion(0, 2, 3, 4), q2);
        }

        [TestMethod()]
        public void BadParseTest() {
            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "++";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "+-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "--";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "-+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "-1+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "+1-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "+1+i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "1+i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "1i+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "1i+";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "1i+1i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2i-";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2p";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2jk";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2j2k";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2j+2k+2k";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2j+2k+2k+1i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "2+2j++2k+2k+1i";
            });

            Assert.ThrowsException<FormatException>(() => {
                Quaternion _ = "+4k+1+3j+2i+";
            });
        }
    }
}
using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexParseTests {
        [TestMethod()]
        public void NormalTest() {
            foreach ((ddouble v, string s) r in new[] {
                (0, ""), (1, "1"), (-1, "-1"), (+1, "+1"),
                (+0.25, "+0.25"), (-0.25, "-0.25"), (+0.125, "+1.25e-1"), (125, "+1.25e+2"), (125, "+1.25e2") }) {

                foreach ((ddouble v, string s) i in new[] {
                    (0, ""), (2, "2i"), (-2, "-2i"), (+2, "+2i"),
                    (+0.25, "+0.25i"), (-0.25, "-0.25i"), (+0.125, "+1.25e-1i"), (125, "+1.25e+2i"), (-125, "-1.25e2i") }) {
                    if (r.s != "" && i.s == "2i") {
                        continue;
                    }

                    string s = $"{r.s}{i.s}";

                    if (string.IsNullOrEmpty(s)) {
                        continue;
                    }

                    Console.WriteLine(s);

                    Complex c = s;
                    Assert.AreEqual(new Complex(r.v, i.v), c, s);
                }
            }

            Complex c1 = "2i+1";
            Assert.AreEqual(new Complex(1, 2), c1);
        }

        [TestMethod()]
        public void BadParseTest() {
            string[] tests = [
                "",
                "+",
                "-",
                "++",
                "+-",
                "--",
                "-+",
                "+1-",
                "+1+i",
                "+1++i",
                "+1+-i",
                "1+i",
                "i",
                "1i+",
                "1i+1i",
                "2+2",
                "2+2i-",
                "2+2i-2i",
                "2+2i-2",
                "2+2p",
                "2+2e",
                "2+2e1",
                "2+e",
                "e"
            ];

            foreach (string test in tests) {
                Console.WriteLine(test);

                Assert.ThrowsException<FormatException>(() => {
                    Complex _ = test;
                });
            }
        }
    }
}
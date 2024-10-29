using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class OctonionParseTests {
        [TestMethod()]
        public void NormalTest() {
            (ddouble v, string s)[] test0 = { (0, ""), (1, "1"), (-1, "-1") };
            (ddouble v, string s)[] test1 = { (0, ""), (1, "+1"), ("2e5", "2e5") };
            (ddouble v, string s)[] test2 = { (0, ""), ("2e-5", "2e-5"), ("-4.0e-5", "-4.0e-5") };

            foreach ((ddouble v, string s)[] vs in new (ddouble v, string s)[][] { test0, test1, test2 }) {
                foreach ((ddouble v, string s) r in vs) {
                    string rs = r.s;

                    foreach ((ddouble v, string s) s in vs) {
                        string ss = string.IsNullOrEmpty(s.s) ? string.Empty : $"+{s.s}s".Replace("+-", "-").Replace("++", "+");

                        foreach ((ddouble v, string s) t in vs) {
                            string ts = string.IsNullOrEmpty(t.s) ? string.Empty : $"+{t.s}t".Replace("+-", "-").Replace("++", "+");

                            foreach ((ddouble v, string s) u in vs) {
                                string us = string.IsNullOrEmpty(u.s) ? string.Empty : $"+{u.s}u".Replace("+-", "-").Replace("++", "+");

                                foreach ((ddouble v, string s) w in vs) {
                                    string ws = string.IsNullOrEmpty(w.s) ? string.Empty : $"+{w.s}w".Replace("+-", "-").Replace("++", "+");

                                    foreach ((ddouble v, string s) x in vs) {
                                        string xs = string.IsNullOrEmpty(x.s) ? string.Empty : $"+{x.s}x".Replace("+-", "-").Replace("++", "+");

                                        foreach ((ddouble v, string s) y in vs) {
                                            string ys = string.IsNullOrEmpty(y.s) ? string.Empty : $"+{y.s}y".Replace("+-", "-").Replace("++", "+");

                                            foreach ((ddouble v, string s) z in vs) {
                                                string zs = string.IsNullOrEmpty(z.s) ? string.Empty : $"+{z.s}z".Replace("+-", "-").Replace("++", "+");

                                                string str = $"{rs}{ss}{ts}{us}{ws}{xs}{ys}{zs}";

                                                if (string.IsNullOrEmpty(str)) {
                                                    str = "0";
                                                }

                                                Console.WriteLine(str);

                                                Octonion o = str;

                                                Assert.AreEqual(r.v, o.R);
                                                Assert.AreEqual(s.v, o.S);
                                                Assert.AreEqual(t.v, o.T);
                                                Assert.AreEqual(u.v, o.U);
                                                Assert.AreEqual(w.v, o.W);
                                                Assert.AreEqual(x.v, o.X);
                                                Assert.AreEqual(y.v, o.Y);
                                                Assert.AreEqual(z.v, o.Z);

                                                Octonion o2 = o.ToString();

                                                Console.WriteLine(o2);

                                                Assert.AreEqual(r.v, o2.R);
                                                Assert.AreEqual(s.v, o2.S);
                                                Assert.AreEqual(t.v, o2.T);
                                                Assert.AreEqual(u.v, o2.U);
                                                Assert.AreEqual(w.v, o2.W);
                                                Assert.AreEqual(x.v, o2.X);
                                                Assert.AreEqual(y.v, o2.Y);
                                                Assert.AreEqual(z.v, o2.Z);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod()]
        public void BadParseTest() {
            string[] tests = [
                "",
                " ",
                "+",
                "-",
                "++",
                "+-",
                "--",
                "-+",
                "+1-",
                "+1+s",
                "+1++s",
                "+1+-s",
                "1+s",
                "s",
                "1s+",
                "1s+1s",
                "2+2",
                "2+2s-",
                "2+2s-2s",
                "2+2s-2",
                "2+2p",
                "2+2e",
                "2+2e1",
                "2+e",
                "e",
                "2+2tu",
                "2+2t+2u+2u",
                "2+2t+2u+2u+1s",
                "2+2t++2u+2u+1s",
                "2+2t++2u+2u+es",
                "+4u+1+3t+2s+",
                "+1+x",
                "+1++x",
                "+1+-x",
                "1+x",
                "x",
                "1x+",
                "1x+1x",
                "2+2",
                "2+2x-",
                "2+2x-2x",
                "2+2x-2",
                "2+2p",
                "2+2e",
                "2+2e1",
                "2+e",
                "e",
                "2+2yz",
                "2+2y+2z+2z",
                "2+2y+2z+2z+1x",
                "2+2y++2z+2z+1x",
                "2+2y++2z+2z+ex",
                "+4z+1+3y+2x+",
                "2+2i"
            ];

            foreach (string test in tests) {
                Console.WriteLine(test);

                Assert.ThrowsException<FormatException>(() => {
                    Octonion _ = test;
                });
            }
        }
    }
}
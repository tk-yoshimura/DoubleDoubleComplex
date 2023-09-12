using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DoubleDoubleComplexTests {
    [TestClass()]
    public class ComplexGammaTests {
        [TestMethod()]
        public void SandboxTest() {
            Complex c = Complex.Gamma((1, 16));

            Console.WriteLine(c);
        }
    }
}
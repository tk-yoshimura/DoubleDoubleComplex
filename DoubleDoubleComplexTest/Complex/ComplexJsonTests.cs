using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class ComplexJsonTests {
        [TestMethod]
        public void JsonTest() {
            Complex c = (ddouble.Pi, ddouble.E);

            string str = JsonSerializer.Serialize<Complex>(c);

            Complex c2 = JsonSerializer.Deserialize<Complex>(str);

            Assert.AreEqual(c.ToString(), c2.ToString());
        }
    }
}

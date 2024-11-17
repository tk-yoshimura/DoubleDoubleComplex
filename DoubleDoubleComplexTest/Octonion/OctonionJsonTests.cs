using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class OctonionJsonTests {
        [TestMethod]
        public void JsonTest() {
            Octonion o = (ddouble.Pi, ddouble.E, ddouble.RcpPi, ddouble.RcpE, -ddouble.Pi, -ddouble.E, -ddouble.RcpPi, -ddouble.RcpE);

            string str = JsonSerializer.Serialize<Octonion>(o);

            Octonion o2 = JsonSerializer.Deserialize<Octonion>(str);

            Assert.AreEqual(o.ToString(), o2.ToString());
        }
    }
}

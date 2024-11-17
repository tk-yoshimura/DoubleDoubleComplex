using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class QuaternionJsonTests {
        [TestMethod]
        public void JsonTest() {
            Quaternion q = (ddouble.Pi, ddouble.E, ddouble.RcpPi, ddouble.RcpE);

            string str = JsonSerializer.Serialize<Quaternion>(q);

            Quaternion q2 = JsonSerializer.Deserialize<Quaternion>(str);

            Assert.AreEqual(q.ToString(), q2.ToString());
        }
    }
}

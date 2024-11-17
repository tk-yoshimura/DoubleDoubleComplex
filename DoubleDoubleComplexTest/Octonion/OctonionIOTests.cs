using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class OctonionIOTests {
        [TestMethod]
        public void IOTest() {
            const string filename_bin = "o_iotest.bin";

            Octonion v = (ddouble.Pi, ddouble.E, ddouble.RcpPi, ddouble.RcpE, -ddouble.Pi, -ddouble.E, -ddouble.RcpPi, -ddouble.RcpE);

            using (BinaryWriter stream = new BinaryWriter(File.Open(filename_bin, FileMode.Create))) {
                stream.Write(v);
            }

            Octonion u;

            using (BinaryReader stream = new BinaryReader(File.OpenRead(filename_bin))) {
                u = stream.ReadOctonion();
            }

            Assert.AreEqual(v, u);

            File.Delete(filename_bin);
        }
    }
}

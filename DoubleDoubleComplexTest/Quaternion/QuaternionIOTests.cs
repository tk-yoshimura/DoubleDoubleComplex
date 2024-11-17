using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class QuaternionIOTests {
        [TestMethod]
        public void IOTest() {
            const string filename_bin = "q_iotest.bin";

            Quaternion v = (ddouble.Pi, ddouble.E, ddouble.RcpPi, ddouble.RcpE);

            using (BinaryWriter stream = new BinaryWriter(File.Open(filename_bin, FileMode.Create))) {
                stream.Write(v);
            }

            Quaternion u;

            using (BinaryReader stream = new BinaryReader(File.OpenRead(filename_bin))) {
                u = stream.ReadQuaternion();
            }

            Assert.AreEqual(v, u);

            File.Delete(filename_bin);
        }
    }
}

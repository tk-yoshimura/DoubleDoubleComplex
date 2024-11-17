using DoubleDouble;
using DoubleDoubleComplex;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DoubleDoubleComplexTests {
    [TestClass]
    public partial class ComplexIOTests {
        [TestMethod]
        public void IOTest() {
            const string filename_bin = "c_iotest.bin";

            Complex v = (ddouble.Pi, ddouble.E);

            using (BinaryWriter stream = new BinaryWriter(File.Open(filename_bin, FileMode.Create))) {
                stream.Write(v);
            }

            Complex u;

            using (BinaryReader stream = new BinaryReader(File.OpenRead(filename_bin))) {
                u = stream.ReadComplex();
            }

            Assert.AreEqual(v, u);

            File.Delete(filename_bin);
        }
    }
}

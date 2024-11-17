using DoubleDouble;
using System.IO;

namespace DoubleDoubleComplex {

    public static class ComplexIOExpand {

        public static void Write(this BinaryWriter writer, Complex v) {
            DoubleDoubleIOExpand.Write(writer, v.R);
            DoubleDoubleIOExpand.Write(writer, v.I);
        }

        public static Complex ReadComplex(this BinaryReader reader) {
            ddouble r = reader.ReadDDouble();
            ddouble i = reader.ReadDDouble();

            return (r, i);
        }
    }
}

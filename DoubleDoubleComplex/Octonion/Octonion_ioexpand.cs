using DoubleDouble;
using System.IO;

namespace DoubleDoubleComplex {

    public static class OctonionIOExpand {

        public static void Write(this BinaryWriter writer, Octonion v) {
            DoubleDoubleIOExpand.Write(writer, v.R);
            DoubleDoubleIOExpand.Write(writer, v.I);
            DoubleDoubleIOExpand.Write(writer, v.J);
            DoubleDoubleIOExpand.Write(writer, v.K);
            DoubleDoubleIOExpand.Write(writer, v.W);
            DoubleDoubleIOExpand.Write(writer, v.X);
            DoubleDoubleIOExpand.Write(writer, v.Y);
            DoubleDoubleIOExpand.Write(writer, v.Z);
        }

        public static Octonion ReadOctonion(this BinaryReader reader) {
            ddouble r = reader.ReadDDouble();
            ddouble i = reader.ReadDDouble();
            ddouble j = reader.ReadDDouble();
            ddouble k = reader.ReadDDouble();
            ddouble w = reader.ReadDDouble();
            ddouble x = reader.ReadDDouble();
            ddouble y = reader.ReadDDouble();
            ddouble z = reader.ReadDDouble();

            return (r, i, j, k, w, x, y, z);
        }
    }
}

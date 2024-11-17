using DoubleDouble;
using System.IO;

namespace DoubleDoubleComplex {

    public static class QuaternionIOExpand {

        public static void Write(this BinaryWriter writer, Quaternion v) {
            DoubleDoubleIOExpand.Write(writer, v.R);
            DoubleDoubleIOExpand.Write(writer, v.I);
            DoubleDoubleIOExpand.Write(writer, v.J);
            DoubleDoubleIOExpand.Write(writer, v.K);
        }

        public static Quaternion ReadQuaternion(this BinaryReader reader) {
            ddouble r = reader.ReadDDouble();
            ddouble i = reader.ReadDDouble();
            ddouble j = reader.ReadDDouble();
            ddouble k = reader.ReadDDouble();

            return (r, i, j, k);
        }
    }
}

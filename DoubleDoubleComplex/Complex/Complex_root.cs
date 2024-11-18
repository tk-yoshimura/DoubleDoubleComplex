using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Sqrt(Complex z) {
            return FromPolarCoordinates(ddouble.Sqrt(z.Norm), ddouble.Ldexp(z.Phase, -1));
        }

        public static Complex Cbrt(Complex z) {
            return FromPolarCoordinates(ddouble.Cbrt(z.Norm), z.Phase / 3d);
        }

        public static Complex RootN(Complex z, int n) {
            return FromPolarCoordinates(ddouble.RootN(z.Norm, n), z.Phase / n);
        }
    }
}
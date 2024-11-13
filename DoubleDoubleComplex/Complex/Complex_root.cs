using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Sqrt(Complex z) {
            return FromPolarCoordinates(ddouble.Sqrt(z.Magnitude), ddouble.Ldexp(z.Phase, -1));
        }

        public static Complex Cbrt(Complex z) {
            return FromPolarCoordinates(ddouble.Cbrt(z.Magnitude), z.Phase / 3d);
        }

        public static Complex RootN(Complex z, int n) {
            return FromPolarCoordinates(ddouble.RootN(z.Magnitude, n), z.Phase / n);
        }
    }
}
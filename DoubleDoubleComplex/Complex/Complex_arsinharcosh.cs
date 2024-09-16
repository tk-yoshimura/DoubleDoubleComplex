using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Arsinh(Complex z) {
            return MulI(Asin(MulMinusI(z)));
        }

        public static Complex Arcosh(Complex z) {
            Complex y = MulI(Acos(z));

            if (ddouble.IsNegative(y.R)) {
                y = -y;
            }

            return y;
        }

        public static Complex Artanh(Complex z) {
            return MulMinusI(Atan(MulI(z)));
        }
    }
}
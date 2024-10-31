using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Asinh(Complex z) {
            return MulI(Asin(MulMinusI(z)));
        }

        public static Complex Acosh(Complex z) {
            Complex y = MulI(Acos(z));

            if (ddouble.IsNegative(y.R)) {
                y = -y;
            }

            return y;
        }

        public static Complex Atanh(Complex z) {
            return MulMinusI(Atan(MulI(z)));
        }
    }
}
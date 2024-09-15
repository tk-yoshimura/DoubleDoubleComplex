using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Arsinh(Complex z) {
            return ImaginaryOne * Asin(-ImaginaryOne * z);
        }

        public static Complex Arcosh(Complex z) {
            Complex y = ImaginaryOne * Acos(z);

            if (ddouble.IsNegative(y.R)) {
                y = -y;
            }

            return y;
        }

        public static Complex Artanh(Complex z) {
            return -ImaginaryOne * Atan(ImaginaryOne * z);
        }
    }
}
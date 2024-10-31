using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex Sinc(Complex z, bool normalized = true) {
            if (ILogB(z) > -53) {
                return normalized ? (SinPi(z) / (z * ddouble.Pi)) : (Sin(z) / z);
            }
            else {
                if (normalized) {
                    z *= ddouble.Pi;
                }

                Complex z2 = z * z;

                return (120d + z2 * (-20d + z2)) / 120d;
            }
        }

        public static Complex Sinhc(Complex z) {
            if (ILogB(z) > -53) {
                return Sinh(z) / z;
            }
            else {
                Complex z2 = z * z;

                return (120d + z2 * (20d + z2)) / 120d;
            }
        }
    }
}

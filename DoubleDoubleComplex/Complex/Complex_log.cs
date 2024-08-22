using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Log(Complex z) {
            return new Complex(
                ddouble.Log(z.Magnitude),
                z.Phase
            );
        }

        public static Complex Log2(Complex z) {
            return new Complex(
                ddouble.Log2(z.Magnitude),
                z.Phase * ddouble.LbE
            );
        }

        public static Complex Log10(Complex z) {
            return new Complex(
                ddouble.Log10(z.Magnitude),
                z.Phase / ddouble.Log(10)
            );
        }

        public static Complex Log(Complex z, ddouble b) {
            return Log(z) / ddouble.Log(b);
        }
    }
}
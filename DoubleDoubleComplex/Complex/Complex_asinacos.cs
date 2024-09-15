namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Asin(Complex z) {
            return -ImaginaryOne * Log(ImaginaryOne * z + Sqrt(1d - z * z));
        }

        public static Complex Acos(Complex z) {
            return -ImaginaryOne * Log(z + ImaginaryOne * Sqrt(1d - z * z));
        }

        public static Complex Atan(Complex z) {
            return ImaginaryOne / 2 * (Log1p(-ImaginaryOne * z) - Log1p(ImaginaryOne * z));
        }
    }
}
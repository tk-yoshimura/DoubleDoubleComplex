using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex FromPolarCoordinates(ddouble magnitude, ddouble phase) {
            return new Complex(ddouble.Cos(phase) * magnitude, ddouble.Sin(phase) * magnitude);
        }
    }
}
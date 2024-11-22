using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex FromPhase(ddouble phase)
            => (ddouble.Cos(phase), ddouble.Sin(phase));

        public static Complex FromPhasePi(ddouble phase)
            => (ddouble.CosPi(phase), ddouble.SinPi(phase));

        public static Complex FromPolar(ddouble magnitude, ddouble phase)
            => (magnitude * ddouble.Cos(phase), magnitude * ddouble.Sin(phase));

        public static Complex FromPolarPi(ddouble magnitude, ddouble phase)
            => (magnitude * ddouble.CosPi(phase), magnitude * ddouble.SinPi(phase));
    }
}

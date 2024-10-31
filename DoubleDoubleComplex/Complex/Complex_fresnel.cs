using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex FresnelS(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.FresnelS(z.R);
            }

            Complex y = (0.25d, 0.25d) * (
                Erf(Consts.Fresnel.CPlus * z) -
                MulI(Erf(Consts.Fresnel.CMinus * z))
            );

            return y;
        }

        public static Complex FresnelC(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.FresnelC(z.R);
            }

            Complex y = (0.25d, -0.25d) * (
                Erf(Consts.Fresnel.CPlus * z) +
                MulI(Erf(Consts.Fresnel.CMinus * z))
            );

            return y;
        }

        internal static partial class Consts {
            public static class Fresnel {
                public static readonly ddouble SqrtPiD2 = (+1, -1, 0xE2DFC48DA77B553CuL, 0xE1D82906AEDC9C1FuL);
                public static readonly Complex CPlus = new Complex(1, 1) * SqrtPiD2;
                public static readonly Complex CMinus = new Complex(1, -1) * SqrtPiD2;
            }
        }
    }
}

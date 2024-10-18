using DoubleDouble;
using System.Diagnostics;
using static DoubleDoubleComplex.Complex.ComplexEiUtil;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex E1(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                if (ddouble.IsPositive(z.R)) {
                    return -ddouble.Ei(-z.R);
                }
                else {
                    return (-ddouble.Ei(-z.R), -ddouble.PI);
                }
            }

            if (!IsFinite(z)) {
                return NaN;
            }

            if (z.R <= 4d && z.R >= -LimitThreshold && ddouble.Abs(z.I) <= PowerSeriesImagThreshold) {
                return E1PowerSeries(z) - Log(z);
            }
            else if (z.Magnitude <= LimitThreshold) {
                return E1ContinuedFraction(z);
            }
            else {
                return E1Asymptotic(z);
            }
        }

        public static Complex Ei(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Ei(z.R);
            }

            return -E1(-z) + (0, ddouble.Sign(z.I) * ddouble.PI);
        }

        public static Complex Ein(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Ein(z.R);
            }

            if (!IsFinite(z)) {
                return NaN;
            }

            if (z.R <= NearZeroThreshold && z.R >= -LimitThreshold && ddouble.Abs(z.I) <= PowerSeriesImagThreshold) {
                return EinPowerSeries(z);
            }
            else if (z.Magnitude <= LimitThreshold) {
                return E1ContinuedFraction(z) + ddouble.EulerGamma + Log(z);
            }
            else {
                return E1Asymptotic(z) + ddouble.EulerGamma + Log(z);
            }
        }

        public static Complex Si(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Si(z.R);
            }
            if (!ddouble.IsFinite(z.I) || AlmostImag(z)) {
                return (0, ddouble.Shi(z.I));
            }

            if (z.Magnitude <= NearZeroThreshold) {
                return SiPowerSeries(z);
            }
            else {
                Complex y = MulMinusI(Ein(MulI(z)) - Ein(MulMinusI(z))) / 2d;

                return y;
            }
        }

        public static Complex Ci(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.IsPositive(z.R) ? ddouble.Ci(z.R) : (ddouble.Ci(-z.R), ddouble.PI);
            }
            if (!ddouble.IsFinite(z.I) || AlmostImag(z)) {
                return (ddouble.Chi(ddouble.Abs(z.I)), ddouble.Sign(z.I) * ddouble.PI / 2d);
            }

            if (z.Magnitude <= NearZeroThreshold) {
                return CiPowerSeries(z) + Log(z);
            }
            else {
                Complex y = (Ein(MulI(z)) + Ein(MulMinusI(z))) / -2d + ddouble.EulerGamma + Log(z);

                return y;
            }
        }

        internal static class ComplexEiUtil {
            public const double LimitThreshold = 90d;
            public const double NearZeroThreshold = 4d;
            public const double PowerSeriesImagThreshold = 4d;

            public static Complex E1Asymptotic(Complex z, int max_terms = 1024) {
                Complex c = One, v = 1d / z, v2 = v * v, u = v;

                for (int k = 1; k <= max_terms; k += 2) {
                    Complex f = v * (k + 1);

                    Debug.Assert(f.Magnitude < 0.5d);

                    Complex dc = u * (1d - f);
                    c -= dc;

                    if (c.Magnitude * 1e-31 > dc.Magnitude) {
                        break;
                    }

                    u *= v2 * ((k + 1) * (k + 2));
                }

                Complex y = Exp(-z) * v * c;

                return y;
            }

            public static Complex E1ContinuedFraction(Complex z, int max_iter = 1024) {
                (Complex a0, Complex b0, Complex a1, Complex b1) = (1d, 0d, 0d, 1d);
                (a0, b0, a1, b1) = (a1, b1, z * a1 + a0, z * b1 + b0);

                Complex s = 1d;

                for (int i = 1; i <= max_iter; i++) {
                    (a0, b0, a1, b1) = (a1, b1, a1 + i * a0, b1 + i * b0);
                    (a0, b0, a1, b1) = (a1, b1, z * a1 + i * a0, z * b1 + i * b0);
                    s = a1 / b1;

                    int exp = int.Max(ILogB(a1), ILogB(b1));
                    if (exp <= int.MinValue) {
                        return NaN;
                    }

                    (a0, a1, b0, b1) = (Ldexp(a0, -exp), Ldexp(a1, -exp), Ldexp(b0, -exp), Ldexp(b1, -exp));

                    if (i > 0 && (i & 3) == 0) {
                        Complex r0 = a0 * b1, r1 = a1 * b0;
                        if (!((r0 - r1).Magnitude > ddouble.Min(r0.Magnitude, r1.Magnitude) * 1e-30)) {
                            break;
                        }
                    }
                }

                Complex y = Exp(-z) * s;

                return y;
            }

            public static Complex E1PowerSeries(Complex z, int max_terms = 1024) {
                Complex c = -ddouble.EulerGamma, u = z;

                for (int k = 1; k <= max_terms; k++) {
                    Complex dc = u / k;
                    c += dc;

                    if (c.Magnitude * 1e-31 > dc.Magnitude) {
                        break;
                    }

                    u *= z / (-(k + 1));
                }

                return c;
            }

            public static Complex EinPowerSeries(Complex z, int max_terms = 1024) {
                Complex c = Zero, u = z;

                for (int k = 1; k <= max_terms; k++) {
                    Complex dc = u / k;
                    c += dc;

                    if (c.Magnitude * 1e-31 > dc.Magnitude) {
                        break;
                    }

                    u *= z / (-(k + 1));
                }

                return c;
            }

            public static Complex SiPowerSeries(Complex z, int max_terms = 1024) {
                Complex z2 = z * z, c = z, u = -z * z2 / 6d;

                for (int k = 1; k <= max_terms; k++) {
                    Complex dc = u / (2 * k + 1);
                    c += dc;

                    if (c.Magnitude * 1e-31 > dc.Magnitude) {
                        break;
                    }

                    u *= z2 / (-(2 * k + 2) * (2 * k + 3));
                }

                return c;
            }

            public static Complex CiPowerSeries(Complex z, int max_terms = 1024) {
                Complex z2 = z * z, c = ddouble.EulerGamma, u = -z2 / 2d;

                for (int k = 1; k <= max_terms; k++) {
                    Complex dc = u / (2 * k);
                    c += dc;

                    if (c.Magnitude * 1e-31 > dc.Magnitude) {
                        break;
                    }

                    u *= z2 / (-(2 * k + 1) * (2 * k + 2));
                }

                return c;
            }
        }
    }
}

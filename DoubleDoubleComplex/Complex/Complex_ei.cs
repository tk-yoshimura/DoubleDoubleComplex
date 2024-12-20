﻿using DoubleDouble;
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
                    return (-ddouble.Ei(-z.R), -ddouble.Pi);
                }
            }

            if (!IsFinite(z)) {
                return NaN;
            }

            if (z.R <= NearZeroThreshold && z.R >= -LimitThreshold && ddouble.Abs(z.I) <= PowerSeriesImagThreshold) {
                return E1PowerSeries(z) - Log(z);
            }
            else if (z.Norm <= LimitThreshold) {
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

            return -E1(-z) + (0d, ddouble.Sign(z.I) * ddouble.Pi);
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
            else if (z.Norm <= LimitThreshold) {
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
                return (0d, ddouble.Shi(z.I));
            }

            if (z.Norm <= NearZeroThreshold) {
                return SiPowerSeries(z);
            }
            else {
                Complex y = Ldexp(MulMinusI(Ein(MulI(z)) - Ein(MulMinusI(z))), -1);

                return y;
            }
        }

        public static Complex Ci(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.IsPositive(z.R) ? ddouble.Ci(z.R) : (ddouble.Ci(-z.R), ddouble.Pi);
            }
            if (!ddouble.IsFinite(z.I) || AlmostImag(z)) {
                return (ddouble.Chi(ddouble.Abs(z.I)), ddouble.Sign(z.I) * ddouble.Ldexp(ddouble.Pi, -1));
            }

            if (z.Norm <= NearZeroThreshold) {
                return CiPowerSeries(z) + Log(z);
            }
            else {
                Complex y = ddouble.EulerGamma + Log(z) - Ldexp(Ein(MulI(z)) + Ein(MulMinusI(z)), -1);

                return y;
            }
        }

        public static Complex Shi(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Shi(z.R);
            }
            if (!ddouble.IsFinite(z.I) || AlmostImag(z)) {
                return (0d, ddouble.Si(z.I));
            }

            if (z.Norm <= NearZeroThreshold) {
                return MulMinusI(SiPowerSeries(MulI(z)));
            }
            else {
                Complex y = Ldexp(Ein(z) - Ein(-z), -1);

                return y;
            }
        }

        public static Complex Chi(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.IsPositive(z.R) ? ddouble.Chi(z.R) : (ddouble.Chi(-z.R), ddouble.Pi);
            }
            if (!ddouble.IsFinite(z.I) || AlmostImag(z)) {
                return ddouble.IsPositive(z.I) ? (ddouble.Ci(z.I), ddouble.Ldexp(ddouble.Pi, -1)) : (ddouble.Ci(-z.I), -ddouble.Ldexp(ddouble.Pi, -1));
            }

            if (z.Norm <= NearZeroThreshold) {
                return CiPowerSeries(MulI(z)) + Log(z);
            }
            else {
                Complex y = ddouble.EulerGamma + Log(z) - Ldexp(Ein(-z) + Ein(z), -1);

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

                    Debug.Assert(f.Norm < 0.5d);

                    Complex dc = u * (1d - f);
                    c -= dc;

                    if (c.Norm * 1e-31 > dc.Norm) {
                        break;
                    }

                    u *= v2 * ((k + 1) * (k + 2));
                }

                Complex y = Exp(-z) * v * c;

                return y;
            }

            public static Complex E1ContinuedFraction(Complex z, int max_iter = 1024) {
                (Complex a0, Complex b0, Complex a1, Complex b1) = (0d, 1d, 1d, z);

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
                        if (!((r0 - r1).Norm > ddouble.Min(r0.Norm, r1.Norm) * 1e-30)) {
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

                    if (c.Norm * 1e-31 > dc.Norm) {
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

                    if (c.Norm * 1e-31 > dc.Norm) {
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

                    if (c.Norm * 1e-31 > dc.Norm) {
                        break;
                    }

                    u *= z2 / (-(2 * k + 2) * (2 * k + 3));
                }

                return c;
            }

            public static Complex CiPowerSeries(Complex z, int max_terms = 1024) {
                Complex z2 = z * z, c = ddouble.EulerGamma, u = Ldexp(-z2, -1);

                for (int k = 1; k <= max_terms; k++) {
                    Complex dc = u / (2 * k);
                    c += dc;

                    if (c.Norm * 1e-31 > dc.Norm) {
                        break;
                    }

                    u *= z2 / (-(2 * k + 1) * (2 * k + 2));
                }

                return c;
            }
        }
    }
}

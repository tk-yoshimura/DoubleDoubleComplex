using DoubleDouble;
using System;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex Erf(Complex z) {
            if (!ddouble.IsFinite(z.R) || double.Abs((double)z.I) <= double.Abs((double)z.R) * 5e-31) {
                return ddouble.Erf(z.R);
            }
            if (double.Abs((double)z.R) <= double.Abs((double)z.I) * 5e-31) {
                return (0, ddouble.Erfi(z.I));
            }
            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.R)) {
                return -Erf(-z);
            }
            if (ddouble.IsNegative(z.I)) {
                return Conjugate(Erf(Conjugate(z)));
            }

            Complex w = z * z;

            if (z.R < Consts.Erf.MinCFracR) {
                Complex c = One, u = -w;
                for (int k = 1, convergence_time = 0; k < 1280 && convergence_time < 4; k++) {
                    Complex dc = u / (2 * k + 1);
                    c += dc;

                    u *= w / -(k + 1);

                    if ((double.Abs((double)dc.R) <= double.Abs((double)c.R) * 1e-30) &&
                        (double.Abs((double)dc.I) <= double.Abs((double)c.I) * 1e-30)) {

                        convergence_time++;
                    }
                }

                Complex y = c * z * (2 * Consts.Erf.RcpSqrtPI);

                return y;
            }
            else {
                int n = Consts.Erf.CFracIter(z);

                Complex f = One;

                for (int k = 4 * n - 3; k >= 1; k -= 4) {
                    Complex c0 = (k + 2) * f;
                    Complex c1 = w * ((k + 3) + f * 2d);
                    Complex d0 = (k + 1) * (k + 3) + (4 * k + 6) * f;
                    Complex d1 = c1 * 2d;

                    f = w + k * (c0 + c1) / (d0 + d1);
                }

                Complex c = z * Exp(-w) * Consts.Erf.RcpSqrtPI;

                Complex y = One - c / f;

                return y;
            }
        }

        public static Complex Erfc(Complex z) {
            if (!ddouble.IsFinite(z.R) || double.Abs((double)z.I) <= double.Abs((double)z.R) * 5e-31) {
                return ddouble.Erfc(z.R);
            }
            if (double.Abs((double)z.R) <= double.Abs((double)z.I) * 5e-31) {
                return (1, -ddouble.Erfi(z.I));
            }
            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return Conjugate(Erfc(Conjugate(z)));
            }

            if (z.R < Consts.Erf.MinCFracR) {
                return One - Erf(z);
            }
            else {
                int n = Consts.Erf.CFracIter(z) + 4;

                Complex f = One, w = z * z;

                for (int k = 4 * n - 3; k >= 1; k -= 4) {
                    Complex c0 = (k + 2) * f;
                    Complex c1 = w * ((k + 3) + f * 2d);
                    Complex d0 = (k + 1) * (k + 3) + (4 * k + 6) * f;
                    Complex d1 = c1 * 2d;

                    f = w + k * (c0 + c1) / (d0 + d1);
                }

                Complex c = z * Exp(-w) * Consts.Erf.RcpSqrtPI;

                Complex y = c / f;

                return y;
            }
        }

        public static Complex Erfcx(Complex z) {
            if (!ddouble.IsFinite(z.R) || double.Abs((double)z.I) <= double.Abs((double)z.R) * 5e-31) {
                return ddouble.Erfcx(z.R);
            }

            if (ddouble.IsNegative(z.I)) {
                return Conjugate(Erfcx(Conjugate(z)));
            }

            Complex w = z * z;
            
            if (!ddouble.IsFinite(z.I) || z.R < Consts.Erf.MinCFracR) {
                return Erfc(z) * Exp(w);
            }
            else {
                int n = Consts.Erf.CFracIter(z) + 4;

                Complex f = One;

                for (int k = 4 * n - 3; k >= 1; k -= 4) {
                    Complex c0 = (k + 2) * f;
                    Complex c1 = w * ((k + 3) + f * 2d);
                    Complex d0 = (k + 1) * (k + 3) + (4 * k + 6) * f;
                    Complex d1 = c1 * 2d;

                    f = w + k * (c0 + c1) / (d0 + d1);
                }

                Complex y = z * Consts.Erf.RcpSqrtPI / f;

                return y;
            }
        }

        internal static partial class Consts {
            public static class Erf {
                public const double MinCFracR = 2d;
                public static readonly ddouble RcpSqrtPI = 1d / ddouble.Sqrt(ddouble.PI);

                const int min_c_frac_iter = 8;
                static readonly int[,] c_frac_iter_table =
                    { {38, 25, 17, 12,  9,  8},
                      {39, 25, 17, 13, 10,  8},
                      {39, 25, 18, 13, 10,  8},
                      {36, 25, 18, 14, 11,  8},
                      {30, 21, 17, 14, 11,  9},
                      {24, 17, 14, 12, 11, 10},
                      {17, 14, 12, 10,  9,  9},
                      {12, 10,  9,  9,  8,  8},
                      { 9,  8,  8,  8,  8,  8} };

                public static int CFracIter(Complex z) {
#if DEBUG
                    if (!(z.R >= MinCFracR && ddouble.IsPositive(z.I))) {
                        throw new ArgumentOutOfRangeException(nameof(z));
                    }
#endif

                    int r_index = (int)double.Floor(((double)z.R - MinCFracR) * 2);
                    int i_index = (int)double.Floor((double)z.I);

                    if (r_index >= c_frac_iter_table.GetLength(1) || i_index >= c_frac_iter_table.GetLength(0)) {
                        return min_c_frac_iter;
                    }

                    int n = c_frac_iter_table[i_index, r_index];

                    return n;
                }
            }
        }
    }
}

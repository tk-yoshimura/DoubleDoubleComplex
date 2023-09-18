﻿using DoubleDouble;
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

            if (!Consts.Erf.UseCFrac(z)) {
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

            if (ddouble.IsNegative(z.R) || !Consts.Erf.UseCFrac(z)) {
                return One - Erf(z);
            }
            else {
                int n = Consts.Erf.CFracIter(z);

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

            if (!ddouble.IsFinite(z.I) || ddouble.IsNegative(z.R) || !Consts.Erf.UseCFrac(z)) {
                return (One - Erf(z)) * Exp(w);
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

                Complex y = z * Consts.Erf.RcpSqrtPI / f;

                return y;
            }
        }

        internal static partial class Consts {
            public static class Erf {
                public static readonly ddouble RcpSqrtPI = 1d / ddouble.Sqrt(ddouble.PI);

                const int min_c_frac_iter = 8;
                static readonly int[,] c_frac_iter_table =
                    { {40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40},
                      {40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 25, 11,  8},
                      {40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 28, 19, 12,  9,  8},
                      {40, 40, 40, 40, 40, 40, 40, 40, 40, 37, 31, 26, 21, 16, 13, 10,  8,  8},
                      {39, 39, 38, 37, 36, 34, 32, 29, 26, 23, 20, 17, 15, 12, 10,  9,  8,  8},
                      {27, 27, 26, 26, 25, 24, 22, 21, 19, 17, 15, 14, 12, 10,  9,  8,  8,  8},
                      {20, 20, 20, 19, 19, 18, 17, 16, 15, 14, 12, 11, 10,  9,  8,  8,  8,  8},
                      {16, 16, 16, 16, 15, 15, 14, 13, 12, 12, 11, 10,  9,  8,  8,  8,  8,  8},
                      {13, 13, 13, 13, 13, 12, 12, 11, 11, 10, 10,  9,  8,  8,  8,  8,  8,  8},
                      {12, 12, 11, 11, 11, 11, 10, 10, 10,  9,  9,  8,  8,  8,  8,  8,  8,  8},
                      {10, 10, 10, 10, 10, 10,  9,  9,  9,  8,  8,  8,  8,  8,  8,  8,  8,  8},
                      { 9,  9,  9,  9,  9,  9,  9,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8,  8} };

                public static int CFracIter(Complex z) {
#if DEBUG
                    if (!(ddouble.IsPositive(z.R) && ddouble.IsPositive(z.I))) {
                        throw new ArgumentOutOfRangeException(nameof(z));
                    }
#endif

                    int r_index = (int)double.Floor((double)z.R * 2);
                    int i_index = (int)double.Floor((double)z.I * 2);

                    if (r_index >= c_frac_iter_table.GetLength(0) || i_index >= c_frac_iter_table.GetLength(1)) {
                        return min_c_frac_iter;
                    }

                    int n = c_frac_iter_table[r_index, i_index];

                    return n;
                }

                public static bool UseCFrac(Complex z) {
#if DEBUG
                    if (!(ddouble.IsPositive(z.R) && ddouble.IsPositive(z.I))) {
                        throw new ArgumentOutOfRangeException(nameof(z));
                    }
#endif

                    double zid = (double)z.I;
                    double zr_thr = 2d - 7d / 256 * zid * zid;

                    return zr_thr <= z.R;
                }
            }
        }
    }
}

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

            if (ErfUtil.UseNeroZero(z)) {
                Complex c = ErfUtil.ErfNearZero(w);

                Complex y = c * z * (2 * ErfUtil.RcpSqrtPI);

                return y;
            }
            else {
                Complex c = z * Exp(-w) * ErfUtil.RcpSqrtPI;

                Complex f = ErfUtil.ErfcxCFrac(z, w);

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

            if (ddouble.IsNegative(z.R) || ErfUtil.UseNeroZero(z)) {
                return One - Erf(z);
            }
            else {
                Complex w = z * z;
                Complex c = z * Exp(-w) * ErfUtil.RcpSqrtPI;

                Complex f = ErfUtil.ErfcxCFrac(z, w);

                Complex y = c / f;

                return y;
            }
        }

        public static Complex Erfcx(Complex z) {
            if (!ddouble.IsFinite(z.R) || double.Abs((double)z.I) <= double.Abs((double)z.R) * 5e-31) {
                return ddouble.Erfcx(z.R);
            }
            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return Conjugate(Erfcx(Conjugate(z)));
            }

            Complex w = z * z;

            if (ErfUtil.UseNeroZero((ddouble.Abs(z.R), z.I))) {
                return (One - Erf(z)) * Exp(w);
            }
            else if (ddouble.IsPositive(z.R)) {
                Complex f = ErfUtil.ErfcxCFrac(z, w);

                Complex y = z * ErfUtil.RcpSqrtPI / f;

                return y;
            }
            else {
                Complex z_neg = (-z.R, z.I);

                Complex f = ErfUtil.ErfcxCFrac(z_neg, Conjugate(w));

                Complex erfcx = z_neg * ErfUtil.RcpSqrtPI / f;
                Complex erfc = erfcx * Exp((-w.R, w.I));

                if (IsFinite(erfc)) {
                    Complex y = Conjugate((2 / erfc - 1) * erfcx);

                    return y;
                }
                else {
                    Complex y = (-erfcx.R, erfcx.I);

                    return y;
                }
            }
        }


        internal static class ErfUtil {
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

            static int CFracIter(Complex z) {
#if DEBUG
                if (!(ddouble.IsPositive(z.R) && ddouble.IsPositive(z.I))) {
                    throw new ArgumentOutOfRangeException(nameof(z));
                }
#endif

                if (!(z.R < 6d && z.I < 9d)) {
                    return min_c_frac_iter;
                }

                int r_index = (int)double.Floor((double)z.R * 2);
                int i_index = (int)double.Floor((double)z.I * 2);

                if (r_index >= c_frac_iter_table.GetLength(0) || i_index >= c_frac_iter_table.GetLength(1)) {
                    return min_c_frac_iter;
                }

                int n = c_frac_iter_table[r_index, i_index];

                return n;
            }

            public static bool UseNeroZero(Complex z) {
#if DEBUG
                if (!(ddouble.IsPositive(z.R) && ddouble.IsPositive(z.I))) {
                    throw new ArgumentOutOfRangeException(nameof(z));
                }
#endif

                double zid = (double)z.I;
                double zr_thr = 2d - 7d / 256 * zid * zid;

                return zr_thr > z.R;
            }


            public static Complex ErfcxCFrac(Complex z, Complex w) {
                int n = CFracIter(z);

                Complex f = One;

                for (int k = 4 * n - 3; k >= 1; k -= 4) {
                    Complex c0 = (k + 2) * f;
                    Complex c1 = w * ((k + 3) + f * 2d);
                    Complex d0 = (k + 1) * (k + 3) + (4 * k + 6) * f;
                    Complex d1 = c1 * 2d;

                    f = w + k * (c0 + c1) / (d0 + d1);
                }

                return f;
            }

            public static Complex ErfNearZero(Complex w) {
                Complex c = One, u = -w;

                for (int k = 1, convergence_time = 0; k <= 196 && convergence_time < 4; k++) {
                    Complex dc = u / (2 * k + 1);
                    c += dc;

                    u *= w / -(k + 1);

                    if ((double.Abs((double)dc.R) <= double.Abs((double)c.R) * 1e-30) &&
                        (double.Abs((double)dc.I) <= double.Abs((double)c.I) * 1e-30)) {

                        convergence_time++;
                    }
                }

                return c;
            }
        }
    }
}

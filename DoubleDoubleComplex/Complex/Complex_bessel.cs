using DoubleDouble;
using DoubleDouble.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex BesselJ(ddouble nu, Complex x) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselJ(nu, x.R);
            }

            BesselUtil.CheckNu(nu);

            if (IsNaN(x)) {
                return NaN;
            }

            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselJ(nu, x);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                if (nu == 0d) {
                    return 1d;
                }
                if (BesselUtil.NearlyInteger(nu, out _) || nu > 0d) {
                    return 0d;
                }
                return ddouble.IsEvenInteger(nu) ? ddouble.NegativeInfinity : ddouble.PositiveInfinity;
            }
            if (x.Magnitude <= 40.5d) {
                return BesselMillerBackward.BesselJ(nu, x);
            }

            return BesselLimit.BesselJ(nu, x);
        }

        public static Complex BesselJ(int n, Complex x) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselJ(n, x.R);
            }

            BesselUtil.CheckN(n);

            if (IsNaN(x)) {
                return NaN;
            }

            if (ddouble.IsNegative(x.R)) {
                return ((n & 1) == 0) ? BesselJ(n, -x) : -BesselJ(n, -x);
            }
            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselJ(n, x);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                return (n == 0) ? 1d : 0d;
            }
            if (x.Magnitude <= 40.5d) {
                return BesselMillerBackward.BesselJ(n, x);
            }

            return BesselLimit.BesselJ(n, x);
        }

        public static Complex BesselY(ddouble nu, Complex x) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselY(nu, x.R);
            }

            BesselUtil.CheckNu(nu);

            if (IsNaN(x)) {
                return NaN;
            }

            if (nu < 0d && ddouble.Abs((nu - ddouble.Floor(nu)) - 0.5d) < 0.0625d) {
                if (x.R <= 4d - nu) {
                    Complex y = BesselNearZero.BesselY(nu, x);

                    if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                        return y;
                    }

                    if (nu - ddouble.Floor(nu) == 0.5d) {
                        return 0d;
                    }
                    int n = (int)(ddouble.Floor(nu + 0.5d));
                    return ((n & 1) == 0) ? ddouble.NegativeInfinity : ddouble.PositiveInfinity;
                }
            }

            if (x.Magnitude <= 2d) {
                Complex y;

                if (BesselUtil.NearlyInteger(nu, out _) || ddouble.Abs(ddouble.Round(nu) - nu) >= BesselUtil.InterpolationThreshold) {
                    y = BesselNearZero.BesselY(nu, x);
                }
                else {
                    y = BesselInterpolate.BesselYCubicInterpolate(nu, x);
                }

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                if (nu > 0d) {
                    return ddouble.NegativeInfinity;
                }
                if (nu - ddouble.Floor(nu) == 0.5d) {
                    return 0d;
                }
                int n = (int)(ddouble.Floor(nu + 0.5d));
                return ((n & 1) == 0) ? ddouble.NegativeInfinity : ddouble.PositiveInfinity;
            }

            if (x.Magnitude <= 40.5d) {
                if (!BesselUtil.NearlyInteger(nu, out _) && (ddouble.Ceiling(nu) - nu) < BesselUtil.InterpolationThreshold) {
                    return BesselInterpolate.BesselYCubicInterpolate(nu, x);
                }

                return BesselMillerBackward.BesselY(nu, x);
            }

            return BesselLimit.BesselY(nu, x);
        }

        public static Complex BesselY(int n, Complex x) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselY(n, x.R);
            }

            BesselUtil.CheckN(n);

            if (IsNaN(x)) {
                return NaN;
            }

            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselY(n, x);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                if (n > 0) {
                    return ddouble.NegativeInfinity;
                }
                return ((n & 1) == 0) ? ddouble.NegativeInfinity : ddouble.PositiveInfinity;
            }
            if (x.Magnitude <= 40.5d) {
                return BesselMillerBackward.BesselY(n, x);
            }

            return BesselLimit.BesselY(n, x);
        }

        public static Complex BesselI(ddouble nu, Complex x, bool scale = false) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselI(nu, x.R);
            }

            BesselUtil.CheckNu(nu);

            if (IsNaN(x)) {
                return NaN;
            }

            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselI(nu, x, scale);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                if (nu == 0d) {
                    return 1d;
                }
                if (BesselUtil.NearlyInteger(nu, out _) || nu > 0d) {
                    return 0d;
                }
                return ddouble.IsEvenInteger(nu) ? ddouble.NegativeInfinity : ddouble.PositiveInfinity;
            }
            if (x.Magnitude <= 40d) {
                return BesselMillerBackward.BesselI(nu, x, scale);
            }

            return BesselLimit.BesselI(nu, x, scale);
        }

        public static Complex BesselI(int n, Complex x, bool scale = false) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselI(n, x.R);
            }

            BesselUtil.CheckN(n);

            if (IsNaN(x)) {
                return NaN;
            }

            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselI(n, x, scale);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                return (n == 0) ? 1d : 0d;
            }
            if (x.Magnitude <= 40d) {
                return BesselMillerBackward.BesselI(n, x, scale);
            }

            return BesselLimit.BesselI(n, x, scale);
        }

        public static Complex BesselK(ddouble nu, Complex x, bool scale = false) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselK(nu, x.R);
            }

            BesselUtil.CheckNu(nu);

            if (IsNaN(x)) {
                return NaN;
            }

            nu = ddouble.Abs(nu);

            if (x.Magnitude <= 2d) {
                Complex y;

                if (BesselUtil.NearlyInteger(nu, out _) || ddouble.Abs(ddouble.Round(nu) - nu) >= BesselUtil.InterpolationThreshold) {
                    y = BesselNearZero.BesselK(nu, x, scale);
                }
                else {
                    y = BesselInterpolate.BesselKCubicInterpolate(nu, x, scale);
                }

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                return ddouble.PositiveInfinity;
            }

            if (x.Magnitude <= 35d) {
                return BesselYoshidaPade.BesselK(nu, x, scale);
            }

            return BesselLimit.BesselK(nu, x, scale);
        }

        public static Complex BesselK(int n, Complex x, bool scale = false) {
            if (ddouble.IsPositive(x.R) && double.Abs((double)x.I) <= double.Abs((double)x.R) * 5e-31) {
                return ddouble.BesselK(n, x.R);
            }

            BesselUtil.CheckN(n);

            if (IsNaN(x)) {
                return NaN;
            }

            n = int.Abs(n);

            if (x.Magnitude <= 2d) {
                Complex y = BesselNearZero.BesselK(n, x, scale);

                if (IsFinite(y) && !(IsZero(y) && x.Magnitude < BesselUtil.ExtremelyNearZero)) {
                    return y;
                }

                return ddouble.PositiveInfinity;
            }
            if (x.Magnitude <= 35d) {
                return BesselYoshidaPade.BesselK(n, x, scale);
            }

            return BesselLimit.BesselK(n, x, scale);
        }

        private static class BesselUtil {
            public static readonly double Eps = double.ScaleB(1, -1000);
            public static readonly double ExtremelyNearZero = double.ScaleB(1, -28);
            public static readonly double InterpolationThreshold = double.ScaleB(1, -25);
            public static readonly double MillerBwdBesselYEps = double.ScaleB(1, -30);

            public const int MaxN = 16;

            public static void CheckNu(ddouble nu) {
                if (!(ddouble.Abs(nu) <= (double)MaxN)) {
                    throw new ArgumentOutOfRangeException(
                        nameof(nu),
                        $"In the calculation of the Bessel function, nu with an absolute value greater than {MaxN} is not supported."
                    );
                }
            }

            public static void CheckN(int n) {
                if (n < -MaxN || n > MaxN) {
                    throw new ArgumentOutOfRangeException(
                        nameof(n),
                        $"In the calculation of the Bessel function, n with an absolute value greater than {MaxN} is not supported."
                    );
                }
            }

            public static bool NearlyInteger(ddouble nu, out int n) {
                n = (int)ddouble.Round(nu);

                return ddouble.Abs(nu - n) < Eps;
            }
        }

        private static class BesselNearZero {
            private static readonly Dictionary<ddouble, DoubleFactDenomTable> dfactdenom_coef_table = new();
            private static readonly Dictionary<ddouble, X2DenomTable> x2denom_coef_table = new();
            private static readonly Dictionary<ddouble, GammaDenomTable> gammadenom_coef_table = new();
            private static readonly Dictionary<ddouble, GammaTable> gamma_coef_table = new();
            private static readonly Dictionary<ddouble, GammaPNTable> gammapn_coef_table = new();
            private static readonly YCoefTable y_coef_table = new();
            private static readonly Y0CoefTable y0_coef_table = new();
            private static readonly Y1CoefTable y1_coef_table = new();
            private static readonly KCoefTable k_coef_table = new();
            private static readonly K0CoefTable k0_coef_table = new();
            private static readonly K1CoefTable k1_coef_table = new();

            public static Complex BesselJ(ddouble nu, Complex x) {
                if (ddouble.IsNegative(nu) && BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselJ(-nu, x);

                    return ((n & 1) == 0) ? y : -y;
                }
                else {
                    Complex y = BesselJIKernel(nu, x, sign_switch: true, terms: 9);

                    return y;
                }
            }

            public static Complex BesselY(ddouble nu, Complex x) {
                if (BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselYKernel(n, x, terms: 9);

                    return y;
                }
                else if (nu < 0d && ddouble.Abs((nu - ddouble.Floor(nu)) - 0.5d) < 0.0625d) {
                    Complex y = BesselYKernel(nu, x, terms: 32);

                    return y;
                }
                else {
                    Complex y = BesselYKernel(nu, x, terms: 10);

                    return y;
                }
            }

            public static Complex BesselI(ddouble nu, Complex x, bool scale = false) {
                if (ddouble.IsNegative(nu) && BesselUtil.NearlyInteger(nu, out _)) {
                    Complex y = BesselI(-nu, x);

                    if (scale) {
                        y *= Exp(-x);
                    }

                    return y;
                }
                else {
                    Complex y = BesselJIKernel(nu, x, sign_switch: false, terms: 10);

                    if (scale) {
                        y *= Exp(-x);
                    }

                    return y;
                }
            }

            public static Complex BesselK(ddouble nu, Complex x, bool scale = false) {
                if (BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselKKernel(n, x, terms: 20);

                    if (scale) {
                        y *= Exp(x);
                    }

                    return y;
                }
                else {
                    Complex y = BesselKKernel(nu, x, terms: 20);

                    if (scale) {
                        y *= Exp(x);
                    }

                    return y;
                }
            }

            private static Complex BesselJIKernel(ddouble nu, Complex x, bool sign_switch, int terms) {
                if (!dfactdenom_coef_table.TryGetValue(nu, out DoubleFactDenomTable dfactdenom_table)) {
                    dfactdenom_table = new DoubleFactDenomTable(nu);
                    dfactdenom_coef_table.Add(nu, dfactdenom_table);
                }
                if (!x2denom_coef_table.TryGetValue(nu, out X2DenomTable x2denom_table)) {
                    x2denom_table = new X2DenomTable(nu);
                    x2denom_coef_table.Add(nu, x2denom_table);
                }

                DoubleFactDenomTable r = dfactdenom_table;
                X2DenomTable d = x2denom_table;

                Complex x2 = x * x, x4 = x2 * x2;

                Complex c = 0d, u = Pow(Ldexp(x, -1), nu);

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex w = x2 * d[k];
                    Complex dc = u * r[k] * (sign_switch ? (1d - w) : (1d + w));

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x4;

                    if (!IsFinite(c)) {
                        break;
                    }
                }

                return c;
            }

            private static Complex BesselYKernel(ddouble nu, Complex x, int terms) {
                if (!gamma_coef_table.TryGetValue(nu, out GammaTable gamma_table)) {
                    gamma_table = new GammaTable(nu);
                    gamma_coef_table.Add(nu, gamma_table);
                }
                if (!gammapn_coef_table.TryGetValue(nu, out GammaPNTable gammapn_table)) {
                    gammapn_table = new GammaPNTable(nu);
                    gammapn_coef_table.Add(nu, gammapn_table);
                }

                YCoefTable r = y_coef_table;
                GammaTable g = gamma_table;
                GammaPNTable gpn = gammapn_table;

                ddouble cos = ddouble.CosPI(nu), sin = ddouble.SinPI(nu);
                Complex p = IsZero(cos) ? 0d : Pow(x, Ldexp(nu, 1)) * cos, s = Ldexp(Pow(Ldexp(x, 1), nu), 2);

                Complex x2 = x * x, x4 = x2 * x2;

                Complex c = 0d, u = 1d / sin;

                for (int k = 0, t = 1, conv_times = 0; k <= terms && conv_times < 2; k++, t += 2) {
                    Complex a = t * s * g[t], q = gpn[t];
                    Complex pa = p / a, qa = q / a;

                    Complex dc = u * r[k] * ((4 * t * nu) * (pa + qa) - (x2 - (4 * t * t)) * (pa - qa));

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x4;

                    if (!IsFinite(c)) {
                        break;
                    }
                }

                return c;
            }

            private static Complex BesselYKernel(int n, Complex x, int terms) {
                if (n < 0) {
                    Complex y = BesselYKernel(-n, x, terms);

                    return ((n & 1) == 0) ? y : -y;
                }
                else {
                    if (n == 0) {
                        return BesselY0Kernel(x, terms);
                    }
                    if (n == 1) {
                        return BesselY1Kernel(x, terms);
                    }
                }

                Complex v = 1d / x;
                Complex y0 = BesselY0Kernel(x, terms);
                Complex y1 = BesselY1Kernel(x, terms);

                int exp_sum = 0;

                for (int k = 1; k < n; k++) {
                    (int exp, (y0, y1)) = ddouble.AdjustScale(0, (y0, y1));
                    (y1, y0) = ((2 * k) * v * y1 - y0, y1);

                    exp_sum += exp;
                }

                y1 = Ldexp(y1, -exp_sum);

                return y1;
            }

            private static Complex BesselY0Kernel(Complex x, int terms) {
                if (!dfactdenom_coef_table.ContainsKey(0)) {
                    dfactdenom_coef_table.Add(0, new DoubleFactDenomTable(0));
                }
                if (!x2denom_coef_table.ContainsKey(0)) {
                    x2denom_coef_table.Add(0, new X2DenomTable(0));
                }

                DoubleFactDenomTable r = dfactdenom_coef_table[0];
                X2DenomTable d = x2denom_coef_table[0];
                Y0CoefTable q = y0_coef_table;

                Complex h = Log(Ldexp(x, -1)) + ddouble.EulerGamma;

                Complex x2 = x * x, x4 = x2 * x2;

                Complex c = 0d, u = Ldexp(ddouble.RcpPI, 1);

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex dc = u * r[k] * ((h - ddouble.HarmonicNumber(2 * k)) * (1d - x2 * d[k]) + x2 * q[k]);

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x4;
                }

                return c;
            }

            private static Complex BesselY1Kernel(Complex x, int terms) {
                if (!dfactdenom_coef_table.ContainsKey(1)) {
                    dfactdenom_coef_table.Add(1, new DoubleFactDenomTable(1));
                }
                if (!x2denom_coef_table.ContainsKey(1)) {
                    x2denom_coef_table.Add(1, new X2DenomTable(1));
                }

                DoubleFactDenomTable r = dfactdenom_coef_table[1];
                X2DenomTable d = x2denom_coef_table[1];
                Y1CoefTable q = y1_coef_table;

                Complex h = Ldexp(Log(Ldexp(x, -1)) + ddouble.EulerGamma, 1);

                Complex x2 = x * x, x4 = x2 * x2;

                Complex c = -2d / (x * ddouble.PI), u = x / Ldexp(ddouble.PI, 1);

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex dc = u * r[k] * ((h - ddouble.HarmonicNumber(2 * k) - ddouble.HarmonicNumber(2 * k + 1)) * (1d - x2 * d[k]) + x2 * q[k]);

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x4;
                }

                return c;
            }

            private static Complex BesselKKernel(ddouble nu, Complex x, int terms) {
                if (!gammadenom_coef_table.TryGetValue(nu, out GammaDenomTable gammadenomp_table)) {
                    gammadenomp_table = new GammaDenomTable(nu);
                    gammadenom_coef_table.Add(nu, gammadenomp_table);
                }
                if (!gammadenom_coef_table.TryGetValue(-nu, out GammaDenomTable gammadenomn_table)) {
                    gammadenomn_table = new GammaDenomTable(-nu);
                    gammadenom_coef_table.Add(-nu, gammadenomn_table);
                }

                KCoefTable r = k_coef_table;
                GammaDenomTable gp = gammadenomp_table, gn = gammadenomn_table;

                Complex tp = Pow(Ldexp(x, -1), nu), tn = 1d / tp;

                Complex x2 = x * x;

                Complex c = 0d, u = ddouble.PI / Ldexp(ddouble.SinPI(nu), 1);

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex dc = u * r[k] * (tn * gn[k] - tp * gp[k]);

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x2;

                    if (!IsFinite(c)) {
                        break;
                    }
                }

                return c;
            }

            private static Complex BesselKKernel(int n, Complex x, int terms) {
                if (n == 0) {
                    return BesselK0Kernel(x, terms);
                }
                if (n == 1) {
                    return BesselK1Kernel(x, terms);
                }

                Complex v = 1d / x;
                Complex y0 = BesselK0Kernel(x, terms);
                Complex y1 = BesselK1Kernel(x, terms);

                for (int k = 1; k < n; k++) {
                    (y1, y0) = ((2 * k) * v * y1 + y0, y1);
                }

                return y1;
            }

            private static Complex BesselK0Kernel(Complex x, int terms) {
                K0CoefTable r = k0_coef_table;
                Complex h = -Log(Ldexp(x, -1)) - ddouble.EulerGamma;

                Complex x2 = x * x;

                Complex c = 0d, u = 1d;

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex dc = u * r[k] * (h + ddouble.HarmonicNumber(k));

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x2;
                }

                return c;
            }

            private static Complex BesselK1Kernel(Complex x, int terms) {
                K1CoefTable r = k1_coef_table;
                Complex h = Log(Ldexp(x, -1)) + ddouble.EulerGamma;

                Complex x2 = x * x;

                Complex c = 1d / x, u = Ldexp(x, -1);

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex dc = u * r[k] * (h - Ldexp(ddouble.HarmonicNumber(k) + ddouble.HarmonicNumber(k + 1), -1));

                    Complex c_next = c + dc;

                    if (c == c_next || !IsFinite(c_next)) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    c = c_next;
                    u *= x2;
                }

                return c;
            }

            private class DoubleFactDenomTable {
                private ddouble c;
                private readonly ddouble nu;
                private readonly List<ddouble> table = new();

                public DoubleFactDenomTable(ddouble nu) {
                    this.c = ddouble.Gamma(nu + 1d);
                    this.nu = nu;
                    this.table.Add(ddouble.Rcp(c));
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= (nu + (2 * k)) * (nu + (2 * k - 1)) * (32 * k * (2 * k - 1));

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }

            private class X2DenomTable {
                private readonly ddouble nu;
                private readonly List<ddouble> table = new();

                public X2DenomTable(ddouble nu) {
                    ddouble a = ddouble.Rcp(4d * (nu + 1d));

                    this.nu = nu;
                    this.table.Add(a);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        ddouble a = ddouble.Rcp(4d * (2 * k + 1) * (2 * k + 1 + nu));

                        table.Add(a);
                    }

                    return table[n];
                }
            }

            private class GammaDenomTable {
                private ddouble c;
                private readonly ddouble nu;
                private readonly List<ddouble> table = new();

                public GammaDenomTable(ddouble nu) {
                    this.c = ddouble.Gamma(nu + 1d);
                    this.nu = nu;
                    this.table.Add(ddouble.Rcp(c));
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= nu + k;

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }

            private class GammaTable {
                private ddouble c;
                private readonly ddouble nu;
                private readonly List<ddouble> table = new();

                public GammaTable(ddouble nu) {
                    this.c = ddouble.Gamma(nu + 1d);
                    this.nu = nu;
                    this.table.Add(c);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= nu + k;

                        table.Add(c);
                    }

                    return table[n];
                }
            }

            private class GammaPNTable {
                private readonly ddouble r;
                private readonly GammaTable positive_table, negative_table;
                private readonly List<ddouble> table = new();

                public GammaPNTable(ddouble nu) {
                    this.r = ddouble.Pow(4, nu);
                    this.positive_table = new(nu);
                    this.negative_table = new(-nu);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        ddouble c = r * positive_table[k] / negative_table[k];

                        table.Add(c);
                    }

                    return table[n];
                }
            }

            private class YCoefTable {
                private ddouble c;
                private readonly List<ddouble> table = new();

                public YCoefTable() {
                    this.c = 1d;
                    this.table.Add(1d);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= (32 * k * (2 * k - 1));

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }

            private class Y0CoefTable {
                private readonly List<ddouble> table = new();

                public Y0CoefTable() {
                    this.table.Add(ddouble.Rcp(4));
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        ddouble c = ddouble.Rcp((4 * (2 * k + 1) * (2 * k + 1) * (2 * k + 1)));

                        table.Add(c);
                    }

                    return table[n];
                }
            }

            private class Y1CoefTable {
                private readonly List<ddouble> table = new();

                public Y1CoefTable() {
                    this.table.Add(ddouble.Ldexp(3, -4));
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        ddouble c = (ddouble)(4 * k + 3) / (ddouble)(4 * (2 * k + 1) * (2 * k + 1) * (2 * k + 2) * (2 * k + 2));

                        table.Add(c);
                    }

                    return table[n];
                }
            }

            private class KCoefTable {
                private ddouble c;
                private readonly List<ddouble> table = new();

                public KCoefTable() {
                    this.c = 1d;
                    this.table.Add(1d);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= (4 * k);

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }

            private class K0CoefTable {
                private ddouble c;
                private readonly List<ddouble> table = new();

                public K0CoefTable() {
                    this.c = 1d;
                    this.table.Add(1d);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= (4 * k * k);

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }

            private class K1CoefTable {
                private ddouble c;
                private readonly List<ddouble> table = new();

                public K1CoefTable() {
                    this.c = 1d;
                    this.table.Add(1d);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        c *= (4 * k * (k + 1));

                        table.Add(ddouble.Rcp(c));
                    }

                    return table[n];
                }
            }
        }

        private static class BesselMillerBackward {
            private static readonly Dictionary<ddouble, BesselJPhiTable> phi_coef_table = new();
            private static readonly Dictionary<ddouble, BesselIPsiTable> psi_coef_table = new();
            private static readonly Dictionary<ddouble, BesselYEtaTable> eta_coef_table = new();
            private static readonly Dictionary<ddouble, BesselYXiTable> xi_coef_table = new();

            public static Complex BesselJ(int n, Complex x) {
                int m = BesselJYIterM((double)x.Magnitude);

                Complex y = BesselJKernel(n, x, m);

                return y;
            }

            public static Complex BesselJ(ddouble nu, Complex x) {
                int m = BesselJYIterM((double)x.Magnitude);

                if (BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselJKernel(n, x, m);

                    return y;
                }
                else {
                    Complex y = BesselJKernel(nu, x, m);

                    return y;
                }
            }

            public static Complex BesselY(int n, Complex x) {
                int m = BesselJYIterM((double)x.Magnitude);

                Complex y = BesselYKernel(n, x, m);

                return y;
            }

            public static Complex BesselY(ddouble nu, Complex x) {
                int m = BesselJYIterM((double)x.Magnitude);

                if (BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselYKernel(n, x, m);

                    return y;
                }
                else {
                    Complex y = BesselYKernel(nu, x, m);

                    return y;
                }
            }

            private static int BesselJYIterM(double x) {
                return (int)double.Ceiling(74 + 1.36 * x - 54.25 / double.Sqrt(double.Sqrt(x))) & ~1;
            }

            public static Complex BesselI(int n, Complex x, bool scale = false) {
                int m = BesselIIterM((double)x.Magnitude);

                Complex y = BesselIKernel(n, x, m, scale);

                return y;
            }

            public static Complex BesselI(ddouble nu, Complex x, bool scale = false) {
                int m = BesselIIterM((double)x.Magnitude);

                if (BesselUtil.NearlyInteger(nu, out int n)) {
                    Complex y = BesselIKernel(n, x, m, scale);

                    return y;
                }
                else {
                    Complex y = BesselIKernel(nu, x, m, scale);

                    return y;
                }
            }

            private static int BesselIIterM(double x) {
                return (int)double.Ceiling(86 + 0.75 * x - 67.25 / double.Sqrt(double.Sqrt(x))) & ~1;
            }

            private static Complex BesselJKernel(int n, Complex x, int m) {
                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (n < 0) {
                    return ((n & 1) == 0) ? BesselJKernel(-n, x, m) : -BesselJKernel(-n, x, m);
                }
                if (n == 0) {
                    return BesselJ0Kernel(x, m);
                }
                if (n == 1) {
                    return BesselJ1Kernel(x, m);
                }

                Complex f0 = 1e-256, f1 = 0d, fn = 0d, lambda = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);

                    if (k - 1 == n) {
                        fn = f0;
                    }
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex yn = fn / lambda;

                return yn;
            }

            private static Complex BesselJKernel(ddouble nu, Complex x, int m) {
                int n = (int)ddouble.Floor(nu);
                ddouble alpha = nu - n;

                if (alpha == 0d) {
                    return BesselJKernel(n, x, m);
                }

                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (!phi_coef_table.TryGetValue(alpha, out BesselJPhiTable phi_table)) {
                    phi_table = new BesselJPhiTable(alpha);
                    phi_coef_table.Add(alpha, phi_table);
                }

                BesselJPhiTable phi = phi_table;

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                if (n >= 0) {
                    Complex fn = 0d;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0 * phi[k / 2];
                        }

                        (f0, f1) = (Ldexp(k + alpha, 1) * v * f0 - f1, f0);

                        if (k - 1 == n) {
                            fn = f0;
                        }
                    }

                    lambda += f0 * phi[0];
                    lambda *= Pow(Ldexp(v, 1), alpha);

                    Complex yn = fn / lambda;

                    return yn;
                }
                else {
                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0 * phi[k / 2];
                        }

                        (f0, f1) = (Ldexp(k + alpha, 1) * v * f0 - f1, f0);
                    }

                    lambda += f0 * phi[0];
                    lambda *= Pow(Ldexp(v, 1), alpha);

                    for (int k = 0; k > n; k--) {
                        (f0, f1) = (Ldexp(k + alpha, 1) * v * f0 - f1, f0);
                    }

                    Complex yn = f0 / lambda;

                    return yn;
                }
            }

            private static Complex BesselJ0Kernel(Complex x, int m) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex y0 = f0 / lambda;

                return y0;
            }

            private static Complex BesselJ1Kernel(Complex x, int m) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex y1 = f1 / lambda;

                return y1;
            }

            private static Complex BesselYKernel(int n, Complex x, int m) {
                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (n < 0) {
                    return ((n & 1) == 0) ? BesselYKernel(-n, x, m) : -BesselYKernel(-n, x, m);
                }
                if (n == 0) {
                    return BesselY0Kernel(x, m);
                }
                if (n == 1) {
                    return BesselY1Kernel(x, m);
                }

                if (!eta_coef_table.ContainsKey(0)) {
                    eta_coef_table.Add(0, new BesselYEtaTable(0));
                }

                BesselYEtaTable eta = eta_coef_table[0];

                if (!xi_coef_table.ContainsKey(0)) {
                    xi_coef_table.Add(0, new BesselYXiTable(0, eta));
                }

                BesselYXiTable xi = xi_coef_table[0];

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex se = 0d, sx = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;

                        se += f0 * eta[k / 2];
                    }
                    else if (k >= 3) {
                        sx += f0 * xi[k];
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex c = Log(Ldexp(x, -1)) + ddouble.EulerGamma;

                Complex y0 = se + f0 * c;
                Complex y1 = sx - v * f0 + (c - 1d) * f1;

                for (int k = 1; k < n; k++) {
                    (y1, y0) = ((2 * k) * v * y1 - y0, y1);
                }

                Complex yn = Ldexp(y1 / (lambda * ddouble.PI), 1);

                return yn;
            }

            private static Complex BesselYKernel(ddouble nu, Complex x, int m) {
                int n = (int)ddouble.Floor(nu);
                ddouble alpha = nu - n;

                if (alpha == 0d) {
                    return BesselYKernel(n, x, m);
                }

                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (!eta_coef_table.TryGetValue(alpha, out BesselYEtaTable eta_table)) {
                    eta_table = new BesselYEtaTable(alpha);
                    eta_coef_table.Add(alpha, eta_table);
                }

                BesselYEtaTable eta = eta_table;

                if (!xi_coef_table.TryGetValue(alpha, out BesselYXiTable xi_table)) {
                    xi_table = new BesselYXiTable(alpha, eta);
                    xi_coef_table.Add(alpha, xi_table);
                }

                BesselYXiTable xi = xi_table;

                if (!phi_coef_table.TryGetValue(alpha, out BesselJPhiTable phi_table)) {
                    phi_table = new BesselJPhiTable(alpha);
                    phi_coef_table.Add(alpha, phi_table);
                }

                BesselJPhiTable phi = phi_table;

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex se = 0d, sxo = 0d, sxe = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0 * phi[k / 2];

                        se += f0 * eta[k / 2];
                        sxe += f0 * xi[k];
                    }
                    else if (k >= 3) {
                        sxo += f0 * xi[k];
                    }

                    (f0, f1) = (Ldexp(k + alpha, 1) * v * f0 - f1, f0);
                }

                Complex s = Pow(Ldexp(v, 1), alpha), sqs = s * s;

                lambda += f0 * phi[0];
                lambda *= s;

                ddouble rcot = 1d / ddouble.TanPI(alpha), rgamma = ddouble.Gamma(1d + alpha), rsqgamma = rgamma * rgamma;
                Complex r = Ldexp(ddouble.RcpPI * sqs, 1);
                Complex p = sqs * rsqgamma * ddouble.RcpPI;

                Complex eta0 = (ddouble.Abs(alpha) > BesselUtil.MillerBwdBesselYEps)
                    ? (rcot - p / alpha)
                    : BesselYEta0Eps(alpha, x);

                Complex xi0 = -Ldexp(v, 1) * p;
                Complex xi1 = (ddouble.Abs(alpha) > BesselUtil.MillerBwdBesselYEps)
                    ? rcot + p * (alpha * (alpha + 1d) + 1d) / (alpha * (alpha - 1d))
                    : BesselYXi1Eps(alpha, x);

                Complex y0 = r * se + eta0 * f0;
                Complex y1 = r * (3d * alpha * v * sxe + sxo) + xi0 * f0 + xi1 * f1;

                if (n == 0) {
                    Complex yn = y0 / lambda;

                    return yn;
                }
                if (n == 1) {
                    Complex yn = y1 / lambda;

                    return yn;
                }
                if (n >= 0) {
                    for (int k = 1; k < n; k++) {
                        (y1, y0) = (Ldexp(k + alpha, 1) * v * y1 - y0, y1);
                    }

                    Complex yn = y1 / lambda;

                    return yn;
                }
                else {
                    for (int k = 0; k > n; k--) {
                        (y0, y1) = (Ldexp(k + alpha, 1) * v * y0 - y1, y0);
                    }

                    Complex yn = y0 / lambda;

                    return yn;
                }
            }

            private static Complex BesselYEta0Eps(ddouble alpha, Complex x) {
                Complex lnx = Log(x), lnhalfx = Log(Ldexp(x, -1));
                ddouble pi = ddouble.PI, sqpi = pi * pi;
                ddouble ln2 = ddouble.Ln2, sqln2 = ln2 * ln2, cbln2 = sqln2 * ln2, qdln2 = sqln2 * sqln2;
                ddouble g = ddouble.EulerGamma;

                Complex r0 = lnhalfx + g;
                Complex r1 =
                    (-sqln2 + lnx * (ln2 * 2d - lnx)) * 4d
                    - sqpi
                    - g * (lnhalfx * 2d + g) * 4d;
                Complex r2 =
                    (-cbln2 + lnx * (sqln2 * 3d + lnx * (ln2 * -3d + lnx))) * 4d
                    + ddouble.Zeta3 * 2d
                    + sqpi * (lnhalfx + g)
                    + g * ((sqln2 + lnx * (ln2 * -2d + lnx)) * 3d + g * (lnhalfx * 3d + g)) * 4d;
                Complex r3 =
                    (-qdln2 + lnx * (cbln2 * 4d + lnx * (sqln2 * -6d + lnx * (ln2 * 4d - lnx)))) * 16d
                    - ddouble.Zeta3 * (lnhalfx + g) * 32d
                    - sqpi * (((sqln2 + lnx * (-ln2 * 2d + lnx) + g * (lnhalfx * 2d + g)) * 8d) + sqpi)
                    + g * ((cbln2 + lnx * (sqln2 * -3d + lnx * (ln2 * 3d - lnx))) * 4d
                    + g * ((sqln2 + lnx * (ln2 * -2d + lnx)) * -6d
                    + g * (lnhalfx * -4d
                    - g))) * 16d;

                Complex eta0 = (r0 * 48d + alpha * (r1 * 12d + alpha * (r2 * 8d + alpha * r3))) / (24d * ddouble.PI);

                return eta0;
            }

            static Complex BesselYXi1Eps(ddouble alpha, Complex x) {
                Complex lnx = Log(x), lnhalfx = Log(Ldexp(x, -1)), lnxm1 = lnx - 1, lnhalfxm1 = lnhalfx - 1;
                ddouble pi = ddouble.PI, sqpi = pi * pi;
                ddouble ln2 = ddouble.Ln2, sqln2 = ln2 * ln2, cbln2 = sqln2 * ln2, qdln2 = sqln2 * sqln2;
                ddouble g = ddouble.EulerGamma;

                Complex r0 = lnhalfxm1 + g;
                Complex r1 =
                    (-sqln2 + ln2 * lnxm1 * 2d + lnx * (2 - lnx)) * 4d
                    - sqpi
                    - g * (lnhalfxm1 * 2d + g) * 4d
                    - 6d;
                Complex r2 =
                    (-cbln2 * 4d + sqln2 * lnxm1 * 12d + lnx * (18d + lnx * (-12d + lnx * 4d)))
                    + ln2 * (lnx * (2d - lnx) * 12d - 18d)
                    + ddouble.Zeta3 * 2d
                    + sqpi * (lnhalfxm1 + g)
                    + g * ((((sqln2 - ln2 * lnxm1 * 2d) + lnx * (-2d + lnx)) * 12d + 18d)
                    + g * (lnhalfxm1 * 12d
                    + g * 4d))
                    - 9d;
                Complex r3 =
                    -qdln2 * 16d
                    + cbln2 * lnxm1 * 64d
                    + sqln2 * (lnx * (2d - lnx) * 96d - 144d)
                    + ln2 * (lnx * (9d + lnx * (-6d + lnx * 2d)) * 32d - 144d)
                    + lnx * (9d + lnx * (-9d + lnx * (4d - lnx))) * 16d
                    + ddouble.Zeta3 * (lnhalfxm1 + g) * -32d
                    + sqpi * (((-sqln2 + ln2 * lnxm1 * 2d + lnx * (2d - lnx) - g * (lnhalfxm1 * 2d + g)) * 8d - 12d) - sqpi)
                    + g * (((cbln2 - sqln2 * lnxm1 * 3d) * 64d + ln2 * (lnx * (-2d + lnx) * 192d + 288d) + lnx * (-9d + lnx * (6d - lnx * 2d)) * 32d + 144d)
                    + g * (((-sqln2 + ln2 * lnxm1 * 2d + lnx * (2d - lnx)) * 96d - 144d)
                    + g * (lnhalfxm1 * -64d
                    - g * 16d)))
                    - 72d;

                Complex xi1 = (r0 * 48d + alpha * (r1 * 12d + alpha * (r2 * 8d + alpha * r3))) / (24d * ddouble.PI);

                return xi1;
            }

            private static Complex BesselY0Kernel(Complex x, int m) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (!eta_coef_table.TryGetValue(0, out BesselYEtaTable eta_table)) {
                    eta_table = new BesselYEtaTable(0);
                    eta_coef_table.Add(0, eta_table);
                }

                BesselYEtaTable eta = eta_table;

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex se = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;

                        se += f0 * eta[k / 2];
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex y0 = Ldexp((se + f0 * (Log(Ldexp(x, -1)) + ddouble.EulerGamma)) / (ddouble.PI * lambda), 1);

                return y0;
            }

            private static Complex BesselY1Kernel(Complex x, int m) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (!xi_coef_table.ContainsKey(0)) {
                    if (!eta_coef_table.ContainsKey(0)) {
                        eta_coef_table.Add(0, new BesselYEtaTable(0));
                    }

                    xi_coef_table.Add(0, new BesselYXiTable(0, eta_coef_table[0]));
                }

                BesselYXiTable xi = xi_coef_table[0];

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                Complex sx = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    if ((k & 1) == 0) {
                        lambda += f0;
                    }
                    else if (k >= 3) {
                        sx += f0 * xi[k];
                    }

                    (f0, f1) = ((2 * k) * v * f0 - f1, f0);
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex y1 = Ldexp((sx - v * f0 + (Log(Ldexp(x, -1)) + ddouble.EulerGamma - 1d) * f1) / (lambda * ddouble.PI), 1);

                return y1;
            }

            private static Complex BesselIKernel(int n, Complex x, int m, bool scale = false) {
                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                n = int.Abs(n);

                if (n == 0) {
                    return BesselI0Kernel(x, m, scale);
                }
                if (n == 1) {
                    return BesselI1Kernel(x, m, scale);
                }

                Complex f0 = 1e-256, f1 = 0d, lambda = 0d, fn = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    lambda += f0;

                    (f0, f1) = ((2 * k) * v * f0 + f1, f0);

                    if (k - 1 == n) {
                        fn = f0;
                    }
                }

                lambda = Ldexp(lambda, 1) + f0;

                Complex yn = fn / lambda;

                if (!scale) {
                    yn *= Exp(x);
                }

                return yn;
            }

            private static Complex BesselIKernel(ddouble nu, Complex x, int m, bool scale = false) {
                int n = (int)ddouble.Floor(nu);
                ddouble alpha = nu - n;

                if (alpha == 0d) {
                    return BesselIKernel(n, x, m, scale);
                }

                if (m < 2 || (m & 1) != 0 || n >= m) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                if (!psi_coef_table.TryGetValue(alpha, out BesselIPsiTable psi_table)) {
                    psi_table = new BesselIPsiTable(alpha);
                    psi_coef_table.Add(alpha, psi_table);
                }

                BesselIPsiTable psi = psi_table;

                Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                if (n >= 0) {
                    Complex gn = 0d;

                    for (int k = m; k >= 1; k--) {
                        lambda += g0 * psi[k];

                        (g0, g1) = (Ldexp(k + alpha, 1) * v * g0 + g1, g0);

                        if (k - 1 == n) {
                            gn = g0;
                        }
                    }

                    lambda += g0 * psi[0];
                    lambda *= Pow(Ldexp(v, 1), alpha);

                    Complex yn = gn / lambda;

                    if (!scale) {
                        yn *= Exp(x);
                    }

                    return yn;
                }
                else {
                    for (int k = m; k >= 1; k--) {
                        lambda += g0 * psi[k];

                        (g0, g1) = (Ldexp(k + alpha, 1) * v * g0 + g1, g0);
                    }

                    lambda += g0 * psi[0];
                    lambda *= Pow(Ldexp(v, 1), alpha);

                    for (int k = 0; k > n; k--) {
                        (g0, g1) = (Ldexp(k + alpha, 1) * v * g0 + g1, g0);
                    }

                    Complex yn = g0 / lambda;

                    if (!scale) {
                        yn *= Exp(x);
                    }

                    return yn;
                }
            }

            private static Complex BesselI0Kernel(Complex x, int m, bool scale = false) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    lambda += g0;

                    (g0, g1) = ((2 * k) * v * g0 + g1, g0);
                }

                lambda = Ldexp(lambda, 1) + g0;

                Complex y0 = g0 / lambda;

                if (!scale) {
                    y0 *= Exp(x);
                }

                return y0;
            }

            private static Complex BesselI1Kernel(Complex x, int m, bool scale = false) {
                if (m < 2 || (m & 1) != 0) {
                    throw new ArgumentOutOfRangeException(nameof(m));
                }

                Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                Complex v = 1d / x;

                for (int k = m; k >= 1; k--) {
                    lambda += g0;

                    (g0, g1) = ((2 * k) * v * g0 + g1, g0);
                }

                lambda = Ldexp(lambda, 1) + g0;

                Complex y1 = g1 / lambda;

                if (!scale) {
                    y1 *= Exp(x);
                }

                return y1;
            }

            private class BesselJPhiTable {
                private readonly ddouble alpha;
                private readonly List<ddouble> table = new();

                private ddouble g;

                public BesselJPhiTable(ddouble alpha) {
                    Debug.Assert((alpha > 0d && alpha < 1d), nameof(alpha));

                    this.alpha = alpha;

                    ddouble phi0 = ddouble.Gamma(1 + alpha);
                    ddouble phi1 = phi0 * (alpha + 2d);

                    this.g = phi0;

                    this.table.Add(phi0);
                    this.table.Add(phi1);
                }

                public ddouble this[int n] => Value(n);

                private ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int m = table.Count; m <= n; m++) {
                        g = g * (alpha + m - 1d) / m;

                        ddouble phi = g * (alpha + 2 * m);

                        table.Add(phi);
                    }

                    return table[n];
                }
            };

            private class BesselIPsiTable {
                private readonly ddouble alpha;
                private readonly List<ddouble> table = new();

                private ddouble g;

                public BesselIPsiTable(ddouble alpha) {
                    Debug.Assert((alpha > 0d && alpha < 1d), nameof(alpha));

                    this.alpha = alpha;

                    ddouble psi0 = ddouble.Gamma(1d + alpha);
                    ddouble psi1 = ddouble.Ldexp(psi0, 1) * (1d + alpha);

                    this.g = ddouble.Ldexp(psi0, 1);

                    this.table.Add(psi0);
                    this.table.Add(psi1);
                }

                public ddouble this[int n] => Value(n);

                private ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int m = table.Count; m <= n; m++) {
                        g = g * (ddouble.Ldexp(alpha, 1) + m - 1d) / m;

                        ddouble phi = g * (alpha + m);

                        table.Add(phi);
                    }

                    return table[n];
                }
            };

            private class BesselYEtaTable {
                private readonly ddouble alpha;
                private readonly List<ddouble> table = new();

                private ddouble g;

                public BesselYEtaTable(ddouble alpha) {
                    Debug.Assert((alpha >= 0d && alpha < 1d), nameof(alpha));

                    this.alpha = alpha;
                    this.table.Add(ddouble.NaN);

                    if (alpha > 0d) {
                        ddouble c = ddouble.Gamma(1d + alpha);
                        c *= c;
                        this.g = 1d / (1d - alpha) * c;

                        ddouble eta1 = (alpha + 2d) * g;

                        this.table.Add(eta1);
                    }
                }

                public ddouble this[int n] => Value(n);

                private ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int m = table.Count; m <= n; m++) {
                        if (alpha > 0d) {
                            g = -g * (alpha + m - 1) * (ddouble.Ldexp(alpha, 1) + m - 1d) / (m * (m - alpha));

                            ddouble eta = g * (alpha + 2 * m);

                            table.Add(eta);
                        }
                        else {
                            ddouble eta = (ddouble)2d / m;

                            table.Add(((m & 1) == 1) ? eta : -eta);
                        }
                    }

                    return table[n];
                }
            };

            private class BesselYXiTable {
                private readonly ddouble alpha;
                private readonly List<ddouble> table = new();
                private readonly BesselYEtaTable eta;

                public BesselYXiTable(ddouble alpha, BesselYEtaTable eta) {
                    Debug.Assert((alpha >= 0d && alpha < 1d), nameof(alpha));

                    this.alpha = alpha;
                    this.table.Add(ddouble.NaN);
                    this.table.Add(ddouble.NaN);

                    this.eta = eta;
                }

                public ddouble this[int n] => Value(n);

                private ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int m = table.Count; m <= n; m++) {
                        if (alpha > 0d) {
                            if ((m & 1) == 0) {
                                table.Add(eta[m / 2]);
                            }
                            else {
                                table.Add((eta[m / 2] - eta[m / 2 + 1]) / 2);
                            }
                        }
                        else {
                            if ((m & 1) == 1) {
                                ddouble xi = (ddouble)(2 * (m / 2) + 1) / ((m / 2) * ((m / 2) + 1));
                                table.Add(((m & 2) > 0) ? xi : -xi);
                            }
                            else {
                                table.Add(ddouble.NaN);
                            }
                        }
                    }

                    return table[n];
                }
            };
        }

        private static class BesselYoshidaPade {
            private static readonly ReadOnlyCollection<ReadOnlyCollection<ddouble>> ess_coef_table;
            private static readonly Dictionary<ddouble, ReadOnlyCollection<(ddouble c, ddouble s)>> cds_coef_table = new();

            static BesselYoshidaPade() {
                Dictionary<string, ReadOnlyCollection<ddouble>> tables =
                    ResourceUnpack.NumTable(Resource.BesselKTable);

                ReadOnlyCollection<ddouble> cs0 = tables["CS0Table"], cs1 = tables["CS1Table"];
                ReadOnlyCollection<ddouble> ds0 = tables["DS0Table"], ds1 = tables["DS1Table"];

                if (cs0.Count != ds0.Count || cs1.Count != ds1.Count) {
                    throw new IOException("The format of resource file is invalid.");
                }

                List<(ddouble c, ddouble s)> cd0 = new(), cd1 = new();

                for (int i = 0; i < cs0.Count; i++) {
                    cd0.Add((cs0[i], ds0[i]));
                }
                for (int i = 0; i < cs1.Count; i++) {
                    cd1.Add((cs1[i], ds1[i]));
                }

                cd0.Reverse();
                cd1.Reverse();

                cds_coef_table.Add(0, Array.AsReadOnly(cd0.ToArray()));
                cds_coef_table.Add(1, Array.AsReadOnly(cd1.ToArray()));

                List<ReadOnlyCollection<ddouble>> es = new();

                for (int i = 0; i < 32; i++) {
                    es.Add(tables[$"ES{i}Table"]);
                }

                ess_coef_table = Array.AsReadOnly(es.ToArray());
            }

            public static Complex BesselK(ddouble nu, Complex x, bool scale = false) {
                if (nu < 2d) {
                    if (!cds_coef_table.TryGetValue(nu, out ReadOnlyCollection<(ddouble c, ddouble s)> cds_table)) {
                        cds_table = Table(nu);
                        cds_coef_table.Add(nu, cds_table);
                    }

                    ReadOnlyCollection<(ddouble, ddouble)> cds = cds_table;

                    Complex y = Value(x, cds, scale);

                    return y;
                }
                else {
                    int n = (int)ddouble.Floor(nu);
                    ddouble alpha = nu - n;

                    Complex y0 = BesselK(alpha, x, scale);
                    Complex y1 = BesselK(alpha + 1d, x, scale);

                    Complex v = 1d / x;

                    for (int k = 1; k < n; k++) {
                        (y1, y0) = (Ldexp(k + alpha, 1) * v * y1 + y0, y1);
                    }

                    return y1;
                }
            }

            private static Complex Value(Complex x, ReadOnlyCollection<(ddouble c, ddouble d)> cds, bool scale = false) {
                Complex t = 1d / x;
                (Complex sc, Complex sd) = cds[0];

                for (int i = 1; i < cds.Count; i++) {
                    (ddouble c, ddouble d) = cds[i];

                    sc = sc * t + c;
                    sd = sd * t + d;
                }

                Debug.Assert(sd.Magnitude > 0.0625d, $"[BesselK x={x}] Too small pade denom!!");

                Complex y = Sqrt(Ldexp(t * ddouble.PI, -1)) * sc / sd;

                if (!scale) {
                    y *= Exp(-x);
                }

                return y;
            }

            private static ReadOnlyCollection<(ddouble c, ddouble d)> Table(ddouble nu) {
                int m = ess_coef_table.Count - 1;

                ddouble squa_nu = nu * nu;
                List<(ddouble c, ddouble d)> cds = new();
                ddouble[] us = new ddouble[m + 1], vs = new ddouble[m];

                ddouble u = 1d;
                for (int i = 0; i <= m; i++) {
                    us[i] = u;
                    u *= squa_nu;
                }
                for (int i = 0; i < m; i++) {
                    ddouble r = m - i + 0.5d;
                    vs[i] = r * r - squa_nu;
                }

                for (int i = 0; i <= m; i++) {
                    ReadOnlyCollection<ddouble> es = ess_coef_table[i];
                    ddouble d = es[i], c = 0d;

                    for (int l = 0; l < i; l++) {
                        d *= vs[l];
                    }
                    for (int j = 0; j <= i; j++) {
                        c += es[j] * us[j];
                    }

                    cds.Add((c, d));
                }

                cds.Reverse();

                return Array.AsReadOnly(cds.ToArray());
            }
        }

        private static class BesselLimit {
            private static readonly Dictionary<ddouble, ACoefTable> a_coef_table = new();
            private static readonly Dictionary<ddouble, JYCoefTable> jy_coef_table = new();
            private static readonly Dictionary<ddouble, IKCoefTable> ik_coef_table = new();

            public static Complex BesselJ(ddouble nu, Complex x) {
                if (ddouble.IsInfinity(x.R)) {
                    return Zero;
                }

                (Complex c, Complex s) = BesselJYKernel(nu, x, terms: 32);

                Complex omega = x - Ldexp((Ldexp(nu, 1) + 1d) * ddouble.PI, -2);
                Complex m = c * Cos(omega) - s * Sin(omega);
                Complex t = m * Sqrt(2d / (ddouble.PI * x));

                return t;
            }

            public static Complex BesselY(ddouble nu, Complex x) {
                if (ddouble.IsInfinity(x.R)) {
                    return Zero;
                }

                (Complex s, Complex c) = BesselJYKernel(nu, x, terms: 32);

                Complex omega = x - Ldexp((Ldexp(nu, 1) + 1d) * ddouble.PI, -2);
                Complex m = s * Sin(omega) + c * Cos(omega);
                Complex t = m * Sqrt(2d / (ddouble.PI * x));

                return t;
            }

            public static Complex BesselI(ddouble nu, Complex x, bool scale = false) {
                if (ddouble.IsInfinity(x.R)) {
                    return scale ? ddouble.PlusZero : ddouble.PositiveInfinity;
                }

                Complex c = BesselIKKernel(nu, x, sign_switch: true, terms: 36);

                Complex t = c / Sqrt(Ldexp(ddouble.PI, 1) * x);

                if (!scale) {
                    t *= Exp(x);

                    if (IsNaN(t)) {
                        return ddouble.PositiveInfinity;
                    }
                }

                return t;
            }

            public static Complex BesselK(ddouble nu, Complex x, bool scale = false) {
                if (ddouble.IsInfinity(x.R)) {
                    return Zero;
                }

                Complex c = BesselIKKernel(nu, x, sign_switch: false, terms: 34);

                Complex t = c * Sqrt(ddouble.PI / Ldexp(x, 1));

                if (!scale) {
                    t *= Exp(-x);
                }

                return t;
            }

            private static (Complex s, Complex t) BesselJYKernel(ddouble nu, Complex x, int terms = 64) {
                if (!a_coef_table.TryGetValue(nu, out ACoefTable a_table)) {
                    a_table = new ACoefTable(nu);
                    a_coef_table.Add(nu, a_table);
                }
                if (!jy_coef_table.TryGetValue(nu, out JYCoefTable jy_table)) {
                    jy_table = new JYCoefTable(nu);
                    jy_coef_table.Add(nu, jy_table);
                }

                ACoefTable a = a_table;
                JYCoefTable c = jy_table;

                Complex v = 1d / x, v2 = v * v, v4 = v2 * v2;
                Complex s = 0d, t = 0d, p = 1d, q = v;

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex ds = p * a[k * 4] * (1d - v2 * c[k].p0);
                    Complex dt = q * a[k * 4 + 1] * (1d - v2 * c[k].p1);

                    Complex s_next = s + ds;
                    Complex t_next = t + dt;

                    if (s == s_next && t == t_next) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    p *= v4;
                    q *= v4;
                    s = s_next;
                    t = t_next;
                }

                return (s, t);
            }

            private static Complex BesselIKKernel(ddouble nu, Complex x, bool sign_switch, int terms) {
                if (!a_coef_table.TryGetValue(nu, out ACoefTable a_table)) {
                    a_table = new ACoefTable(nu);
                    a_coef_table.Add(nu, a_table);
                }
                if (!ik_coef_table.TryGetValue(nu, out IKCoefTable ik_table)) {
                    ik_table = new IKCoefTable(nu);
                    ik_coef_table.Add(nu, ik_table);
                }

                ACoefTable a = a_table;
                IKCoefTable c = ik_table;

                Complex v = 1d / x, v2 = v * v;
                Complex r = 0d, u = 1d;

                for (int k = 0, conv_times = 0; k <= terms && conv_times < 2; k++) {
                    Complex w = v * c[k];
                    Complex dr = u * a[k * 2] * (sign_switch ? (1d - w) : (1d + w));

                    Complex r_next = r + dr;

                    if (r == r_next) {
                        conv_times++;
                    }
                    else {
                        conv_times = 0;
                    }

                    r = r_next;
                    u *= v2;
                }

                return r;
            }

            private class ACoefTable {
                private readonly ddouble squa_nu4;
                private readonly List<ddouble> table = new();

                public ACoefTable(ddouble nu) {
                    this.squa_nu4 = ddouble.Ldexp(nu * nu, 2);

                    ddouble a1 = ddouble.Ldexp(squa_nu4 - 1d, -3);

                    this.table.Add(1d);
                    this.table.Add(a1);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    for (int k = table.Count; k <= n; k++) {
                        ddouble a = table.Last() * (squa_nu4 - ((2 * k - 1) * (2 * k - 1))) / (k * 8);

                        table.Add(a);
                    }

                    return table[n];
                }
            }

            private class JYCoefTable {
                private readonly ddouble squa_nu4;
                private readonly List<(ddouble p0, ddouble p1)> table = new();

                public JYCoefTable(ddouble nu) {
                    this.squa_nu4 = ddouble.Ldexp(nu * nu, 2);
                }

                public (ddouble p0, ddouble p1) this[int n] => Value(n);

                public (ddouble p0, ddouble p1) Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    static int square(int n) => (n * n);

                    for (int k = table.Count; k <= n; k++) {
                        ddouble p0 = (squa_nu4 - square(8 * k + 1)) * (squa_nu4 - square(8 * k + 3)) / (64 * (4 * k + 1) * (4 * k + 2));
                        ddouble p1 = (squa_nu4 - square(8 * k + 3)) * (squa_nu4 - square(8 * k + 5)) / (64 * (4 * k + 2) * (4 * k + 3));

                        table.Add((p0, p1));
                    }

                    return table[n];
                }
            }

            private class IKCoefTable {
                private readonly ddouble squa_nu4;
                private readonly List<ddouble> table = new();

                public IKCoefTable(ddouble nu) {
                    this.squa_nu4 = ddouble.Ldexp(nu * nu, 2);
                }

                public ddouble this[int n] => Value(n);

                public ddouble Value(int n) {
                    ArgumentOutOfRangeException.ThrowIfNegative(n, nameof(n));

                    if (n < table.Count) {
                        return table[n];
                    }

                    static int square(int n) => (n * n);

                    for (int k = table.Count; k <= n; k++) {
                        ddouble p = (squa_nu4 - square(4 * k + 1)) / (8 * (2 * k + 1));

                        table.Add(p);
                    }

                    return table[n];
                }
            }
        }

        private static class BesselInterpolate {
            public static Complex BesselYCubicInterpolate(ddouble nu, Complex x) {
                int n = (int)ddouble.Round(nu);
                ddouble alpha = nu - n;

                Complex y0 = BesselY(n, x);
                Complex y1 = BesselY(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold, x);
                Complex y2 = BesselY(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold * 1.5, x);
                Complex y3 = BesselY(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold * 2, x);

                ddouble t = ddouble.Abs(alpha) / BesselUtil.InterpolationThreshold;
                Complex y = CubicInterpolate(t, y0, y1, y2, y3);

                return y;
            }

            public static Complex BesselKCubicInterpolate(ddouble nu, Complex x, bool scale) {
                int n = (int)ddouble.Round(nu);
                ddouble alpha = nu - n;

                Complex y0 = BesselK(n, x, scale: true);
                Complex y1 = BesselK(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold, x, scale: true);
                Complex y2 = BesselK(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold * 1.5, x, scale: true);
                Complex y3 = BesselK(n + ddouble.Sign(alpha) * BesselUtil.InterpolationThreshold * 2, x, scale: true);

                ddouble t = ddouble.Abs(alpha) / BesselUtil.InterpolationThreshold;
                Complex y = CubicInterpolate(t, y0, y1, y2, y3);

                if (!scale) {
                    y *= Exp(-x);
                }

                return y;
            }

            private static Complex CubicInterpolate(ddouble t, Complex y0, Complex y1, Complex y2, Complex y3) {
                return y0 + (
                    -(13d + t * (-9d + t * 2d)) / 6d * y0
                    + (6d + t * (-7d + t * 2d)) * y1
                    - (16d + t * (-24d + t * 8d)) / 3d * y2
                    + (3d + t * (-5d + t * 2d)) / 2d * y3) * t;
            }
        }
    }
}
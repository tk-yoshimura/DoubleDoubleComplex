﻿using DoubleDouble;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using static DoubleDoubleComplex.Complex.ComplexBesselUtil;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex BesselJ(ddouble nu, Complex z) {
            if (ddouble.IsPositive(z.R) && AlmostReal(z)) {
                return ddouble.BesselJ(nu, z.R);
            }

            CheckNu(nu);

            if (!IsFinite(z)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return BesselJ(nu, z.Conj).Conj;
            }

            if (ddouble.IsNegative(z.R)) {
                return (ddouble.CosPi(nu), ddouble.SinPi(nu)) * BesselJ(nu, (-z.R, z.I)).Conj;
            }

            if (UseRecurrence(nu)) {
                return Recurrence.BesselJ(nu, z);
            }

            if (z.Magnitude >= HankelThreshold) {
                return Limit.BesselJ(nu, z);
            }
            else if (z.R <= PowerSeriesThreshold(nu, z.I)) {
                return PowerSeries.BesselJ(nu, z);
            }
            else if (z.I <= MillerBackwardThreshold) {
                return MillerBackward.BesselJ(nu, z);
            }
            else {
                return (ddouble.CosPi(nu / 2d), ddouble.SinPi(nu / 2d)) * MillerBackward.BesselI(nu, (z.I, z.R)).Conj;
            }
        }

        public static Complex BesselY(ddouble nu, Complex z) {
            if (ddouble.IsPositive(z.R) && AlmostReal(z)) {
                return ddouble.BesselY(nu, z.R);
            }

            CheckNu(nu);

            if (!IsFinite(z)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return BesselY(nu, z.Conj).Conj;
            }

            if (ddouble.IsNegative(z.R)) {
                return (ddouble.CosPi(nu), -ddouble.SinPi(nu)) * BesselY(nu, (-z.R, z.I)).Conj
                        + (0d, 2d * ddouble.CosPi(nu)) * BesselJ(nu, (-z.R, z.I)).Conj;
            }

            if (UseRecurrence(nu)) {
                return Recurrence.BesselY(nu, z);
            }

            if (z.Magnitude >= HankelThreshold) {
                return Limit.BesselY(nu, z);
            }
            else if (z.R <= PowerSeriesThreshold(nu, z.I) - BesselJYPowerseriesBias) {
                if (NearlyInteger(nu, out int n) || ddouble.ILogB(n - nu) >= BesselYKNearIntegerExponent) {
                    return PowerSeries.BesselY(nu, z);
                }
                else if (z.I <= MillerBackwardThreshold / 2d) {
                    return MillerBackward.BesselY(nu, z);
                }
            }
            else if (z.I <= MillerBackwardThreshold) {
                return MillerBackward.BesselY(nu, z);
            }
            {
                Complex c = (ddouble.CosPi(nu / 2d), ddouble.SinPi(nu / 2d));

                Complex bi = (z.R <= PowerSeriesThreshold(nu, z.I))
                    ? PowerSeries.BesselI(nu, (z.I, z.R))
                    : MillerBackward.BesselI(nu, (z.I, z.R));

                Complex bk = YoshidaPade.BesselK(nu, (z.I, z.R));

                Complex y = MulI(c * bi.Conj) - 2d * ddouble.RcpPi * (c * bk).Conj;

                return y;
            }
        }

        public static Complex BesselI(ddouble nu, Complex z) {
            if (ddouble.IsPositive(z.R) && AlmostReal(z)) {
                return ddouble.BesselI(nu, z.R);
            }

            CheckNu(nu);

            if (!IsFinite(z)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return BesselI(nu, z.Conj).Conj;
            }

            if (ddouble.IsNegative(z.R)) {
                return (ddouble.CosPi(nu), ddouble.SinPi(nu)) * BesselI(nu, (-z.R, z.I)).Conj;
            }

            if (UseRecurrence(nu)) {
                return Recurrence.BesselI(nu, z);
            }

            if (z.Magnitude >= HankelThreshold) {
                return Limit.BesselI(nu, z);
            }
            else if (z.I <= PowerSeriesThreshold(nu, z.R)) {
                return PowerSeries.BesselI(nu, z);
            }
            else {
                return MillerBackward.BesselI(nu, z);
            }
        }

        public static Complex BesselK(ddouble nu, Complex z) {
            if (ddouble.IsPositive(z.R) && AlmostReal(z)) {
                return ddouble.BesselK(nu, z.R);
            }

            CheckNu(nu);

            if (!IsFinite(z)) {
                return NaN;
            }

            nu = ddouble.Abs(nu);

            if (ddouble.IsNegative(z.I)) {
                return BesselK(nu, z.Conj).Conj;
            }

            if (ddouble.IsNegative(z.R)) {
                return (ddouble.CosPi(nu), -ddouble.SinPi(nu)) * BesselK(nu, (-z.R, z.I)).Conj
                        - (0d, ddouble.Pi) * BesselI(nu, (-z.R, z.I)).Conj;
            }

            if (UseRecurrence(nu)) {
                return Recurrence.BesselK(nu, z);
            }

            if (z.Magnitude >= HankelThreshold) {
                return Limit.BesselK(nu, z);
            }
            else if (z.Magnitude <= BesselKNearZeroThreshold) {
                if (NearlyInteger(nu, out int n) || ddouble.ILogB(n - nu) >= BesselYKNearIntegerExponent) {
                    return PowerSeries.BesselK(nu, z);
                }
                else {
                    return AmosPowerSeries.BesselK(nu, z);
                }
            }
            else if (z.R >= BesselKPadeThreshold) {
                return YoshidaPade.BesselK(nu, z);
            }
            else {
                Complex c = (ddouble.CosPi(nu / 2d), -ddouble.SinPi(nu / 2d));

                Complex bi = (z.I <= PowerSeriesThreshold(nu, z.R))
                    ? PowerSeries.BesselI(nu, z)
                    : MillerBackward.BesselI(nu, z);

                Complex by =
                    ((z.I <= PowerSeriesThreshold(nu, z.R) - BesselJYPowerseriesBias) &&
                     (NearlyInteger(nu, out int n) || ddouble.ILogB(n - nu) >= BesselYKNearIntegerExponent))
                    ? PowerSeries.BesselY(nu, (z.I, z.R))
                    : MillerBackward.BesselY(nu, (z.I, z.R));

                Complex y = c * (MulMinusI(c * bi) - by.Conj) * ddouble.Pi / 2d;

                return y;
            }
        }

        public static Complex HankelH1(ddouble nu, Complex z) {
            if (ddouble.IsNegative(z.R) && ddouble.IsNegative(z.I)) {
                return BesselJ(nu, z) + MulI(BesselY(nu, z));
            }
            else {
                Complex c = (-ddouble.SinPi(nu / 2), -ddouble.CosPi(nu / 2));
                Complex f = BesselK(nu, (z.I, z.R)).Conj;

                Complex y = 2d * ddouble.RcpPi * c * f;

                return y;
            }
        }

        public static Complex HankelH2(ddouble nu, Complex z) {
            if (ddouble.IsNegative(z.R) && ddouble.IsPositive(z.I)) {
                return BesselJ(nu, z) - MulI(BesselY(nu, z));
            }
            else {
                Complex c = (-ddouble.SinPi(nu / 2), ddouble.CosPi(nu / 2));
                Complex f = BesselK(nu, (-z.I, z.R));

                Complex y = 2d * ddouble.RcpPi * c * f;

                return y;
            }
        }

        internal static class ComplexBesselUtil {
            public const int RecurrenceMaxN = 256;
            public const int DirectMaxN = 16;
            public const int EpsExponent = -105;
            public const int NearZeroExponent = -950;
            public const int BesselYKNearIntegerExponent = -3;
            public const double HankelThreshold = 38.875, MillerBackwardThreshold = 6;
            public const double BesselKPadeThreshold = 2, BesselKNearZeroThreshold = 2, BesselJYPowerseriesBias = 2;

            static ComplexBesselUtil() {
                Debug.Assert(BesselKPadeThreshold <= MillerBackwardThreshold / 2d);
            }

            public static ddouble PowerSeriesThreshold(ddouble nu, ddouble x) {
                ddouble nu_abs = ddouble.Abs(nu);

                return 7.5 + nu_abs * (3.57e-1 + nu_abs * 5.23e-3) + x * (4.67e-1 - nu_abs * 1.51e-2);
            }

            public static void CheckNu(ddouble nu) {
                if (!(ddouble.Abs(nu) <= RecurrenceMaxN)) {
                    throw new ArgumentOutOfRangeException(
                        nameof(nu),
                        $"In the calculation of the Bessel function, nu with an absolute value greater than {RecurrenceMaxN} is not supported."
                    );
                }
            }

            public static void CheckN(int n) {
                if (n < -RecurrenceMaxN || n > RecurrenceMaxN) {
                    throw new ArgumentOutOfRangeException(
                        nameof(n),
                        $"In the calculation of the Bessel function, n with an absolute value greater than {RecurrenceMaxN} is not supported."
                    );
                }
            }

            public static bool UseRecurrence(ddouble nu) {
                return ddouble.Abs(nu) > DirectMaxN;
            }

            public static bool NearlyInteger(ddouble nu, out int n) {
                n = (int)ddouble.Round(nu);

                return ddouble.ILogB(nu - n) < EpsExponent;
            }

            public class PowerSeries {
                private static readonly ConcurrentDictionary<ddouble, DoubleFactDenomTable> dfactdenom_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, X2DenomTable> x2denom_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, GammaDenomTable> gammadenom_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, GammaTable> gamma_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, GammaPNTable> gammapn_coef_table = [];
                private static readonly YCoefTable y_coef_table = new();
                private static readonly Y0CoefTable y0_coef_table = new();
                private static readonly ConcurrentDictionary<int, YNCoefTable> yn_coef_table = [];
                private static readonly ConcurrentDictionary<int, ReadOnlyCollection<ddouble>> yn_finitecoef_table = [];
                private static readonly KCoefTable k_coef_table = new();
                private static readonly K0CoefTable k0_coef_table = new();
                private static readonly K1CoefTable k1_coef_table = new();

                public static Complex BesselJ(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (ddouble.IsNegative(nu) && NearlyInteger(nu, out int n)) {
                        Complex y = BesselJKernel(-nu, z, terms: 42);

                        return (n & 1) == 0 ? y : -y;
                    }
                    else {
                        Complex y = BesselJKernel(nu, z, terms: 42);

                        return y;
                    }
                }

                public static Complex BesselY(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (NearlyInteger(nu, out int n)) {
                        Complex y = BesselYKernel(n, z, terms: 44);

                        return y;
                    }
                    else {
                        if (ddouble.IsNegative(nu) && NearlyInteger(nu + 0.5d, out int n_p5)) {
                            Complex y = BesselJKernel(-nu, z, terms: 42);

                            return (n_p5 & 1) == 0 ? y : -y;
                        }
                        else {
                            Complex y = BesselYKernel(nu, z, terms: 44);

                            return y;
                        }
                    }
                }

                public static Complex BesselI(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (ddouble.IsNegative(nu) && NearlyInteger(nu, out _)) {
                        Complex y = BesselIKernel(-nu, z, terms: 42);

                        return y;
                    }
                    else {
                        Complex y = BesselIKernel(nu, z, terms: 42);

                        return y;
                    }
                }

                public static Complex BesselK(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (NearlyInteger(nu, out int n)) {
                        Complex y = BesselKKernel(n, z, terms: 27);

                        return y;
                    }
                    else {
                        Complex y = BesselKKernel(nu, z, terms: 30);

                        return y;
                    }
                }

                private static Complex BesselJKernel(ddouble nu, Complex z, int terms) {
                    if (!dfactdenom_coef_table.TryGetValue(nu, out DoubleFactDenomTable r)) {
                        r = new DoubleFactDenomTable(nu);
                        dfactdenom_coef_table[nu] = r;
                    }
                    if (!x2denom_coef_table.TryGetValue(nu, out X2DenomTable d)) {
                        d = new X2DenomTable(nu);
                        x2denom_coef_table[nu] = d;
                    }

                    Complex z2 = z * z, z4 = z2 * z2;

                    Complex c = 0d, u = Pow(Ldexp(z, -1), nu);

                    if (!IsFinite(u) || IsZero(z2)) {
                        if (ddouble.IsZero(nu)) {
                            return 1d;
                        }
                        if (NearlyInteger(nu, out _) || ddouble.IsPositive(nu)) {
                            return 0d;
                        }

                        return NaN;
                    }

                    for (int k = 0; k <= terms; k++) {
                        c = SeriesUtil.Add(c, u * r[k], 1d, -z2 * d[k], out bool convergence);

                        if (convergence) {
                            break;
                        }

                        u *= z4;

                        if (!IsFinite(c)) {
                            break;
                        }
                    }

                    return c;
                }

                private static Complex BesselYKernel(ddouble nu, Complex z, int terms) {
                    if (!gamma_coef_table.TryGetValue(nu, out GammaTable g)) {
                        g = new GammaTable(nu);
                        gamma_coef_table[nu] = g;
                    }
                    if (!gammapn_coef_table.TryGetValue(nu, out GammaPNTable gpn)) {
                        gpn = new GammaPNTable(nu);
                        gammapn_coef_table[nu] = gpn;
                    }

                    YCoefTable r = y_coef_table;

                    ddouble cos = ddouble.CosPi(nu), sin = ddouble.SinPi(nu);
                    Complex p = IsZero(cos) ? 0d : Pow(z, Ldexp(nu, 1)) * cos;
                    Complex s = Ldexp(Pow(Ldexp(z, 1), nu), 2);

                    Complex z2 = z * z, z4 = z2 * z2;

                    if (!IsFinite(p) || !IsFinite(s) || ILogB(s) < NearZeroExponent || IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = 0d, u = 1d / sin;

                    for (int k = 0, t = 1; k <= terms; k++, t += 2) {
                        Complex a = t * s * g[t], q = gpn[t];
                        Complex pa = p / a, qa = q / a;

                        Complex v = 4 * t * t - z2;
                        c = SeriesUtil.Add(c, u * r[k], 4 * t * nu * (pa + qa), v * (pa - qa), out bool convergence);

                        if (convergence && ILogB(v) >= -4) {
                            break;
                        }

                        u *= z4;

                        if (!IsFinite(c)) {
                            break;
                        }
                    }

                    return c;
                }

                private static Complex BesselYKernel(int n, Complex z, int terms) {
                    if (n < 0) {
                        Complex y = BesselYKernel(-n, z, terms);

                        return (n & 1) == 0 ? y : -y;
                    }
                    else if (n == 0) {
                        return BesselY0Kernel(z, terms);
                    }
                    else if (n == 1) {
                        return BesselY1Kernel(z, terms);
                    }
                    else {
                        return BesselYNKernel(n, z, terms);
                    }
                }

                private static Complex BesselY0Kernel(Complex z, int terms) {
                    if (!dfactdenom_coef_table.TryGetValue(0, out DoubleFactDenomTable r)) {
                        r = new DoubleFactDenomTable(0);
                        dfactdenom_coef_table[0] = r;
                    }
                    if (!x2denom_coef_table.TryGetValue(0, out X2DenomTable d)) {
                        d = new X2DenomTable(0);
                        x2denom_coef_table[0] = d;
                    }

                    Y0CoefTable q = y0_coef_table;

                    Complex h = Log(Ldexp(z, -1)) + ddouble.EulerGamma;
                    Complex z2 = z * z, z4 = z2 * z2;

                    if (!IsFinite(h) || IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = 0d, u = Ldexp(ddouble.RcpPi, 1);

                    for (int k = 0; k <= terms; k++) {
                        Complex s = u * r[k], t = h - ddouble.HarmonicNumber(2 * k);
                        c = SeriesUtil.Add(c, s * t, 1d, -z2 * d[k], out bool convergence1);
                        c = SeriesUtil.Add(c, s, z2 * q[k], out bool convergence2);

                        if (convergence1 && convergence2 && ILogB(t) >= -4) {
                            break;
                        }

                        u *= z4;
                    }

                    return c;
                }

                private static Complex BesselY1Kernel(Complex z, int terms) {
                    if (!dfactdenom_coef_table.TryGetValue(1, out DoubleFactDenomTable r)) {
                        r = new DoubleFactDenomTable(1);
                        dfactdenom_coef_table[1] = r;
                    }
                    if (!x2denom_coef_table.TryGetValue(1, out X2DenomTable d)) {
                        d = new X2DenomTable(1);
                        x2denom_coef_table[1] = d;
                    }
                    if (!yn_coef_table.TryGetValue(1, out YNCoefTable q)) {
                        q = new YNCoefTable(1);
                        yn_coef_table[1] = q;
                    }

                    Complex h = Ldexp(Log(Ldexp(z, -1)) + ddouble.EulerGamma, 1);
                    Complex z2 = z * z, z4 = z2 * z2;

                    if (!IsFinite(h) || IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = -2d / (z * ddouble.Pi), u = z / Ldexp(ddouble.Pi, 1);

                    for (int k = 0; k <= terms; k++) {
                        Complex s = u * r[k], t = h - ddouble.HarmonicNumber(2 * k) - ddouble.HarmonicNumber(2 * k + 1);
                        c = SeriesUtil.Add(c, s * t, 1d, -z2 * d[k], out bool convergence1);
                        c = SeriesUtil.Add(c, s, z2 * q[k], out bool convergence2);

                        if (convergence1 && convergence2 && ILogB(t) >= -4) {
                            break;
                        }

                        u *= z4;
                    }

                    return c;
                }

                private static Complex BesselYNKernel(int n, Complex z, int terms) {
                    if (!dfactdenom_coef_table.TryGetValue(n, out DoubleFactDenomTable r)) {
                        r = new DoubleFactDenomTable(n);
                        dfactdenom_coef_table[n] = r;
                    }
                    if (!x2denom_coef_table.TryGetValue(n, out X2DenomTable d)) {
                        d = new X2DenomTable(n);
                        x2denom_coef_table[n] = d;
                    }
                    if (!yn_coef_table.TryGetValue(n, out YNCoefTable q)) {
                        q = new YNCoefTable(n);
                        yn_coef_table[n] = q;
                    }
                    if (!yn_finitecoef_table.TryGetValue(n, out ReadOnlyCollection<ddouble> f)) {
                        f = YNFiniteCoefTable.Value(n);
                        yn_finitecoef_table[n] = f;
                    }

                    Complex h = Ldexp(Log(Ldexp(z, -1)) + ddouble.EulerGamma, 1);

                    if (!IsFinite(h)) {
                        return NaN;
                    }

                    Complex c = 0d;
                    Complex z2 = z * z, z4 = z2 * z2;
                    Complex u = 1d, v = 1d, w = Ldexp(z2, -2);

                    for (int k = 0; k < n; k++) {
                        c += v * f[k];
                        v *= w;
                    }
                    c /= -v;

                    if (!IsFinite(c) || IsZero(z2)) {
                        return NaN;
                    }

                    for (int k = 0; k <= terms; k++) {
                        Complex s = u * r[k], t = (h - ddouble.HarmonicNumber(2 * k) - ddouble.HarmonicNumber(2 * k + n));
                        c = SeriesUtil.Add(c, s * t, 1d, -z2 * d[k], out bool convergence1);
                        c = SeriesUtil.Add(c, s, z2 * q[k], out bool convergence2);

                        if (convergence1 && convergence2 && ILogB(t) >= -4) {
                            break;
                        }

                        u *= z4;
                    }

                    Complex y = c * ddouble.RcpPi * Pow(Ldexp(z, -1), n);

                    return y;
                }

                private static Complex BesselIKernel(ddouble nu, Complex z, int terms) {
                    if (!dfactdenom_coef_table.TryGetValue(nu, out DoubleFactDenomTable r)) {
                        r = new DoubleFactDenomTable(nu);
                        dfactdenom_coef_table[nu] = r;
                    }
                    if (!x2denom_coef_table.TryGetValue(nu, out X2DenomTable d)) {
                        d = new X2DenomTable(nu);
                        x2denom_coef_table[nu] = d;
                    }

                    Complex z2 = z * z, z4 = z2 * z2;

                    Complex c = 0d, u = Pow(Ldexp(z, -1), nu);

                    if (!IsFinite(u) || IsZero(z2)) {
                        if (ddouble.IsZero(nu)) {
                            return 1d;
                        }
                        if (NearlyInteger(nu, out _) || ddouble.IsPositive(nu)) {
                            return 0d;
                        }

                        return NaN;
                    }

                    for (int k = 0; k <= terms; k++) {
                        c = SeriesUtil.Add(c, u * r[k], 1d, z2 * d[k], out bool convergence);

                        if (convergence) {
                            break;
                        }

                        u *= z4;

                        if (!IsFinite(c)) {
                            break;
                        }
                    }

                    return c;
                }

                private static Complex BesselKKernel(ddouble nu, Complex z, int terms) {
                    if (!gammadenom_coef_table.TryGetValue(nu, out GammaDenomTable gp)) {
                        gp = new GammaDenomTable(nu);
                        gammadenom_coef_table[nu] = gp;
                    }
                    if (!gammadenom_coef_table.TryGetValue(-nu, out GammaDenomTable gn)) {
                        gn = new GammaDenomTable(-nu);
                        gammadenom_coef_table[-nu] = gn;
                    }

                    KCoefTable r = k_coef_table;

                    Complex tp = Pow(Ldexp(z, -1), nu), tn = 1d / tp;

                    Complex z2 = z * z;

                    if (ILogB(tp) < NearZeroExponent || IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = 0d, u = ddouble.Pi / Ldexp(ddouble.SinPi(nu), 1);

                    for (int k = 0; k <= terms; k++) {
                        c = SeriesUtil.Add(c, u * r[k], tn * gn[k], -tp * gp[k], out bool convergence);

                        if (convergence) {
                            break;
                        }

                        u *= z2;

                        if (!IsFinite(c)) {
                            break;
                        }
                    }

                    return c;
                }

                private static Complex BesselKKernel(int n, Complex z, int terms) {
                    if (n == 0) {
                        return BesselK0Kernel(z, terms);
                    }
                    else if (n == 1) {
                        return BesselK1Kernel(z, terms);
                    }
                    else {
                        return BesselKNKernel(n, z, terms);
                    }
                }

                private static Complex BesselK0Kernel(Complex z, int terms) {
                    K0CoefTable r = k0_coef_table;
                    Complex h = -Log(Ldexp(z, -1)) - ddouble.EulerGamma;

                    if (!IsFinite(h)) {
                        return NaN;
                    }

                    Complex z2 = z * z;

                    if (IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = 0d, u = 1d;

                    for (int k = 0; k <= terms; k++) {
                        c = SeriesUtil.Add(c, u * r[k], h, ddouble.HarmonicNumber(k), out bool convergence);

                        if (convergence) {
                            break;
                        }

                        u *= z2;
                    }

                    return c;
                }

                private static Complex BesselK1Kernel(Complex z, int terms) {
                    K1CoefTable r = k1_coef_table;
                    Complex h = Log(Ldexp(z, -1)) + ddouble.EulerGamma;

                    if (!IsFinite(h)) {
                        return NaN;
                    }

                    Complex z2 = z * z;

                    if (IsZero(z2)) {
                        return NaN;
                    }

                    Complex c = 1d / z, u = Ldexp(z, -1);

                    for (int k = 0; k <= terms; k++) {
                        c = SeriesUtil.Add(c, u * r[k], h, -ddouble.Ldexp(ddouble.HarmonicNumber(k) + ddouble.HarmonicNumber(k + 1), -1), out bool convergence);

                        if (convergence) {
                            break;
                        }

                        u *= z2;
                    }

                    return c;
                }

                private static Complex BesselKNKernel(int n, Complex z, int terms) {
                    Complex v = 1d / z;
                    Complex y0 = BesselK0Kernel(z, terms);
                    Complex y1 = BesselK1Kernel(z, terms);

                    for (int k = 1; k < n; k++) {
                        (y1, y0) = (2 * k * v * y1 + y0, y1);
                    }

                    return y1;
                }

                private class DoubleFactDenomTable {
                    private ddouble c;
                    private readonly ddouble nu;
                    private readonly List<ddouble> table = [];

                    public DoubleFactDenomTable(ddouble nu) {
                        this.c = ddouble.Gamma(nu + 1d);
                        this.nu = nu;
                        table.Add(ddouble.Rcp(c));
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                c *= checked((nu + 2 * i) * (nu + (2 * i - 1)) * (32 * i * (2 * i - 1)));

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }

                private class X2DenomTable {
                    private readonly ddouble nu;
                    private readonly List<ddouble> table = [];

                    public X2DenomTable(ddouble nu) {
                        ddouble a = ddouble.Rcp(4d * (nu + 1d));

                        this.nu = nu;
                        this.table.Add(a);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                ddouble a = ddouble.Rcp(checked(4d * (2 * i + 1) * (2 * i + 1 + nu)));

                                table.Add(a);
                            }

                            return table[k];
                        }
                    }
                }

                private class GammaDenomTable {
                    private ddouble c;
                    private readonly ddouble nu;
                    private readonly List<ddouble> table = [];

                    public GammaDenomTable(ddouble nu) {
                        this.c = ddouble.Gamma(nu + 1d);
                        this.nu = nu;
                        table.Add(ddouble.Rcp(c));
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                c *= nu + i;

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }

                private class GammaTable {
                    private ddouble c;
                    private readonly ddouble nu;
                    private readonly List<ddouble> table = [];

                    public GammaTable(ddouble nu) {
                        this.c = ddouble.Gamma(nu + 1d);
                        this.nu = nu;
                        table.Add(c);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                c *= nu + i;

                                table.Add(c);
                            }

                            return table[k];
                        }
                    }
                }

                private class GammaPNTable {
                    private readonly ddouble r;
                    private readonly GammaTable positive_table, negative_table;
                    private readonly List<ddouble> table = [];

                    public GammaPNTable(ddouble nu) {
                        this.r = ddouble.Pow(4d, nu);
                        this.positive_table = new(nu);
                        this.negative_table = new(-nu);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                ddouble c = r * positive_table[i] / negative_table[i];

                                table.Add(c);
                            }

                            return table[k];
                        }
                    }
                }

                private class YCoefTable {
                    private ddouble c;
                    private readonly List<ddouble> table = [];

                    public YCoefTable() {
                        this.c = 1d;
                        table.Add(1d);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                c *= checked(32 * i * (2 * i - 1));

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }

                private class Y0CoefTable {
                    private readonly List<ddouble> table = [];

                    public Y0CoefTable() {
                        table.Add(ddouble.Rcp(4));
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                ddouble c = ddouble.Rcp(checked(4 * (2 * i + 1) * (2 * i + 1) * (2 * i + 1)));

                                table.Add(c);
                            }

                            return table[k];
                        }
                    }
                }

                private class YNCoefTable {
                    private readonly int n;
                    private readonly List<ddouble> table = [];

                    public YNCoefTable(int n) {
                        this.n = n;
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                ddouble c = (ddouble)(n + 4 * i + 2) /
                                    (ddouble)checked(4 * (2 * i + 1) * (2 * i + 1) * (n + 2 * i + 1) * (n + 2 * i + 1));

                                table.Add(c);
                            }

                            return table[k];
                        }
                    }
                }

                private static class YNFiniteCoefTable {
                    public static ReadOnlyCollection<ddouble> Value(int n) {
                        Debug.Assert(n >= 0);

                        List<ddouble> frac = [1], coef = [];

                        for (int i = 1; i < n; i++) {
                            frac.Add(i * frac[^1]);
                        }

                        for (int i = 0; i < n; i++) {
                            coef.Add(frac[^(i + 1)] / frac[i]);
                        }

                        return new(coef);
                    }
                }

                private class KCoefTable {
                    private ddouble c;
                    private readonly List<ddouble> table = [];

                    public KCoefTable() {
                        this.c = 1d;
                        table.Add(1d);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                c *= 4 * i;

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }

                private class K0CoefTable {
                    private ddouble c;
                    private readonly List<ddouble> table = [];

                    public K0CoefTable() {
                        this.c = 1d;
                        table.Add(1d);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (long i = table.Count; i <= k; i++) {
                                c *= checked(4 * i * i);

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }

                private class K1CoefTable {
                    private ddouble c;
                    private readonly List<ddouble> table = [];

                    public K1CoefTable() {
                        this.c = 1d;
                        this.table.Add(1d);
                    }

                    public ddouble this[int k] => Value(k);

                    public ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                c *= checked(4 * i * (i + 1));

                                table.Add(ddouble.Rcp(c));
                            }

                            return table[k];
                        }
                    }
                }
            }

            public static class Limit {
                private static readonly ConcurrentDictionary<ddouble, HankelExpansion> table = [];

                public static Complex BesselJ(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (!table.TryGetValue(nu, out HankelExpansion hankel)) {
                        hankel = new HankelExpansion(nu);
                        table[nu] = hankel;
                    }

                    if (!IsFinite(z)) {
                        return NaN;
                    }

                    (Complex c_even, Complex c_odd) = hankel.BesselJYCoef(z);

                    Complex omega = hankel.Omega(z);

                    Complex cos = Cos(omega), sin = Sin(omega);

                    Complex y = Sqrt(2d / (ddouble.Pi * z)) * (cos * c_even - sin * c_odd);

                    return y;
                }

                public static Complex BesselY(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (!table.TryGetValue(nu, out HankelExpansion hankel)) {
                        hankel = new HankelExpansion(nu);
                        table[nu] = hankel;
                    }

                    if (!IsFinite(z)) {
                        return NaN;
                    }

                    (Complex c_even, Complex c_odd) = hankel.BesselJYCoef(z);

                    Complex omega = hankel.Omega(z);

                    Complex cos = Cos(omega), sin = Sin(omega);

                    Complex y = Sqrt(2d / (ddouble.Pi * z)) * (sin * c_even + cos * c_odd);

                    return y;
                }

                public static Complex BesselI(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (!table.TryGetValue(nu, out HankelExpansion hankel)) {
                        hankel = new HankelExpansion(nu);
                        table[nu] = hankel;
                    }

                    Complex ci = hankel.BesselICoef(z), ck = hankel.BesselKCoef(z);

                    Complex y = Sqrt(1d / (2d * ddouble.Pi * z)) * (
                        Exp(z) * ci -
                        (ddouble.SinPi(nu), -ddouble.CosPi(nu)) * Exp(-z) * ck
                    );

                    if (!IsFinite(z) || IsZero(y)) {
                        return NaN;
                    }

                    return y;
                }

                public static Complex BesselK(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(nu));
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    if (!table.TryGetValue(nu, out HankelExpansion hankel)) {
                        hankel = new HankelExpansion(nu);
                        table[nu] = hankel;
                    }

                    Complex c = hankel.BesselKCoef(z);

                    Complex y = Sqrt(ddouble.Pi / (2d * z)) * Exp(-z) * c;

                    if (!IsFinite(z) || IsZero(y)) {
                        return NaN;
                    }

                    return y;
                }

                private class HankelExpansion {
                    public ddouble Nu { get; }

                    private readonly List<ddouble> a_coef;

                    public HankelExpansion(ddouble nu) {
                        Nu = nu;
                        a_coef = [1];
                    }

                    private ddouble ACoef(int n) {
                        if (n < a_coef.Count) {
                            return a_coef[n];
                        }

                        lock (a_coef) {
                            for (int k = a_coef.Count; k <= n; k++) {
                                ddouble a = a_coef.Last() * (4d * Nu * Nu - checked((2 * k - 1) * (2 * k - 1))) / (k * 8);
                                a_coef.Add(a);
                            }

                            return a_coef[n];
                        }
                    }

                    public Complex Omega(Complex z) {
                        Complex omega = z - ddouble.Ldexp(2d * Nu + 1d, -2) * ddouble.Pi;

                        return omega;
                    }

                    public (Complex c_even, Complex c_odd) BesselJYCoef(Complex z, int terms = 35) {
                        Complex v = 1d / (z * z), w = -v;

                        Complex c_even = ACoef(0), c_odd = ACoef(1);

                        for (int k = 1; k <= terms; k++) {
                            Complex dc_even = w * ACoef(2 * k);
                            Complex dc_odd = w * ACoef(2 * k + 1);

                            c_even += dc_even;
                            c_odd += dc_odd;

                            if (((long)ILogB(c_even) - ILogB(dc_even) >= 106L || IsZero(dc_even)) &&
                                ((long)ILogB(c_odd) - ILogB(dc_odd) >= 106L || IsZero(dc_odd))) {

                                break;
                            }

                            w *= -v;
                        }

                        return (c_even, c_odd / z);
                    }

                    public Complex BesselICoef(Complex z, int terms = 75) {
                        Complex v = 1d / z, w = -v;

                        Complex c = ACoef(0);

                        for (int k = 1; k <= terms; k++) {
                            Complex dc = w * ACoef(k);

                            c += dc;

                            if ((long)ILogB(c) - ILogB(dc) >= 106L || IsZero(dc)) {
                                break;
                            }

                            w *= -v;
                        }

                        return c;
                    }

                    public Complex BesselKCoef(Complex z, int terms = 58) {
                        Complex v = 1d / z, w = v;

                        Complex c = ACoef(0);

                        for (int k = 1; k <= terms; k++) {
                            Complex dc = w * ACoef(k);

                            c += dc;

                            if ((long)ILogB(c) - ILogB(dc) >= 106L || IsZero(dc)) {
                                break;
                            }

                            w *= v;
                        }

                        return c;
                    }
                }
            }

            public class MillerBackward {
                public static readonly int BesselYEpsExponent = -12;

                private static readonly ConcurrentDictionary<ddouble, BesselJPhiTable> phi_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, BesselIPsiTable> psi_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, BesselYEtaTable> eta_coef_table = [];
                private static readonly ConcurrentDictionary<ddouble, BesselYXiTable> xi_coef_table = [];
                private static readonly ReadOnlyCollection<ddouble> eta0_coef = new(Consts.BesselYMillerBackwardCoef.Eta0.Reverse().ToArray());
                private static readonly ReadOnlyCollection<ddouble> xi1_coef = new(Consts.BesselYMillerBackwardCoef.Xi1.Reverse().ToArray());

                public static Complex BesselJ(int n, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselJYIterM((double)z.R);

                    Complex y = BesselJKernel(n, z, m);

                    return y;
                }

                public static Complex BesselJ(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselJYIterM((double)z.R);

                    if (NearlyInteger(nu, out int n)) {
                        Complex y = BesselJKernel(n, z, m);

                        return y;
                    }
                    else {
                        Complex y = BesselJKernel(nu, z, m);

                        return y;
                    }
                }

                public static Complex BesselY(int n, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselJYIterM((double)z.R);

                    Complex y = BesselYKernel(n, z, m);

                    return y;
                }

                public static Complex BesselY(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselJYIterM((double)z.R);

                    if (NearlyInteger(nu, out int n)) {
                        Complex y = BesselYKernel(n, z, m);

                        return y;
                    }
                    else {
                        if (ddouble.IsNegative(nu) && NearlyInteger(nu + 0.5d, out int n_p5)) {
                            Complex y = BesselJKernel(-nu, z, m);

                            return (n_p5 & 1) == 0 ? y : -y;
                        }
                        else {
                            Complex y = BesselYKernel(nu, z, m);

                            return y;
                        }
                    }
                }

                private static int BesselJYIterM(double r) {
                    int m = (int)double.Ceiling(3.8029e1 + r * 1.6342e0);

                    return (m + 1) / 2 * 2;
                }

                public static Complex BesselI(int n, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselIIterM((double)z.R, (double)z.I);

                    Complex y = BesselIKernel(n, z, m);

                    return y;
                }

                public static Complex BesselI(ddouble nu, Complex z) {
                    Debug.Assert(ddouble.IsPositive(z.R));
                    Debug.Assert(ddouble.IsPositive(z.I));

                    int m = BesselIIterM((double)z.R, (double)z.I);

                    if (NearlyInteger(nu, out int n)) {
                        Complex y = BesselIKernel(n, z, m);

                        return y;
                    }
                    else {
                        Complex y = BesselIKernel(nu, z, m);

                        return y;
                    }
                }

                private static int BesselIIterM(double r, double i) {
                    int m = (int)double.Ceiling(3.3612e1 + r * 1.3557e0 + i * 1.8485e0 - r * i * 4.3649e-2);

                    return (m + 1) / 2 * 2;
                }

                private static Complex BesselJKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    if (n < 0) {
                        return (n & 1) == 0 ? BesselJKernel(-n, z, m) : -BesselJKernel(-n, z, m);
                    }
                    else if (n == 0) {
                        return BesselJ0Kernel(z, m);
                    }
                    else if (n == 1) {
                        return BesselJ1Kernel(z, m);
                    }
                    else {
                        return BesselJNKernel(n, z, m);
                    }
                }

                private static Complex BesselJKernel(ddouble nu, Complex z, int m) {
                    int n = (int)ddouble.Floor(nu);
                    ddouble alpha = nu - n;

                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    if (!phi_coef_table.TryGetValue(alpha, out BesselJPhiTable phi)) {
                        phi = new BesselJPhiTable(alpha);
                        phi_coef_table[alpha] = phi;
                    }

                    Complex f0 = 1e-256d, f1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

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

                private static Complex BesselJ0Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex f0 = 1e-256d, f1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex y0 = f0 / lambda;

                    return y0;
                }

                private static Complex BesselJ1Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex f0 = 1e-256d, f1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex y1 = f1 / lambda;

                    return y1;
                }

                private static Complex BesselJNKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex f0 = 1e-256d, f1 = 0d, fn = 0d, lambda = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);

                        if (k - 1 == n) {
                            fn = f0;
                        }
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex yn = fn / lambda;

                    return yn;
                }

                private static Complex BesselYKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    if (n < 0) {
                        return (n & 1) == 0 ? BesselYKernel(-n, z, m) : -BesselYKernel(-n, z, m);
                    }
                    else if (n == 0) {
                        return BesselY0Kernel(z, m);
                    }
                    else if (n == 1) {
                        return BesselY1Kernel(z, m);
                    }
                    else {
                        return BesselYNKernel(n, z, m);
                    }
                }

                private static Complex BesselYKernel(ddouble nu, Complex z, int m) {
                    int n = (int)ddouble.Round(nu);
                    ddouble alpha = nu - n;

                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    if (!eta_coef_table.TryGetValue(alpha, out BesselYEtaTable eta)) {
                        eta = new BesselYEtaTable(alpha);
                        eta_coef_table[alpha] = eta;
                    }
                    if (!xi_coef_table.TryGetValue(alpha, out BesselYXiTable xi)) {
                        xi = new BesselYXiTable(alpha, eta);
                        xi_coef_table[alpha] = xi;
                    }
                    if (!phi_coef_table.TryGetValue(alpha, out BesselJPhiTable phi)) {
                        phi = new BesselJPhiTable(alpha);
                        phi_coef_table[alpha] = phi;
                    }

                    Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                    Complex se = 0d, sxo = 0d, sxe = 0d;
                    Complex v = 1d / z;

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

                    ddouble rcot = 1d / ddouble.TanPi(alpha), rgamma = ddouble.Gamma(1d + alpha), rsqgamma = rgamma * rgamma;
                    Complex r = Ldexp(ddouble.RcpPi * sqs, 1);
                    Complex p = sqs * rsqgamma * ddouble.RcpPi;

                    Complex xi0 = -Ldexp(v, 1) * p;

                    (Complex eta0, Complex xi1) = ddouble.ILogB(alpha) >= BesselYEpsExponent
                        ? (rcot - p / alpha, rcot + p * (alpha * (alpha + 1d) + 1d) / (alpha * (alpha - 1d)))
                        : BesselYEta0Xi1Eps(alpha, z);

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

                private static (Complex eta0, Complex xi1) BesselYEta0Xi1Eps(ddouble alpha, Complex z) {
                    const int N = 7;

                    Complex lnz = Log(z);

                    Complex eta0 = 0d, xi1 = 0d;
                    for (int i = N, k = 0; i >= 0; i--) {
                        Complex s = eta0_coef[k], t = xi1_coef[k];
                        k++;

                        for (int j = i; j >= 0; j--, k++) {
                            s = eta0_coef[k] + lnz * s;
                            t = xi1_coef[k] + lnz * t;
                        }

                        eta0 = s + alpha * eta0;
                        xi1 = t + alpha * xi1;
                    }

                    return (eta0, xi1);
                }

                private static Complex BesselY0Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    if (!eta_coef_table.TryGetValue(0, out BesselYEtaTable eta)) {
                        eta = new BesselYEtaTable(0);
                        eta_coef_table[0] = eta;
                    }

                    Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                    Complex se = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;

                            se += f0 * eta[k / 2];
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex y0 = Ldexp((se + f0 * (Log(Ldexp(z, -1)) + ddouble.EulerGamma)) / (ddouble.Pi * lambda), 1);

                    return y0;
                }

                private static Complex BesselY1Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    if (!xi_coef_table.TryGetValue(0, out BesselYXiTable xi)) {
                        if (!eta_coef_table.TryGetValue(0, out BesselYEtaTable eta)) {
                            eta = new BesselYEtaTable(0);
                            eta_coef_table[0] = eta;
                        }

                        xi = new BesselYXiTable(0, eta);
                        xi_coef_table[0] = xi;
                    }

                    Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                    Complex sx = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;
                        }
                        else if (k >= 3) {
                            sx += f0 * xi[k];
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex y1 = Ldexp((sx - v * f0 + (Log(Ldexp(z, -1)) + ddouble.EulerGamma - 1d) * f1) / (lambda * ddouble.Pi), 1);

                    return y1;
                }

                private static Complex BesselYNKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    if (!eta_coef_table.TryGetValue(0, out BesselYEtaTable eta)) {
                        eta = new BesselYEtaTable(0);
                        eta_coef_table[0] = eta;
                    }
                    if (!xi_coef_table.TryGetValue(0, out BesselYXiTable xi)) {
                        xi = new BesselYXiTable(0, eta);
                        xi_coef_table[0] = xi;
                    }

                    Complex f0 = 1e-256, f1 = 0d, lambda = 0d;
                    Complex se = 0d, sx = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        if ((k & 1) == 0) {
                            lambda += f0;

                            se += f0 * eta[k / 2];
                        }
                        else if (k >= 3) {
                            sx += f0 * xi[k];
                        }

                        (f0, f1) = (2 * k * v * f0 - f1, f0);
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex c = Log(Ldexp(z, -1)) + ddouble.EulerGamma;

                    Complex y0 = se + f0 * c;
                    Complex y1 = sx - v * f0 + (c - 1d) * f1;

                    for (int k = 1; k < n; k++) {
                        (y1, y0) = (2 * k * v * y1 - y0, y1);
                    }

                    Complex yn = Ldexp(y1 / (lambda * ddouble.Pi), 1);

                    return yn;
                }

                private static Complex BesselIKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    n = int.Abs(n);

                    if (n == 0) {
                        return BesselI0Kernel(z, m);
                    }
                    else if (n == 1) {
                        return BesselI1Kernel(z, m);
                    }
                    else {
                        return BesselINKernel(n, z, m);
                    }
                }

                private static Complex BesselIKernel(ddouble nu, Complex z, int m) {
                    int n = (int)ddouble.Floor(nu);
                    ddouble alpha = nu - n;

                    Debug.Assert(m >= 2 && (m & 1) == 0 && n < m);

                    if (!psi_coef_table.TryGetValue(alpha, out BesselIPsiTable psi)) {
                        psi = new BesselIPsiTable(alpha);
                        psi_coef_table[alpha] = psi;
                    }

                    Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

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

                        yn *= Exp(z);

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

                        yn *= Exp(z);

                        return yn;
                    }
                }

                private static Complex BesselI0Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        lambda += g0;

                        (g0, g1) = (2 * k * v * g0 + g1, g0);
                    }

                    lambda = Ldexp(lambda, 1) + g0;

                    Complex y0 = g0 / lambda;

                    y0 *= Exp(z);

                    return y0;
                }

                private static Complex BesselI1Kernel(Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex g0 = 1e-256, g1 = 0d, lambda = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        lambda += g0;

                        (g0, g1) = (2 * k * v * g0 + g1, g0);
                    }

                    lambda = Ldexp(lambda, 1) + g0;

                    Complex y1 = g1 / lambda;

                    y1 *= Exp(z);

                    return y1;
                }

                private static Complex BesselINKernel(int n, Complex z, int m) {
                    Debug.Assert(m >= 2 && (m & 1) == 0);

                    Complex f0 = 1e-256, f1 = 0d, lambda = 0d, fn = 0d;
                    Complex v = 1d / z;

                    for (int k = m; k >= 1; k--) {
                        lambda += f0;

                        (f0, f1) = (2 * k * v * f0 + f1, f0);

                        if (k - 1 == n) {
                            fn = f0;
                        }
                    }

                    lambda = Ldexp(lambda, 1) + f0;

                    Complex yn = fn / lambda;

                    yn *= Exp(z);

                    return yn;
                }

                private class BesselJPhiTable {
                    private readonly ddouble alpha;
                    private readonly List<ddouble> table = [];

                    private ddouble g;

                    public BesselJPhiTable(ddouble alpha) {
                        Debug.Assert(alpha > -1d && alpha < 1d, nameof(alpha));

                        this.alpha = alpha;

                        ddouble phi0 = ddouble.Gamma(1d + alpha);
                        ddouble phi1 = phi0 * (alpha + 2d);

                        this.g = phi0;

                        table.Add(phi0);
                        table.Add(phi1);
                    }

                    public ddouble this[int k] => Value(k);

                    private ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                g = g * (alpha + i - 1d) / i;

                                ddouble phi = g * (alpha + 2 * i);

                                table.Add(phi);
                            }

                            return table[k];
                        }
                    }
                }

                private class BesselIPsiTable {
                    private readonly ddouble alpha;
                    private readonly List<ddouble> table = [];

                    private ddouble g;

                    public BesselIPsiTable(ddouble alpha) {
                        Debug.Assert(alpha > -1d && alpha < 1d, nameof(alpha));

                        this.alpha = alpha;

                        ddouble psi0 = ddouble.Gamma(1d + alpha);
                        ddouble psi1 = ddouble.Ldexp(psi0, 1) * (1d + alpha);

                        this.g = ddouble.Ldexp(psi0, 1);

                        table.Add(psi0);
                        table.Add(psi1);
                    }

                    public ddouble this[int k] => Value(k);

                    private ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                g = g * (ddouble.Ldexp(alpha, 1) + i - 1d) / i;

                                ddouble phi = g * (alpha + i);

                                table.Add(phi);
                            }

                            return table[k];
                        }
                    }
                }

                private class BesselYEtaTable {
                    private readonly ddouble alpha;
                    private readonly List<ddouble> table = [];

                    private ddouble g;

                    public BesselYEtaTable(ddouble alpha) {
                        Debug.Assert(alpha > -1d && alpha < 1d, nameof(alpha));

                        this.alpha = alpha;
                        table.Add(ddouble.NaN);

                        if (alpha != 0d) {
                            ddouble c = ddouble.Gamma(1d + alpha);
                            c *= c;
                            this.g = 1d / (1d - alpha) * c;

                            ddouble eta1 = (alpha + 2d) * g;

                            table.Add(eta1);
                        }
                    }

                    public ddouble this[int k] => Value(k);

                    private ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                if (alpha != 0d) {
                                    g = -g * (alpha + i - 1) * (ddouble.Ldexp(alpha, 1) + i - 1d) / (i * (i - alpha));

                                    ddouble eta = g * (alpha + 2 * i);

                                    table.Add(eta);
                                }
                                else {
                                    ddouble eta = (ddouble)2d / i;

                                    table.Add((i & 1) == 1 ? eta : -eta);
                                }
                            }

                            return table[k];
                        }
                    }
                }

                private class BesselYXiTable {
                    private readonly ddouble alpha;
                    private readonly List<ddouble> table = [];
                    private readonly BesselYEtaTable eta;

                    public BesselYXiTable(ddouble alpha, BesselYEtaTable eta) {
                        Debug.Assert(alpha >= -1d && alpha < 1d, nameof(alpha));

                        this.alpha = alpha;
                        table.Add(ddouble.NaN);
                        table.Add(ddouble.NaN);

                        this.eta = eta;
                    }

                    public ddouble this[int k] => Value(k);

                    private ddouble Value(int k) {
                        Debug.Assert(k >= 0);

                        if (k < table.Count) {
                            return table[k];
                        }

                        lock (table) {
                            for (int i = table.Count; i <= k; i++) {
                                if (alpha != 0d) {
                                    if ((i & 1) == 0) {
                                        table.Add(eta[i / 2]);
                                    }
                                    else {
                                        table.Add((eta[i / 2] - eta[i / 2 + 1]) / 2);
                                    }
                                }
                                else {
                                    if ((i & 1) == 1) {
                                        ddouble xi = (ddouble)(2 * (i / 2) + 1) / (i / 2 * (i / 2 + 1));
                                        table.Add((i & 2) > 0 ? xi : -xi);
                                    }
                                    else {
                                        table.Add(ddouble.NaN);
                                    }
                                }
                            }

                            return table[k];
                        }
                    }
                }
            }

            public static class YoshidaPade {
                private static readonly ReadOnlyCollection<ReadOnlyCollection<ddouble>> ess_coef_table;
                private static readonly ConcurrentDictionary<ddouble, ReadOnlyCollection<(ddouble c, ddouble s)>> cds_coef_table = [];

                static YoshidaPade() {
                    cds_coef_table[0] = Array.AsReadOnly(Consts.BesselKYoshidaPadeCoefM38.Nu0.Reverse().ToArray());
                    cds_coef_table[1] = Array.AsReadOnly(Consts.BesselKYoshidaPadeCoefM38.Nu1.Reverse().ToArray());

                    List<ReadOnlyCollection<ddouble>> es = [];

                    for (int i = 0; i < Consts.BesselKYoshidaPadeCoefM38.Ess.Length; i++) {
                        es.Add(Array.AsReadOnly(Consts.BesselKYoshidaPadeCoefM38.Ess[i].ToArray()));
                    }

                    ess_coef_table = Array.AsReadOnly(es.ToArray());
                }

                public static Complex BesselK(ddouble nu, Complex z) {
                    if (nu < 2d) {
                        if (!cds_coef_table.TryGetValue(nu, out ReadOnlyCollection<(ddouble c, ddouble s)> cds)) {
                            cds = Table(nu);
                            cds_coef_table[nu] = cds;
                        }

                        Complex y = Value(z, cds);

                        return y;
                    }
                    else {
                        int n = (int)ddouble.Floor(nu);
                        ddouble alpha = nu - n;

                        Complex y0 = BesselK(alpha, z);
                        Complex y1 = BesselK(alpha + 1d, z);

                        Complex v = 1d / z;

                        for (int k = 1; k < n; k++) {
                            (y1, y0) = (Ldexp(k + alpha, 1) * v * y1 + y0, y1);
                        }

                        return y1;
                    }
                }

                private static Complex Value(Complex z, ReadOnlyCollection<(ddouble c, ddouble d)> cds) {
                    Complex t = 1d / z;
                    (Complex sc, Complex sd) = cds[0];

                    for (int i = 1; i < cds.Count; i++) {
                        (ddouble c, ddouble d) = cds[i];

                        sc = sc * t + c;
                        sd = sd * t + d;
                    }

                    Complex y = Sqrt(Ldexp(t * ddouble.Pi, -1)) * sc / sd;

                    y *= Exp(-z);

                    return y;
                }

                private static ReadOnlyCollection<(ddouble c, ddouble d)> Table(ddouble nu) {
                    int m = ess_coef_table.Count - 1;

                    ddouble squa_nu = nu * nu;
                    List<(ddouble c, ddouble d)> cds = [];
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

            public static class AmosPowerSeries {
                private static readonly ReadOnlyCollection<ddouble> g1_coef = new(Consts.BesselKAmosG1.G1.Reverse().ToArray());

                private static readonly ConcurrentDictionary<ddouble, (ddouble, ddouble)> gammapm_table = [];
                private static readonly ConcurrentDictionary<ddouble, (ddouble, ddouble)> gamma12_table = [];

                public static Complex BesselK(ddouble nu, Complex z) {
                    Complex y = BesselKKernel(nu, z);

                    if (IsNaN(y) && !IsNaN(z)) {
                        y = ddouble.PositiveInfinity;
                    }

                    return y;
                }

                private static Complex BesselKKernel(ddouble nu, Complex z) {
                    Debug.Assert(nu >= 0d);

                    int n = (int)ddouble.Round(nu);
                    ddouble alpha = nu - n;

                    if (n == 0) {
                        Complex k0 = BesselKNearZeroNu(alpha, z, terms: 20);

                        return k0;
                    }
                    else if (n == 1) {
                        Complex k1 = BesselKNearOneNu(alpha, z, terms: 21);

                        return k1;
                    }
                    else {
                        Complex kn = BesselKNearIntNu(n, alpha, z, terms: 21);

                        return kn;
                    }
                }

                private static Complex BesselKNearZeroNu(ddouble alpha, Complex z, int terms) {
                    ddouble alpha2 = alpha * alpha;
                    Complex s = 1d / z, t = Log(2d * s), mu = alpha * t;
                    (ddouble g1, ddouble g2) = Gamma12(alpha);
                    (ddouble gp, ddouble gm) = GammaPM(alpha);

                    Complex f = (g1 * Cosh(mu) + g2 * t * Sinhc(mu)) / ddouble.Sinc(alpha);
                    Complex r = Pow(z / 2d, alpha);
                    Complex p = gp / (r * 2d), q = gm * r * 0.5d;

                    Complex c = f, v = Ldexp(z * z, -2), u = v;

                    for (int k = 1; k <= terms; k++) {
                        f = (k * f + p + q) / (k * k - alpha2);
                        c = SeriesUtil.Add(c, u, f, out bool convergence);

                        if (convergence && ILogB(f) >= -4) {
                            break;
                        }

                        p /= k - alpha;
                        q /= k + alpha;
                        u *= v / (k + 1);
                    }

                    return c;
                }

                private static Complex BesselKNearOneNu(ddouble alpha, Complex z, int terms) {
                    ddouble alpha2 = alpha * alpha;
                    Complex s = 1d / z, t = Log(2d * s), mu = alpha * t;
                    (ddouble g1, ddouble g2) = Gamma12(alpha);
                    (ddouble gp, ddouble gm) = GammaPM(alpha);

                    Complex f = (g1 * Cosh(mu) + g2 * t * Sinhc(mu)) / ddouble.Sinc(alpha);
                    Complex r = Pow(z / 2d, alpha);
                    Complex p = gp / (r * 2d), q = gm * r * 0.5d;

                    Complex c = p, v = Ldexp(z * z, -2), u = v;

                    for (int k = 1; k <= terms; k++) {
                        f = (k * f + p + q) / (k * k - alpha2);
                        p /= k - alpha;
                        c = SeriesUtil.Add(c, u, p, -k * f, out bool convergence);

                        if (convergence && ILogB(f) >= -4) {
                            break;
                        }

                        q /= k + alpha;
                        u *= v / (k + 1);
                    }

                    c *= 2d * s;

                    return c;
                }

                private static Complex BesselKNearIntNu(int n, ddouble alpha, Complex z, int terms) {
                    ddouble alpha2 = alpha * alpha;
                    Complex s = 1d / z, t = Log(2d * s), mu = alpha * t;
                    (ddouble g1, ddouble g2) = Gamma12(alpha);
                    (ddouble gp, ddouble gm) = GammaPM(alpha);

                    Complex f = (g1 * Cosh(mu) + g2 * t * Sinhc(mu)) / ddouble.Sinc(alpha);
                    Complex r = Pow(z / 2d, alpha);
                    Complex p = gp / (r * 2d), q = gm * r * 0.5d;

                    Complex c0 = f, c1 = p, v = Ldexp(z * z, -2), u = v;

                    for (int k = 1; k <= terms; k++) {
                        f = (k * f + p + q) / (k * k - alpha2);
                        p /= k - alpha;
                        c0 = SeriesUtil.Add(c0, u, f, out bool convergence0);
                        c1 = SeriesUtil.Add(c1, u, p, -k * f, out bool convergence1);

                        if (convergence0 && convergence1 && ILogB(f) >= -4) {
                            break;
                        }

                        q /= k + alpha;
                        u *= v / (k + 1);
                    }

                    c1 *= 2d * s;

                    for (int k = 1; k < n; k++) {
                        (c1, c0) = (ddouble.Ldexp(k + alpha, 1) * s * c1 + c0, c1);
                    }

                    return c1;
                }

                public static (ddouble g1, ddouble g2) Gamma12(ddouble nu) {
                    Debug.Assert(ddouble.Abs(nu) <= 0.5);

                    if (!gamma12_table.TryGetValue(nu, out (ddouble g1, ddouble g2) g)) {
                        (ddouble gp, ddouble gm) = GammaPM(nu);

                        ddouble nu2 = nu * nu;

                        ddouble g1 = g1_coef[0];

                        for (int i = 1; i < g1_coef.Count; i++) {
                            g1 = g1 * nu2 + g1_coef[i];
                        }

                        ddouble g2 = (1d / gm + 1d / gp) / 2d;

                        g = (g1, g2);

                        gamma12_table[nu] = g;
                    }

                    return g;
                }

                public static (ddouble gp, ddouble gm) GammaPM(ddouble nu) {
                    Debug.Assert(ddouble.Abs(nu) <= 0.5);

                    if (!gammapm_table.TryGetValue(nu, out (ddouble gp, ddouble gm) g)) {
                        g = (ddouble.Gamma(1d + nu), ddouble.Gamma(1d - nu));

                        gammapm_table[nu] = g;
                    }

                    return g;
                }


            }

            public static class Recurrence {

                public static Complex BesselJ(ddouble nu, Complex z) {
                    Debug.Assert(nu >= DirectMaxN || nu <= -DirectMaxN);

                    ddouble nu_abs = ddouble.Abs(nu);
                    int n = (int)ddouble.Floor(nu_abs);
                    ddouble alpha = nu_abs - n;

                    Complex v = 1d / z;

                    if (ddouble.IsPositive(nu)) {
                        (Complex a0, Complex b0, Complex a1, Complex b1) = (1d, 0d, 0d, 1d);

                        Complex r = Ldexp(nu_abs * v, 1);
                        (a0, b0, a1, b1) = (a1, b1, r * a1 + a0, r * b1 + b0);

                        Complex s = 1d;

                        for (int i = 1; i <= 1024; i++) {
                            r = Ldexp((nu_abs + i) * v, 1);

                            (a0, b0, a1, b1) = (a1, b1, r * a1 - a0, r * b1 - b0);
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

                        long exp_sum = 0;
                        bool s_overone = s.Magnitude > 1d;
                        (Complex j0, Complex j1) = s_overone ? (One, 1d / s) : (s, 1d);

                        for (int k = n - 1; k >= DirectMaxN - 1; k--) {
                            (j1, j0) = (ddouble.Ldexp(k + alpha, 1) * v * j1 - j0, j1);

                            int j0_exp = ILogB(j0), j1_exp = ILogB(j1);
                            if (int.Sign(j0_exp) * int.Sign(j1_exp) > 0) {
                                int exp = j0_exp > 0 ? int.Max(j0_exp, j1_exp) : int.Min(j0_exp, j1_exp);
                                exp_sum += exp;
                                (j0, j1) = (Ldexp(j0, -exp), Ldexp(j1, -exp));
                            }
                        }

                        Complex y = Ldexp(
                            j0.Magnitude >= j1.Magnitude
                            ? Complex.BesselJ(alpha + (DirectMaxN - 1), z) / j0
                            : Complex.BesselJ(alpha + (DirectMaxN - 2), z) / j1,
                            (int)long.Clamp(-exp_sum, int.MinValue, int.MaxValue)
                        ) * (s_overone ? 1d : s);

                        return y;
                    }
                    else {
                        if (NearlyInteger(nu, out int near_n)) {
                            return (near_n & 1) == 0 ? BesselJ(-near_n, z) : -BesselJ(-near_n, z);
                        }

                        return (ddouble.CosPi(nu / 2), ddouble.SinPi(nu / 2)) * BesselI(nu, (z.I, z.R)).Conj;
                    }
                }

                public static Complex BesselY(ddouble nu, Complex z) {
                    Debug.Assert(nu >= DirectMaxN || nu <= -DirectMaxN);

                    if (ddouble.IsNegative(nu) && NearlyInteger(nu + 0.5d, out int n_p5)) {
                        Complex y = Recurrence.BesselJ(-nu, z);

                        return (n_p5 & 1) == 0 ? y : -y;
                    }

                    ddouble nu_abs = ddouble.Abs(nu);

                    Complex c = (ddouble.CosPi(nu_abs / 2), ddouble.SinPi(nu_abs / 2));
                    Complex bi = BesselI(nu_abs, (z.I, z.R));
                    Complex bk = BesselK(nu_abs, (z.I, z.R));

                    if (ddouble.IsPositive(nu)) {
                        Complex y = MulI(c) * bi.Conj - 2 * ddouble.RcpPi * (c * bk).Conj;

                        return y;
                    }
                    else {
                        Complex y = MulI((c * bi).Conj)
                            + 2 * ddouble.RcpPi * (MulI(c.Conj) * ddouble.SinPi(nu_abs) - c) * bk.Conj;

                        return y;
                    }
                }

                public static Complex BesselI(ddouble nu, Complex z) {
                    Debug.Assert(nu >= DirectMaxN || nu <= -DirectMaxN);

                    ddouble nu_abs = ddouble.Abs(nu);
                    int n = (int)ddouble.Floor(nu_abs);
                    ddouble alpha = nu_abs - n;

                    Complex v = 1d / z;

                    (Complex a0, Complex b0, Complex a1, Complex b1) = (1d, 0d, 0d, 1d);
                    Complex s = 1d;

                    for (int i = 0; i <= 1024; i++) {
                        Complex r = Ldexp((nu_abs + i) * v, 1);

                        (a0, b0, a1, b1) = (a1, b1, r * a1 + a0, r * b1 + b0);
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

                    if (!IsFinite(s)) {
                        return 0d;
                    }

                    long exp_sum = 0;
                    bool s_overone = s.Magnitude > 1d;
                    (Complex i0, Complex i1) = s_overone ? (One, 1d / s) : (s, 1d);

                    for (int k = n - 1; k >= DirectMaxN - 1; k--) {
                        (i1, i0) = (ddouble.Ldexp(k + alpha, 1) * v * i1 + i0, i1);

                        int j0_exp = ILogB(i0), j1_exp = ILogB(i1);
                        if (int.Sign(j0_exp) * int.Sign(j1_exp) > 0) {
                            int exp = j0_exp > 0 ? int.Max(j0_exp, j1_exp) : int.Min(j0_exp, j1_exp);
                            exp_sum += exp;
                            (i0, i1) = (Ldexp(i0, -exp), Ldexp(i1, -exp));
                        }
                    }

                    Complex y = Ldexp(
                        i0.Magnitude >= i1.Magnitude
                        ? Complex.BesselI(alpha + (DirectMaxN - 1), z) / i0
                        : Complex.BesselI(alpha + (DirectMaxN - 2), z) / i1,
                        (int)long.Clamp(-exp_sum, int.MinValue, int.MaxValue)
                    ) * (s_overone ? 1d : s);

                    if (ddouble.IsNegative(nu) && !ddouble.IsInteger(nu_abs)) {
                        Complex bk = 2d * ddouble.RcpPi * ddouble.SinPi(nu_abs) * Complex.BesselK(nu_abs, z);

                        y += bk;
                    }

                    return y;
                }

                public static Complex BesselK(ddouble nu, Complex z) {
                    nu = ddouble.Abs(nu);

                    Debug.Assert(nu >= DirectMaxN);

                    int n = (int)ddouble.Floor(nu);
                    ddouble alpha = nu - n;

                    Complex k0 = Complex.BesselK(alpha + (DirectMaxN - 2), z);
                    Complex k1 = Complex.BesselK(alpha + (DirectMaxN - 1), z);

                    if (IsZero(k0) && IsZero(k1)) {
                        return 0d;
                    }

                    long exp_sum = 0;

                    Complex v = 1d / z;

                    for (int k = DirectMaxN - 1; k < n; k++) {
                        (k1, k0) = (ddouble.Ldexp(k + alpha, 1) * v * k1 + k0, k1);

                        if (ILogB(k1) > 0) {
                            int exp = ILogB(k1);
                            exp_sum += exp;
                            (k0, k1) = (Ldexp(k0, -exp), Ldexp(k1, -exp));
                        }
                    }

                    k1 = Ldexp(k1, (int)long.Max(exp_sum, int.MinValue));

                    return k1;
                }
            }
        }
    }
}
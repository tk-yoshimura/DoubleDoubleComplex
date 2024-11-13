using DoubleDouble;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex Gamma(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Gamma(z.R);
            }

            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (z.R < ddouble.Point5) {
                Complex y = ddouble.Pi / (SinPi(z) * Gamma(One - z));

                return y;
            }
            else {
                static Complex stirling(Complex z) {
                    Complex x = Zero, u = One;
                    Complex v = One / z, w = v * v;

                    foreach (Complex t in Consts.Gamma.StirlingTable) {
                        Complex c = u * t;
                        x += c;
                        u *= w;
                    }

                    x *= v;

                    return x;
                }

                int rn = (int)double.Floor((double)z.R);
                ddouble rf = z.R - rn;

                double zid = (double)z.I, zrd = double.Sqrt(Consts.Gamma.StirlingConvergenceNorm - zid * zid);
                int rk = (int)double.Ceiling(zrd);

                if (double.IsNaN(zrd) || rn >= rk) {
                    Complex x = stirling(z);
                    Complex p = (z - 0.5d) * Log2(z);
                    Complex y = Consts.Gamma.SqrtPi2 * Pow2(p + (x - z) * ddouble.LbE);

                    return y;
                }
                else {
                    Complex u = (rf + rk, z.I);
                    Complex x = stirling(u);
                    Complex p = (u - 0.5d) * Log2(u);
                    Complex b = (x - u) * ddouble.LbE;
                    Complex m = z;

                    for (int k = rn + 1; k < rk; k++) {
                        m *= (rf + k, z.I);
                    }

                    int prn = (int)ddouble.Floor(p.R), brn = (int)ddouble.Floor(b.R);

                    p = (p.R - prn, p.I);
                    b = (b.R - brn, b.I);
                    Complex c = Consts.Gamma.SqrtPi2 * Pow2(p + b);

                    Complex y = Ldexp(c / m, prn + brn);

                    return y;
                }
            }
        }

        public static Complex LogGamma(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.IsPositive(z.R) ? ddouble.LogGamma(z.R) : Log(ddouble.Gamma(z.R));
            }

            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            static Complex pv(Complex z) {
                return (z.R, z.I % ddouble.Ldexp(ddouble.Pi, 1));
            };

            if (z.R < ddouble.Point5) {
                Complex y = Log(ddouble.Pi / SinPi(z)) - LogGamma(One - z);

                return pv(y);
            }
            else if ((z - 1d).Norm <= Consts.LogGamma.NearRoot) {
                Complex v = 1d - z;

                ReadOnlyCollection<ddouble> table = Consts.LogGamma.Near2BackwardCoefTable;

                Complex s = table[0];
                for (int i = 1; i < table.Count; i++) {
                    s = s * v + table[i];
                }

                s *= v;

                Complex y = s - Log1p(-v);

                return y;
            }
            else if ((z - 2d).Norm <= Consts.LogGamma.NearRoot) {
                Complex v = 2d - z;

                ReadOnlyCollection<ddouble> table = Consts.LogGamma.Near2BackwardCoefTable;

                Complex s = table[0];
                for (int i = 1; i < table.Count; i++) {
                    s = s * v + table[i];
                }

                Complex y = s * v;

                return y;
            }
            else {
                static Complex stirling(Complex z) {
                    Complex x = Zero, u = One;
                    Complex v = One / z, w = v * v;

                    foreach (Complex t in Consts.Gamma.StirlingTable) {
                        Complex c = u * t;
                        x += c;
                        u *= w;
                    }

                    x *= v;

                    Complex r = Consts.Gamma.StirlingLogBias - z + (z - ddouble.Point5) * Log(z);

                    Complex y = r + x;

                    return y;
                }

                int rn = (int)double.Floor((double)z.R);
                ddouble rf = z.R - rn;

                double zid = (double)z.I, zrd = double.Sqrt(Consts.Gamma.StirlingConvergenceNorm - zid * zid);
                int rk = (int)double.Ceiling(zrd);

                if (double.IsNaN(zrd) || rn >= rk) {
                    return pv(stirling(z));
                }

                Complex c = stirling((rf + rk, z.I));
                Complex m = z;
                for (int k = rn + 1; k < rk; k++) {
                    m *= (rf + k, z.I);
                }

                Complex y = c - Log(m);

                return pv(y);
            }
        }

        internal static partial class Consts {
            public static class Gamma {
                public const double StirlingConvergenceNorm = 230.5d;
                public static readonly ddouble SqrtPi2 = (+1, 1, 0xA06C98FFB1382CB2uL, 0xBE520FD739167717uL);
                public static readonly ddouble StirlingLogBias = ddouble.Log(SqrtPi2);

                public static readonly ReadOnlyCollection<ddouble> StirlingTable = new([
                    (+1, -4, 0xAAAAAAAAAAAAAAAAuL, 0xAAAAAAAAAAAAAAAAuL),
                    (-1, -9, 0xB60B60B60B60B60BuL, 0x60B60B60B60B60B6uL),
                    (+1, -11, 0xD00D00D00D00D00DuL, 0x00D00D00D00D00D0uL),
                    (-1, -11, 0x9C09C09C09C09C09uL, 0xC09C09C09C09C09CuL),
                    (+1, -11, 0xDCA8F158C7F91AB8uL, 0x7539C0372A3C5631uL),
                    (-1, -10, 0xFB5586CCC9E3E40FuL, 0xB5586CCC9E3E40FBuL),
                    (+1, -8, 0xD20D20D20D20D20DuL, 0x20D20D20D20D20D2uL),
                    (-1, -6, 0xF21436587A9CBEE1uL, 0x032547698BADCFF2uL),
                    (+1, -3, 0xB7F4B1C0F033FFD0uL, 0xC3B7F4B1C0F033FFuL),
                    (-1, 0, 0xB23B3808C0F9CF6DuL, 0xEDCE7312CC3EA607uL),
                    (+1, 3, 0xD672219167002D3AuL, 0x7A9C886459C00B4EuL),
                    (-1, 7, 0x9CD9292E6660D55BuL, 0x3F712EB9E07CA39DuL),
                    (+1, 11, 0x8911A740DA740DA7uL, 0x40DA740DA740DA74uL),
                    (-1, 15, 0x8D0CC570E255BF59uL, 0xFF6EEC24B48FF1B3uL),
                    (+1, 19, 0xA8D1044D3708D1C2uL, 0x19EE4FDC4469CCAEuL),
                ]);
            }

            public static class LogGamma {
                public const double NearRoot = 0.25d;
                public static readonly ReadOnlyCollection<ddouble> Near2BackwardCoefTable;

                static LogGamma() {
                    List<ddouble> near2_coef = [ddouble.EulerGamma - 1d];

                    for (int i = 2; i < ddouble.TaylorSequence.Count; i++) {
                        ddouble c = ddouble.HurwitzZeta(i, 2d) / i;

                        if (ILogB(Ldexp(c, -i)) < -105) {
                            break;
                        }

                        near2_coef.Add(c);
                    }

                    near2_coef.Reverse();

                    Near2BackwardCoefTable = new(near2_coef.ToArray());
                }
            }
        }
    }
}

using DoubleDouble;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using static DoubleDoubleComplex.Complex.ComplexAiryUtil;

namespace DoubleDoubleComplex {
    public partial class Complex {
        public static Complex AiryAi(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.AiryAi(z.R);
            }

            if (IsNaN(z)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return AiryAi(z.Conj).Conj;
            }

            if (z.Magnitude < NearZero) {
                Complex x2 = z * z;
                Complex s = z * NearZeroCoefs[0] + NearZeroCoefs[1];

                for (int i = 2; i + 1 < NearZeroCoefs.Count; i += 2) {
                    s = s * x2 - NearZeroCoefs[i];
                    s = s * z + NearZeroCoefs[i + 1];
                }

                s /= AiNearZeroC;

                return s;
            }
            else {
                Complex v = Sqrt(z), w = Ldexp(v * v * v * Rcp3, 1);

                if (ddouble.IsPositive(z.R)) {
                    Complex y = v * ddouble.RcpPI * RcpSqrt3 * BesselK(Rcp3, w);

                    return y;
                }
                else {
                    w = MulI(w);

                    Complex y = MulMinusI(v) * Rcp3 * (BesselJ(-Rcp3, w) + BesselJ(Rcp3, w));

                    return y;
                }
            }
        }

        public static Complex AiryBi(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.AiryBi(z.R);
            }

            if (IsNaN(z)) {
                return NaN;
            }

            if (ddouble.IsNegative(z.I)) {
                return AiryBi(z.Conj).Conj;
            }

            if (z.Magnitude < NearZero) {
                Complex z2 = z * z;
                Complex s = z * NearZeroCoefs[0] + NearZeroCoefs[1];

                for (int i = 2; i + 1 < NearZeroCoefs.Count; i += 2) {
                    s = s * z2 + NearZeroCoefs[i];
                    s = s * z + NearZeroCoefs[i + 1];
                }

                s /= BiNearZeroC;

                return s;
            }
            else {
                Complex v = Sqrt(z), w = Ldexp(v * v * v * Rcp3, 1);

                if (ddouble.IsPositive(z.R)) {
                    Complex y = v * RcpSqrt3 * (BesselI(-Rcp3, w) + BesselI(Rcp3, w));

                    return y;
                }
                else {
                    w = MulI(w);

                    Complex y = MulMinusI(v) * RcpSqrt3 * (BesselJ(-Rcp3, w) - BesselJ(Rcp3, w));

                    return y;
                }
            }
        }

        internal static class ComplexAiryUtil {
            public static ddouble Rcp3 { get; } = ddouble.Rcp(3);
            public static ddouble RcpSqrt3 { get; } = ddouble.Rcp(ddouble.Sqrt(3));
            public static ddouble Cbrt3 { get; } = ddouble.Cbrt(3);
            public static ddouble NearZero { get; } = double.ScaleB(1, -4);

            public static ddouble AiNearZeroC = Cbrt3 * Cbrt3 * ddouble.PI, BiNearZeroC = ddouble.Sqrt(Cbrt3) * ddouble.PI;

            public static ddouble Gamma1d3 = (+1, 1, 0xAB73BA9CA4178B3BuL, 0xB234FA4B356011B6uL);
            public static ddouble Gamma2d3 = (+1, 0, 0xAD53BC9461B3C655uL, 0x97A7F5B815934C85uL);

            public static readonly ReadOnlyCollection<ddouble> NearZeroCoefs;

            static ComplexAiryUtil() {
                NearZeroCoefs = GenerateNearZeroCoefs();
            }

            private static ReadOnlyCollection<ddouble> GenerateNearZeroCoefs() {
                ddouble[] coefs = new ddouble[17];

                coefs[0] = ddouble.Ldexp(Gamma1d3 * ddouble.Sqrt(3), -1);
                coefs[1] = ddouble.Ldexp(Gamma2d3 * Cbrt3 * ddouble.Sqrt(3), -1);
                coefs[2] = 0d;

                for (int k = 3; k < coefs.Length; k++) {
                    coefs[k] = coefs[k - 3] / ((k - 1) * k);
                }

                coefs = coefs.Where(v => !ddouble.IsZero(v)).Reverse().ToArray();

                return Array.AsReadOnly(coefs);
            }
        }
    }
}
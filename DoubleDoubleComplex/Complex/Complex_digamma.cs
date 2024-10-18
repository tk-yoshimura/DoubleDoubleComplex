using DoubleDouble;
using System.Collections.ObjectModel;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex Digamma(Complex z) {
            if (!ddouble.IsFinite(z.R) || AlmostReal(z)) {
                return ddouble.Digamma(z.R);
            }

            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (z.R < ddouble.Point5) {
                Complex y = Digamma(One - z) - ddouble.PI / TanPI(z);

                return y;
            }

            static Complex stirling(Complex z) {
                Complex v = One / z, w = v * v;
                Complex x = Zero, u = w;

                foreach (Complex t in Consts.Digamma.StirlingTable) {
                    Complex c = u * t;
                    x += c;
                    u *= w;
                }

                x += v / 2d;

                Complex y = Log(z) - x;

                return y;
            }

            if (z.Norm >= Consts.Digamma.StirlingConvergenceNorm) {
                return stirling(z);
            }
            else {
                int rn = (int)double.Floor((double)z.R);
                ddouble rf = z.R - rn;

                double zid = (double)z.I, zrd = double.Sqrt(Consts.Digamma.StirlingConvergenceNorm - zid * zid);
                int rk = (int)double.Ceiling(zrd);

                if (double.IsNaN(zrd) || rn >= rk) {
                    return stirling(z);
                }

                Complex c = stirling((rf + rk, z.I));
                Complex m = One / z;
                for (int k = rn + 1; k < rk; k++) {
                    m += One / (rf + k, z.I);
                }

                Complex y = c - m;

                return y;
            }
        }

        internal static partial class Consts {
            public static class Digamma {
                public const double StirlingConvergenceNorm = 462.25d;

                public static readonly ReadOnlyCollection<ddouble> StirlingTable = new([
                    (+1, -4, 0xAAAAAAAAAAAAAAAAuL, 0xAAAAAAAAAAAAAAAAuL),
                    (-1, -7, 0x8888888888888888uL, 0x8888888888888888uL),
                    (+1, -8, 0x8208208208208208uL, 0x2082082082082082uL),
                    (-1, -8, 0x8888888888888888uL, 0x8888888888888888uL),
                    (+1, -8, 0xF83E0F83E0F83E0FuL, 0x83E0F83E0F83E0F8uL),
                    (-1, -6, 0xACCACCACCACCACCAuL, 0xCCACCACCACCACCACuL),
                    (+1, -4, 0xAAAAAAAAAAAAAAAAuL, 0xAAAAAAAAAAAAAAAAuL),
                    (-1, -2, 0xE2F2F2F2F2F2F2F2uL, 0xF2F2F2F2F2F2F2F2uL),
                    (+1, 1, 0xC373FCDCFF373FCDuL, 0xCFF373FCDCFF373FuL),
                    (-1, 4, 0xD3A6528A6528A652uL, 0x8A6528A6528A6528uL),
                    (+1, 8, 0x8CBAE6076B981DAEuL, 0x6076B981DAE6076BuL),
                    (-1, 11, 0xE1782B32B32B32B3uL, 0x2B32B32B32B32B32uL),
                    (+1, 15, 0xD62B955555555555uL, 0x5555555555555555uL),
                    (-1, 19, 0xEE058D2E7DF0B2E7uL, 0xDF0B2E7DF0B2E7DFuL),
                    (+1, 24, 0x98FD6BE5F9DFFE17uL, 0xE77FF85F9DFFE17EuL),
                    (-1, 28, 0xE1402B1DC5E5E5E5uL, 0xE5E5E5E5E5E5E5E5uL),
                ]);
            }
        }
    }
}

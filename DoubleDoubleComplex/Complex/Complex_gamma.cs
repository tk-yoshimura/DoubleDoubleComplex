using DoubleDouble;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace DoubleDoubleComplex {

    public partial class Complex {
        public static Complex Gamma(Complex z) {
            if (!ddouble.IsFinite(z.R) || ddouble.Abs(z.I) <= ddouble.Abs(z.R) * 5e-31) {
                return ddouble.Gamma(z.R);
            }

            if (!ddouble.IsFinite(z.I)) {
                return NaN;
            }

            if (z.R < ddouble.Point5) {
                Complex y = ddouble.PI / (SinPI(z) * Gamma(One - z));

                return y;
            }

            static Complex stirling(Complex z) {
                Complex x = Zero, u = One;
                Complex v = One / z, w = v * v;

                foreach (Complex t in Consts.Gamma.StirlingTable) {
                    Complex c = u * t;
                    x += c;
                    u *= w;
                }

                x *= v;

                Complex r = Sqrt(2 * ddouble.PI / z);
                Complex p = Pow(z / ddouble.E, z);
                Complex s = Exp(x);

                Complex y = r * p * s;

                return y;
            }

            if (z.Norm >= Consts.Gamma.StirlingConvergenceNorm) {
                return stirling(z);
            }
            else {
                int rn = (int)double.Floor((double)z.R);
                ddouble rf = z.R - rn;

                double zid = (double)z.I, zrd = double.Sqrt(Consts.Gamma.StirlingConvergenceNorm - zid * zid);
                int rk = (int)double.Ceiling(zrd);

                if (double.IsNaN(zrd) || rn >= rk) {
                    return stirling(z);
                }

                Complex c = stirling((rf + rk, z.I));
                Complex m = z;
                for (int k = rn + 1; k < rk; k++) {
                    m *= (rf + k, z.I);
                }

                Complex y = c / m;

                return y;
            }
        }

    }

    internal static partial class Consts {
        public static class Gamma {
            public const double StirlingConvergenceNorm = 230.5d;

            public static readonly ReadOnlyCollection<ddouble> StirlingTable = new(new ddouble[] {
                "8.333333333333333333333333333333333333333e-2",
                "-2.777777777777777777777777777777777777778e-3",
                "7.936507936507936507936507936507936507937e-4",
                "-5.952380952380952380952380952380952380952e-4",
                "8.417508417508417508417508417508417508417e-4",
                "-1.917526917526917526917526917526917526918e-3",
                "6.41025641025641025641025641025641025641e-3",
                "-2.955065359477124183006535947712418300654e-2",
                "1.796443723688305731649384900158893966944e-1",
                "-1.392432216905901116427432216905901116427e0",
                "1.340286404416839199447895100069013112491e1",
                "-1.568482846260020173063651324520889738281e2",
                "2.193103333333333333333333333333333333333e3",
                "-3.610877125372498935717326521924223073648e4",
                "6.914722688513130671083952507756734675533e5",
            });
        }
    }
}

using DoubleDouble;
using System.Collections.ObjectModel;
using System.Linq;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Log(Complex z) {
            return new Complex(
                ddouble.Log(z.Norm),
                z.Phase
            );
        }

        public static Complex Log2(Complex z) {
            return new Complex(
                ddouble.Log2(z.Norm),
                z.Phase * ddouble.LbE
            );
        }

        public static Complex Log10(Complex z) {
            return new Complex(
                ddouble.Log10(z.Norm),
                z.Phase / ddouble.Log(10)
            );
        }

        public static Complex Log(Complex z, ddouble b) {
            return Log(z) / ddouble.Log(b);
        }

        public static Complex Log1p(Complex z) {
            if (z.R >= -0.125d && z.R <= 0.25d && ddouble.Abs(z.I) <= 0.125d) {
                ReadOnlyCollection<(ddouble c, ddouble d)> table = Consts.Log.PadeTable;

                (Complex sc, Complex sd) = table[0];
                for (int i = 1; i < table.Count; i++) {
                    (ddouble c, ddouble d) = table[i];

                    sc = sc * z + c;
                    sd = sd * z + d;
                }

                Complex y = z * sc / sd;

                return y;
            }
            else {
                Complex y = Log(1d + z);

                return y;
            }
        }

        internal static partial class Consts {
            public static class Log {
                public static readonly ReadOnlyCollection<(ddouble c, ddouble d)> PadeTable = new(new (ddouble c, ddouble d)[]{
                    ((+1, 0, 0x8000000000000000uL, 0x0000000000000000uL), (+1, 0, 0x8000000000000000uL, 0x0000000000000000uL)),
                    ((+1, 2, 0xE7BDEF7BDEF7BDEFuL, 0x7BDEF7BDEF7BDEF7uL), (+1, 2, 0xF7BDEF7BDEF7BDEFuL, 0x7BDEF7BDEF7BDEF7uL)),
                    ((+1, 4, 0xBC791E4791E4791EuL, 0x4791E4791E4791E4uL), (+1, 4, 0xD8C6318C6318C631uL, 0x8C6318C6318C6318uL)),
                    ((+1, 5, 0xB5DEAED7D945C433uL, 0x41C79E0AD2274C07uL), (+1, 5, 0xE2BDA695C8C1A32AuL, 0xFFB719E9C9E53B83uL)),
                    ((+1, 5, 0xE7A25EE1DF4F3870uL, 0x8998B052EC1FAA58uL), (+1, 6, 0x9DE8A64CE2AB6D10uL, 0x3B3AF20BF5BFA4E0uL)),
                    ((+1, 5, 0xCCE6A8024730B1B0uL, 0xD623E13EEEA12CB5uL), (+1, 6, 0x9A6652F5D7F19276uL, 0x454AB3C76D716E03uL)),
                    ((+1, 5, 0x8104B24051DDD1D5uL, 0x2427D4925BA82626uL), (+1, 5, 0xD9BE407B85DE8658uL, 0x05D25FFEFCCDE9EAuL)),
                    ((+1, 3, 0xE93018F35244CDBBuL, 0xA159D5D409D145A8uL), (+1, 4, 0xDFF6E33D393CA770uL, 0x7B04457B4D2B9183uL)),
                    ((+1, 2, 0x9683640911F605D7uL, 0xC65F729701F37DDAuL), (+1, 3, 0xA7F92A6DEAED7D94uL, 0x5C43341C79E0AD22uL)),
                    ((+1, 0, 0x886B4928EDA1C84BuL, 0x97358831F86EA744uL), (+1, 1, 0xB5C4A995DCD4790DuL, 0x602125D715D1BC97uL)),
                    ((+1, -3, 0xA82C520F1DA91194uL, 0xF8371EA4531520FCuL), (+1, -1, 0x8ACE087BBFE811F7uL, 0x9889059F97A4D12DuL)),
                    ((+1, -6, 0x85BF51846D8678E9uL, 0x34EFD975CC402713uL), (+1, -4, 0x903679DA56582D43uL, 0xBC6676E1A786AAC8uL)),
                    ((+1, -11, 0xFB869933CFC6BF4BuL, 0x35EAEB215A12250FuL), (+1, -8, 0xC048A27873203C5AuL, 0x50889E8234B38E60uL)),
                    ((+1, -16, 0xEF16B468EE704C25uL, 0x8A63E022A43FDBDFuL), (+1, -12, 0x9577AF0162D18B28uL, 0x25BA00E9E06A6874uL)),
                    ((+1, -22, 0xA2E2B81180621DA9uL, 0x9E29D10092D12612uL), (+1, -18, 0xE3C290C528DDC7DBuL, 0xA733D0A155F77A98uL)),
                    ((+1, -33, 0xE4A737FD2603CBA7uL, 0x4E8252F44A41BC55uL), (+1, -25, 0xE4A737FD2603CBA7uL, 0x4E8252F44A41BC55uL))
                 }.Reverse().ToArray());
            }
        }
    }
}
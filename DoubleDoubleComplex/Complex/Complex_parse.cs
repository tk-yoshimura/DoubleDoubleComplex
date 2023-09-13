using System;
using DoubleDouble;
using System.Diagnostics.CodeAnalysis;

namespace DoubleDoubleComplex {

    public partial class Complex : IParsable<Complex> {
        public static Complex Parse(string s) {
            if (string.IsNullOrEmpty(s) || s.Length < 1) {
                throw new FormatException($"Invalid numeric string. : {s}");
            }

            (ddouble v, bool set) r = (ddouble.Zero, false), i = (ddouble.Zero, false);

            int index_e0 = (s[0] == '+' || s[0] == '-') ? 1 : 0;
            int index_e1 = int.Max(s.IndexOf('+', index_e0), s.IndexOf('-', index_e0));

            string e0 = (index_e1 > 0) ? s[..index_e1] : s;
            string e1 = (index_e1 > 0) ? s[index_e1..] : string.Empty;

            ValidateElem(e0, out ddouble v0, out char s0);
            ValidateElem(e1, out ddouble v1, out char s1);

            if (s0 == 'r') {
                r = (v0, true);
            }
            else if (s0 == 'i') {
                i = (v0, true);
            }

            if (s1 == 'r') {
                if (r.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                r = (v1, true);
            }
            else if (s1 == 'i') {
                if (i.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                i = (v1, true);
            }

            return new Complex(r.v, i.v);
        }

        public static Complex Parse(string s, IFormatProvider provider) {
            return Parse(s);
        }

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out Complex result) {
            try {
                result = Parse(s);
                return true;
            }
            catch (FormatException) {
                result = Zero;
                return false;
            }
        }

        internal static void ValidateElem(string s, out ddouble v, out char symbol) {
            if (string.IsNullOrEmpty(s)) {
                (v, symbol) = (ddouble.Zero, '_');
            }
            else {
                (v, symbol) = s[^1] switch {
                    'i' => (s[..^1], 'i'),
                    >= '0' and <= '9' => (s, 'r'),
                    _ => throw new FormatException($"Invalid numeric string. : {s}")
                };
            }
        }
    }
}

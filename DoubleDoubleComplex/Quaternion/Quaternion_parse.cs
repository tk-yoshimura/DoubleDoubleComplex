using DoubleDouble;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DoubleDoubleComplex {

    public partial class Quaternion : IParsable<Quaternion> {
        public static Quaternion Parse(string s) {
            if (string.IsNullOrEmpty(s) || s.Length < 1) {
                throw new FormatException($"Invalid numeric string. : {s}");
            }

            (ddouble v, bool set) r = (ddouble.Zero, false), i = (ddouble.Zero, false);
            (ddouble v, bool set) j = (ddouble.Zero, false), k = (ddouble.Zero, false);

            int index_e0 = (s[0] == '+' || s[0] == '-') ? 1 : 0;
            int index_e1 = s.IndexOfAny(new[] { '+', '-' }, index_e0);
            int index_e2 = (index_e1 > 0) ? s.IndexOfAny(new[] { '+', '-' }, index_e1 + 1) : -1;
            int index_e3 = (index_e2 > 0) ? s.IndexOfAny(new[] { '+', '-' }, index_e2 + 1) : -1;

            string e0 = (index_e1 > 0) ? s[..index_e1] : s;
            string e1 = (index_e1 > 0) ? ((index_e2 > 0) ? s[index_e1..index_e2] : s[index_e1..]) : string.Empty;
            string e2 = (index_e2 > 0) ? ((index_e3 > 0) ? s[index_e2..index_e3] : s[index_e2..]) : string.Empty;
            string e3 = (index_e3 > 0) ? s[index_e3..] : string.Empty;

            ValidateElem(e0, out ddouble v0, out char s0);
            ValidateElem(e1, out ddouble v1, out char s1);
            ValidateElem(e2, out ddouble v2, out char s2);
            ValidateElem(e3, out ddouble v3, out char s3);

            if (s0 == 'r') {
                r = (v0, true);
            }
            else if (s0 == 'i') {
                i = (v0, true);
            }
            else if (s0 == 'j') {
                j = (v0, true);
            }
            else if (s0 == 'k') {
                k = (v0, true);
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
            else if (s1 == 'j') {
                if (j.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                j = (v1, true);
            }
            else if (s1 == 'k') {
                if (k.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                k = (v1, true);
            }

            if (s2 == 'r') {
                if (r.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                r = (v2, true);
            }
            else if (s2 == 'i') {
                if (i.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                i = (v2, true);
            }
            else if (s2 == 'j') {
                if (j.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                j = (v2, true);
            }
            else if (s2 == 'k') {
                if (k.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                k = (v2, true);
            }

            if (s3 == 'r') {
                if (r.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                r = (v3, true);
            }
            else if (s3 == 'i') {
                if (i.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                i = (v3, true);
            }
            else if (s3 == 'j') {
                if (j.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                j = (v3, true);
            }
            else if (s3 == 'k') {
                if (k.set) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                k = (v3, true);
            }

            return new Quaternion(r.v, i.v, j.v, k.v);
        }

        public static Quaternion Parse(string s, IFormatProvider provider) {
            return Parse(s);
        }

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out Quaternion result) {
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
                    'j' => (s[..^1], 'j'),
                    'k' => (s[..^1], 'k'),
                    >= '0' and <= '9' => (s, 'r'),
                    _ => throw new FormatException($"Invalid numeric string. : {s}")
                };
            }
        }
    }
}

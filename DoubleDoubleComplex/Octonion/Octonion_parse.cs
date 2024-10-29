using DoubleDouble;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace DoubleDoubleComplex {

    public partial class Octonion : IParsable<Octonion> {
        public static Octonion Parse(string s) {
            if (string.IsNullOrEmpty(s) || s.Length < 1 || s.Contains('|') || s.Contains('#')) {
                throw new FormatException($"Invalid numeric string. : {s}");
            }

            StringBuilder str = new(s.ToLower());
            s = str.Replace("e+", "#P").Replace("e-", "#M").Replace("+", "|+").Replace("-", "|-").Replace("#P", "e+").Replace("#M", "e-").ToString();

            string[] terms = s.Split("|", StringSplitOptions.RemoveEmptyEntries);

            ddouble[] elems = new ddouble[8];
            bool[] elem_sets = new bool[8];

            foreach (string term in terms) {
                ValidateElem(term, out ddouble v, out int index_elem);

                if (elem_sets[index_elem]) {
                    throw new FormatException($"Invalid numeric string. : {s}");
                }

                if (index_elem >= 0) {
                    elems[index_elem] = v;
                    elem_sets[index_elem] = true;
                }
            }

            if (!elem_sets.Any(b => b)) {
                throw new FormatException($"Invalid numeric string. : {s}");
            }

            Octonion o = new(elems[0], elems[1], elems[2], elems[3], elems[4], elems[5], elems[6], elems[7]);

            return o;
        }

        public static Octonion Parse(string s, IFormatProvider provider) {
            return Parse(s);
        }

        public static bool TryParse([NotNullWhen(true)] string s, IFormatProvider provider, [MaybeNullWhen(false)] out Octonion result) {
            try {
                result = Parse(s);
                return true;
            }
            catch (FormatException) {
                result = Zero;
                return false;
            }
        }

        private static void ValidateElem(string s, out ddouble v, out int index_elem) {
            if (string.IsNullOrEmpty(s)) {
                (v, index_elem) = (ddouble.Zero, -1);
            }
            else {
                (v, index_elem) = s[^1] switch {
                    's' => (s[..^1], 1),
                    't' => (s[..^1], 2),
                    'u' => (s[..^1], 3),
                    'w' => (s[..^1], 4),
                    'x' => (s[..^1], 5),
                    'y' => (s[..^1], 6),
                    'z' => (s[..^1], 7),
                    >= '0' and <= '9' => (s, 0),
                    _ => throw new FormatException($"Invalid numeric string. : {s}")
                };
            }
        }
    }
}

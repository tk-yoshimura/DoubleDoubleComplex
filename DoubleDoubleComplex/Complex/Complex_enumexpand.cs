using DoubleDouble;
using System.Collections.Generic;
using System.Linq;

namespace DoubleDoubleComplex {

    public static class ComplexEnumerableExpand {
        public static Complex Sum(this IEnumerable<Complex> source) {
            Complex acc = Complex.Zero, carry = Complex.Zero;

            foreach (var v in source) {
                Complex d = v - carry;
                Complex acc_next = acc + d;

                carry = (acc_next - acc) - d;
                acc = acc_next;
            }

            return acc;
        }

        public static Complex Average(this IEnumerable<Complex> source) {
            return source.Sum() / source.Count();
        }

        public static IEnumerable<ddouble> R(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.R;
            }
        }

        public static IEnumerable<ddouble> I(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.I;
            }
        }

        public static IEnumerable<ddouble> Norm(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.Norm;
            }
        }

        public static IEnumerable<ddouble> Magnitude(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.Magnitude;
            }
        }

        public static IEnumerable<ddouble> Phase(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.Phase;
            }
        }

        public static IEnumerable<Complex> Conjugate(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return v.Conj;
            }
        }

        public static IEnumerable<Complex> Normal(this IEnumerable<Complex> source) {
            foreach (var v in source) {
                yield return Complex.Normal(v);
            }
        }
    }
}

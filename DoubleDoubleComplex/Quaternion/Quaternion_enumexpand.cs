using DoubleDouble;
using System.Collections.Generic;
using System.Linq;

namespace DoubleDoubleComplex {

    public static class QuaternionEnumerableExpand {
        public static Quaternion Sum(this IEnumerable<Quaternion> source) {
            Quaternion acc = Quaternion.Zero, carry = Quaternion.Zero;

            foreach (var v in source) {
                Quaternion d = v - carry;
                Quaternion acc_next = acc + d;

                carry = (acc_next - acc) - d;
                acc = acc_next;
            }

            return acc;
        }

        public static Quaternion Average(this IEnumerable<Quaternion> source) {
            return source.Sum() / source.Count();
        }

        public static IEnumerable<ddouble> R(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.R;
            }
        }

        public static IEnumerable<ddouble> I(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.I;
            }
        }

        public static IEnumerable<ddouble> J(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.J;
            }
        }

        public static IEnumerable<ddouble> K(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.K;
            }
        }

        public static IEnumerable<ddouble> Norm(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.Norm;
            }
        }

        public static IEnumerable<ddouble> Magnitude(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.Magnitude;
            }
        }

        public static IEnumerable<Quaternion> Conjugate(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return v.Conj;
            }
        }

        public static IEnumerable<Quaternion> Normal(this IEnumerable<Quaternion> source) {
            foreach (var v in source) {
                yield return Quaternion.Normal(v);
            }
        }
    }
}
using DoubleDouble;
using System.Collections.Generic;
using System.Linq;

namespace DoubleDoubleComplex {

    public static class OctonionEnumerableExpand {
        public static Octonion Sum(this IEnumerable<Octonion> source) {
            Octonion acc = Octonion.Zero, carry = Octonion.Zero;

            foreach (var v in source) {
                Octonion d = v - carry;
                Octonion acc_next = acc + d;

                carry = (acc_next - acc) - d;
                acc = acc_next;
            }

            return acc;
        }

        public static Octonion Average(this IEnumerable<Octonion> source) {
            return source.Sum() / source.Count();
        }

        public static IEnumerable<ddouble> R(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.R;
            }
        }

        public static IEnumerable<ddouble> I(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.I;
            }
        }

        public static IEnumerable<ddouble> J(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.J;
            }
        }

        public static IEnumerable<ddouble> K(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.K;
            }
        }

        public static IEnumerable<ddouble> W(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.W;
            }
        }

        public static IEnumerable<ddouble> X(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.X;
            }
        }

        public static IEnumerable<ddouble> Y(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Y;
            }
        }

        public static IEnumerable<ddouble> Z(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Z;
            }
        }

        public static IEnumerable<ddouble> SquareNorm(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.SquareNorm;
            }
        }

        public static IEnumerable<ddouble> Norm(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Norm;
            }
        }

        public static IEnumerable<ddouble> Magnitude(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Magnitude;
            }
        }

        public static IEnumerable<Octonion> Conjugate(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Conj;
            }
        }

        public static IEnumerable<Octonion> Normal(this IEnumerable<Octonion> source) {
            foreach (var v in source) {
                yield return v.Normal;
            }
        }
    }
}
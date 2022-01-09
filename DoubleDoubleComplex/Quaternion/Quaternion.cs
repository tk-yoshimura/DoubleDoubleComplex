﻿using DoubleDouble;
using System.Diagnostics;

namespace DoubleDoubleComplex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Quaternion {
        public readonly ddouble R, I, J, K;

        public ddouble Norm => R * R + I * I + J * J + K * K;

        public ddouble Magnitude => ddouble.Sqrt(Norm);

        public static Complex Zero { get; } = ddouble.Zero;

        public static Complex NaN { get; } = ddouble.NaN;

        public Quaternion(ddouble r, ddouble i, ddouble j, ddouble k) {
            this.R = r;
            this.I = i;
            this.J = j;
            this.K = k;
        }

        public static implicit operator Quaternion((ddouble r, ddouble i, ddouble j, ddouble k) v) {
            return new(v.r, v.i, v.j, v.k);
        }

        public static implicit operator Quaternion(int v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion(double v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator Quaternion(ddouble v) {
            return new(v, ddouble.Zero, ddouble.Zero, ddouble.Zero);
        }

        public static implicit operator (ddouble r, ddouble i, ddouble j, ddouble k)(Quaternion v) {
            return (v.R, v.I, v.J, v.K);
        }

        public static implicit operator Quaternion(System.Numerics.Quaternion q) {
            return new Quaternion(q.W, q.X, q.Y, q.Z);
        }

        public static explicit operator System.Numerics.Quaternion(Quaternion q) {
            return new System.Numerics.Quaternion((float)(double)q.I, (float)(double)q.J, (float)(double)q.K, (float)(double)q.R);
        }

        public void Deconstruct(out ddouble r, out ddouble i, out ddouble j, out ddouble k)
            => (r, i, j, k) = (R, I, J, K);

        public override string ToString() {
            if (IsNaN(this)) {
                return double.NaN.ToString();
            }

            string str = string.Empty;

            str += R == 0 ? string.Empty : $"{R}";
            str += I == 0 ? string.Empty : I > 0 ? $"+{I}i" : $"{I}i";
            str += J == 0 ? string.Empty : J > 0 ? $"+{J}j" : $"{J}j";
            str += K == 0 ? string.Empty : K > 0 ? $"+{K}k" : $"{K}k";

            str = str.TrimStart('+');

            return (str.Length > 0) ? str : "0";
        }
    }
}

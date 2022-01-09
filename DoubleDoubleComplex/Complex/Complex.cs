using System;
using System.Diagnostics;

namespace DoubleDouble.Complex {

    [DebuggerDisplay("{ToString(),nq}")]
    public partial class Complex {
        public const char ImaginaryUnit = 'i';

        public readonly ddouble R, I;

        public ddouble Norm => R * R + I * I;

        public ddouble Magnitude => ddouble.Sqrt(Norm);

        public ddouble Phase => ddouble.Atan2(I, R);

        public Complex(ddouble r, ddouble i) {
            this.R = r;
            this.I = i;
        }

        public static implicit operator Complex((ddouble r, ddouble i) v) {
            return new(v.r, v.i);
        }

        public static implicit operator Complex(ddouble v) {
            return new(v, ddouble.Zero);
        }

        public static implicit operator (ddouble r, ddouble i)(Complex v) {
            return (v.R, v.I);
        }

        public void Deconstruct(out ddouble r, out ddouble i) => (r, i) = (R, I);

        public override string ToString() {
            if (R != 0d) {
                if (I == 0d) {
                    return $"{R}";
                }

                return (I > 0d) ? $"{R}+{I}{ImaginaryUnit}" : $"{R}{I}{ImaginaryUnit}";
            }
            else { 
                return $"{I}{ImaginaryUnit}";
            }
        }
    }
}

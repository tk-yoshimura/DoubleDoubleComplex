using DoubleDouble;
using System.Collections.ObjectModel;
using System.Linq;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static Complex Asin(Complex z) {
            if (ddouble.Abs(z.R) <= 0.125 && ddouble.Abs(z.I) <= 0.125) {
                ReadOnlyCollection<ddouble> table = Consts.Asin.TaylorTable;

                Complex z2 = z * z, s = table[0];
                for (int i = 1; i < table.Count; i++) {
                    s = s * z2 + table[i];
                }

                Complex y = z * s;

                return y;
            }
            else {
                return -ImaginaryOne * Log(ImaginaryOne * z + Sqrt(1d - z * z));
            }
        }

        public static Complex Acos(Complex z) {
            return -ImaginaryOne * Log(z + ImaginaryOne * Sqrt(1d - z * z));
        }

        public static Complex Atan(Complex z) {
            return ImaginaryOne / 2 * (Log1p(-ImaginaryOne * z) - Log1p(ImaginaryOne * z));
        }

        internal static partial class Consts {
            public static class Asin {
                public static readonly ReadOnlyCollection<ddouble> TaylorTable = new(new ddouble[]{
                    (+1, 0, 0x8000000000000000uL, 0x0000000000000000uL),
                    (+1, -3, 0xAAAAAAAAAAAAAAAAuL, 0xAAAAAAAAAAAAAAAAuL),
                    (+1, -4, 0x9999999999999999uL, 0x9999999999999999uL),
                    (+1, -5, 0xB6DB6DB6DB6DB6DBuL, 0x6DB6DB6DB6DB6DB6uL),
                    (+1, -6, 0xF8E38E38E38E38E3uL, 0x8E38E38E38E38E38uL),
                    (+1, -6, 0xB745D1745D1745D1uL, 0x745D1745D1745D17uL),
                    (+1, -6, 0x8E27627627627627uL, 0x6276276276276276uL),
                    (+1, -7, 0xE4CCCCCCCCCCCCCCuL, 0xCCCCCCCCCCCCCCCCuL),
                    (+1, -7, 0xBD43C3C3C3C3C3C3uL, 0xC3C3C3C3C3C3C3C3uL),
                    (+1, -7, 0x9FEF286BCA1AF286uL, 0xBCA1AF286BCA1AF2uL),
                    (+1, -7, 0x89779E79E79E79E7uL, 0x9E79E79E79E79E79uL),
                    (+1, -8, 0xEF9DE9BD37A6F4DEuL, 0x9BD37A6F4DE9BD37uL),
                    (+1, -8, 0xD3431EB851EB851EuL, 0xB851EB851EB851EBuL),
                    (+1, -8, 0xBC16ED097B425ED0uL, 0x97B425ED097B425EuL),
                    (+1, -8, 0xA8DD18469EE58469uL, 0xEE58469EE58469EEuL),
                    (+1, -8, 0x98B41DEF7BDEF7BDuL, 0xEF7BDEF7BDEF7BDEuL),
                    (+1, -8, 0x8AF74EA2E8BA2E8BuL, 0xA2E8BA2E8BA2E8BAuL),
                    (+1, -9, 0xFE57C7DB6DB6DB6DuL, 0xB6DB6DB6DB6DB6DBuL),
                    (+1, -9, 0xE9E954706EB3E453uL, 0x06EB3E45306EB3E4uL),
                    (+1, -9, 0xD8137ABD89D89D89uL, 0xD89D89D89D89D89DuL),
                 }.Reverse().ToArray());
            }
        }
    }
}
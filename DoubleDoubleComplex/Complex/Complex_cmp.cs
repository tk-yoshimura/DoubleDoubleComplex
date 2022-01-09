using System;
using System.Diagnostics;

using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Complex {

        public static bool IsNaN(Complex c) => ddouble.IsNaN(c.R) || ddouble.IsNaN(c.I);

        public static bool IsFinite(Complex c) => ddouble.IsFinite(c.R) && ddouble.IsFinite(c.I);

        public static bool IsZero(Complex c) => ddouble.IsZero(c.R) && ddouble.IsZero(c.I);
    }
}

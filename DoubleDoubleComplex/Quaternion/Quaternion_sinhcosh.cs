﻿namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion Sinh(Quaternion q) {
            Quaternion q_pexp = Exp(q), q_mexp = Exp(-q);

            return Ldexp(q_pexp - q_mexp, -1);
        }

        public static Quaternion Cosh(Quaternion q) {
            Quaternion q_pexp = Exp(q), q_mexp = Exp(-q);

            return Ldexp(q_pexp + q_mexp, -1);
        }

        public static Quaternion Tanh(Quaternion q) {
            Quaternion q_pexp = Exp(q), q_mexp = Exp(-q);

            return (q_pexp - q_mexp) / (q_pexp + q_mexp);
        }
    }
}

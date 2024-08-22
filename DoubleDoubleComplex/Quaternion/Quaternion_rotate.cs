using DoubleDouble;

namespace DoubleDoubleComplex {

    public partial class Quaternion {

        public static Quaternion FromAxisAngle((ddouble x, ddouble y, ddouble z) axis, ddouble angle) {
            ddouble half_angle = angle / 2;
            ddouble c = ddouble.Cos(half_angle), s = ddouble.Sin(half_angle);

            return new Quaternion(c, s * axis.x, s * axis.y, s * axis.z);
        }

        public static Quaternion FromYawPitchRoll(ddouble yaw, ddouble pitch, ddouble roll) {
            ddouble half_yaw = yaw / 2, half_pitch = pitch / 2, half_roll = roll / 2;

            ddouble cy = ddouble.Cos(half_yaw), sy = ddouble.Sin(half_yaw);
            ddouble cp = ddouble.Cos(half_pitch), sp = ddouble.Sin(half_pitch);
            ddouble cr = ddouble.Cos(half_roll), sr = ddouble.Sin(half_roll);

            return new Quaternion(
                cy * cp * cr + sy * sp * sr,
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr
            );
        }
    }
}

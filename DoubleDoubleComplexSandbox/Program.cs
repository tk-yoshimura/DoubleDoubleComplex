using DoubleDouble;
using DoubleDoubleComplex;
using System.Security.Cryptography;

namespace DoubleDoubleComplexSandbox {
    internal class Program {
        static void Main() {
            using StreamWriter sw = new("../../erf_cfrac_result_6.csv");

            for (ddouble i = 0; i <= 8.75; i += 1d / 1024) {
                ddouble r = 2 - 7d / 256 * i * i;
                
                if (r < 0) {
                    break;
                }

                Complex z = (r, i);

                int n = ErfNZ(z);

                sw.WriteLine($"{r},{i},{n}");

                sw.Flush();
            }

            sw.Close();

            Console.WriteLine("END");
            Console.Read();
        }

        static Complex ErfCFrac(Complex z, int n) {
            Complex w = z * z;

            Complex f = Complex.One;

            for (long k = 4 * n - 3; k >= 1; k -= 4) {
                Complex c0 = (k + 2) * f;
                Complex c1 = w * ((k + 3) + f * 2d);
                Complex d0 = (k + 1) * (k + 3) + (4 * k + 6) * f;
                Complex d1 = c1 * 2d;

                f = w + k * (c0 + c1) / (d0 + d1);
            }

            return f;
        }

        static int ErfNZ(Complex z) {
            Complex w = z * z;
            Complex c = Complex.One, u = -w;

            int k = 1;

            for (int convergence_time = 0; k < 256 && convergence_time < 4; k++) {
                Complex dc = u / (2 * k + 1);
                c += dc;

                u *= w / -(k + 1);

                if ((double.Abs((double)dc.R) <= double.Abs((double)c.R) * 1e-30) &&
                    (double.Abs((double)dc.I) <= double.Abs((double)c.I) * 1e-30)) {

                    convergence_time++;
                }
            }

            return k;
        }

        static bool UseCFrac(Complex z) {
            double zid = (double)z.I;
            double zr_thr = 2d - 7d / 256 * zid * zid;

            return zr_thr <= z.R;
        }
    }
}
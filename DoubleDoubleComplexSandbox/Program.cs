using DoubleDouble;
using DoubleDoubleComplex;

namespace DoubleDoubleComplexSandbox {
    internal class Program {
        static void Main() {
            const int max_n = 1024;
            using StreamWriter sw = new("../../erf_cfrac_result_5.csv");

            sw.Write("[n] r-i");
            for (ddouble i = 0; i <= 8.5; i += 0.5) {
                sw.Write($",{i}");
            }
            sw.Write("\n");

            for (ddouble r = 0; r <= 6; r += 0.5) {
                sw.Write($"{r}");

                for (ddouble i = 0; i <= 8.5; i += 0.5) {
                    Complex z = (r, i);

                    if (!UseCFrac(z)) { 
                        sw.Write(", 40");
                        continue;
                    }

                    Complex c_expected = ErfCFrac(z, max_n);

                    for (int n = 2; n <= max_n; n++) {
                        Complex c_actual = ErfCFrac(z, n);

                        ddouble err = (c_expected - c_actual).Magnitude / c_expected.Magnitude;

                        if (err < 1e-28) {
                            Console.WriteLine($"{r},{i},{n}");
                            sw.Write($", {int.Max(8, n),2}");
                            break;
                        }
                    }
                }
                sw.Write("\n");

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

        static bool UseCFrac(Complex z) {
            double zid = (double)z.I;
            double zr_thr = 2d - 7d / 256 * zid * zid;

            return zr_thr <= z.R;
        }
    }
}
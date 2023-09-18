using DoubleDouble;
using DoubleDoubleComplex;

namespace DoubleDoubleComplexSandbox {
    internal class Program {
        static void Main() {
            const int max_n = 1024;
            using StreamWriter sw = new("../../erf_cfrac_result_4.csv");

            sw.Write("[r] n-i");
            for (ddouble i = 0; i <= 32; i += 1d / 64) {
                sw.Write($",{i}");
            }
            sw.Write("\n");

            for(int target_n = 4; target_n <= 64; target_n++){
                sw.Write($"{target_n}");

                for (ddouble i = 0; i <= 32; i += 1d / 64) {
                    ddouble r = 0, dr = 16;

                    while (dr >= double.ScaleB(1, -20)) {
                        Complex z = (r + dr, i);

                        Complex c_expected = ErfCFrac(z, max_n);

                        for (int n = 2; n <= target_n + 1; n++) {
                            Complex c_actual = ErfCFrac(z, n);

                            ddouble err = (c_expected - c_actual).Magnitude / c_expected.Magnitude;

                            if (err < 1e-28 && n < target_n) {
                                dr /= 2;
                                break;
                            }
                            if (n >= target_n) {
                                r += dr;
                                break;
                            }
                        }
                    }

                    Console.WriteLine($"{r},{i},{target_n}");

                    sw.Write($",{r}");

                    if (r == 0d) {
                        break;
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

            return zr_thr >= z.R;
        }
    }
}
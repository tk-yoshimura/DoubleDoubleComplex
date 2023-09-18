using DoubleDouble;
using DoubleDoubleComplex;

namespace DoubleDoubleComplexSandbox {
    internal class Program {
        static void Main() {
            const int max_n = 1024;
            using StreamWriter sw = new("../../erf_cfrac_result.csv");

            for (ddouble r = 0; r <= 4; r += 1d / 256) {
                sw.Write($",{r}");
            }
            sw.Write("\n");

            for (ddouble i = 0; i <= 256; i += 0.125) {
                sw.Write($"{i}");

                for (ddouble r = 1d / 256; r <= 4; r += 1d / 256) {
                    Complex z = (r, i);

                    Complex c_expected = ErfCFrac(z, max_n);

                    if (!Complex.IsFinite(c_expected)) {
                        sw.Write(",-1");
                        continue;
                    }

                    for (int n = 2; n <= max_n; n++) {
                        Complex c_actual = ErfCFrac(z, n);

                        ddouble err = (c_expected - c_actual).Magnitude / c_expected.Magnitude;

                        if (err < 1e-28) {
                            sw.Write($",{(n < max_n ? n : -1)}");
                            Console.WriteLine($"{r},{i},{(n < max_n ? n : -1)}");
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
    }
}
using DoubleDouble;
using DoubleDoubleComplex;

namespace DoubleDoubleComplexSandbox {
    internal class Program {
        static void Main() {
            for (ddouble r = -40; r <= 40; r += 1d / 8) {
                Complex c = Complex.BesselJ(1.5, (r, 1));

                Console.WriteLine($"{(r, 1)}: {c}");
            }

            Console.WriteLine("END");
            Console.Read();
        }
    }
}
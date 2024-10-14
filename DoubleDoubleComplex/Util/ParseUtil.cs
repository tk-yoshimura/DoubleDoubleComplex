using System.Diagnostics;

namespace DoubleDoubleComplex.Util {
    internal static class ParseUtil {
        public static int IndexOfElem(string str, int start_index) {
            Debug.Assert(start_index >= 0);

            int index = start_index - 1;

            do {
                index = str.IndexOfAny(['+', '-'], index + 1);
            } while (index > 0 && (str[index - 1] == 'e' || str[index - 1] == 'E'));

            return index;
        }
    }
}

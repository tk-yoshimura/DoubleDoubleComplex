using System;

namespace DoubleDoubleComplex.Util {
    internal static class ParseUtil {
        public static int IndexOfElem(string str, int start_index) {
            if (start_index < 0) {
                throw new ArgumentOutOfRangeException(nameof(start_index));
            }

            int index = start_index - 1;

            do {
                index = str.IndexOfAny(['+', '-'], index + 1);
            } while (index > 0 && (str[index - 1] == 'e' || str[index - 1] == 'E'));

            return index;
        }
    }
}

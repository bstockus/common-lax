using System;
using System.Text;

namespace Lax.Helpers.Consoles {

    public static class ConsoleHelpers {

        /// <summary>
        /// Gets the console password.
        /// </summary>
        /// <returns></returns>
        public static string GetConsolePassword() {
            var sb = new StringBuilder();
            while (true) {
                var cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter) {
                    Console.WriteLine();
                    break;
                }

                if (cki.Key == ConsoleKey.Backspace) {
                    if (sb.Length > 0) {
                        Console.Write("\b\0\b");
                        sb.Length--;
                    }

                    continue;
                }

                Console.Write('*');
                sb.Append(cki.KeyChar);
            }

            return sb.ToString();
        }

    }

}
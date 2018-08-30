using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VocableParser.Models;

namespace VocableParser.Utils
{
    public class ConsoleUtils
    {
        /// <summary>
        /// Writes a window width row of the specified character.
        /// </summary>
        /// <param name="character"></param>
        public static void WriteRow(char character)
        {
            Console.WriteLine(new string(character, Console.WindowWidth));
        }

        /// <summary>
        /// Clears the current console line.
        /// </summary>
        public static void ClearLine()
        {
            int cursorTop = Console.CursorTop;
            Console.SetCursorPosition(0, cursorTop);
            WriteRow(' ');
            Console.SetCursorPosition(0, cursorTop);
        }

        /// <summary>
        /// Writes a list to the console having each item on its own line and numbered.
        /// </summary>
        /// <param name="words"></param>
        public static void WriteListWithLineNumbers(IEnumerable<Word> words)
        {
            var maxLength = words.Count().ToString().Length;

            int lineNum = 1;
            for (int i = 0; i < words.Count(); i++)
            {
                Console.WriteLine("> {0}. {1}",
                    lineNum.ToString().PadLeft(maxLength, '0'),
                    words.ElementAt(i).ToString());
                lineNum++;
            }
        }

        /// <summary>
        /// Writes a list to the console, but shows a progress percentage while it's building
        /// the list to be written.
        /// </summary>
        /// <param name="e"></param>
        public static void WriteListWithPercentProgress(IEnumerable<string> e)
        {
            decimal totalLines = e.Count();
            var cTop = Console.CursorTop;
            var cLeft = Console.CursorLeft;

            e = e.OrderBy(s => s.Length).ThenBy(s => s);

            var maxLength = e.Count().ToString().Length;

            int lineNum = 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < e.Count(); i++)
            {
                sb.AppendFormat("> {0}. {1}",
                    lineNum.ToString().PadLeft(maxLength, '0'),
                    e.ElementAt(i));
                sb.AppendLine();
                lineNum++;

                decimal percentComplete = ((i + 1) / totalLines) * 100;
                ClearLine();
                Console.Write("> Processing: {0:##0.00}%", percentComplete);
            }
            ClearLine();
            Console.Write(sb.ToString());
        }
    }
}

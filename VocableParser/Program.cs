using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VocableParser.Constants;

namespace VocableParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("| Vocable Parser                    |");
            Console.WriteLine("-------------------------------------");

            // read the word structure.
            Console.Write("> Enter word structure: ");
            var strStructure = Console.ReadLine().ToUpper();

            // create the word structure.
            WordStructure structure = WordStructure.Parse(strStructure);

            // get a list of unique characters used in the word structure.
            var uniqueChars = strStructure
                .Replace(ParserSymbols.OPTIONAL_START.ToString(), string.Empty)
                .Replace(ParserSymbols.OPTIONAL_END.ToString(), string.Empty);

            uniqueChars = new string(uniqueChars.Distinct().ToArray());

            // loop through the list of unique characters and prompt the
            // user to enter its word list.
            foreach (char letter in uniqueChars)
            {
                Console.Write(String.Format("> Enter values for {0}: ", letter));
                var chars = Console.ReadLine();
                structure.FillComponents(letter, chars.Split(','));
            }

            // build the word list.
            var words = structure.BuildWords();

            // print the results.
            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("| Generated Words                   |");
            Console.WriteLine("-------------------------------------");

            PrintEnumerable(words);

            // prevent app from closing.
            Console.ReadLine();
        }

        private static void PrintEnumerable(IEnumerable<string> e)
        {
            int lineNum = 1;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < e.Count(); i++)
            {
                sb.AppendFormat("> {0}. {1}", lineNum, e.ElementAt(i));
                sb.AppendLine();
                lineNum++;
            }
            Console.Write(sb.ToString());
        }
    }
}

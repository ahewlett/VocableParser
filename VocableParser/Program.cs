using System;
using System.Linq;
using VocableParser.Constants;
using VocableParser.Utils;

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
            var currentTime = DateTime.Now.TimeOfDay;
            var words = structure.BuildWords();
            var elapsedTime = DateTime.Now.TimeOfDay.Subtract(currentTime);

            // print stats about the generated results.
            Console.WriteLine();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("| Generated Words (in {0:0.0000} secs)  |", elapsedTime.TotalSeconds);
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("| Total Words: {0}", words.Count());
            Console.WriteLine();

            // print the results.
            ConsoleUtils.WriteListWithLineNumbers(words.OrderBy(s => s.Length).ThenBy(s => s));

            // prevent app from closing.
            Console.ReadLine();
        }
    }
}

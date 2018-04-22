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
            var strStructure = "VC(V)";
            Console.WriteLine(string.Format("Word Structure: {0}", strStructure));
            Console.WriteLine("-----------------------");

            // parse and populate the word structure.
            WordStructure structure = WordStructure.Parse(strStructure);
            structure.FillComponents(SoundSymbols.VOWEL, "A", "E");
            structure.FillComponents(SoundSymbols.CONSONANT, "C", "D");

            // build the word list.
            var words = structure.BuildWords();

            // print the results.
            Console.WriteLine("-----------------------");
            PrintEnumerable(words);

            // prevent app from closing.
            Console.ReadLine();
        }

        private static void PrintEnumerable(IEnumerable<string> e)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < e.Count(); i++)
            {
                sb.Append(e.ElementAt(i));
                if (i < (e.Count() - 1))
                {
                    sb.Append(", ").AppendLine();
                }
            }
            Console.Write(sb.ToString());
        }
    }
}

using System.Collections.Generic;
using VocableParser.Models;

namespace VocableParser
{
    public class WordStructureComponent
    {
        public bool IsOptional { get; set; }
        public IList<Word> Words { get; set; }
        public char Symbol { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WordStructureComponent()
        {
            Words = new List<Word>();
        }

        /// <summary>
        /// Builds a <see cref="WordStructureComponent"/> with the specified list of characters.
        /// </summary>
        /// <param name="words"></param>
        public WordStructureComponent(params Word[] words)
        {
            Words = words;
        }

        /// <summary>
        /// Creates a permutation between this component and another component's characters.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public IEnumerable<Word> Permutate(WordStructureComponent component)
        {
            var permWords = new List<Word>();

            foreach (Word word in Words)
            {
                foreach (Word newWord in component.Words)
                {
                    Word combinedWord = new Word();
                    combinedWord.Sounds.AddRange(word.Sounds);
                    combinedWord.Sounds.AddRange(newWord.Sounds);
                    permWords.Add(combinedWord);
                }
            }

            return permWords;
        }
    }
}

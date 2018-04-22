using System.Collections.Generic;

namespace VocableParser
{
    public class WordStructureComponent
    {
        public bool IsOptional { get; set; }
        public IList<string> Characters { get; set; }
        public char Symbol { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WordStructureComponent()
        {
            Characters = new List<string>();
        }

        /// <summary>
        /// Builds a <see cref="WordStructureComponent"/> with the specified list of characters.
        /// </summary>
        /// <param name="characters"></param>
        public WordStructureComponent(params string[] characters)
        {
            Characters = characters;
        }

        /// <summary>
        /// Creates a permutation between this component and another component's characters.
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public IEnumerable<string> Permutate(WordStructureComponent component)
        {
            var permCharacters = new List<string>();

            foreach (string character in Characters)
            {
                foreach (string newCharacter in component.Characters)
                {
                    permCharacters.Add(character + newCharacter);
                }
            }

            return permCharacters;
        }
    }
}

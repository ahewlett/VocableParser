﻿using System;
using System.Collections.Generic;
using System.Linq;
using VocableParser.Constants;

namespace VocableParser
{
    public class WordStructure
    {
        public IList<WordStructureComponent> Components { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WordStructure()
        {
            Components = new List<WordStructureComponent>();
        }

        /// <summary>
        /// Constructs a structure comprised of a list of components. 
        /// </summary>
        /// <param name="list"></param>
        public WordStructure(params WordStructureComponent[] list)
        {
            Components = list;
        }

        /// <summary>
        /// Parses a string into a <see cref="WordStructure"/> object.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static WordStructure Parse(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentNullException("str");

            WordStructure structure = new WordStructure();

            bool isOptional = false;
            for (int i = 0; i < str.Length; i++)
            {
                var character = str.ElementAt(i);
 
                // check for special characters.
                switch (character)
                {
                    case ParserSymbols.OPTIONAL_START:
                        isOptional = true;
                        continue;

                    case ParserSymbols.OPTIONAL_END:
                        isOptional = false;
                        continue;
                }

                // build the component.
                WordStructureComponent component = new WordStructureComponent
                {
                    IsOptional = isOptional,
                    Symbol = character
                };

                structure.Components.Add(component);
            }

            return structure;
        }

        /// <summary>
        /// Fills all components that have a matching symbol with the specified characters.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="characters"></param>
        public void FillComponents(char symbol, params string[] characters)
        {
            if (Components.Count == 0) throw new Exception("No components to fill.");

            foreach (WordStructureComponent component in Components.Where(c => c.Symbol == symbol))
            {
                component.Characters = characters;
            }
        }

        /// <summary>
        /// Builds the structure's comprehensive list of words.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> BuildWords()
        {
            if (Components.Count == 0) throw new Exception("At least one component must exist to build words.");

            var words = new List<string>();

            var structures = GetStructureSubsets();
            foreach (WordStructure structure in structures)
            {
                Console.WriteLine(structure.ToString());
                words.AddRange(structure.PermutateAllComponents());
            }

            return words.Distinct();
        }

        /// <summary>
        /// Returns the <see cref="WordStructure"/>'s unparsed string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var output = string.Empty;
            foreach (WordStructureComponent component in Components)
            {
                if (component.IsOptional)
                {
                    output += string.Format("{0}{1}{2}", 
                        ParserSymbols.OPTIONAL_START, 
                        component.Symbol.ToString(), 
                        ParserSymbols.OPTIONAL_END);
                }
                else
                {
                    output += component.Symbol.ToString();
                }
            }

            return output;
        }

        /// <summary>
        /// Returns a list of all subset structures based on this word structure.
        /// Subsets exist when optional components exist in the word structure.
        /// </summary>
        /// <returns></returns>
        private IList<WordStructure> GetStructureSubsets()
        {
            // TODO: need to fix bug where not all subsets are generated.
            // ie. for (V)VC(V) the following subsets are created: (V)VC(V), (V)VC, VC. we're missing VC(V).
            var structures = new List<WordStructure>();
            var tempComponents = new List<WordStructureComponent>(Components);

            // add the default structure to the list.
            structures.Add(new WordStructure(Components.ToArray()));

            // loop through the optional components in reverse.
            foreach (WordStructureComponent component in tempComponents.Where(c => c.IsOptional).Reverse())
            {
                // generate a new structure with the current component removed.
                tempComponents.Remove(component);
                var structure = new WordStructure(tempComponents.ToArray());
                structures.Add(structure);
            }

            return structures;
        }

        /// <summary>
        /// Creates a permutation of words using all components in this word structure.
        /// </summary>
        /// <returns></returns>
        private IList<string> PermutateAllComponents()
        {
            var words = new List<string>();

            // if only one component exists, we'll just add it's characters to the word list.
            // otherwise, we'll loop through the components and permutate their characters.
            if (Components.Count == 1)
            {
                words.AddRange(Components.First().Characters);
            }
            else
            {
                WordStructureComponent currentComponent = null;

                foreach (WordStructureComponent nextComponent in Components)
                {
                    if (currentComponent == null)
                    {
                        currentComponent = nextComponent;
                        continue;
                    }
                    else
                    {
                        var list = currentComponent.Permutate(nextComponent);
                        currentComponent = new WordStructureComponent(list.ToArray());
                        words.AddRange(list);
                    }
                }

                // remove words that have less characters than the number of components.
                words.RemoveAll(c => c.Length < Components.Count);
            }
            
            return words;
        }

    }
}

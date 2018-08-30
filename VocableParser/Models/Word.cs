using System;
using System.Collections.Generic;

namespace VocableParser.Models
{
    public class Word : IComparable<Word>
    {
        public List<Sound> Sounds { get; set; }

        public Word()
        {
            Sounds = new List<Sound>();
        }

        public Word(params Sound[] sounds)
        {
            Sounds = new List<Sound>(sounds);
        }

        public override string ToString()
        {
            string word = string.Empty;
            foreach (Sound sound in Sounds)
            {
                word += sound.IpaUnicode;
            }
            return word;
        }

        public int CompareTo(Word other)
        {
            return this.ToString().CompareTo(other.ToString());
        }
    }
}

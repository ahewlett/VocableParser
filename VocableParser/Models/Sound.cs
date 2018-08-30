using System;

namespace VocableParser.Models
{
    public class Sound : IComparable<Sound>
    {
        public string IpaUnicode { get; set; }

        public Sound(string ipaUnicode)
        {
            this.IpaUnicode = ipaUnicode;
        }

        public int CompareTo(Sound other)
        {
            return IpaUnicode.CompareTo(other.IpaUnicode);
        }
    }
}

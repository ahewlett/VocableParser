using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocableParser.Comparers
{
    public class WordStructureComparer : IEqualityComparer<WordStructure>
    {
        public bool Equals(WordStructure x, WordStructure y)
        {
            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(WordStructure obj)
        {
            int hash = 17;
            hash = hash * 23 + obj.ToString().GetHashCode();
            return hash;
        }
    }
}

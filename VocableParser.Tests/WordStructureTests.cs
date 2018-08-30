using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VocableParser.Models;

namespace VocableParser.Tests
{
    [TestClass]
    public class WordStructureTests
    {
        private Word[] _oneVowelWord = {
            new Word(new Sound("a"))
        };

        private Word[] _oneConsonantWord =
        {
            new Word(new Sound("b"))
        };

        private Word[] _twoVowelWords = {
            new Word(new Sound("a")),
            new Word(new Sound("i"))
        };

        private Word[] _twoConsonantWords =
        {
            new Word(new Sound("b")),
            new Word(new Sound("c"))
        };

        [TestMethod]
        public void WordStructure_OnlyConstantComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "ac", "ib", "ic" };
            WordStructure structure = WordStructure.Parse("VC");
            structure.FillComponents('V', _twoVowelWords);
            structure.FillComponents('C', _twoConsonantWords);
            // act
            var results = structure.BuildWords();
            var simpleResults = WordsAsStrings(results);
            // assert
            Assert.IsTrue(simpleResults.All(expectedResults.AsEnumerable().Contains), "Unexpected results using only constant components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }

        [TestMethod]
        public void WordStructure_ConstantAndOptionalComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "ac", "ib", "ic", "aba", "abi", "aca", "aci", "iba", "ibi", "ica", "ici" };
            WordStructure structure = WordStructure.Parse("VC(V)");
            structure.FillComponents('V', _twoVowelWords);
            structure.FillComponents('C', _twoConsonantWords);
            // act
            var results = structure.BuildWords();
            var simpleResults = WordsAsStrings(results);
            // assert
            Assert.IsTrue(simpleResults.All(expectedResults.AsEnumerable().Contains), "Unexpected results using constant and optional components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }

        [TestMethod]
        public void WordStructure_OnlyOptionalComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "a", "b" };
            WordStructure structure = WordStructure.Parse("(V)(C)");
            structure.FillComponents('V', _oneVowelWord);
            structure.FillComponents('C', _oneConsonantWord);
            // act
            var results = structure.BuildWords();
            var simpleResults = WordsAsStrings(results);
            // assert
            Assert.IsTrue(simpleResults.All(expectedResults.AsEnumerable().Contains), "Unexpected results using only optional components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }

        private string[] WordsAsStrings(IEnumerable<Word> words)
        {
            List<string> output = new List<string>();
            foreach (Word word in words)
            {
                output.Add(word.ToString());
            }
            return output.ToArray();
        }
    }
}

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VocableParser.Tests
{
    [TestClass]
    public class WordStructureTests
    {
        [TestMethod]
        public void WordStructure_OnlyConstantComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "ac", "ib", "ic" };
            WordStructure structure = WordStructure.Parse("VC");
            structure.FillComponents('V', "a", "i");
            structure.FillComponents('C', "b", "c");
            // act
            var results = structure.BuildWords();
            // assert
            Assert.IsTrue(results.All(expectedResults.AsEnumerable().Contains), "Unexpected results using only constant components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }

        [TestMethod]
        public void WordStructure_ConstantAndOptionalComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "ac", "ib", "ic", "aba", "abi", "aca", "aci", "iba", "ibi", "ica", "ici" };
            WordStructure structure = WordStructure.Parse("VC(V)");
            structure.FillComponents('V', "a", "i");
            structure.FillComponents('C', "b", "c");
            // act
            var results = structure.BuildWords();
            // assert
            Assert.IsTrue(results.All(expectedResults.AsEnumerable().Contains), "Unexpected results using constant and optional components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }

        [TestMethod]
        public void WordStructure_OnlyOptionalComponents_ValidResult()
        {
            // arrange
            string[] expectedResults = { "ab", "a", "b" };
            WordStructure structure = WordStructure.Parse("(V)(C)");
            structure.FillComponents('V', "a");
            structure.FillComponents('C', "b");
            // act
            var results = structure.BuildWords();
            // assert
            Assert.IsTrue(results.All(expectedResults.AsEnumerable().Contains), "Unexpected results using only optional components.");
            Assert.IsTrue(results.Count() == expectedResults.Count(), "Count of result does not match expected.");
        }
    }
}

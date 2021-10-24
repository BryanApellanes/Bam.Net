using System;
using Bam.Net.Testing;
using System.Collections.Generic;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Tests
{
    [Serializable]
    public class ExtensionsUnitTests : CommandLineTool
    {
        [UnitTest]
        public static void SmallestShouldReturnSmallest()
        {
            Expect.AreEqual(5, new long[] { 90, 30, 5, 25, 67 }.Smallest());
        }

        [UnitTest]
        public static void LargestShouldReturnLargets()
        {
            Expect.AreEqual(545, new long[] { 90, 30, 545, 5, 25, 67 }.Largest());
        }

        [UnitTest]
        public static void CamelCaseShouldLeaveFirstLetterLower()
        {
            string test = "The quick brown fox jumps over the lazy dog";

            Expect.AreEqual("TheQuickBrownFoxJumpsOverTheLazyDog", test.PascalCase(true, new string[] { " " }));
            Expect.AreEqual("theQuickBrownFoxJumpsOverTheLazyDog", test.CamelCase(true, new string[] { " " }));
        }

        [UnitTest]
        public void CaseAcronymShouldReturnAcronym()
        {
            string value = "TheQuickFox";
            string value2 = "The Quick Fox";
            string value3 = "theQuickFox";
            string value4 = "charTheQuickFox";
            string expected = "TQF";

            Expect.AreEqual(expected, value.CaseAcronym());
            Expect.AreEqual(expected, value2.CaseAcronym());
            Expect.AreEqual(expected, value3.CaseAcronym());
            Expect.AreEqual(expected, value4.CaseAcronym(false));
            Expect.IsFalse(expected.Equals(value4.CaseAcronym()));
        }

        [UnitTest]
        public void ShouldDelimitReplace()
        {
            string input = "{ some kind of \" value \" baloney, $$~Do replace in \"Here\" yay ~$$good";
            string shouldBe = "{ some kind of \" value \" baloney, Do replace in 'Here' yay good";
            Expect.AreEqual(shouldBe, input.DelimitedReplace("\"", "'"));
        }

        [UnitTest]
        public void ShouldDelimitReplaceWithDelimiters()
        {
            string input = "{ some kind@ of \" value \" baloney, $$~Do replace in \"Here\" /@yay ~$$good";
            string shouldBe = "{ some kind of \" value \" banana, $$~Do replace in \"Here\" yay ~$$good";
            Expect.AreEqual(shouldBe, input.DelimitedReplace("baloney", "banana", "@", "/@"));
        }
    }
}

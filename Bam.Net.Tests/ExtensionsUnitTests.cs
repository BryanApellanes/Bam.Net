using System;
using Bam.Net.Testing;
using System.Collections.Generic;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Tests
{
    [Serializable]
    public class ExtensionsUnitTests : CommandLineTestInterface
    {
        [Serializable]
        public class ValueCloneTestClass
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public byte[] ByteArray { get; set; }
            public List<object> List { get; set; }
        }

        [UnitTest]
        public static void DataCloneShouldNotCopyListProperty()
        {
            List<object> list = new List<object>();
            5.Times(i => list.Add(i));
            ValueCloneTestClass one = new ValueCloneTestClass { Name = 8.RandomLetters(), Number = RandomNumber.Between(1, 20) };
            ValueCloneTestClass two = new ValueCloneTestClass { Name = 5.RandomLetters(), Number = RandomNumber.Between(30, 100), ByteArray = one.ToBinaryBytes(), List = list };
            ValueCloneTestClass three = two.DataClone();
            Expect.AreEqual(two.Name, three.Name, "Names didn't match");
            Expect.AreEqual(two.Number, three.Number, "Numbers didn't match");
            Expect.AreEqual(two.ByteArray, three.ByteArray, "ByteArrays didn't match");
            Expect.IsNull(three.List);
        }

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

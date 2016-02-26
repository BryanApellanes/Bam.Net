using System;
using Bam.Net.Testing;

namespace Bam.Net.Tests
{
    [Serializable]
    public class ExtensionsUnitTests : CommandLineTestInterface
    {
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
    }
}

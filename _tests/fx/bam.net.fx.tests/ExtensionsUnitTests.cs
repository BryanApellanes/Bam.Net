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
    }
}

using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Tests
{
    public class JsonEncoderTests
    {
        [UnitTest]
        public void JsonEncoderDecoderTest()
        {
            JsonEncoder<TestMonkey> encoder = new JsonEncoder<TestMonkey>();
            TestMonkey testMonkey = new TestMonkey()
            {
                Name = "Fred"
            };
           
            string json = encoder.Encode(testMonkey);
            IValueDecoder<string, TestMonkey> decoder = encoder.GetDecoder();

            TestMonkey decoded = decoder.Decode(json);

            Expect.AreEqual(testMonkey.Name, decoded.Name);
        }
    }
}

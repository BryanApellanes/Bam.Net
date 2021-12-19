using Bam.Net.Testing.Unit;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Tests
{
    public class Base64EncoderTests
    {
        [UnitTest]
        public void Base64EncoderDecoderTest()
        {
            Base64Encoder base64Encoder = new Base64Encoder();
            SecureRandom secureRandom = new SecureRandom();
            byte[] randomBytes = secureRandom.GenerateSeed(64);

            string encoded = base64Encoder.Encode(randomBytes);

            IValueDecoder<string, byte[]> base64Decoder = base64Encoder.GetDecoder();
            byte[] decoded = base64Decoder.Decode(encoded);
            Expect.AreEqual(randomBytes, decoded);
        }
    }
}

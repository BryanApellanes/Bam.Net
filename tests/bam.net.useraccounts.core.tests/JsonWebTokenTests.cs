using System;
using Bam.Net.CoreServices.Auth;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.UserAccounts.Tests
{
    [Serializable]
    public class JsonWebTokenTests
    {
        public const string TestKey = "some-weak-key";
        public const string TestSignature = "pbCk-9ADti5HwJmyfTKYvuklT4r-zU8BDtTjPocptTg";
        public const string TestToken = "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI1ODM2Y2RmNi1hMTgxLTRmMTQtOGYzZS0xY2Q4NGRlNTkzMDIiLCJzdWIiOiI1MTRlYzg1MC03ZGEwLTRkYzAtYTdiNi1hZjMxY2Q0ZDBiNWQiLCJpc3MiOiJodHRwczovL2Zha2UuY3htIiwiaWF0IjoxNTgyNTc1OTY0LCJuYmYiOjE1ODI1NzU2NjQsImV4cCI6MTU4MjU3Njg2NCwidmVyIjoxLCJ0eXAiOiJVU0VSIiwicm9sIjpbXX0.pbCk-9ADti5HwJmyfTKYvuklT4r-zU8BDtTjPocptTg";

        public const string TestHeader = "{\"alg\":\"HS256\"}";

        public const string TestPayload = "{\"jti\":\"5836cdf6-a181-4f14-8f3e-1cd84de59302\",\"sub\":\"514ec850-7da0-4dc0-a7b6-af31cd4d0b5d\",\"iss\":\"https://fake.cxm\",\"iat\":1582575964,\"nbf\":1582575664,\"exp\":1582576864,\"ver\":1,\"typ\":\"USER\",\"rol\":[]}";
        
        [UnitTest]
        [TestGroup("Auth")]
        public void CanInitializeFromBearerToken()
        {
            JsonWebToken testToken = new JsonWebToken(TestToken);
            Expect.AreEqual(TestHeader, testToken.Header, "Header didn't match");
            Expect.AreEqual(TestPayload, testToken.Payload, "Payload didn't match");
        }

        [UnitTest]
        [TestGroup("Auth")]
        public void CanGetSignature()
        {
            JsonWebToken testToken = new JsonWebToken(TestToken);
            string signature = testToken.GetSignature(TestKey);
            Expect.AreEqual(TestSignature, signature);
        }

        [UnitTest]
        [TestGroup("Auth")]
        public void CanValidateWithValidKey()
        {
            JsonWebToken testToken = new JsonWebToken(TestToken);
            testToken.IsValid(TestKey).IsTrue();
        }
        
        [UnitTest]
        [TestGroup("Auth")]
        public void WontValidateWithInvalidKey()
        {
            JsonWebToken testToken = new JsonWebToken(TestToken);
            testToken.IsValid("BAD Key").IsFalse();
        }
    }
}
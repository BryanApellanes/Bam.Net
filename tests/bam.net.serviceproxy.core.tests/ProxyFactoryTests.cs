using Bam.Net.CoreServices;
using Bam.Net.Encryption;
using Bam.Net.Testing.Unit;

namespace Bam.Net.ServiceProxy.Tests
{
    
    public class ProxyFactoryTests
    {
        [UnitTest]
        public void CanWriteProxy()
        {
            ProxyFactory factory = new ProxyFactory();
            EncryptedEcho echo = factory.GetProxy<EncryptedEcho>();
            Expect.IsNotNull(echo);
        } 
    }
}
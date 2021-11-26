/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CommandLine;
using Bam.Net.Server.PathHandlers;
using Bam.Net.Testing.Unit;
using System;

namespace Bam.Net.ServiceProxy.Tests
{
    public class NamedPathShould
    {
        [UnitTest]
        public void ParseServiceProxyUriWithoutPort()
        {
            Uri testUri = new Uri("http://test-app.domain.com/serviceproxy/echo/send?data=value");

            ServiceProxyPath serviceProxyPath = ServiceProxyPath.FromUri(testUri);

            Expect.IsTrue(serviceProxyPath.IsMatch(testUri));
            Expect.AreEqual(80, serviceProxyPath.Port);
            Expect.IsTrue("ServiceProxy".Equals(serviceProxyPath.PathName, StringComparison.InvariantCultureIgnoreCase));
            Expect.AreEqual("echo", serviceProxyPath.TypeIdentifier);
            Expect.AreEqual("send", serviceProxyPath.MethodName);
        }

        [UnitTest]
        public void ParseServiceProxyUriWithPort()
        {
            Uri testUri = new Uri("http://test-app.domain.com:4100/serviceproxy/echo/send?data=value");

            ServiceProxyPath serviceProxyPath = ServiceProxyPath.FromUri(testUri);

            Expect.IsTrue(serviceProxyPath.IsMatch(testUri));
            Expect.AreEqual(4100, serviceProxyPath.Port);
            Expect.IsTrue("ServiceProxy".Equals(serviceProxyPath.PathName, StringComparison.InvariantCultureIgnoreCase));
            Expect.AreEqual("echo", serviceProxyPath.TypeIdentifier);
            Expect.AreEqual("send", serviceProxyPath.MethodName);
        }

        [UnitTest]
        public void ReadUriUsingReadUntilMethodTest()
        {
            Uri testUri = new Uri("http://test-app.domain.com/serviceproxy/echo/send?data=value");

            Message.PrintLine("AbsolutePath: {0}", testUri.AbsolutePath);
            Message.PrintLine("PathAndQuery: {0}", testUri.PathAndQuery);
            
            string responderName = testUri.PathAndQuery.ReadUntil('/', true, out string pathAndQuery);          
            string classAndMethod = pathAndQuery.ReadUntil('?', out string queryString);
            Message.PrintLine("ResponderName: {0}", responderName);
            Message.PrintLine("ClassAndMethod: {0}", classAndMethod);
            Message.PrintLine("QueryString: {0}", queryString);

            Expect.AreEqual("serviceproxy", responderName);
            Expect.AreEqual("echo/send", classAndMethod);
            Expect.AreEqual("data=value", queryString);
        }

        [UnitTest]
        public void ParseUsingReadUntilTest()
        {
            string value = "/name//class/method";
            string name = value.ReadUntil('/', true, out string classAndMethod);
            string className = classAndMethod.ReadUntil('/', true, out string methodName);

            Message.PrintLine("Value: {0}", value);
            Message.PrintLine("Name: {0}", name);
            Message.PrintLine("ClassName: {0}", className);
            Message.PrintLine("MethodName: {0}", methodName);

            Expect.AreEqual("name", name);
            Expect.AreEqual("class", className);
            Expect.AreEqual("method", methodName);
        }
    }
}

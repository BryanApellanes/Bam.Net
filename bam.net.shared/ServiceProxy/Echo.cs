/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// This class exists for testing
    /// </summary>
    public class EchoData
    {
        public string StringProperty { get; set; }
        public bool BoolProperty { get; set; }
        public int IntProperty { get; set; }
    }

    /// <summary>
    /// Echo class that requires an api key
    /// </summary>
    [ApiKeyRequired]
    public class ApiKeyRequiredEcho : Echo
    {

    }

    [ServiceSubdomain("echo")]
    public class ServiceSubdomainEcho: Echo
    {

    }

    /// <summary>
    /// Echo class that requires encryption when used as a service
    /// </summary>
    [Proxy]
    [Encrypt]
    public class EncryptedEcho: Echo
    {
    }



    /// <summary>
    /// Used specifically for testing ServiceProxy calls
    /// </summary>
    [Proxy("srvrEcho")]
    public class Echo
    {
        public virtual string Send(string value)
        {
            return TestStringParameter(value);
        }

        public virtual string TestStringParameter(string value)
        {
            return value;
        }

        public virtual string TestObjectParameter(EchoData data, string more)
        {
            return string.Format("The data was: {0}\r\n***\r\nMore: {1}", data.PropertiesToString(), more);
        }

        public virtual string TestCompoundParameter(TestObject test)
        {
            return string.Format("Name: {0}, Number: {1}, SubNumber: {2}, SubObject: {3}"
                ._Format(test.Name, test.Number, test.SubNumber, test.SubObject.PropertiesToString()));
        }

        public virtual EchoData TestObjectOut(string value, bool bp = false, int ip = 500)
        {
            return new EchoData { StringProperty = value, BoolProperty = bp, IntProperty = ip };
        }

        public virtual EchoData TestObjectInObjectOut(EchoData data)
        {
            return data;
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Naizari.Testing
{
    [Serializable]
    public class ExpectFailedException: Exception, ISerializable
    {        
        private const string defaultMessage = "Unexpected result: Expected <{0}>, Actual <{1}>";

        public ExpectFailedException(SerializationInfo info, StreamingContext context)
            : base(info.GetString("Message"))
        {
        }

        public ExpectFailedException(string message)
            : base(message)// + ":\r\n" + Environment.StackTrace)
        {             
        }

        public ExpectFailedException(string message, bool htmlEncode) :
            this(htmlEncode ? HttpUtility.HtmlEncode(message) : message)
        { }

        public ExpectFailedException(string expected, string actual)
            : this(expected, actual, false)
        { }

        public ExpectFailedException(string expected, string actual, bool htmlEncode)
            : this(string.Format(htmlEncode ? HttpUtility.HtmlEncode(defaultMessage) : defaultMessage, expected, actual))
        { }

        public ExpectFailedException(bool expected, bool actual)
            : this(expected.ToString(), actual.ToString())
        { }

        public ExpectFailedException(bool expected, bool actual, bool htmlEncode)
            : this(expected.ToString(), actual.ToString(), htmlEncode)
        { }
        
        public ExpectFailedException(Type expected, object actual)
            : this(expected.Name, actual.GetType().Name)
        { }

        public ExpectFailedException(Type expected, object actual, bool htmlEncode)
            : this(expected.Name, actual.GetType().Name, htmlEncode)
        { }


        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Message", this.Message);
        }

        #endregion
    }
}

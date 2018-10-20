/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy.Secure
{
    public class SecureChannelMessage<T>: SecureChannelMessage
    {
        public SecureChannelMessage() : base() { }
        public SecureChannelMessage(string message) : base(message) { }
        public SecureChannelMessage(string message, bool success) : base(message, success) { }

        public SecureChannelMessage(T data)
            : base()
        {
            this.Data = data;
            this.Success = true;
        }

        public SecureChannelMessage(Exception ex):base(ex)
        {

        }

        public SecureChannelMessage(bool success)
        {
            this.Success = success;
        }
        public T Data
        {
            get;
            set;
        }
    }

    public class SecureChannelMessage
    {
        public SecureChannelMessage() { }
        public SecureChannelMessage(string message)
        {
            this.Message = message;
        }
        public SecureChannelMessage(string message, bool success)
        {
            this.Message = message;
            this.Success = success;
        }

        public SecureChannelMessage(bool success)
        {
            this.Success = success;
        }

        public SecureChannelMessage(Exception ex)
        {
            this.Message = ex.Message;
            this.Success = false;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}

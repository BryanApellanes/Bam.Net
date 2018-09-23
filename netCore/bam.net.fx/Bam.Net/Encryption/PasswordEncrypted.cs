/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace Bam.Net.Encryption
{
    public class PasswordEncrypted
    {
        protected PasswordEncrypted() { }

        public PasswordEncrypted(string data, string password)
        {
            this.Data = data;
            this.Encrypt(password);
        }

        public static implicit operator string(PasswordEncrypted p)
        {
            return p.Cipher;
        }

        public string Value
        {
            get
            {
                return Cipher;
            }
        }

        public string Data
        {
            internal get;
            set;
        }

        public string Cipher
        {
            get;
            internal set;
        }

        string _password;
        protected internal string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Encrypt(value);
            }
        }

        public string Encrypt(string password)
        {
            _password = password;
            Cipher = Crypto.EncryptStringAES(Data, password);
            return Cipher;
        }
    }

}

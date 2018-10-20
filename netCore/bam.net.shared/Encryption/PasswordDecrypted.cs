/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class PasswordDecrypted: PasswordEncrypted
    {
        public PasswordDecrypted(PasswordEncrypted encrypted, string password)
            : base(encrypted.Data, password)
        {            
        }

        public PasswordDecrypted(string cipher, string password)
        {
            Cipher = cipher;
            Data = Decrypt(password);
        }

        public static implicit operator string(PasswordDecrypted d)
        {
            return d.Data;
        }

        public string Value
        {
            get
            {
                return Data;
            }
        }

        public string Decrypt(string password)
        {
            string result = string.Empty; 
            if (!string.IsNullOrEmpty(Data))
            {
                result = Data;// no need to decrypt if Data was already set by ctor
            }
            else if(!string.IsNullOrEmpty(Cipher))
            {
                result = Crypto.DecryptStringAES(Cipher, password);
                Data = result;
            }
            else
            {
                throw new InvalidOperationException("Data and Cipher were both null or empty");
            }

            return result;
        }
    }
}

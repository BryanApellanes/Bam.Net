using Bam.Net;
using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public static partial class HashExtensions
    {
        static HashExtensions()
        {
            Hmacs.Add(Bam.Net.HashAlgorithms.RIPEMD160, (byte[] key) => new HMACRIPEMD160(key));
            HashAlgorithms.Add(Bam.Net.HashAlgorithms.RIPEMD160, () => RIPEMD160.Create());
        }
    }
}

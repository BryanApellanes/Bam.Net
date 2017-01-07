using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Google.Protobuf;

namespace Bam.Net.CoreServices.ProtoBuf
{
    public static class ProtocolBuffersExtensions
    {
        public static void WriteTo<T>(this T data, Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}

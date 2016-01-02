using System;
namespace Bam.Net.Server.Rpc
{
    public interface IRpcMessageParser
    {
        IRpcRequest Parse();
    }
}

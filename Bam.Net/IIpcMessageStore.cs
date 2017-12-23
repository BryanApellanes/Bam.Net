using System;

namespace Bam.Net
{
    public interface IIpcMessageStore
    {
        IpcMessage GetMessage(Type type, string name);
        IpcMessage GetMessage<T>(string name);

        bool SetMessage(string name, object data);
    }
}
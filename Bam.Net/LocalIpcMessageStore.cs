/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net
{
	[Serializable]
    public class LocalIpcMessageStore: IIpcMessageStore
    {
        public LocalIpcMessageStore(string rootDirectory)
        {
            this._rootDirectory = new DirectoryInfo(rootDirectory);
        }

        public LocalIpcMessageStore(DirectoryInfo directory)
        {
            this._rootDirectory = directory;
        }

        DirectoryInfo _rootDirectory;
        public string RootDirectory
        {
            get
            {
                return _rootDirectory.FullName;
            }
        }

        /// <summary>
        /// Gets a message with the specified name 
        /// of the specified type creating it if necessary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IpcMessage GetMessage<T>(string name)
        {
            return GetMessage(typeof(T), name);
        }

        /// <summary>
        /// Gets a message with the specified name 
        /// of the specified type creating it if necessary
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IpcMessage GetMessage(Type type, string name)
        {
            IpcMessage result = IpcMessage.Get(name, type, RootDirectory);
            return result;
        }

        public bool SetMessage(string name, object data)
        {
            IpcMessage msg = GetMessage(data.GetType(), name);
            return msg.Write(data);
        }
    }
}

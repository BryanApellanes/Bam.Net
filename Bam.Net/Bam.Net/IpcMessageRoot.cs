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
    public class IpcMessageRoot
    {
        public IpcMessageRoot(string rootDirectory)
        {
            this._rootDirectory = new DirectoryInfo(rootDirectory);
        }

        public IpcMessageRoot(DirectoryInfo directory)
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
            return GetMessage(name, typeof(T));
        }

        /// <summary>
        /// Gets a message with the specified name 
        /// of the specified type creating it if necessary
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IpcMessage GetMessage(string name, Type type)
        {
            IpcMessage result = IpcMessage.Get(name, type, RootDirectory);
            return result;
        }
    }
}

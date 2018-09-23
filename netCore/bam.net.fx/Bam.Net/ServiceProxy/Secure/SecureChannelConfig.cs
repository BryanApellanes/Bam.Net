/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using System.IO;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// Used to specify the SchemaInitializer for the underlying
    /// SecureServiceProxy schema
    /// </summary>
    public class SecureChannelConfig
    {
        private static string FileName
        {
            get
            {
                return ".\\{0}.json"._Format(typeof(SecureChannelConfig).Name);
            }
        }

        public SecureChannelConfig()
        {
            this.SchemaInitializer = new SchemaInitializer(typeof(SecureServiceProxyContext), typeof(SQLiteRegistrarCaller));
        }

        public SchemaInitializer SchemaInitializer
        {
            get;
            set;
        }

        public static SecureChannelConfig Load(string filePath)
        {
            return Load(new FileInfo(filePath));
        }

        public static SecureChannelConfig Load()
        {
            FileInfo info = new FileInfo(FileName);
            return Load(info);
        }
        
        public static SecureChannelConfig Load(FileInfo file)
        {
            if (!file.Exists)
            {
                Save(new SecureChannelConfig(), file);
            }
            return file.FromJsonFile<SecureChannelConfig>();
        }

        public void Save()
        {
            FileInfo info = new FileInfo(FileName);
        }

        public void Save(FileInfo file)
        {
            Save(this, file);
        }

        public static void Save(SecureChannelConfig config, FileInfo file)
        {
            config.ToJsonFile(file);
        }
    }
}

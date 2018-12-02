using Bam.Net.CoreServices;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Bam.Net.Services.Clients
{
    public class ProxyCode
    {
        public ProxyModel ProxyModel { get; set; }
        public string Code { get; set; }
        public string Hash
        {
            get
            {
                return Code.Sha256();
            }
        }

        public FileInfo Save()
        {
            SystemPaths paths = SystemPaths.Get(DefaultDataDirectoryProvider.Current);
            return Save(paths.Generated);
        }

        public FileInfo Save(string path, string applicationName = "common")
        {
            DirectoryInfo proxyCodePath = new DirectoryInfo(Path.Combine(path, applicationName, ProxyModel.Host));
            FileInfo codeFile = new FileInfo(Path.Combine(proxyCodePath.FullName, $"{ProxyModel.Protocol}_{ProxyModel.Host}_{ProxyModel.Port}_{ProxyModel.TypeName}_{Hash}.cs"));
            return codeFile;
        }
    }
}

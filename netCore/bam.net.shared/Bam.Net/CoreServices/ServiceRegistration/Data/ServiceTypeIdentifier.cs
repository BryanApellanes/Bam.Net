using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files;
using Bam.Net.Data.Repositories;
using Bam.Net.Logging;

namespace Bam.Net.CoreServices.ServiceRegistration.Data
{
    public class ServiceTypeIdentifier: RepoData
    {
        /// <summary>
        /// The git commit hash at the point of the build
        /// </summary>
        public string BuildNumber { get; set; }
        public string Namespace { get; set; }
        public string TypeName { get; set; }

        /// <summary>
        /// The name of the assembly file; used to attempt 
        /// to load the assembly locally before attempting
        /// to load from the AssemblyFileHash
        /// </summary>
        public string AssemblyName { get; set; }
        public string AssemblyFileHash { get; set; }

        /// <summary>
        /// The Sha1Int() of ToString ("{Namespace}.{TypeName}[{BuildNumber}]::{AssemblyFullName}({AssemblyFileHash})")
        /// </summary>
        public int DurableHash { get; set; }

        /// <summary>
        /// The Sha1Int() of "{Namespace}.{TypeName}::{AssemblyFullName}({AssemblyFileHash})";
        /// </summary>
        public int DurableSecondaryHash { get; set; }

        public new ServiceTypeIdentifier Save(IRepository repo)
        {
            SetDurableHashes();
            if(QueryFirstOrDefault<ServiceTypeIdentifier>(repo, nameof(DurableHash), nameof(DurableSecondaryHash)) == null)
            {
                return repo.Save(this);
            }
            return this;
        }

        public static ServiceTypeIdentifier FromType(Type type, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            FileInfo commitFile = new FileInfo(Path.Combine(Assembly.GetEntryAssembly().GetFileInfo().Directory.FullName, "commit"));
            string buildNumber = "UNKNOWN";
            if (!commitFile.Exists)
            {
                logger.Warning("commit file not found: {0}", commitFile.FullName);
            }
            else
            {
                buildNumber = commitFile.ReadAllText().Trim();
            }
            FileInfo assemblyFileInfo = type.Assembly.GetFileInfo();
            ServiceTypeIdentifier result = new ServiceTypeIdentifier
            {
                BuildNumber = buildNumber,
                Namespace = type.Namespace,
                TypeName = type.Name,
                AssemblyName = assemblyFileInfo.Name,
                AssemblyFileHash = assemblyFileInfo.Sha1()
            };
            result.SetDurableHashes();
            return result;
        }

        public override string ToString()
        {
            return $"{Namespace}.{TypeName}[{BuildNumber}]::{AssemblyName}({AssemblyFileHash})";
        }

        private string ToSecondaryString()
        {
            return $"{Namespace}.{TypeName}::{AssemblyName}({AssemblyFileHash})";
        }

        public void SetDurableHashes(ILogger logger = null)
        {
            WarnForBlanks(logger);
            DurableHash = ToString().ToSha1Int();
            DurableSecondaryHash = ToSecondaryString().ToSha1Int();
        }

        public override bool Equals(object obj)
        {
            if(obj is ServiceTypeIdentifier sti)
            {
                return sti.DurableHash.Equals(DurableHash);
            }
            return false;
        }

        private void WarnForBlanks(ILogger logger = null)
        {
            foreach(string property in new[] { "BuildNumber", "Namespace", "TypeName", "AssemblyName", "AssemblyFileHash" })
            {
                WarnForBlank(property, logger);
            }
        }

        private void WarnForBlank(string propertyName, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            if (string.IsNullOrEmpty(this.Property<string>(propertyName)))
            {
                logger.Warning("{0} was blank: {1}", propertyName, ToString());
            }
        }
    }
}

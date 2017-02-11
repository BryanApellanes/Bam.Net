using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.Reflection;

namespace Bam.Net.Services.VersioningService.Data
{
    public class Versionable: RepoData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Root { get; set; }
        public List<Version> Versions { get; set; }

        public static Versionable FromObjects(IHashProvider hashProvider, params Type[] types)
        {
            Args.ThrowIfNull(types);
            Args.ThrowIf(types.Length == 0, "No objects specified");
            Versionable result = new Versionable();
            Assembly assembly = types.First().GetType().Assembly;
            foreach(Type type in types)
            {
                Args.ThrowIf(type.Assembly != assembly, "Specified types must be defined in the same assembly");
                throw new NotImplementedException("this method is not complete");
            }
            return result;
        }
    }
}

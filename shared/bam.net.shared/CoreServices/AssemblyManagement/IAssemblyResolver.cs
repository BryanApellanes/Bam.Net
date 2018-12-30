using System.Reflection;

namespace Bam.Net.CoreServices
{
    public interface IAssemblyResolver
    {
        Assembly ResolveAssembly(string assemblyName, string assemblyDirectory = null);
    }
}
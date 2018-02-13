using Bam.Net.Incubation;
using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// Represents a ClassName and MethodName plus file extension
    /// referenced for execution.
    /// </summary>
    public class ExecutionTargetInfo
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Ext { get; set; }

        public static ExecutionTargetInfo ResolveExecutionTarget(string path, Incubator serviceProvider, ProxyAlias[] proxyAliases)
        {
            ExecutionTargetInfo result = new ExecutionTargetInfo();

            Queue<string> split = new Queue<string>(path.DelimitSplit("/", "."));
            while (split.Count > 0)
            {
                string currentChunk = split.Dequeue();
                string upperred = currentChunk.ToUpperInvariant();

                if (string.IsNullOrEmpty(result.ClassName))
                {
                    if (!serviceProvider.HasClass(currentChunk) && proxyAliases != null)
                    {
                        ProxyAlias alias = proxyAliases.Where(pa => pa.Alias.Equals(currentChunk)).FirstOrDefault();
                        if (alias != null)
                        {
                            result.ClassName = alias.ClassName;
                        }
                        else
                        {
                            result.ClassName = currentChunk;
                        }
                    }
                    else
                    {
                        result.ClassName = currentChunk;
                    }
                }
                else if (string.IsNullOrEmpty(result.MethodName))
                {
                    result.MethodName = currentChunk;
                }
                else if (string.IsNullOrEmpty(result.Ext))
                {
                    result.Ext = currentChunk;
                }
            }

            return result;
        }
    }
}

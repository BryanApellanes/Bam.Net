/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.CommandLine
{
    public class ArgumentInfoHash
    {
        Dictionary<string, ArgumentInfo> innerHash;
        public ArgumentInfoHash(ArgumentInfo[] argumentInfo)
        {
            innerHash = new Dictionary<string, ArgumentInfo>(argumentInfo.Length);
            foreach (ArgumentInfo info in argumentInfo)
            {
                if (innerHash.ContainsKey(info.Name))
                    innerHash[info.Name] = info;
                else
                    innerHash.Add(info.Name, info);
            }
        }
        public string[] ArgumentNames
        {
            get
            {
                List<string> retval = new List<string>(innerHash.Keys.Count);
                foreach (string name in innerHash.Keys)
                {
                    retval.Add(name);
                }
                return retval.ToArray();
            }
        }

        public ArgumentInfo this[string argumentName]
        {
            get
            {
                if (innerHash.ContainsKey(argumentName))
                    return innerHash[argumentName];

                return null;
            }
        }
    }
}

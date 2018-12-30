/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class SearchDirectory
    {
        public SearchDirectory(int order, string relativePath)
        {
            this.SortOrder = order;
            this.RelativePath = relativePath;
        }

        public static implicit operator string(SearchDirectory searchDir)
        {
            return searchDir.RelativePath;
        }

        public static implicit operator SearchDirectory(string relativePath)
        {
            return new SearchDirectory(0, relativePath);
        }

        public int SortOrder
        {
            get;
            set;
        }

        public string RelativePath
        {
            get;
            set;
        }
    }
}

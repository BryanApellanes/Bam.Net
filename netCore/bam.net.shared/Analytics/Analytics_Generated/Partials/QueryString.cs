/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Analytics
{
    public partial class QueryString
    {
        static QueryString _empty;
        static object _emptyLock = new object();
        public static QueryString Empty
        {
            get
            {
                return _emptyLock.DoubleCheckLock(ref _empty, () =>
                {
                    QueryString val = QueryString.OneWhere(c => c.Value == "");
                    if (val == null)
                    {
                        val = new QueryString();
                        val.Value = "";
                        val.Save();
                    }

                    return val;
                });
            }
        }
    }
}

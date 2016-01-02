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
    public partial class Port
    {
        static Port _defaultPort;
        static object _ddefaultPortLock = new object();
        public static Port Default
        {
            get
            {
                return _ddefaultPortLock.DoubleCheckLock(ref _defaultPort, () =>
                {
                    Port val = Port.OneWhere(c => c.Value == 80);
                    if (val == null)
                    {
                        val = new Port();
                        val.Value = 80;
                        val.Save();
                    }

                    return val;
                });
            }
        }
    }
}

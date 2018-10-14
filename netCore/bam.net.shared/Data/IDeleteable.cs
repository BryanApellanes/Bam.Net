/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public interface IDeleteable
    {
        void Delete(Database db = null);
        void WriteDelete(SqlStringBuilder sql);
    }
}

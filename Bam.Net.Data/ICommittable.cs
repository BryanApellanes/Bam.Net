/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public interface ICommittable: IDeleteable
    {
        event ICommittableDelegate AfterCommit;
        void Commit(Database db = null);
        void WriteCommit(SqlStringBuilder sql, Database db = null);
    }
}

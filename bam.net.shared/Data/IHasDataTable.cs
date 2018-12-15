/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Bam.Net.Data
{
    public interface IHasDataTable: IHasDataRow
    {
        DataTable DataTable { get; }

        void SetDataTable(DataTable table);

        T As<T>() where T: IHasDataTable, new();
    }
}

/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataPropertyPagedQuery: PagedQuery<DataPropertyColumns, DataProperty>
    { 
		public DataPropertyPagedQuery(DataPropertyColumns orderByColumn, DataPropertyQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
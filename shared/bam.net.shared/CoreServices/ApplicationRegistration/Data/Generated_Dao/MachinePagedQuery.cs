/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class MachinePagedQuery: PagedQuery<MachineColumns, Machine>
    { 
		public MachinePagedQuery(MachineColumns orderByColumn, MachineQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
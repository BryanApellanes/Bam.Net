/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoReferenceObjects.Data
{
    public class DaoReferenceObjectCollection: DaoCollection<DaoReferenceObjectColumns, DaoReferenceObject>
    { 
		public DaoReferenceObjectCollection(){}
		public DaoReferenceObjectCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		
    }
}
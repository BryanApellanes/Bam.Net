/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.DaoReferenceObjects.Data.Qi
{
	[Proxy("daoReferenceObjectWithForeignKey")]
    public class QiDaoReferenceObjectWithForeignKey
    {	
		public object OneWhere(QiQuery query)
		{
			return DaoReferenceObjectWithForeignKey.OneWhere(query).ToJsonSafe();
		}

		public object[] Where(QiQuery query)
		{
			return DaoReferenceObjectWithForeignKey.Where(query).ToJsonSafe();
		}
	}
}
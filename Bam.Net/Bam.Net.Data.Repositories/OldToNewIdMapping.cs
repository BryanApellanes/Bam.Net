/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	public class OldToNewIdMapping
	{
		public OldToNewIdMapping() { }		

		public Type PocoType { get; set; }
		public Type DaoType { get; set; }
		public long OldId { get; set; }
		public long NewId { get; set; }
		public string Uuid { get; set; }

		public override int GetHashCode()
		{
			return Uuid.GetHashCode();
		}
		public override bool Equals(object obj)
		{
			OldToNewIdMapping o = obj as OldToNewIdMapping;
			if (o != null)
			{
				return o.Uuid == this.Uuid;
			}
			else
			{
				return base.Equals(obj);
			}
		}
	}
}

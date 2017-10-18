/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
	public class DtoPropertyModel
	{
        internal DtoPropertyModel()
        { }

		public DtoPropertyModel(PropertyInfo property)
		{
			this.PropertyName = property.Name;
			this.PropertyType = property.PropertyType.Name;
		}

		public string PropertyName { get; set; }
		public string PropertyType { get; set; }
	}
}

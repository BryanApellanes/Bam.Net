using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Newtonsoft.Json;
using Bam.Net.Data.Dynamic.Data;
using Bam.Net.Data.Dynamic.Data.Dao;

namespace Bam.Net.Data.Dynamic.Data.Wrappers
{
	// generated
	[Serializable]
	public class DataInstanceWrapper: Bam.Net.Data.Dynamic.Data.DataInstance, IHasUpdatedXrefCollectionProperties
	{
		public DataInstanceWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public DataInstanceWrapper(DaoRepository repository) : this()
		{
			this.Repository = repository;
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		[JsonIgnore]
		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(UpdatedXrefCollectionProperties != null && !UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else if(UpdatedXrefCollectionProperties != null)
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}

System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue> _properties;
		public override System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue> Properties
		{
			get
			{
				if (_properties == null)
				{
					_properties = Repository.ForeignKeyCollectionLoader<Bam.Net.Data.Dynamic.Data.DataInstance, Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue>(this).ToList();
				}
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}


	}
	// -- generated
}																								

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
	public class DataInstancePropertyValueWrapper: Bam.Net.Data.Dynamic.Data.DataInstancePropertyValue, IHasUpdatedXrefCollectionProperties
	{
		public DataInstancePropertyValueWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public DataInstancePropertyValueWrapper(DaoRepository repository) : this()
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


Bam.Net.Data.Dynamic.Data.DataInstance _dataInstance;
		public override Bam.Net.Data.Dynamic.Data.DataInstance DataInstance
		{
			get
			{
				if (_dataInstance == null)
				{
					_dataInstance = (Bam.Net.Data.Dynamic.Data.DataInstance)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Data.Dynamic.Data.DataInstance));
				}
				return _dataInstance;
			}
			set
			{
				_dataInstance = value;
			}
		}

	}
	// -- generated
}																								

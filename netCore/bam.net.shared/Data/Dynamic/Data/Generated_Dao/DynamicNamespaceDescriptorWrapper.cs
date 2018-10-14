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
	public class DynamicNamespaceDescriptorWrapper: Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public DynamicNamespaceDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public DynamicNamespaceDescriptorWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor> _types;
		public override System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor> Types
		{
			get
			{
				if (_types == null)
				{
					_types = Repository.ForeignKeyCollectionLoader<Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor, Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor>(this).ToList();
				}
				return _types;
			}
			set
			{
				_types = value;
			}
		}


	}
	// -- generated
}																								

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
	public class DynamicTypeDescriptorWrapper: Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public DynamicTypeDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public DynamicTypeDescriptorWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor> _properties;
		public override System.Collections.Generic.List<Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor> Properties
		{
			get
			{
				if (_properties == null)
				{
					_properties = Repository.ForeignKeyCollectionLoader<Bam.Net.Data.Dynamic.Data.DynamicTypeDescriptor, Bam.Net.Data.Dynamic.Data.DynamicTypePropertyDescriptor>(this).ToList();
				}
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}
Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor _dynamicNamespaceDescriptor;
		public override Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor DynamicNamespaceDescriptor
		{
			get
			{
				if (_dynamicNamespaceDescriptor == null)
				{
					_dynamicNamespaceDescriptor = (Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Data.Dynamic.Data.DynamicNamespaceDescriptor));
				}
				return _dynamicNamespaceDescriptor;
			}
			set
			{
				_dynamicNamespaceDescriptor = value;
			}
		}

	}
	// -- generated
}																								

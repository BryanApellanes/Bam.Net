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
using Bam.Net.CoreServices.AccessControl.Data;
using Bam.Net.CoreServices.AccessControl.Data.Dao;

namespace Bam.Net.CoreServices.AccessControl.Data.Wrappers
{
	// generated
	[Serializable]
	public class PermissionSpecificationWrapper: Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification, IHasUpdatedXrefCollectionProperties
	{
		public PermissionSpecificationWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public PermissionSpecificationWrapper(DaoRepository repository) : this()
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


Bam.Net.CoreServices.AccessControl.Data.Resource _resource;
		public override Bam.Net.CoreServices.AccessControl.Data.Resource Resource
		{
			get
			{
				if (_resource == null)
				{
					_resource = (Bam.Net.CoreServices.AccessControl.Data.Resource)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.CoreServices.AccessControl.Data.Resource));
				}
				return _resource;
			}
			set
			{
				_resource = value;
			}
		}

	}
	// -- generated
}																								

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
	public class ResourceWrapper: Bam.Net.CoreServices.AccessControl.Data.Resource, IHasUpdatedXrefCollectionProperties
	{
		public ResourceWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ResourceWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification> _permissions;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification> Permissions
		{
			get
			{
				if (_permissions == null)
				{
					_permissions = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.AccessControl.Data.Resource, Bam.Net.CoreServices.AccessControl.Data.PermissionSpecification>(this).ToList();
				}
				return _permissions;
			}
			set
			{
				_permissions = value;
			}
		}


	}
	// -- generated
}																								

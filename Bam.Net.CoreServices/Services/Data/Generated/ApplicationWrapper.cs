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
using Bam.Net.CoreServices.Data;
using Bam.Net.CoreServices.Data.Daos;

namespace Bam.Net.CoreServices.Data.Wrappers
{
	// generated
	[Serializable]
	public class ApplicationWrapper: Bam.Net.CoreServices.Data.Application, IHasUpdatedXrefCollectionProperties
	{
		public ApplicationWrapper(DaoRepository repository)
		{
			this.Repository = repository;
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(!UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}

System.Collections.Generic.List<Bam.Net.CoreServices.Data.HostName> _hostNames;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.Data.HostName> HostNames
		{
			get
			{
				if (_hostNames == null)
				{
					_hostNames = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.HostName>(this).ToList();
				}
				return _hostNames;
			}
			set
			{
				_hostNames = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.Data.ApiKey> _apiKeys;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.Data.ApiKey> ApiKeys
		{
			get
			{
				if (_apiKeys == null)
				{
					_apiKeys = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.ApiKey>(this).ToList();
				}
				return _apiKeys;
			}
			set
			{
				_apiKeys = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.Data.ApplicationInstance> _instances;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.Data.ApplicationInstance> Instances
		{
			get
			{
				if (_instances == null)
				{
					_instances = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.ApplicationInstance>(this).ToList();
				}
				return _instances;
			}
			set
			{
				_instances = value;
			}
		}
Bam.Net.CoreServices.Data.Organization _organization;
		public override Bam.Net.CoreServices.Data.Organization Organization
		{
			get
			{
				if (_organization == null)
				{
					_organization = (Bam.Net.CoreServices.Data.Organization)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.CoreServices.Data.Organization));
				}
				return _organization;
			}
			set
			{
				_organization = value;
			}
		}

	}
	// -- generated
}																								

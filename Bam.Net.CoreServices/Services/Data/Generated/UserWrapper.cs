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
	public class UserWrapper: Bam.Net.CoreServices.Data.User, IHasUpdatedXrefCollectionProperties
	{
		public UserWrapper(DaoRepository repository)
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

Bam.Net.CoreServices.Data.Subscription[] _subscriptions;
		public override Bam.Net.CoreServices.Data.Subscription[] Subscriptions
		{
			get
			{
				if (_subscriptions == null)
				{
					_subscriptions = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.Subscription>(this).ToArray();
				}
				return _subscriptions;
			}
			set
			{
				_subscriptions = value;
			}
		}

// Xref property: Left -> User ; Right -> Organization

		List<Bam.Net.CoreServices.Data.Organization> _organizations;
		public override List<Bam.Net.CoreServices.Data.Organization> Organizations
		{
			get
			{
				if(_organizations == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.UserOrganization,  Bam.Net.CoreServices.Data.Daos.Organization>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _organizations = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.Organization>().ToList();
					 SetUpdatedXrefCollectionProperty("Organizations", this.GetType().GetProperty("Organizations"));
				}

				return _organizations;
			}
			set
			{
				_organizations = value;
				SetUpdatedXrefCollectionProperty("Organizations", this.GetType().GetProperty("Organizations"));
			}
		}
	}
	// -- generated
}																								

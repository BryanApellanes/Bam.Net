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
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.CoreServices.ApplicationRegistration.Dao;

namespace Bam.Net.CoreServices.ApplicationRegistration.Wrappers
{
	// generated
	[Serializable]
	public class UserWrapper: Bam.Net.CoreServices.ApplicationRegistration.User, IHasUpdatedXrefCollectionProperties
	{
		public UserWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public UserWrapper(DaoRepository repository) : this()
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

Bam.Net.CoreServices.ApplicationRegistration.Subscription[] _subscriptions;
		public override Bam.Net.CoreServices.ApplicationRegistration.Subscription[] Subscriptions
		{
			get
			{
				if (_subscriptions == null)
				{
					_subscriptions = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.User, Bam.Net.CoreServices.ApplicationRegistration.Subscription>(this).ToArray();
				}
				return _subscriptions;
			}
			set
			{
				_subscriptions = value;
			}
		}


// Xref property: Left -> Organization ; Right -> User

		List<Bam.Net.CoreServices.ApplicationRegistration.Organization> _organizations;
		public override List<Bam.Net.CoreServices.ApplicationRegistration.Organization> Organizations
		{
			get
			{
				if(_organizations == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.ApplicationRegistration.Dao.OrganizationUser, Bam.Net.CoreServices.ApplicationRegistration.Dao.Organization>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _organizations = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ApplicationRegistration.Organization>().ToList();
					 SetUpdatedXrefCollectionProperty("Organizations", this.GetType().GetProperty("Organizations"));
				}

				return _organizations;
			}
			set
			{
				_organizations = value;
				SetUpdatedXrefCollectionProperty("Organizations", this.GetType().GetProperty("Organizations"));
			}
		}	}
	// -- generated
}																								

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
	public class OrganizationWrapper: Bam.Net.CoreServices.Data.Organization, IHasUpdatedXrefCollectionProperties
	{
		public OrganizationWrapper(DaoRepository repository)
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

Bam.Net.CoreServices.Data.Application[] _applications;
		public override Bam.Net.CoreServices.Data.Application[] Applications
		{
			get
			{
				if (_applications == null)
				{
					_applications = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.Application>(this).ToArray();
				}
				return _applications;
			}
			set
			{
				_applications = value;
			}
		}


// Xref property: Left -> User ; Right -> Organization

		Bam.Net.CoreServices.Data.User[] _users;
		public override Bam.Net.CoreServices.Data.User[] Users
		{
			get
			{
				if(_users == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.UserOrganization, Bam.Net.CoreServices.Data.Daos.User>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _users = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.User>().ToArray();
					 SetUpdatedXrefCollectionProperty("Users", this.GetType().GetProperty("Users"));
				}

				return _users;
			}
			set
			{
				_users = value;
				SetUpdatedXrefCollectionProperty("Users", this.GetType().GetProperty("Users"));
			}
		}	}
	// -- generated
}																								

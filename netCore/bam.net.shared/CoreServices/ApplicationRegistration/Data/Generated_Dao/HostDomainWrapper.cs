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
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Wrappers
{
	// generated
	[Serializable]
	public class HostDomainWrapper: Bam.Net.CoreServices.ApplicationRegistration.Data.HostDomain, IHasUpdatedXrefCollectionProperties
	{
		public HostDomainWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public HostDomainWrapper(DaoRepository repository) : this()
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



// Xref property: Left -> HostDomain ; Right -> Application

		List<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> _applications;
		public override List<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> Applications
		{
			get
			{
				if(_applications == null || _applications.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.HostDomainApplication,  Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_applications = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ApplicationRegistration.Data.Application>().ToList();
					SetUpdatedXrefCollectionProperty("Applications", this.GetType().GetProperty("Applications"));					
				}

				return _applications;
			}
			set
			{
				_applications = value;
				SetUpdatedXrefCollectionProperty("Applications", this.GetType().GetProperty("Applications"));
			}
		}
	}
	// -- generated
}																								

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
	public class ApplicationWrapper: Bam.Net.CoreServices.ApplicationRegistration.Application, IHasUpdatedXrefCollectionProperties
	{
		public ApplicationWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ApplicationWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ApiKey> _apiKeys;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ApiKey> ApiKeys
		{
			get
			{
				if (_apiKeys == null)
				{
					_apiKeys = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Application, Bam.Net.CoreServices.ApplicationRegistration.ApiKey>(this).ToList();
				}
				return _apiKeys;
			}
			set
			{
				_apiKeys = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor> _instances;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor> Instances
		{
			get
			{
				if (_instances == null)
				{
					_instances = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Application, Bam.Net.CoreServices.ApplicationRegistration.ProcessDescriptor>(this).ToList();
				}
				return _instances;
			}
			set
			{
				_instances = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Configuration> _configurations;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Configuration> Configurations
		{
			get
			{
				if (_configurations == null)
				{
					_configurations = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Application, Bam.Net.CoreServices.ApplicationRegistration.Configuration>(this).ToList();
				}
				return _configurations;
			}
			set
			{
				_configurations = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Client> _clients;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Client> Clients
		{
			get
			{
				if (_clients == null)
				{
					_clients = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Application, Bam.Net.CoreServices.ApplicationRegistration.Client>(this).ToList();
				}
				return _clients;
			}
			set
			{
				_clients = value;
			}
		}
Bam.Net.CoreServices.ApplicationRegistration.Organization _organization;
		public override Bam.Net.CoreServices.ApplicationRegistration.Organization Organization
		{
			get
			{
				if (_organization == null)
				{
					_organization = (Bam.Net.CoreServices.ApplicationRegistration.Organization)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.CoreServices.ApplicationRegistration.Organization));
				}
				return _organization;
			}
			set
			{
				_organization = value;
			}
		}
// Xref property: Left -> Application ; Right -> Machine

		List<Bam.Net.CoreServices.ApplicationRegistration.Machine> _machines;
		public override List<Bam.Net.CoreServices.ApplicationRegistration.Machine> Machines
		{
			get
			{
				if(_machines == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.ApplicationRegistration.Dao.ApplicationMachine,  Bam.Net.CoreServices.ApplicationRegistration.Dao.Machine>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _machines = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ApplicationRegistration.Machine>().ToList();
					 SetUpdatedXrefCollectionProperty("Machines", this.GetType().GetProperty("Machines"));
				}

				return _machines;
			}
			set
			{
				_machines = value;
				SetUpdatedXrefCollectionProperty("Machines", this.GetType().GetProperty("Machines"));
			}
		}
// Xref property: Left -> HostDomain ; Right -> Application

		List<Bam.Net.CoreServices.ApplicationRegistration.HostDomain> _hostDomains;
		public override List<Bam.Net.CoreServices.ApplicationRegistration.HostDomain> HostDomains
		{
			get
			{
				if(_hostDomains == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomainApplication, Bam.Net.CoreServices.ApplicationRegistration.Dao.HostDomain>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _hostDomains = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ApplicationRegistration.HostDomain>().ToList();
					 SetUpdatedXrefCollectionProperty("HostDomains", this.GetType().GetProperty("HostDomains"));
				}

				return _hostDomains;
			}
			set
			{
				_hostDomains = value;
				SetUpdatedXrefCollectionProperty("HostDomains", this.GetType().GetProperty("HostDomains"));
			}
		}	}
	// -- generated
}																								

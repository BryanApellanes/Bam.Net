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
	public class MachineWrapper: Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, IHasUpdatedXrefCollectionProperties
	{
		public MachineWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public MachineWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration> _configurations;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration> Configurations
		{
			get
			{
				if (_configurations == null)
				{
					_configurations = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, Bam.Net.CoreServices.ApplicationRegistration.Data.Configuration>(this).ToList();
				}
				return _configurations;
			}
			set
			{
				_configurations = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress> _hostAddresses;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress> HostAddresses
		{
			get
			{
				if (_hostAddresses == null)
				{
					_hostAddresses = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, Bam.Net.CoreServices.ApplicationRegistration.Data.HostAddress>(this).ToList();
				}
				return _hostAddresses;
			}
			set
			{
				_hostAddresses = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor> _processes;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor> Processes
		{
			get
			{
				if (_processes == null)
				{
					_processes = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, Bam.Net.CoreServices.ApplicationRegistration.Data.ProcessDescriptor>(this).ToList();
				}
				return _processes;
			}
			set
			{
				_processes = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic> _networkInterfaces;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Nic> NetworkInterfaces
		{
			get
			{
				if (_networkInterfaces == null)
				{
					_networkInterfaces = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, Bam.Net.CoreServices.ApplicationRegistration.Data.Nic>(this).ToList();
				}
				return _networkInterfaces;
			}
			set
			{
				_networkInterfaces = value;
			}
		}System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Client> _clients;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.Data.Client> Clients
		{
			get
			{
				if (_clients == null)
				{
					_clients = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Data.Machine, Bam.Net.CoreServices.ApplicationRegistration.Data.Client>(this).ToList();
				}
				return _clients;
			}
			set
			{
				_clients = value;
			}
		}


// Xref property: Left -> Application ; Right -> Machine

		List<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> _applications;
		public override List<Bam.Net.CoreServices.ApplicationRegistration.Data.Application> Applications
		{
			get
			{
				if(_applications == null || _applications.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.ApplicationMachine, Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Application>(Repository.GetDaoInstance(this), false);
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
		}	}
	// -- generated
}																								

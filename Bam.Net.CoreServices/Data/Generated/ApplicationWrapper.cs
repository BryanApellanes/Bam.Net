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

System.Collections.Generic.List<Bam.Net.CoreServices.Data.ApiKey> _apiKeys;
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
		}System.Collections.Generic.List<Bam.Net.CoreServices.Data.ProcessDescriptor> _instances;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.Data.ProcessDescriptor> Instances
		{
			get
			{
				if (_instances == null)
				{
					_instances = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.ProcessDescriptor>(this).ToList();
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

// Xref property: Left -> Configuration ; Right -> Application

		List<Bam.Net.CoreServices.Data.Configuration> _configurations;
		public override List<Bam.Net.CoreServices.Data.Configuration> Configuration
		{
			get
			{
				if(_configurations == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.ConfigurationApplication, Bam.Net.CoreServices.Data.Daos.Configuration>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _configurations = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.Configuration>().ToList();
					 SetUpdatedXrefCollectionProperty("Configurations", this.GetType().GetProperty("Configuration"));
				}

				return _configurations;
			}
			set
			{
				_configurations = value;
				SetUpdatedXrefCollectionProperty("Configurations", this.GetType().GetProperty("Configuration"));
			}
		}// Xref property: Left -> Machine ; Right -> Application

		List<Bam.Net.CoreServices.Data.Machine> _machines;
		public override List<Bam.Net.CoreServices.Data.Machine> Machines
		{
			get
			{
				if(_machines == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.MachineApplication, Bam.Net.CoreServices.Data.Daos.Machine>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _machines = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.Machine>().ToList();
					 SetUpdatedXrefCollectionProperty("Machines", this.GetType().GetProperty("Machines"));
				}

				return _machines;
			}
			set
			{
				_machines = value;
				SetUpdatedXrefCollectionProperty("Machines", this.GetType().GetProperty("Machines"));
			}
		}	}
	// -- generated
}																								

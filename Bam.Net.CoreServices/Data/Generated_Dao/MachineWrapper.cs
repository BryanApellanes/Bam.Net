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
	public class MachineWrapper: Bam.Net.CoreServices.Data.Machine, IHasUpdatedXrefCollectionProperties
	{
		public MachineWrapper(DaoRepository repository)
		{
			this.Repository = repository;
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		[JsonIgnore]
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

System.Collections.Generic.List<Bam.Net.CoreServices.Data.ProcessDescriptor> _processes;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.Data.ProcessDescriptor> Processes
		{
			get
			{
				if (_processes == null)
				{
					_processes = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.Data.ProcessDescriptor>(this).ToList();
				}
				return _processes;
			}
			set
			{
				_processes = value;
			}
		}

// Xref property: Left -> Machine ; Right -> Application

		List<Bam.Net.CoreServices.Data.Application> _applications;
		public override List<Bam.Net.CoreServices.Data.Application> Applications
		{
			get
			{
				if(_applications == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.MachineApplication,  Bam.Net.CoreServices.Data.Daos.Application>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _applications = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.Application>().ToList();
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
// Xref property: Left -> Configuration ; Right -> Machine

		List<Bam.Net.CoreServices.Data.Configuration> _configurations;
		public override List<Bam.Net.CoreServices.Data.Configuration> Configurations
		{
			get
			{
				if(_configurations == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.ConfigurationMachine, Bam.Net.CoreServices.Data.Daos.Configuration>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _configurations = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.Data.Configuration>().ToList();
					 SetUpdatedXrefCollectionProperty("Configurations", this.GetType().GetProperty("Configurations"));
				}

				return _configurations;
			}
			set
			{
				_configurations = value;
				SetUpdatedXrefCollectionProperty("Configurations", this.GetType().GetProperty("Configurations"));
			}
		}	}
	// -- generated
}																								

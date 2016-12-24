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
	public class ConfigurationWrapper: Bam.Net.CoreServices.Data.Configuration, IHasUpdatedXrefCollectionProperties
	{
		public ConfigurationWrapper(DaoRepository repository)
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



// Xref property: Left -> Configuration ; Right -> Machine

		List<Bam.Net.CoreServices.Data.Machine> _machines;
		public override List<Bam.Net.CoreServices.Data.Machine> Machines
		{
			get
			{
				if(_machines == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.ConfigurationMachine,  Bam.Net.CoreServices.Data.Daos.Machine>(Repository.GetDaoInstance(this), false);
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
		}// Xref property: Left -> Configuration ; Right -> Application

		List<Bam.Net.CoreServices.Data.Application> _applications;
		public override List<Bam.Net.CoreServices.Data.Application> Applications
		{
			get
			{
				if(_applications == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.CoreServices.Data.Daos.ConfigurationApplication,  Bam.Net.CoreServices.Data.Daos.Application>(Repository.GetDaoInstance(this), false);
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
	}
	// -- generated
}																								

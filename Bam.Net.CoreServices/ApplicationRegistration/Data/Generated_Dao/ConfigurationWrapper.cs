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
	public class ConfigurationWrapper: Bam.Net.CoreServices.ApplicationRegistration.Configuration, IHasUpdatedXrefCollectionProperties
	{
		public ConfigurationWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ConfigurationWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting> _settings;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting> Settings
		{
			get
			{
				if (_settings == null)
				{
					_settings = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.ApplicationRegistration.Configuration, Bam.Net.CoreServices.ApplicationRegistration.ConfigurationSetting>(this).ToList();
				}
				return _settings;
			}
			set
			{
				_settings = value;
			}
		}
Bam.Net.CoreServices.ApplicationRegistration.Application _application;
		public override Bam.Net.CoreServices.ApplicationRegistration.Application Application
		{
			get
			{
				if (_application == null)
				{
					_application = (Bam.Net.CoreServices.ApplicationRegistration.Application)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.CoreServices.ApplicationRegistration.Application));
				}
				return _application;
			}
			set
			{
				_application = value;
			}
		}Bam.Net.CoreServices.ApplicationRegistration.Machine _machine;
		public override Bam.Net.CoreServices.ApplicationRegistration.Machine Machine
		{
			get
			{
				if (_machine == null)
				{
					_machine = (Bam.Net.CoreServices.ApplicationRegistration.Machine)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.CoreServices.ApplicationRegistration.Machine));
				}
				return _machine;
			}
			set
			{
				_machine = value;
			}
		}

	}
	// -- generated
}																								

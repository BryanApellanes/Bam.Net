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
	public class ApiKeyWrapper: Bam.Net.CoreServices.ApplicationRegistration.ApiKey, IHasUpdatedXrefCollectionProperties
	{
		public ApiKeyWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ApiKeyWrapper(DaoRepository repository) : this()
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
		}

	}
	// -- generated
}																								

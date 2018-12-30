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
using Bam.Net.CoreServices.ServiceRegistration.Data;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao;

namespace Bam.Net.CoreServices.ServiceRegistration.Data.Wrappers
{
	// generated
	[Serializable]
	public class ServiceRegistryDescriptorWrapper: Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public ServiceRegistryDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ServiceRegistryDescriptorWrapper(DaoRepository repository) : this()
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




// Xref property: Left -> ServiceDescriptor ; Right -> ServiceRegistryDescriptor

		List<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor> _serviceDescriptors;
		public override List<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor> Services
		{
			get
			{
				if(_serviceDescriptors == null || _serviceDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor, Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_serviceDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("ServiceDescriptors", this.GetType().GetProperty("Services"));					
				}

				return _serviceDescriptors;
			}
			set
			{
				_serviceDescriptors = value;
				SetUpdatedXrefCollectionProperty("ServiceDescriptors", this.GetType().GetProperty("Services"));
			}
		}	}
	// -- generated
}																								

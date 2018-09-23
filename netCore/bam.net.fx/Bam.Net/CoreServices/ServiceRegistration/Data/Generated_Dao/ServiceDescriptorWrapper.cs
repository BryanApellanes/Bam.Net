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
	public class ServiceDescriptorWrapper: Bam.Net.CoreServices.ServiceRegistration.Data.ServiceDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public ServiceDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ServiceDescriptorWrapper(DaoRepository repository) : this()
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

		List<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor> _serviceRegistryDescriptors;
		public override List<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor> ServiceRegistry
		{
			get
			{
				if(_serviceRegistryDescriptors == null || _serviceRegistryDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceDescriptorServiceRegistryDescriptor,  Bam.Net.CoreServices.ServiceRegistration.Data.Dao.ServiceRegistryDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_serviceRegistryDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.ServiceRegistration.Data.ServiceRegistryDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("ServiceRegistryDescriptors", this.GetType().GetProperty("ServiceRegistry"));					
				}

				return _serviceRegistryDescriptors;
			}
			set
			{
				_serviceRegistryDescriptors = value;
				SetUpdatedXrefCollectionProperty("ServiceRegistryDescriptors", this.GetType().GetProperty("ServiceRegistry"));
			}
		}
	}
	// -- generated
}																								

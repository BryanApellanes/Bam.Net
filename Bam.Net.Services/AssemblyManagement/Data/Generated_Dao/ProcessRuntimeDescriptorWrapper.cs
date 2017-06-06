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
using Bam.Net.Services.AssemblyManagement.Data;
using Bam.Net.Services.AssemblyManagement.Data.Dao;

namespace Bam.Net.Services.AssemblyManagement.Data.Wrappers
{
	// generated
	[Serializable]
	public class ProcessRuntimeDescriptorWrapper: Bam.Net.Services.AssemblyManagement.Data.ProcessRuntimeDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public ProcessRuntimeDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public ProcessRuntimeDescriptorWrapper(DaoRepository repository) : this()
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




// Xref property: Left -> AssemblyDescriptor ; Right -> ProcessRuntimeDescriptor

		Bam.Net.Services.AssemblyManagement.Data.AssemblyDescriptor[] _assemblyDescriptors;
		public override Bam.Net.Services.AssemblyManagement.Data.AssemblyDescriptor[] AssemblyDescriptors
		{
			get
			{
				if(_assemblyDescriptors == null)
				{
					 var xref = new XrefDaoCollection<Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor, Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyDescriptor>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _assemblyDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.Services.AssemblyManagement.Data.AssemblyDescriptor>().ToArray();
					 SetUpdatedXrefCollectionProperty("AssemblyDescriptors", this.GetType().GetProperty("AssemblyDescriptors"));
				}

				return _assemblyDescriptors;
			}
			set
			{
				_assemblyDescriptors = value;
				SetUpdatedXrefCollectionProperty("AssemblyDescriptors", this.GetType().GetProperty("AssemblyDescriptors"));
			}
		}	}
	// -- generated
}																								

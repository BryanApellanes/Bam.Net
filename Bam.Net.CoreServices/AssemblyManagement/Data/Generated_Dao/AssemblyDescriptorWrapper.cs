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
using Bam.Net.CoreServices.AssemblyManagement.Data;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Wrappers
{
	// generated
	[Serializable]
	public class AssemblyDescriptorWrapper: Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public AssemblyDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public AssemblyDescriptorWrapper(DaoRepository repository) : this()
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

		List<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor> _processRuntimeDescriptors;
		public override List<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor> ProcessRuntimeDescriptors
		{
			get
			{
				if(_processRuntimeDescriptors == null || _processRuntimeDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptorProcessRuntimeDescriptor,  Bam.Net.CoreServices.AssemblyManagement.Data.Dao.ProcessRuntimeDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_processRuntimeDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.AssemblyManagement.Data.ProcessRuntimeDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("ProcessRuntimeDescriptors", this.GetType().GetProperty("ProcessRuntimeDescriptors"));					
				}

				return _processRuntimeDescriptors;
			}
			set
			{
				_processRuntimeDescriptors = value;
				SetUpdatedXrefCollectionProperty("ProcessRuntimeDescriptors", this.GetType().GetProperty("ProcessRuntimeDescriptors"));
			}
		}// Xref property: Left -> AssemblyDescriptor ; Right -> AssemblyReferenceDescriptor

		List<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor> _assemblyReferenceDescriptors;
		public override List<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor> AssemblyReferenceDescriptors
		{
			get
			{
				if(_assemblyReferenceDescriptors == null || _assemblyReferenceDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptorAssemblyReferenceDescriptor,  Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_assemblyReferenceDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("AssemblyReferenceDescriptors", this.GetType().GetProperty("AssemblyReferenceDescriptors"));					
				}

				return _assemblyReferenceDescriptors;
			}
			set
			{
				_assemblyReferenceDescriptors = value;
				SetUpdatedXrefCollectionProperty("AssemblyReferenceDescriptors", this.GetType().GetProperty("AssemblyReferenceDescriptors"));
			}
		}
	}
	// -- generated
}																								

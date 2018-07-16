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
using Bam.Net.Automation.Testing.Data;
using Bam.Net.Automation.Testing.Data.Dao;

namespace Bam.Net.Automation.Testing.Data.Wrappers
{
	// generated
	[Serializable]
	public class TestSuiteDefinitionWrapper: Bam.Net.Automation.Testing.Data.TestSuiteDefinition, IHasUpdatedXrefCollectionProperties
	{
		public TestSuiteDefinitionWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public TestSuiteDefinitionWrapper(DaoRepository repository) : this()
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

Bam.Net.Automation.Testing.Data.TestDefinition[] _testDefinitions;
		public override Bam.Net.Automation.Testing.Data.TestDefinition[] TestDefinitions
		{
			get
			{
				if (_testDefinitions == null)
				{
					_testDefinitions = Repository.ForeignKeyCollectionLoader<Bam.Net.Automation.Testing.Data.TestSuiteDefinition, Bam.Net.Automation.Testing.Data.TestDefinition>(this).ToArray();
				}
				return _testDefinitions;
			}
			set
			{
				_testDefinitions = value;
			}
		}


	}
	// -- generated
}																								

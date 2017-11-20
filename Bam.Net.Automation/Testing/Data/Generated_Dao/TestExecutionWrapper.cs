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
	public class TestExecutionWrapper: Bam.Net.Automation.Testing.Data.TestExecution, IHasUpdatedXrefCollectionProperties
	{
		public TestExecutionWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public TestExecutionWrapper(DaoRepository repository) : this()
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


Bam.Net.Automation.Testing.Data.TestDefinition _testDefinition;
		public override Bam.Net.Automation.Testing.Data.TestDefinition TestDefinition
		{
			get
			{
				if (_testDefinition == null)
				{
					_testDefinition = (Bam.Net.Automation.Testing.Data.TestDefinition)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Automation.Testing.Data.TestDefinition));
				}
				return _testDefinition;
			}
			set
			{
				_testDefinition = value;
			}
		}Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary _testSuiteExecutionSummary;
		public override Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary TestSuiteExecutionSummary
		{
			get
			{
				if (_testSuiteExecutionSummary == null)
				{
					_testSuiteExecutionSummary = (Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary));
				}
				return _testSuiteExecutionSummary;
			}
			set
			{
				_testSuiteExecutionSummary = value;
			}
		}

	}
	// -- generated
}																								

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
	public class TestSuiteExecutionSummaryWrapper: Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary, IHasUpdatedXrefCollectionProperties
	{
		public TestSuiteExecutionSummaryWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public TestSuiteExecutionSummaryWrapper(DaoRepository repository) : this()
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

Bam.Net.Automation.Testing.Data.TestExecution[] _testExecutions;
		public override Bam.Net.Automation.Testing.Data.TestExecution[] TestExecutions
		{
			get
			{
				if (_testExecutions == null)
				{
					_testExecutions = Repository.ForeignKeyCollectionLoader<Bam.Net.Automation.Testing.Data.TestSuiteExecutionSummary, Bam.Net.Automation.Testing.Data.TestExecution>(this).ToArray();
				}
				return _testExecutions;
			}
			set
			{
				_testExecutions = value;
			}
		}


	}
	// -- generated
}																								

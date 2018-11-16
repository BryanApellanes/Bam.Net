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
using Bam.Net.Server.Tests;
using Bam.Net.Server.Tests.Dao;

namespace TypeDaos.Wrappers
{
	// generated
	[Serializable]
	public class TestClassWrapper: Bam.Net.Server.Tests.TestClass, IHasUpdatedXrefCollectionProperties
	{
		public TestClassWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public TestClassWrapper(DaoRepository repository) : this()
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

Bam.Net.Server.Tests.TestStudent[] _testStudents;
		public override Bam.Net.Server.Tests.TestStudent[] TestStudents
		{
			get
			{
				if (_testStudents == null)
				{
					_testStudents = Repository.ForeignKeyCollectionLoader<Bam.Net.Server.Tests.TestClass, Bam.Net.Server.Tests.TestStudent>(this).ToArray();
				}
				return _testStudents;
			}
			set
			{
				_testStudents = value;
			}
		}


	}
	// -- generated
}																								

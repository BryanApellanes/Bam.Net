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
	public class TestStudentWrapper: Bam.Net.Server.Tests.TestStudent, IHasUpdatedXrefCollectionProperties
	{
		public TestStudentWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public TestStudentWrapper(DaoRepository repository) : this()
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


Bam.Net.Server.Tests.TestClass _testClass;
		public override Bam.Net.Server.Tests.TestClass TestClass
		{
			get
			{
				if (_testClass == null)
				{
					_testClass = (Bam.Net.Server.Tests.TestClass)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Server.Tests.TestClass));
				}
				return _testClass;
			}
			set
			{
				_testClass = value;
			}
		}

	}
	// -- generated
}																								

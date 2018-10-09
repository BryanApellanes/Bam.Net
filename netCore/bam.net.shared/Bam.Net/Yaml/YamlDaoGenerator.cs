/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Logging;
using System.IO;
using System.Reflection;

namespace Bam.Net.Yaml
{
	/// <summary>
	/// A class used to generate Daos.  Internally
	/// uses a YamlSchemaGenerator to generate a YamlSchema, a 
	/// YamlTypeSchemaGenerator to transform the YamlSchema into 
	/// a TypeSchema and a TypeDaoGenerator to create a Dao Assembly
	/// from the TypeSchema.  NOTE: this is not well tested
	/// </summary>
	public partial class YamlDaoGenerator : TypeDaoGenerator
	{
		public YamlDaoGenerator(ILogger logger = null): base()
		{
			this.YamlSchemaGenerator = new YamlSchemaGenerator();
			this.CheckIdField = true;
			this.IncludeModifiedBy = true;
			
			if (logger != null)
			{
				this.Subscribe(logger);
				this.Subscribe(this.YamlSchemaGenerator);
			}
		}

		public List<YamlDeserializationFailure> DeserializationFailures
		{
			get;
			private set;
		}

		public List<Type> DynamicYamlTypes { get; private set; }

		protected YamlSchemaGenerator YamlSchemaGenerator { get; private set; }
	}
}

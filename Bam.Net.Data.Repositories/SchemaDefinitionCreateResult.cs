/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
	public sealed class SchemaDefinitionCreateResult
	{
		public SchemaDefinitionCreateResult(SchemaDefinition schemaDefinition, TypeSchema typeSchema, KeyColumn[] missingKeyColumns = null, ForeignKeyColumn[] missingForeignKeyColumns = null) 
		{
			this.SchemaDefinition = schemaDefinition;
			this.TypeSchema = typeSchema;
			this.Warnings = new SchemaWarnings(missingKeyColumns, missingForeignKeyColumns);
		}

		public TypeSchema TypeSchema { get; private set; }

		public SchemaDefinition SchemaDefinition { get; private set; }

		public SchemaWarnings Warnings { get; private set; }

		public bool MissingColumns 
		{
			get 
			{
				return Warnings.MissingKeyColumns.Length > 0 || Warnings.MissingForeignKeyColumns.Length > 0; 
			}
		}
	}
}

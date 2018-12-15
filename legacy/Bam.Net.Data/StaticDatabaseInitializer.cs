/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Data
{
	public class StaticDatabaseInitializer: Loggable, IDatabaseInitializer
	{
		public StaticDatabaseInitializer()
		{
			_databasesByConnectionName = new Dictionary<string, Database>();
		}

		Dictionary<string, Database> _databasesByConnectionName;
		public Dictionary<string, Database> DatabasesByConnectionName
		{
			get
			{
				return _databasesByConnectionName;
			}
		}

		public string LastConnectionName { get; set; }

		[Verbosity(VerbosityLevel.Information, MessageFormat="No database was added for the connection named {LastConnectionName}")]
		public event EventHandler DatabaseNotFound;

		#region IDatabaseInitializer Members

		public DatabaseInitializationResult Initialize(string connectionName)
		{
			LastConnectionName = connectionName;
			if(DatabasesByConnectionName.ContainsKey(connectionName))
			{
				return new DatabaseInitializationResult(DatabasesByConnectionName[connectionName], null);
			}
			else
			{
				FireEvent(DatabaseNotFound, EventArgs.Empty);
			}

			return new DatabaseInitializationResult(null, new DatabaseInitializationFailedException(connectionName));
		}

		public void Ignore(params Type[] types)
		{
			throw new NotImplementedException("{0} doesn't implement Ignore"._Format(typeof(StaticDatabaseInitializer).FullName));
		}

		public void Ignore(params string[] connectionNames)
		{
			throw new NotImplementedException("{0} doesn't implement Ignore"._Format(typeof(StaticDatabaseInitializer).FullName));
		}

		#endregion
	}
}

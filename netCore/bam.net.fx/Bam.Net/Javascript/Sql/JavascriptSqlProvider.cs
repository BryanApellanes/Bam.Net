/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Bam.Net.Data;
using System.Data;
using Bam.Net.Configuration;

namespace Bam.Net.Javascript.Sql
{
	[Proxy("sql")]
	public abstract class JavaScriptSqlProvider : IConfigurable
	{
		public JavaScriptSqlProvider() { }

		public Database Database
		{
			get;
			set;
		}
		public SqlResponse Execute(string sql)
		{
			EnsureInitialized();
			SqlResponse result = new SqlResponse();
			try
			{
				DataTable results = Database.GetDataTable(sql, CommandType.Text);
				result.Results = results.ToDynamicList().ToArray();
				result.Count = results.Rows.Count;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = ex.Message;
				result.Results = new object[] { };
			}

			return result;
		}

		bool _initialized;
		public void EnsureInitialized()
		{
			if(!_initialized)
			{
				_initialized = true;
				Initialize();				
			}
		}
		protected abstract void Initialize();

		#region IConfigurable Members

        [Exclude]
        public virtual void Configure(IConfigurer configurer)
        {
            configurer.Configure(this);
            this.CheckRequiredProperties();
        }

        [Exclude]
        public virtual void Configure(object configuration)
        {
            this.CopyProperties(configuration);
            this.CheckRequiredProperties();
        }
		#endregion

		#region IHasRequiredProperties Members

		public abstract string[] RequiredProperties { get; }
		#endregion
	}
}

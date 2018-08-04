/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
	public enum SqlDialect
	{
		Invalid,
		SQLite,
        /// <summary>
        /// Microsoft sql; same as MsSql
        /// </summary>
        Ms,
        /// <summary>
        /// Microsoft sql; same as Ms
        /// </summary>
		MsSql,
        /// <summary>
        /// My sql; same as MySql
        /// </summary>
        My,
        /// <summary>
        /// My sql; same as My
        /// </summary>
        MySql,
		Oracle,
        /// <summary>
        /// Postgres sql; same as Npgsql
        /// </summary>
        Postgres,
        /// <summary>
        /// Postgres sql; same as Postgres
        /// </summary>
        Npgsql,

        InterSystems
	}
}

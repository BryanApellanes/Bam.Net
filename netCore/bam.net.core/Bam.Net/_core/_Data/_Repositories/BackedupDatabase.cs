/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Incubation;
using System.Reflection;

namespace Bam.Net.Data.Repositories //core
{
	public partial class BackedupDatabase: Database
	{
		public BackedupDatabase(Assembly daoAssembly, Database databaseToTrack, string objectRepoPath = ".\\DbBackupRepo")
		{
			this.Repository = new DaoRepository(objectRepoPath);
			this.Backup = new DaoBackup(daoAssembly, databaseToTrack, this.Repository);
		}
	}
}

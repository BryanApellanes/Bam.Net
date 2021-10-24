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
using System.IO;

namespace Bam.Net.Data.Repositories //core
{
	public partial class BackedupDatabase: Database
	{
		public BackedupDatabase(Assembly daoAssembly, Database databaseToTrack, string objectRepoPath = ".\\DbBackupRepo")
		{
            DirectoryInfo backupDir = new DirectoryInfo(objectRepoPath);
			this.Repository = new DaoRepository(databaseToTrack, Log.Default, backupDir.Name);
			this.Backup = new DaoBackup(daoAssembly, databaseToTrack, this.Repository);
		}
	}
}

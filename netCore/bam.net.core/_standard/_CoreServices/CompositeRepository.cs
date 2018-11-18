﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.Caching;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A repository that is made up of a variety of different
    /// types of repositories used for different purposes such
    /// as reading, writing, caching and backup
    /// </summary>
    public partial class CompositeRepository : AsyncRepository, IHasTypeSchemaTempPathProvider
    {
        public CompositeRepository(DaoRepository sourceRepository, DefaultDataDirectoryProvider dataSettings = null)
        {
            DataSettings = dataSettings ?? DefaultDataDirectoryProvider.Current;
            SourceRepository = sourceRepository;
            ReadRepository = new CachingRepository(sourceRepository);
            WriteRepositories = new HashSet<IRepository>();
            WorkspacePath = DataSettings.GetWorkspaceDirectory(this.GetType()).FullName;
            BackupRepository = ServiceRegistry.Default.Get<IRepository>();
            TypeSchemaTempPathProvider = (sd, ts) => Path.Combine(WorkspacePath, sd.Name, ts.Hash);

            WireBackup();

            sourceRepository.StorableTypes.Each(type => AddType(type));
        }
    }
}

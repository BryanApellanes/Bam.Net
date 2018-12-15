using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Caching;
using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.CoreServices.AssemblyManagement.Data;
using Dao = Bam.Net.CoreServices.AssemblyManagement.Data.Dao;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Testing;
using Bam.Net.CoreServices.AssemblyManagement;
using Bam.Net.CoreServices.Files;
using Bam.Net.Configuration;
using Bam.Net.Services.DataReplication.Data;
using Bam.Net.Testing.Integration;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.CoreServices;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class AssemblyServiceTests : CommandLineTestInterface
    {
        [IntegrationTest]
        [ConsoleAction]
        public void ProcessRuntimeDescriptorTest()
        {
            ProcessRuntimeDescriptor instance = ProcessRuntimeDescriptor.GetCurrent();
            OutLineFormat("MachineName: {0}", ConsoleColor.Cyan, instance.MachineName);
            OutLineFormat("CommandLine: {0}", ConsoleColor.Cyan, instance.CommandLine);
            OutLineFormat("FilePath: {0}", ConsoleColor.Cyan, instance.FilePath);
            instance.AssemblyDescriptors.Each(ad =>
            {
                OutLineFormat("Name: {0}", ConsoleColor.DarkCyan, ad.Name);
                OutLineFormat("FileHash: {0}", ConsoleColor.DarkCyan, ad.FileHash);
                OutLineFormat("AssemblyFullName: {0}", ConsoleColor.DarkCyan, ad.AssemblyFullName);
            });
            OutLine();
        }

        [UnitTest]
        public void AssemblyDescriptorHasReferenceDescriptorsTest()
        {
            Assembly current = Assembly.GetExecutingAssembly();
            AssemblyName[] assNames = current.GetReferencedAssemblies().Where(AssemblyDescriptor.AssemblyNameFilter).ToArray();
            AssemblyDescriptor descriptor = new AssemblyDescriptor(current);
            Expect.IsTrue(assNames.Length > 0);
            Expect.AreEqual(assNames.Length, descriptor.AssemblyReferenceDescriptors.Count);
        }

        [UnitTest]
        public void CanSaveAndRetrieveAssemblyDescriptorTest()
        {
            DaoRepository daoRepo = GetAssemblyManagementRepository(GetConsoleLogger());
            AssemblyDescriptor descriptor = new AssemblyDescriptor(Assembly.GetExecutingAssembly());
            Expect.IsTrue(descriptor.AssemblyReferenceDescriptors.Count > 0, "No references found");
            AssemblyDescriptor saved = daoRepo.Save(descriptor);
            Expect.AreEqual(descriptor.AssemblyReferenceDescriptors?.Count, saved.AssemblyReferenceDescriptors?.Count, "Saved references didn't match");
            AssemblyDescriptor retrieved = daoRepo.Retrieve<AssemblyDescriptor>(descriptor.Uuid);
            Expect.AreEqual(descriptor.AssemblyReferenceDescriptors?.Count, saved.AssemblyReferenceDescriptors?.Count, "Retrieved references didn't match");
        }

        [UnitTest]
        public void CanSaveAndRetrieveAllCurrentAppDomainDescriptorsTest()
        {
            DaoRepository daoRepo = GetAssemblyManagementRepository(GetConsoleLogger());
            AssemblyDescriptor[] descriptors = AssemblyDescriptor.GetCurrentAppDomainDescriptors().ToArray();
            foreach (AssemblyDescriptor descriptor in descriptors)
            {
                OutLine("Before save");
                OutputInfo(descriptor);

                AssemblyDescriptor saved = daoRepo.Save(descriptor);
                OutLine("Original after save", ConsoleColor.Yellow);
                OutputInfo(descriptor);

                OutLine("Result after save", ConsoleColor.DarkYellow);
                OutputInfo(saved);

                Expect.AreEqual(descriptor.AssemblyFullName, saved.AssemblyFullName, "AssemlbyFullName didn't match");
                Expect.AreEqual(descriptor.AssemblyReferenceDescriptors?.Count, saved.AssemblyReferenceDescriptors?.Count, "AssemblyReferenceDescriptors count didn't match");

                AssemblyDescriptor retrieved = daoRepo.Retrieve<AssemblyDescriptor>(saved.Uuid);
                OutLine("Retrieved after save", ConsoleColor.DarkGreen);
                OutputInfo(saved);

                Expect.AreEqual(descriptor.AssemblyFullName, retrieved.AssemblyFullName, "AssemlbyFullName didn't match");
                Expect.AreEqual(descriptor.AssemblyReferenceDescriptors?.Count, retrieved.AssemblyReferenceDescriptors?.Count, "AssemblyReferenceDescriptors count didn't match");
            }
        }

        [UnitTest]
        public void SaveDescriptorDoesntDuplicate()
        {
            AssemblyServiceRepository repo = GetTestRepo(nameof(SaveDescriptorDoesntDuplicate));

            AssemblyDescriptor descriptor = new AssemblyDescriptor(Assembly.GetExecutingAssembly());

            AssemblyDescriptor one = repo.Save(descriptor);
            AssemblyDescriptor two = repo.Save(descriptor);
            Expect.IsNotNull(one, "first save returned null");
            Expect.IsNotNull(two, "second save returned null");
            Expect.AreEqual(one.Id, two.Id, "Ids didn't match");
            Expect.AreEqual(one.Uuid, two.Uuid, "Uuids didn't match");

            Dao.AssemblyDescriptorCollection descriptors = Dao.AssemblyDescriptor
                .Where(q =>
                    q.AssemblyFullName == descriptor.AssemblyFullName &&
                    q.FileHash == descriptor.FileHash &&
                    q.Name == descriptor.Name, repo.Database);

            Expect.AreEqual(1, descriptors.Count);
        }

        [UnitTest]
        public void CastNullToNullableLong()
        {
            long? one = (long?)1;
            long? n = (long?)null;
            Expect.IsNull(n);
            Expect.IsFalse(n.HasValue);
            Expect.IsNotNull(one);
            Expect.IsTrue(one.HasValue);
        }

        [UnitTest]
        public void SaveDescriptorSavesReferences()
        {
            AssemblyServiceRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(SaveDescriptorSavesReferences));
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            AssemblyDescriptor[] descriptors = AssemblyDescriptor.GetCurrentAppDomainDescriptors().ToArray();

            foreach (AssemblyDescriptor descriptor in descriptors)
            {
                int? referenceCount = descriptor.AssemblyReferenceDescriptors?.Count;
                if (referenceCount > 0)
                {
                    AssemblyDescriptor wrapped = repo.Save(descriptor);
                    AssemblyDescriptor retrieved = repo.Retrieve<AssemblyDescriptor>(wrapped.Id);
                    Expect.AreEqual(referenceCount, retrieved.AssemblyReferenceDescriptors?.Count);
                    Pass(descriptor.AssemblyFullName + " " + referenceCount.ToString());
                }
                else
                {
                    OutLineFormat("No references: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
                }
            }
        }

        [IntegrationTest]
        [ConsoleAction]
        public void ProcessRuntimeDescriptorSaveTest()
        {
            AssemblyServiceRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(ProcessRuntimeDescriptorSaveTest));
            repo.SetDaoNamespace<ProcessRuntimeDescriptor>();
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            List<AssemblyDescriptor> descriptors = AssemblyDescriptor.GetCurrentAppDomainDescriptors().ToList();
            ProcessRuntimeDescriptor current = ProcessRuntimeDescriptor.PersistCurrentToRepo(repo);

            Expect.AreEqual(descriptors.Count, current.AssemblyDescriptors.Length, "Number of descriptors didn't match");

            ProcessRuntimeDescriptor retrieved = repo.Retrieve<ProcessRuntimeDescriptor>(current.Id);
            Expect.AreEqual(descriptors.Count, retrieved.AssemblyDescriptors.Length, "Number of retrieved descriptors didn't match");

            foreach (AssemblyDescriptor descriptor in retrieved.AssemblyDescriptors)
            {
                AssemblyDescriptor retrievedDescriptor = repo.Retrieve<AssemblyDescriptor>(descriptor.Uuid);
                OutLineFormat("Checking {0}", ConsoleColor.DarkBlue, retrievedDescriptor.AssemblyFullName);
                AssemblyDescriptor actual = descriptors.FirstOrDefault(d =>
                    d.Name.Equals(retrievedDescriptor.Name) &&
                    d.FileHash.Equals(retrievedDescriptor.FileHash) &&
                    d.AssemblyFullName.Equals(retrievedDescriptor.AssemblyFullName)
                );

                Expect.AreEqual(actual.AssemblyReferenceDescriptors?.Count, retrievedDescriptor.AssemblyReferenceDescriptors?.Count);
                OutLineFormat("ProcessRuntimeDescriptors count {0}", retrievedDescriptor.ProcessRuntimeDescriptors.Count);
            }
        }

        [IntegrationTest]
        public void LoadAndRestoreCurrentRuntimeTestAsync()
        {
            AssemblyServiceRepository assManRepo = GetTestRepo(nameof(LoadAndRestoreCurrentRuntimeTestAsync));
            DaoRepository fileRepo = new DaoRepository()
            {
                Database = assManRepo.Database
            };
            fileRepo.AddTypes(typeof(ChunkedFileDescriptor),
                typeof(ChunkDataDescriptor),
                typeof(ChunkData));
            fileRepo.EnsureDaoAssemblyAndSchema();
            FileService fmSvc = new FileService(fileRepo);

            AssemblyService svc = new AssemblyService(DefaultDataDirectoryProvider.Current, fmSvc, assManRepo, DefaultConfigurationApplicationNameProvider.Instance);
            ProcessRuntimeDescriptor prd1 = svc.CurrentProcessRuntimeDescriptor;
            ProcessRuntimeDescriptor prd2 = svc.CurrentProcessRuntimeDescriptor;
            ProcessRuntimeDescriptor byName = assManRepo.OneProcessRuntimeDescriptorWhere(c => c.ApplicationName == prd1.ApplicationName);
            OutLineFormat("AppName: {0}", ConsoleColor.Cyan, prd1.ApplicationName);
            Expect.AreEqual(prd1.Cuid, prd2.Cuid);
            Expect.AreEqual(prd1.Cuid, byName.Cuid);
            bool? fired = false;
            svc.RuntimeRestored += (o, a) =>
            {
                fired = true;
                OutLine(((ProcessRuntimeDescriptorEventArgs)a).DirectoryPath);
            };
            svc.RestoreApplicationRuntime(byName.ApplicationName, $".\\{nameof(LoadAndRestoreCurrentRuntimeTestAsync)}");
            Expect.IsTrue(fired.Value);
        }

        private static void Output(string name, object sender, RepositoryEventArgs args)
        {
            OutLineFormat("Event Fired: {0}, Schema {1}, Sender: {2}, Type: {3}, Data: {4}, Message: {5}", ConsoleColor.DarkYellow, name, sender?.Property("SchemaName", false), sender, args.Type?.FullName, args.Data, args.Message);
        }

        private static void OutputInfo(AssemblyDescriptor descriptor)
        {
            OutLineFormat("Name: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
            OutLineFormat("Hash: {0}", ConsoleColor.DarkCyan, descriptor.FileHash);
            OutLineFormat("\tReference count: {0}", ConsoleColor.Blue, descriptor.AssemblyReferenceDescriptors?.Count);
            OutLine();
        }

        private static ConsoleLogger GetConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            Log.Default = logger;
            return logger;
        }

        private static AssemblyServiceRepository GetAssemblyManagementRepository(ConsoleLogger logger = null, string databaseName = null, Assembly daoAssembly = null)
        {
            return new AssemblyServiceRepository() { Logger = logger, Database = new SQLiteDatabase(".", databaseName ?? "AssemblyManagementServiceTest_DaoRepoData") };
        }

        private AssemblyServiceRepository GetTestRepo(string databaseName)
        {
            AssemblyServiceRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), databaseName);
            repo.SetDaoNamespace<ProcessRuntimeDescriptor>();
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            Dao.AssemblyDescriptor.LoadAll(repo.Database).Delete(repo.Database);
            repo.Creating += (o, a) => { Output("Creating", o, (RepositoryEventArgs)a); };
            repo.Created += (o, a) => { Output("Created", o, (RepositoryEventArgs)a); };
            repo.Updating += (o, a) => { Output("Updating", o, (RepositoryEventArgs)a); };
            repo.Updated += (o, a) => { Output("Updated", o, (RepositoryEventArgs)a); };
            return repo;
        }

    }
}

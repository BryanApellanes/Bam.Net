using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
    public class AssemblyServiceTests : CommandLineTool
    {
        [IntegrationTest]
        [ConsoleAction]
        public void ProcessRuntimeDescriptorTest()
        {
            ProcessRuntimeDescriptor instance = ProcessRuntimeDescriptor.GetCurrent();
            Message.PrintLine("MachineName: {0}", ConsoleColor.Cyan, instance.MachineName);
            Message.PrintLine("CommandLine: {0}", ConsoleColor.Cyan, instance.CommandLine);
            Message.PrintLine("FilePath: {0}", ConsoleColor.Cyan, instance.FilePath);
            instance.AssemblyDescriptors.Each(ad =>
            {
                Message.PrintLine("Name: {0}", ConsoleColor.DarkCyan, ad.Name);
                Message.PrintLine("FileHash: {0}", ConsoleColor.DarkCyan, ad.FileHash);
                Message.PrintLine("AssemblyFullName: {0}", ConsoleColor.DarkCyan, ad.AssemblyFullName);
            });
            Message.PrintLine();
        }

        [UnitTest]
        public void AssemblyDescriptorHasReferenceDescriptorsTest()
        {
            Assembly current = Assembly.GetExecutingAssembly();
            AssemblyName[] assNames = current.GetReferencedAssemblies().Where(AssemblyDescriptor.AssemblyNameFilter).ToArray();
            AssemblyDescriptor descriptor = new AssemblyDescriptor(current);
            Thread.Sleep(300);
            (assNames.Length > 0).IsTrue();
            Message.PrintLine("Assembly names: {0}", assNames.Select(an=> an.Name).ToArray().ToDelimited(s=> s, ", "));
            (descriptor.AssemblyReferenceDescriptors.Count > 0).IsTrue();
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
                Message.PrintLine("Before save");
                OutputInfo(descriptor);

                AssemblyDescriptor saved = daoRepo.Save(descriptor);
                Message.PrintLine("Original after save", ConsoleColor.Yellow);
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
            AssemblyManagementRepository repo = GetTestRepo(nameof(SaveDescriptorDoesntDuplicate));

            AssemblyDescriptor descriptor = new AssemblyDescriptor(Assembly.GetExecutingAssembly());

            AssemblyDescriptor one = descriptor.Save(repo);// repo.Save(descriptor);
            AssemblyDescriptor two = descriptor.Save(repo);
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
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(SaveDescriptorSavesReferences));
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
                    Message.PrintLine("No references: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
                }
            }
        }

        [IntegrationTest]
        [ConsoleAction]
        public void ProcessRuntimeDescriptorSaveTest()
        {
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(ProcessRuntimeDescriptorSaveTest));
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
                Message.PrintLine("Checking {0}", ConsoleColor.DarkBlue, retrievedDescriptor.AssemblyFullName);
                AssemblyDescriptor actual = descriptors.FirstOrDefault(d =>
                    d.Name.Equals(retrievedDescriptor.Name) &&
                    d.FileHash.Equals(retrievedDescriptor.FileHash) &&
                    d.AssemblyFullName.Equals(retrievedDescriptor.AssemblyFullName)
                );

                Expect.AreEqual(actual.AssemblyReferenceDescriptors?.Count, retrievedDescriptor.AssemblyReferenceDescriptors?.Count);
                Message.PrintLine("ProcessRuntimeDescriptors count {0}", retrievedDescriptor.ProcessRuntimeDescriptors.Count);
            }
        }

        [IntegrationTest]
        public void LoadAndRestoreCurrentRuntimeTestAsync()
        {
            AssemblyManagementRepository assManRepo = GetTestRepo(nameof(LoadAndRestoreCurrentRuntimeTestAsync));
            DaoRepository fileRepo = new DaoRepository()
            {
                Database = assManRepo.Database
            };
            fileRepo.AddTypes(typeof(ChunkedFileDescriptor),
                typeof(ChunkDataDescriptor),
                typeof(ChunkData));
            fileRepo.EnsureDaoAssemblyAndSchema();
            FileService fmSvc = new FileService(fileRepo);

            AssemblyService svc = new AssemblyService(DataProvider.Current, fmSvc, assManRepo, DefaultConfigurationApplicationNameProvider.Instance);
            ProcessRuntimeDescriptor prd1 = svc.CurrentProcessRuntimeDescriptor;
            ProcessRuntimeDescriptor prd2 = svc.CurrentProcessRuntimeDescriptor;
            ProcessRuntimeDescriptor byName = assManRepo.OneProcessRuntimeDescriptorWhere(c => c.ApplicationName == prd1.ApplicationName);
            Message.PrintLine("AppName: {0}", ConsoleColor.Cyan, prd1.ApplicationName);
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
            Message.PrintLine("Event Fired: {0}, Schema {1}, Sender: {2}, Type: {3}, Data: {4}, Message: {5}", ConsoleColor.DarkYellow, name, sender?.Property("SchemaName", false), sender, args.Type?.FullName, args.Data, args.Message);
        }

        private static void OutputInfo(AssemblyDescriptor descriptor)
        {
            Message.PrintLine("Name: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
            Message.PrintLine("Hash: {0}", ConsoleColor.DarkCyan, descriptor.FileHash);
            Message.PrintLine("\tReference count: {0}", ConsoleColor.Blue, descriptor.AssemblyReferenceDescriptors?.Count);
            Message.PrintLine();
        }

        private static ConsoleLogger GetConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            Log.Default = logger;
            return logger;
        }

        private static AssemblyManagementRepository GetAssemblyManagementRepository(ConsoleLogger logger = null, string databaseName = null, Assembly daoAssembly = null)
        {
            return new AssemblyManagementRepository() { Logger = logger, Database = new SQLiteDatabase(".", databaseName ?? "AssemblyManagementServiceTest_DaoRepoData") };
        }

        private AssemblyManagementRepository GetTestRepo(string databaseName)
        {
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), databaseName);
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

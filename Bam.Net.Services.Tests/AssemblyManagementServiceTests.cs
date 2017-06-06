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
using Bam.Net.Services.AssemblyManagement.Data;
using Dao = Bam.Net.Services.AssemblyManagement.Data.Dao;
using Bam.Net.Services.AssemblyManagement.Data.Dao.Repository;
using Bam.Net.Testing;
using Bam.Net.Services.AssemblyManagement;
using Bam.Net.Services.Distributed.Files;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class AssemblyManagementServiceTests: CommandLineTestInterface
    {
        [UnitTest]
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
            Expect.AreEqual(assNames.Length, descriptor.AssemblyReferenceDescriptors.Length);
        }

        [UnitTest]
        public void CanSaveAndRetrieveAssemblyDescriptorTest()
        {
            DaoRepository daoRepo = GetDaoRepository(GetConsoleLogger());
            AssemblyDescriptor descriptor = new AssemblyDescriptor(Assembly.GetExecutingAssembly());
            Expect.IsTrue(descriptor.AssemblyReferenceDescriptors.Length > 0, "No references found");
            AssemblyDescriptor saved = daoRepo.Save(descriptor);
            Expect.AreEqual(descriptor.AssemblyReferenceDescriptors.Length, saved.AssemblyReferenceDescriptors.Length, "Saved references didn't match");
            AssemblyDescriptor retrieved = daoRepo.Retrieve<AssemblyDescriptor>(descriptor.Uuid);
            Expect.AreEqual(descriptor.AssemblyReferenceDescriptors.Length, saved.AssemblyReferenceDescriptors.Length, "Retrieved references didn't match");
        }

        [UnitTest]
        public void CanSaveAndRetrieveAllCurrentAppDomainDescriptorsTest()
        {
            DaoRepository daoRepo = GetDaoRepository(GetConsoleLogger());
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
                Expect.AreEqual(descriptor.AssemblyReferenceDescriptors.Length, saved.AssemblyReferenceDescriptors.Length, "AssemblyReferenceDescriptors count didn't match");

                AssemblyDescriptor retrieved = daoRepo.Retrieve<AssemblyDescriptor>(saved.Uuid);
                OutLine("Retrieved after save", ConsoleColor.DarkGreen);
                OutputInfo(saved);

                Expect.AreEqual(descriptor.AssemblyFullName, retrieved.AssemblyFullName, "AssemlbyFullName didn't match");
                Expect.AreEqual(descriptor.AssemblyReferenceDescriptors.Length, retrieved.AssemblyReferenceDescriptors.Length, "AssemblyReferenceDescriptors count didn't match");
            }
        }

        [UnitTest]
        public void SaveDescriptorDoesntDuplicate()
        {
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(SaveDescriptorDoesntDuplicate));
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            Dao.AssemblyDescriptor.LoadAll(repo.Database).Delete(repo.Database);
            AssemblyDescriptor descriptor = new AssemblyDescriptor(Assembly.GetExecutingAssembly());
            AssemblyDescriptor one = repo.Save(descriptor);
            AssemblyDescriptor two = repo.Save(descriptor);

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
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(SaveDescriptorDoesntDuplicate));
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            AssemblyDescriptor[] descriptors = AssemblyDescriptor.GetCurrentAppDomainDescriptors().ToArray();
            for(int i = 0; i < 3; i++)
            {
                foreach (AssemblyDescriptor descriptor in descriptors)
                {
                    int referenceCount = descriptor.AssemblyReferenceDescriptors.Length;
                    if (referenceCount > 0)
                    {
                        AssemblyDescriptor wrapped = repo.Save(descriptor);
                        AssemblyDescriptor retrieved = repo.Retrieve<AssemblyDescriptor>(wrapped.Id);
                        Expect.AreEqual(referenceCount, retrieved.AssemblyReferenceDescriptors.Length);
                        Pass(descriptor.AssemblyFullName);
                    }
                    else
                    {
                        OutLineFormat("No references: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
                    }
                }
            }
        }

        [UnitTest]
        public void ProcessRuntimeDescriptorSaveTest()
        {
            AssemblyManagementRepository repo = GetAssemblyManagementRepository(GetConsoleLogger(), nameof(ProcessRuntimeDescriptorSaveTest));
            repo.Database.TryEnsureSchema<Dao.AssemblyDescriptor>();
            List<AssemblyDescriptor> descriptors = AssemblyDescriptor.GetCurrentAppDomainDescriptors().ToList();
            ProcessRuntimeDescriptor current = ProcessRuntimeDescriptor.PersistCurrentToRepo(repo);

            Expect.AreEqual(descriptors.Count, current.AssemblyDescriptors.Length, "Number of descriptors didn't match");

            ProcessRuntimeDescriptor retrieved = repo.Retrieve<ProcessRuntimeDescriptor>(current.Id);
            Expect.AreEqual(descriptors.Count, retrieved.AssemblyDescriptors.Length, "Number of retrieved descriptors didn't match");

            foreach(AssemblyDescriptor descriptor in retrieved.AssemblyDescriptors)
            {
                AssemblyDescriptor retrievedDescriptor = repo.Retrieve<AssemblyDescriptor>(descriptor.Uuid);
                OutLineFormat("Checking {0}", ConsoleColor.DarkBlue, retrievedDescriptor.AssemblyFullName);
                AssemblyDescriptor actual = descriptors.FirstOrDefault(d => 
                    d.Name.Equals(retrievedDescriptor.Name) && 
                    d.FileHash.Equals(retrievedDescriptor.FileHash) && 
                    d.AssemblyFullName.Equals(retrievedDescriptor.AssemblyFullName)
                );
                
                Expect.AreEqual(actual.AssemblyReferenceDescriptors.Length, retrievedDescriptor.AssemblyReferenceDescriptors.Length);
                OutLineFormat("ProcessRuntimeDescriptors count {0}", retrievedDescriptor.ProcessRuntimeDescriptor.Count);
            }
        }

        //[UnitTest]
        //public void RestoreCurrentRuntimeTest()
        //{
        //    SQLiteDatabase db = new SQLiteDatabase(".\\", nameof(RestoreCurrentRuntimeTest));
        //    FileService fmSvc = new FileService(new DaoRepository(db));
        //    AssemblyManagementService svc = new AssemblyManagementService(fmSvc);
        //}

        private static void OutputInfo(AssemblyDescriptor descriptor)
        {
            OutLineFormat("Name: {0}", ConsoleColor.Cyan, descriptor.AssemblyFullName);
            OutLineFormat("Hash: {0}", ConsoleColor.DarkCyan, descriptor.FileHash);
            OutLineFormat("\tReference count: {0}", ConsoleColor.Blue, descriptor.AssemblyReferenceDescriptors.Length);
            OutLine();
        }

        private IEnumerable<Repository> GetTestRepositories()
        {
            ConsoleLogger logger = GetConsoleLogger();
            DaoRepository daoRepo = GetDaoRepository(logger);
            yield return daoRepo;
            CachingRepository cachingRepo = new CachingRepository(daoRepo, logger);
            yield return cachingRepo;
        }

        private static ConsoleLogger GetConsoleLogger()
        {
            ConsoleLogger logger = new ConsoleLogger { AddDetails = false, UseColors = true };
            logger.StartLoggingThread();
            Log.Default = logger;
            return logger;
        }

        private static DaoRepository GetDaoRepository(ConsoleLogger logger = null, string databaseName = null)
        {
            string schemaName = "AssemblyManagementServiceTest_SchemaName";
            DaoRepository daoRepo = new DaoRepository(new SQLiteDatabase(".", databaseName ?? "TestDaoRepoData"), logger, schemaName);
            daoRepo.WarningsAsErrors = false;
            daoRepo.AddType(typeof(ProcessRuntimeDescriptor));
            return daoRepo;            
        }

        private static AssemblyManagementRepository GetAssemblyManagementRepository(ConsoleLogger logger = null, string databaseName = null)
        {
            DaoRepository daoRepo = GetDaoRepository(logger, databaseName);
            return new AssemblyManagementRepository() { Database = daoRepo.Database };
        }
    }
}

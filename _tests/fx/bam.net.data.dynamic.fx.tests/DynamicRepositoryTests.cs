using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Dynamic.Data.Dao.Repository;
using Bam.Net.Data.Dynamic.Data;
using Bam.Net.Testing.Integration;
using System.Threading;
using Bam.Net.CommandLine;

namespace Bam.Net.Data.Dynamic.Tests
{

    [Serializable]
    public class DynamicRepositoryTests: CommandLineTestInterface
    {
        [ConsoleAction]
        [IntegrationTest]
        public void CanReadUnc()
        {
            JObject jobj = (JObject)JsonConvert.DeserializeObject("\\\\core\\data\\events\\github\\24745fe6efe498f79b3b165be27b1feb69a851d0.json".SafeReadFile());
            Dictionary<object, object> dic = jobj.ToObject<Dictionary<object, object>>();
            List<object> convertKeys = new List<object>();
            foreach(object key in dic.Keys)
            {
                object val = dic[key];
                Type type = null;
                if(val != null)
                {
                    type = val.GetType();
                }
                string typeName = type == null ? "null" : type.Name;
                OutLineFormat("{0}: Type = {1}", key, typeName);
                if(type == typeof(JObject))
                {
                    convertKeys.Add(key);                    
                }
            }

            convertKeys.Each(k => dic[k] = ((JObject)dic[k]).ToObject<Dictionary<object, object>>());
            
            Type dynamicType = dic.ToDynamicType("GitEvent");
            foreach(PropertyInfo prop in dynamicType.GetProperties())
            {
                OutLineFormat("{0}: {1}", ConsoleColor.Cyan, prop.Name, prop.PropertyType.Name);
            }
        }

        public class TestDynamicTypeManager: DynamicTypeManager
        {
            public TestDynamicTypeManager(DynamicTypeDataRepository descriptorRepository, DefaultDataDirectoryProvider settings) : base(descriptorRepository, settings)
            {
            }

            public void TestSaveTypDescriptor(string typeName, Dictionary<object, object> data)
            {
                SaveTypeDescriptor(typeName, data);
            }
            public void TestSaveData(string sha1, string typeName, Dictionary<object, object> data)
            {
                SaveRootData(sha1, typeName, data);
            }

            public void TestGetClrTypeName()
            {
                string value = "arrayOf(The.Big.Monkey)";
                Expect.AreEqual("Monkey[]", GetClrTypeName(value));
                string value2 = "The.Big.Monkey.Banana";
                Expect.AreEqual("Banana", GetClrTypeName(value2));
            }

            public void TestGetClrPropertyName()
            {
                string value = "commit_author";
                Expect.AreEqual("CommitAuthor", GetClrPropertyName(value));
            }
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanGetClrTypeNameFromArrayOf()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.TestGetClrTypeName();
        }


        [ConsoleAction]
        [IntegrationTest]
        public void CanGetClrPropertyName()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.TestGetClrPropertyName();
        }

        // save descriptor
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDescriptorDoesntDuplicte()
        {
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);            
            JObject jobj = (JObject)JsonConvert.DeserializeObject(new { Name = "some name", Arr = new object[] { new { Fromage = "gooey" } } }.ToJson());
            Dictionary<object, object> data = jobj.ToObject<Dictionary<object, object>>();
            string testTypeName = "test_typeName";
            testRepo.DynamicTypeDataRepository.DeleteWhere<DynamicTypeDescriptor>(new { TypeName = testTypeName });
            DynamicTypeDescriptor descriptor = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).FirstOrDefault();
            Expect.IsNull(descriptor);

            testRepo.TestSaveTypDescriptor(testTypeName, data);
            int count = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).Count();

            Expect.IsTrue(count == 1);
            testRepo.TestSaveTypDescriptor(testTypeName, data);
            count = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.TypeName == testTypeName).Count();
            Expect.IsTrue(count == 1);
        }
        // save child descriptors
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDataSavesTypes()
        {
            string testTypeName = nameof(SaveDataSavesTypes).RandomLetters(5);
            SetupTestData(out TestDynamicTypeManager testRepo, out Dictionary<object, object> data);

            // Test
            testRepo.TestSaveData("sha1NotUsedForThisTest", testTypeName, data);
            List<DynamicTypeDescriptor> descriptors = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).ToList();
            Expect.IsTrue(descriptors.Count == 3);
            descriptors.Each(d => OutLineFormat("{0}: {1}", d.Id, d.TypeName, ConsoleColor.Cyan));
        }

        // save data
        [ConsoleAction]
        [IntegrationTest]
        public void SaveDataSavesInstance()
        {
            string testTypeName = nameof(SaveDataSavesInstance).RandomLetters(5);
            SetupTestData(out TestDynamicTypeManager testRepo, out Dictionary<object, object> data);

            testRepo.TestSaveData("sha1NotUsedForThisTest", testTypeName, data);
            List<DataInstance> datas = testRepo.DynamicTypeDataRepository.DataInstancesWhere(d => d.TypeName == testTypeName).ToList();
            Expect.IsTrue(datas.Count == 1);
            Expect.IsTrue(datas[0].TypeName.Equals(testTypeName));
            datas[0].Properties.Each(p => OutLine($"{p.PropertyName} = {p.Value}"));
        }
        
        [ConsoleAction]
        [IntegrationTest]
        public void SaveRealData()
        {
            AutoResetEvent wait = new AutoResetEvent(false);
            string json = "\\\\core\\data\\events\\github\\04feeb057e9eba4a6ace6413af475f819b54ad0c.json".SafeReadFile();
            DynamicTypeManager typeManager = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            typeManager.ProcessJson("GitHubEvent", json);
            typeManager.JsonFileProcessor.QueueEmptied += (s, a) => wait.Set();
            OutLine(typeManager.DynamicTypeDataRepository.Database.ConnectionString);            
            wait.WaitOne();
        }

        [ConsoleAction]
        [IntegrationTest]
        public void AssociationsAreMade()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            DynamicNamespaceDescriptor ns = new DynamicNamespaceDescriptor { Namespace = $"Test.Name.Space.{nameof(AssociationsAreMade)}" };
            ns = mgr.DynamicTypeDataRepository.Save(ns);

            DynamicTypeDescriptor typeDescriptor = new DynamicTypeDescriptor { DynamicNamespaceDescriptorId = ns.Id };
            Expect.IsNull(typeDescriptor.DynamicNamespaceDescriptor);

            typeDescriptor = mgr.DynamicTypeDataRepository.Save(typeDescriptor);
            Expect.IsNotNull(typeDescriptor.DynamicNamespaceDescriptor);
            Expect.AreEqual(ns, typeDescriptor.DynamicNamespaceDescriptor);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanAddType()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            string testType = "CanAddTypeTest";
            DynamicTypeDescriptor typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNull(typeDescriptor, "typeDescriptor should have been null");
            typeDescriptor = mgr.AddType(testType);
            Expect.IsNotNull(typeDescriptor, "typeDescriptor should NOT have been null");
            typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNotNull(typeDescriptor, "typeDescriptor should NOT have been null");
            OutLineFormat("{0}", typeDescriptor.PropertiesToString());
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanAddPropertyToType()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanAddPropertyToType);
            string testProperty = "SomeProperty";
            string testProperty2 = "BooleanProperty";
            mgr.AddType(testType);
            DynamicTypePropertyDescriptor prop = mgr.AddProperty(testType, testProperty, "String");
            DynamicTypePropertyDescriptor prop2 = mgr.AddProperty(testType, testProperty2, "Boolean");
            Expect.IsNotNull(prop);
            DynamicTypeDescriptor typeDescriptor = mgr.GetTypeDescriptor(testType);
            Expect.IsNotNull(typeDescriptor);
            Expect.IsTrue(typeDescriptor.Properties.Count == 2);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanSpecifyNamespace()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanSpecifyNamespace);
            string testType2 = nameof(CanSpecifyNamespace) + "2";
            string testNamespace = "My.Test.Namespace";
            mgr.AddType(testType, testNamespace);
            mgr.AddType(testType2, testNamespace);
            DynamicNamespaceDescriptor ns = mgr.GetNamespaceDescriptor(testNamespace);
            Expect.IsNotNull(ns, "namspace was null");
            Expect.IsTrue(ns.Types.Count == 2);
        }

        [ConsoleAction]
        [IntegrationTest]
        public void CanGetAssembly()
        {
            DynamicTypeManager mgr = new DynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            mgr.DynamicTypeDataRepository.Query<DynamicTypeDescriptor>(d => d.Id > 0).Each(d => mgr.DynamicTypeDataRepository.Delete(d));
            mgr.DynamicTypeDataRepository.Query<DynamicTypePropertyDescriptor>(p => p.Id > 0).Each(p => mgr.DynamicTypeDataRepository.Delete(p));
            string testType = nameof(CanAddPropertyToType);
            mgr.AddType(testType);
            string testProperty = "SomeProperty";
            string testProperty2 = "BooleanProperty";
            DynamicTypePropertyDescriptor prop = mgr.AddProperty(testType, testProperty, "String");
            DynamicTypePropertyDescriptor prop2 = mgr.AddProperty(testType, testProperty2, "Boolean");

            Assembly ass = mgr.GenerateAssembly();
            Expect.IsNotNull(ass);
        }

        private static void SetupTestData(out TestDynamicTypeManager repo, out Dictionary<object, object> data)
        {
            // setup test data
            JObject jobj = (JObject)JsonConvert.DeserializeObject(new
            {
                Name = "some name",
                ChildObjectProp = new
                {
                    Name = "child name",
                    ChildProp = "only child has this"
                },
                ChildArrProp = new object[]
                {
                    new
                    {
                        Fromage = "gooey"
                    }
                }
            }.ToJson());

            data = jobj.ToObject<Dictionary<object, object>>();
            repo = GetTestDynamicTypeManager();
        }

        private static TestDynamicTypeManager GetTestDynamicTypeManager()
        {
            TestDynamicTypeManager repo;
            // clear existing entries if any
            TestDynamicTypeManager testRepo = new TestDynamicTypeManager(new DynamicTypeDataRepository(), DefaultDataDirectoryProvider.Instance);
            testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).Each(d => testRepo.DynamicTypeDataRepository.Delete(d));
            testRepo.DynamicTypeDataRepository.DataInstancesWhere(d => d.Id > 0).Each(d => testRepo.DynamicTypeDataRepository.Delete(d));

            // make sure the type isn't in the repo 
            DynamicTypeDescriptor descriptor = testRepo.DynamicTypeDataRepository.DynamicTypeDescriptorsWhere(d => d.Id > 0).FirstOrDefault();
            Expect.IsNull(descriptor);

            repo = testRepo;
            return repo;
        }
    }
}

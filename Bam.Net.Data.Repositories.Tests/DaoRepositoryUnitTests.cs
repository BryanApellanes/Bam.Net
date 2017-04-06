/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.Schema;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories.Tests.TestDtos;

namespace Bam.Net.Data.Repositories.Tests
{

    [Serializable]
    public class DaoRepositoryUnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void SchemaNameShouldBeSet()
        {
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", MethodBase.GetCurrentMethod().Name), Log.Default, schemaName);
            Expect.AreEqual(schemaName, repo.SchemaName);
        }

        [UnitTest]
        public void SchemaNameShouldBeUsed()
        {            
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", MethodBase.GetCurrentMethod().Name), Log.Default, schemaName);
            repo.WarningsAsErrors = false;
            repo.AddType<Parent>();
            Assembly daoAssembly = repo.GenerateDaoAssembly();
            Type type = daoAssembly.GetTypes().FirstOrDefault(t => t.HasCustomAttributeOfType<TableAttribute>());
            Expect.IsNotNull(type);
            Expect.AreEqual(schemaName, Dao.ConnectionName(type));
        }

        

        [UnitTest]
        public void SetsModified()
        {
            string schemaName = "TheSchemaName_".RandomLetters(5);
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", MethodBase.GetCurrentMethod().Name), Log.Default, schemaName);
            repo.WarningsAsErrors = false;
            repo.AddType<TestRepoData>();
            TestRepoData data = new TestRepoData();
            Expect.IsNull(data.Modified);
            data = repo.Save(data);
            Expect.IsNotNull(data.Modified);
            Expect.IsFalse(data.Modified == default(DateTime));
        }

        [UnitTest]
        public void RetrieveShouldSetParentOnChildren()
        {
            DaoRepository repo = GetTestDaoRepository();
            repo.EnsureDaoAssemblyAndSchema();
            Parent parent = new Parent();
            parent.Name = "Test parent";
            Son one = new Son();
            one.Name = "Son";
            parent.Sons = new List<Son>(new Son[] { one });
            parent = repo.Save(parent);
            Son checkSon = repo.Retrieve<Son>(parent.Sons[0].Id);
            Expect.AreEqual(one.Name, checkSon.Name);
            Expect.AreEqual(parent.Id, checkSon.Parent.Id);
            Expect.AreEqual(parent.Name, checkSon.Parent.Name);
        }

        [UnitTest]
        public void ParentSaveShouldSaveChildren()
        {
            DaoRepository repo = GetTestDaoRepository();
            repo.EnsureDaoAssemblyAndSchema();
            Parent parent = new Parent();
            parent.Name = "Parent Name";
            Son sonOne = new Son();
            Son sonTwo = new Son();
            parent.Sons = new List<Son>(new Son[] { sonOne, sonTwo });

            parent = repo.Save(parent);
            Parent retrieved = repo.Retrieve<Parent>(parent.Id);
            Expect.AreEqual(2, retrieved.Sons.Count);
        }

        [UnitTest]
        public void SavingParentShouldSaveChildLists()
        {
            DaoRepository repo = GetTestDaoRepository();
            repo.EnsureDaoAssemblyAndSchema();
            House house = new House { Name = "TestHouse", Parents = new List<Parent> { new Parent { Name = "TestParent" } } };
            repo.Save(house);

            House retrieved = repo.Retrieve<House>(house.Id);
            Expect.AreEqual(1, retrieved.Parents.Count);
        }

        [UnitTest]
        public void SavingParentXrefShouldSetChildXref()
        {
            DaoRepository repo = GetTestDaoRepository();
            repo.EnsureDaoAssemblyAndSchema();
            House house = new House { Name = "TestHouse", Parents = new List<Parent> { new Parent { Name = "TestParent" } } };
            repo.Save(house);

            House retrieved = repo.Retrieve<House>(house.Id);
            Parent parent = repo.Retrieve<Parent>(retrieved.Parents[0].Id);
            Expect.AreEqual(1, parent.Houses.Length);
        }

        [UnitTest]
        public void RepoShouldBeQueryable()
        {
            DaoRepository repo = GetTestDaoRepository();
            repo.EnsureDaoAssemblyAndSchema();

            House one = new House { Name = "Get This One" };
            House two = new House { Name = "Get This Too" };
            House three = new House { Name = "Not this one" };
            repo.Save(one);
            repo.Save(three);
            repo.Save(two);

            House[] houses = repo.Query<House>(Query.Where("Name").StartsWith("Get")).ToArray();
            Expect.AreEqual(2, houses.Length);
            houses.Each(house =>
            {
                repo.Delete(house);
            });
            repo.Delete(three);
        }

        [UnitTest]
        public void ShouldHaveEnumerableOfMe()
        {
            Expect.IsTrue(typeof(House).HasEnumerableOfMe(typeof(Parent)), "House didn't have enumerable of Parent");
            Expect.IsTrue(typeof(Parent).HasEnumerableOfMe(typeof(House)), "Parent didn't have enumerable of House");
        }

        [UnitTest]
        public void GetTableTypesShouldGetAllAppropriateTypes()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            HashSet<Type> types = generator.GetTableTypes(typeof(TestContainer));
            Expect.IsTrue(types.Contains(typeof(Parent)));
            Expect.IsTrue(types.Contains(typeof(Daughter)));
            Expect.IsTrue(types.Contains(typeof(Son)));
            Expect.IsTrue(types.Contains(typeof(House)));
        }

        [UnitTest]
        public void ShouldGetFksForDto()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            List<Type> oneToManyTypes = generator.GetForeignKeyTypes(typeof(Parent)).ToList().Select(fk => fk.ForeignKeyType).ToList();
            Expect.AreEqual(2, oneToManyTypes.Count);
            Expect.IsTrue(oneToManyTypes.Contains(typeof(Daughter)));
            Expect.IsTrue(oneToManyTypes.Contains(typeof(Son)));
        }

        [UnitTest]
        public void ShouldGetXrefsForDto()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            List<TypeXref> xrefTypes = generator.GetXrefTypes(typeof(Parent)).ToList();
            Expect.AreEqual(1, xrefTypes.Count);
            Expect.AreEqual(typeof(Parent), xrefTypes[0].Left);
            Expect.AreEqual(typeof(House), xrefTypes[0].Right);
            Expect.IsTrue(xrefTypes[0].LeftCollectionProperty.IsEnumerable());
            OutLineFormat("{0}", ConsoleColor.DarkCyan, xrefTypes[0].LeftCollectionProperty.PropertyType.FullName);
        }

        [UnitTest]
        public void ShouldBeAbleToRenderPocoTemplate()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            TypeSchema typeSchema = generator.CreateTypeSchema(typeof(TestContainer));
            WrapperModel dtoModel = new WrapperModel(typeof(TestContainer), typeSchema);
            string output = dtoModel.Render();
            OutLine(output, ConsoleColor.Cyan);
        }

        [UnitTest]
        public void GenerateDaoForDtosShouldSucceedWithoutErrors()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            TypeDaoGenerator generator = new TypeDaoGenerator();
            generator.AddType(typeof(TestContainer));
            generator.Subscribe(logger);

            CompilerResults result = generator.GenerateAndCompile("TestAssemblyName", ".\\{0}"._Format(MethodBase.GetCurrentMethod().Name));
            if (result.Errors.Count > 0)
            {
                foreach (CompilerError err in result.Errors)
                {
                    OutLineFormat("File: {0}, ErrorNumber: {1}, Line: {2}, Column: {3}, Text: {4}", ConsoleColor.Red, err.FileName, err.ErrorNumber, err.Line, err.Column, err.ErrorText);
                }
            }
            Expect.IsTrue(result.Errors.Count == 0, "There were errors in compilation");
            FileInfo assembly = new FileInfo(result.PathToAssembly);
            OutLineFormat("Assembly is at : {0}", assembly.FullName);
        }

        [UnitTest]
        public void ShouldBeAbleToGenerateSchema()
        {
            TypeDaoGenerator generator = new TypeDaoGenerator();
            generator.AddType(typeof(TestContainer));
            SchemaDefinition schema = generator.CreateSchemaDefinition().SchemaDefinition;
            OutLineFormat("Schema {0} created successfully", ConsoleColor.Green, schema.Name);
        }

        [UnitTest]
        public void GetDaoAssemblyShouldGetTheSameOneEachTime()
        {
            TypeDaoGenerator generator = new TypeDaoGenerator();
            generator.AddType(typeof(TestContainer));
            Assembly first = generator.GetDaoAssembly();
            Assembly again = generator.GetDaoAssembly();
            Assembly andAgain = generator.GetDaoAssembly();
            Assembly oneMoreForGoodMeasure = generator.GetDaoAssembly();

            Expect.AreSame(first, again);
            Expect.AreSame(first, andAgain);
            Expect.AreSame(first, oneMoreForGoodMeasure);
        }

        [UnitTest]
        public void DaoRepositoryCreateShouldSetId()
        {
            DaoRepository repo = GetTestDaoRepository();
            TestContainer toCreate = new TestContainer();
            Expect.AreEqual(0, toCreate.Id);
            string testName = "Test Name".RandomLetters(5);
            toCreate.Name = testName;
            toCreate.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            toCreate = repo.Create(toCreate);
            Expect.IsGreaterThan(toCreate.Id, 0);
            OutLineFormat("{0} passed", ConsoleColor.Green, repo.GetType().Name);
        }

        [UnitTest]
        public void RepositoryRetrieveByIdTest()
        {
            DaoRepository repo = GetTestDaoRepository();
            TestContainer toCreate = new TestContainer();
            Expect.AreEqual(0, toCreate.Id);
            string testName = "Test Name".RandomLetters(5);
            toCreate.Name = testName;
            toCreate.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            toCreate = repo.Create(toCreate);
            TestContainer retrieved = repo.Retrieve<TestContainer>(toCreate.Id);
            Expect.IsNotNull(retrieved);
            Expect.AreEqual(toCreate.Name, retrieved.Name);
            Expect.AreEqual(toCreate.BirthDay.Trim(), retrieved.BirthDay.Trim());
            Expect.AreEqual(toCreate.Id, retrieved.Id);
        }

        [UnitTest]
        public void RepositoryUpdateShouldSetNewPropertyValues()
        {
            DaoRepository repo = GetTestDaoRepository();
            TestContainer toCreate = new TestContainer();
            string testName = "Test Name".RandomLetters(5);
            toCreate.Name = testName;
            toCreate.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            toCreate = repo.Create(toCreate);
            TestContainer toUpdate = repo.Retrieve<TestContainer>(toCreate.Id);
            string newName = "New Name".RandomLetters(8);
            toUpdate.Name = newName;
            DateTime newBirthDay = DateTime.Now.Subtract(new TimeSpan(14, 0, 0, 0));
            toUpdate.BirthDay = newBirthDay;
            toUpdate = repo.Update(toUpdate);
            TestContainer check = repo.Retrieve<TestContainer>(toCreate.Id);
            Expect.AreEqual(newName, check.Name);
            Expect.AreEqual(newBirthDay.Trim(), check.BirthDay.Trim());
            Expect.AreEqual(toCreate.Id, check.Id);
        }

        [UnitTest]
        public void RepositoryDeleteShouldDelete()
        {
            DaoRepository repo = GetTestDaoRepository();
            TestContainer toDelete = new TestContainer();
            string testName = "Test Name".RandomLetters(5);
            toDelete.Name = testName;
            toDelete.BirthDay = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            toDelete = repo.Create(toDelete);
            Expect.IsTrue(repo.Delete<TestContainer>(toDelete));
            TestContainer check = repo.Retrieve<TestContainer>(toDelete.Id);
            Expect.IsNull(check);
        }

        [UnitTest]
        public void SchemaGeneratorShouldWarnAboutMissingForeignKey()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            SchemaDefinitionCreateResult result = generator.CreateSchemaDefinition(new Type[] { typeof(TestParent) });
            Expect.IsTrue(result.MissingColumns);
            Expect.AreEqual(1, result.Warnings.MissingForeignKeyColumns.Length);
        }

        public class NoId
        {
            public string Name { get; set; }
            public TestChild[] Children { get; set; }
        }

        [UnitTest]
        public void SchemaGeneratorShouldWarnAboutMissingKeysIfHasForeignKey()
        {
            TypeSchemaGenerator generator = new TypeSchemaGenerator();
            SchemaDefinitionCreateResult result = generator.CreateSchemaDefinition(new Type[] { typeof(NoId) });
            Expect.IsTrue(result.MissingColumns);
            Expect.AreEqual(1, result.Warnings.MissingKeyColumns.Length);
        }

        [UnitTest]
        public void ShouldBeAbleToReflectOverGeneratedDaoAssembly()
        {
            DaoRepository daoRepo = GetTestDaoRepository();
            Assembly daoAssembly = daoRepo.EnsureDaoAssemblyAndSchema();
            Type[] types = daoAssembly.GetTypes();
            foreach (Type type in types)
            {
                OutLineFormat("{0}", type.FullName);
            }

            Type daoType = daoAssembly.GetType("TypeDaos.Daos.TestContainer");

            Expect.IsNotNull(daoType);
        }

        public class Test<T1, T2>
        {

        }

        [UnitTest]
        public void ShouldOutputTypeAsStringForUseInCode()
        {
            Type type = typeof(Test<Parent, Daughter>);

            OutLineFormat("Is Generic Type: {0}", type.IsGenericType.ToString());
            OutLineFormat("Is Generic Parameter: {0}", type.IsGenericParameter);
            OutLineFormat("Is Generic Type Definition: {0}", type.IsGenericTypeDefinition);
            string genericArguments = type.GetGenericArguments().ToDelimited(t => t.Name);
            OutLineFormat("Generic args: {0}", genericArguments);

            Type arrayOfParent = typeof(Parent[]);

            Expect.AreEqual("Test<Parent, Daughter>", type.ToTypeString(false));
            Expect.AreEqual("Parent[]", arrayOfParent.ToTypeString(false));
        }

        [UnitTest]
        public void OutputTypesToInfoString()
        {
            OutLine((new Type[] { typeof(Parent), typeof(Daughter), typeof(Son) }).ToInfoString());
        }

        protected static DaoRepository GetTestDaoRepository()
        {
            DaoRepository daoRepo = new DaoRepository();
            daoRepo.WarningsAsErrors = false;
            daoRepo.Database = new SQLiteDatabase(".\\", "UNITTESTS");
            daoRepo.AddType(typeof(TestContainer));
            return daoRepo;
        }

        protected static MongoRepository GetMongoRepository()
        {
            return new MongoRepository();
        }
    }
}

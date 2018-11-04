using Bam.Net.Data.Schema;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.IO;
using System.Reflection;

namespace Bam.Net.Tests
{
    [Serializable]
    public class DaoGeneratorTests : CommandLineTestInterface
    {
        [UnitTest]
        public static void GenerateShouldFireAssociatedEvents()
        {
            // TODO: add Query class events (events were added on 08/12/2015)
            DaoGenerator gen = new DaoGenerator("Test.Ns");
            bool? started = false;
            bool? complete = false;
            bool? beforeParse = false;
            bool? afterParse = false;
            bool? beforeResolved = false;
            bool? afterResolved = false;
            bool? beforeWrite = false;
            bool? afterWrite = false;

            bool? beforeCollectionParse = false;
            bool? afterCollectionParse = false;

            bool? beforeCollectionStreamResolved = false;
            bool? afterCollectionStreamResolved = false;

            bool? beforeWriteCollection = false;
            bool? afterWriteCollection = false;

            bool? beforeColumnClassParse = false;
            bool? afterColumnClassParse = false;

            bool? beforeColumnStreamResolved = false;
            bool? afterColumnStreamResolved = false;

            bool? beforeWriteColumns = false;
            bool? afterWriteColumns = false;

            gen.GenerateStarted += (g, s) => { started = true; };
            gen.GenerateComplete += (g, s) => { complete = true; };

            gen.BeforeClassParse += (ns, t) => { beforeParse = true; };
            gen.AfterClassParse += (ns, t) => { afterParse = true; };

            gen.BeforeClassStreamResolved += (s, t) => { beforeResolved = true; };
            gen.AfterClassStreamResolved += (s, t) => { afterResolved = true; };
            gen.BeforeWriteClass += (c, s) => { beforeWrite = true; };
            gen.AfterWriteClass += (c, s) => { afterWrite = true; };

            gen.BeforeCollectionParse += (n, t) => { beforeCollectionParse = true; };
            gen.AfterCollectionParse += (n, t) => { afterCollectionParse = true; };

            gen.BeforeCollectionStreamResolved += (n, t) => { beforeCollectionStreamResolved = true; };
            gen.AfterCollectionStreamResolved += (n, t) => { afterCollectionStreamResolved = true; };

            gen.BeforeWriteCollection += (c, s) => { beforeWriteCollection = true; };
            gen.AfterWriteCollection += (c, s) => { afterWriteCollection = true; };

            gen.BeforeColumnsClassStreamResolved += (s, t) => { beforeColumnStreamResolved = true; };
            gen.AfterColumnsClassStreamResolved += (s, t) => { afterColumnStreamResolved = true; };

            gen.BeforeWriteColumnsClass += (c, s) => { beforeWriteColumns = true; };
            gen.AfterWriteColumnsClass += (c, s) => { afterWriteColumns = true; };

            gen.BeforeColumnsClassParse += (c, s) => { beforeColumnClassParse = true; };
            gen.AfterColumnsClassParse += (c, s) => { afterColumnClassParse = true; };

            gen.Generate(GetTestSchema());

            Expect.IsTrue(started.Value, "GenerateStarted didn't fire");
            Expect.IsTrue(complete.Value, "GenerateComplete didn't fire");
            Expect.IsTrue(beforeParse.Value, "BeforeParse didn't fire");
            Expect.IsTrue(afterParse.Value, "AfterParse didn't fire");
            Expect.IsTrue(beforeResolved.Value, "BeforeStreamResolved didn't fire");
            Expect.IsTrue(afterResolved.Value, "AfterStreamResolved didn't fire");
            Expect.IsTrue(beforeWrite.Value, "BeforeWriteResult didn't fire");
            Expect.IsTrue(afterWrite.Value, "AfterWriteResult didn't fire");

            Expect.IsTrue(beforeCollectionParse.Value, "BeforeCollectionParse didn't fire");
            Expect.IsTrue(afterCollectionParse.Value, "AfterCollectionParse didn't fire");

            Expect.IsTrue(beforeCollectionStreamResolved.Value, "BeforeCollectionStreamResolved didn't fire");
            Expect.IsTrue(afterCollectionStreamResolved.Value, "AfterCollectionStreamResolved didn't fire");

            Expect.IsTrue(beforeWriteCollection.Value, "BeforeWriteCollection didn't fire");
            Expect.IsTrue(afterWriteCollection.Value, "AfterWriteCollection didn't fire");

            Expect.IsTrue(beforeColumnStreamResolved.Value, "BeforeColumnStreamResolved didn't fire");
            Expect.IsTrue(afterColumnStreamResolved.Value, "AfterColumnStreamResolved didn't fire");

            Expect.IsTrue(beforeWriteColumns.Value, "BeforeWriteColumns didn't fire");
            Expect.IsTrue(afterWriteColumns.Value, "AfterWriteColumns didn't fire");

            Expect.IsTrue(beforeColumnClassParse.Value, "BeforeColumnClassParse didn't fire");
            Expect.IsTrue(afterColumnClassParse.Value, "AfterColumnClassParse didn't fire");
        }

        [UnitTest]
        public static void GenerateShouldUseSpecifiedTargetResolver()
        {
            string filePath = MethodBase.GetCurrentMethod().Name;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            DaoGenerator generator = new DaoGenerator("Test");

            SchemaDefinition schema = GetTestSchema();
            generator.DisposeOnComplete = false;
            generator.Generate(schema, (s) => {
                return new FileStream(filePath, FileMode.Create);
            });

            string output = File.ReadAllText(filePath);

            Expect.IsGreaterThan(output.Length, 0);

            OutLine(output, ConsoleColor.Cyan);
            DeleteSchema(schema);
        }

        private static SchemaDefinition GetTestSchema()
        {
            SchemaManager mgr = new SchemaManager();
            SchemaDefinition schema = mgr.SetSchema("test");
            mgr.AddTable("monkey");
            return schema;
        }

        internal static void DeleteSchema(SchemaDefinition schema)
        {
            try
            {
                File.Delete(schema.File);
            }
            catch (Exception ex)
            {
                OutLineFormat("An error occurred deleting schema file: {0}", ConsoleColor.Red, ex.Message);
            }
        }
    }
}

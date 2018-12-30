using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Data.Repositories.Tests.TestDtos;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Unit;
using Bam.Net.CommandLine;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    public class DbExample : CommandLineTestInterface
    {
        [ConsoleAction]
        public void CanSaveManyToMany()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            SQLiteDatabase database = new SQLiteDatabase("TestData");
            DaoRepository repo = new DaoRepository(database, logger);
            repo.AddType<Parent>();
            repo.AddType<House>();
            repo.AddType<Son>();
            repo.AddType<Daughter>();
            repo.EnsureDaoAssemblyAndSchema();

            Parent p = new Parent();
            p.Houses = new House[] { new House { Name = "one" }, new House { Name = "two" } };
            p = repo.Save(p);
            Parent check = repo.Retrieve<Parent>(p.Id);
            Expect.AreEqual(2, check.Houses.Length);
        }
        [ConsoleAction]
        public void PopulateTestData()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            SQLiteDatabase database = new SQLiteDatabase("TestData");
            DaoRepository repo = new DaoRepository(database, logger);
            repo.AddType<Parent>();
            repo.AddType<House>();
            repo.AddType<Son>();
            repo.AddType<Daughter>();
            repo.EnsureDaoAssemblyAndSchema();

            Parent bryan = new Parent { Name = "Bryan" };
            Parent andra = new Parent { Name = "Andra" };
            Parent uncleAlvin = new Parent { Name = "Uncle Alvin" };
            Parent analou = new Parent { Name = "Analou" };
            bryan = repo.Save(bryan);
            andra = repo.Save(andra);
            uncleAlvin = repo.Save(uncleAlvin);
            analou = repo.Save(analou);

            Daughter nayrb = new Daughter { Name = "Nayrb" };
            Son rizal = new Son { Name = "Rizal" };
            Son hodari = new Son { Name = "Hodari" };
            Son arias = new Son { Name = "Arias" };
            nayrb = repo.Save(nayrb);
            rizal = repo.Save(rizal);
            hodari = repo.Save(hodari);
            arias = repo.Save(arias);

            House lakeStevens = new House { Name = "Lake Stevens" };
            House grandpas = new House { Name = "Grandpa's" };
            House uncleAlvins = new House { Name = "Uncle Alvin's" };
            lakeStevens = repo.Save(lakeStevens);
            grandpas = repo.Save(grandpas);
            uncleAlvins = repo.Save(uncleAlvins);
            
            bryan.Houses = new House[] { lakeStevens, grandpas };
            bryan = repo.Save(bryan);
            bryan.Sons = new List<Son> { rizal, hodari };
            bryan = repo.Save(bryan);
            bryan.Daughters = new Daughter[] { nayrb };
            bryan = repo.Save(bryan);
            Parent rBryan = repo.Retrieve<Parent>(bryan.Id);
            Expect.IsTrue(rBryan.Sons.Count == 2);
            Expect.IsTrue(rBryan.Daughters.Length == 1);
            Expect.IsTrue(rBryan.Houses.Length == 2);

            andra.Houses = new House[] { lakeStevens };
            andra.Daughters = new Daughter[] { nayrb };
            andra.Sons = new List<Son> { hodari };
            andra = repo.Save(andra);

            uncleAlvin.Houses = new House[] { uncleAlvins };
            uncleAlvin.Sons = new List<Son> { arias };
            uncleAlvin = repo.Save(uncleAlvin);

            analou.Houses = new House[] { uncleAlvins };
            analou.Sons = new List<Son> { arias };
            analou = repo.Save(analou);

            Parent rAndra = repo.Retrieve<Parent>(andra.Id);
            Expect.IsTrue(rAndra.Sons.Count == 1);
            Expect.IsTrue(rAndra.Daughters.Length == 1);
            Expect.IsTrue(rAndra.Houses.Length == 1);
            
            Parent rUncleA = repo.Retrieve<Parent>(uncleAlvin.Id);
            Expect.IsTrue(rUncleA.Sons.Count == 1);
            Expect.IsTrue(rUncleA.Houses.Length == 1);

            Parent rAnalou = repo.Retrieve<Parent>(analou.Id);
            Expect.IsTrue(rAnalou.Sons.Count == 1);
            Expect.IsTrue(rAnalou.Houses.Length == 1);

            Pass("all good");
        }
    }
}

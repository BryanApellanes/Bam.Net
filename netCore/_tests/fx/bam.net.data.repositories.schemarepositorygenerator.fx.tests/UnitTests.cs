using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Testing;
using Bam.Net.Data.Repositories.Tests.ClrTypes.Daos;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Unit;
using Bam.Net.CommandLine;

namespace Bam.Net.Data.Repositories.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void SavingParentFiresChildCommitAfterAddNew()
        {
            SQLiteDatabase database = new SQLiteDatabase(nameof(SavingParentFiresChildCommitAfterAddNew));
            database.TryEnsureSchema<Parent>();

            Parent parent = new Parent(database);
            parent.Name = 8.RandomLetters();
            parent.Save();

            bool? parentEventFired = false;
            parent.AfterCommit += (db, dao) =>
            {
                parentEventFired = true;
                Parent cast = dao as Parent;
                Expect.IsNotNull(cast);
                Expect.AreSame(parent, cast);
                Expect.AreEqual(parent.Name, cast.Name);
                Expect.AreSame(database, db);
            };
            Son addNewSon = parent.SonsByParentId.AddNew();
            addNewSon.Name = 6.RandomLetters();
            bool? addNewSonEventFired = false;
            addNewSon.AfterCommit += (db, dao) =>
            {
                Son cast = dao as Son;
                Expect.IsNotNull(cast);
                Expect.AreSame(addNewSon, dao);
                Expect.AreEqual(addNewSon.Name, cast.Name);
                Expect.AreSame(database, db);                
                addNewSonEventFired = true;
            };
            parent.Save();
            Expect.IsTrue(parentEventFired.Value, "Parent didn't fire AfterCommit event");
            Expect.IsTrue(addNewSonEventFired.Value, "Son added using AddNew didn't fire AfterCommit event");
        }

        [UnitTest]
        public void SavingParentFiresChildCommitEvent()
        {
            SQLiteDatabase database = new SQLiteDatabase(nameof(SavingParentFiresChildCommitEvent));
            database.TryEnsureSchema<Parent>();

            Parent parent = new Parent(database);            
            parent.Name = 8.RandomLetters();
            parent.Save();
            Son addedSon = new Son();
            addedSon.Name = 7.RandomLetters();
            parent.SonsByParentId.Add(addedSon);

            bool? addedSonEventFired = false;
            addedSon.AfterCommit += (db, dao) =>
            {
                Son cast = dao as Son;
                Expect.IsNotNull(cast);
                Expect.AreSame(addedSon, dao);
                Expect.AreEqual(addedSon.Name, cast.Name);
                Expect.AreSame(database, db);
                addedSonEventFired = true;
            };

            bool? parentEventFired = false;
            parent.AfterCommit += (db, dao) =>
            {
                parentEventFired = true;
                Parent cast = dao as Parent;
                Expect.IsNotNull(cast);
                Expect.AreSame(parent, cast);
                //Expect.IsFalse(cast.IsNew, "Son was still new after commit event fired");
                Expect.AreSame(database, db);
            };

            parent.Save(database);
            Expect.IsTrue(parentEventFired.Value, "Parent didn't fire AfterCommit event");
            Expect.IsTrue(addedSonEventFired.Value, "Son added using SonsById.Add didn't fire AfterCommit event");            
        }
    }
}

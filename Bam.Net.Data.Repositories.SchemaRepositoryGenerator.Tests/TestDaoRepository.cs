using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_;
using Bam.Net.Data.SQLite;

namespace Bam.Net.Data.Repositories.Tests
{
    public class TestDaoRepository: DaoRepository
    {
        public TestDaoRepository(string schemaName) : base(new SQLiteDatabase(".\\", schemaName))
        {
            SchemaName = schemaName;
            AddType<ClrTypes.Parent>();
        }

        public ClrTypes.Parent GetOneParentWhere(WhereDelegate<ParentDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<ClrTypes.Parent>();
            return (ClrTypes.Parent)ParentDao.GetOneWhere(where, Database).CopyAs(wrapperType, this);
        }

        public ClrTypes.Parent OneParentWhere(WhereDelegate<ParentDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<ClrTypes.Parent>();
            return (ClrTypes.Parent)ParentDao.OneWhere(where, Database).CopyAs(wrapperType, this);
        }
        public IEnumerable<ClrTypes.Parent> TopParentsWhere(int count, WhereDelegate<ParentDaoColumns> where)
        {
            return Wrap<ClrTypes.Parent>(ParentDao.Top(count, where, Database));
        }

        public IEnumerable<ClrTypes.Parent> ParentsWhere(WhereDelegate<ParentDaoColumns> where)
        {
            return Query<ClrTypes.Parent>(where(new ParentDaoColumns()));
        }

        public long CountParentsWhere(WhereDelegate<ParentDaoColumns> where)
        {
            return ParentDao.Count(where, Database);
        }
        //public static async Task BatchQuery(int batchSize, WhereDelegate<ParentDaoColumns> where, Func<ParentDaoCollection, Task> batchProcessor, Database database = null)
        public async Task BatchQueryParents(int batchSize, WhereDelegate<ParentDaoColumns> where, Action<IEnumerable<ClrTypes.Parent>> batchProcessor)
        {
            await ParentDao.BatchQuery(batchSize, where, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<ClrTypes.Parent>(batch));
                });
            }, Database);
        }

        public async Task BatchAllParents(int batchSize, Action<IEnumerable<ClrTypes.Parent>> batchProcessor)
        {
            await ParentDao.BatchAll(batchSize, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<ClrTypes.Parent>(batch));
                });
            }, Database);
        }

        public long CountParents()
        {
            return ParentDao.Count(Database);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories.Tests.ClrTypes.Daos;
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

        public ClrTypes.Parent GetOneParentWhere(WhereDelegate<ParentColumns> where)
        {
            Type wrapperType = GetWrapperType<ClrTypes.Parent>();
            return (ClrTypes.Parent)Parent.GetOneWhere(where, Database).CopyAs(wrapperType, this);
        }

        public ClrTypes.Parent OneParentWhere(WhereDelegate<ParentColumns> where)
        {
            Type wrapperType = GetWrapperType<ClrTypes.Parent>();
            return (ClrTypes.Parent)Parent.OneWhere(where, Database).CopyAs(wrapperType, this);
        }
        public IEnumerable<ClrTypes.Parent> TopParentsWhere(int count, WhereDelegate<ParentColumns> where)
        {
            return Wrap<ClrTypes.Parent>(Parent.Top(count, where, Database));
        }

        public IEnumerable<ClrTypes.Parent> ParentsWhere(WhereDelegate<ParentColumns> where)
        {
            return Query<ClrTypes.Parent>(where(new ParentColumns()));
        }

        public long CountParentsWhere(WhereDelegate<ParentColumns> where)
        {
            return Parent.Count(where, Database);
        }
        
        public async Task BatchQueryParents(int batchSize, WhereDelegate<ParentColumns> where, Action<IEnumerable<ClrTypes.Parent>> batchProcessor)
        {
            await Parent.BatchQuery(batchSize, where, (batch) =>
            {
                batchProcessor(Wrap<ClrTypes.Parent>(batch));
            }, Database);
        }

        public async Task BatchAllParents(int batchSize, Action<IEnumerable<ClrTypes.Parent>> batchProcessor)
        {
            await Parent.BatchAll(batchSize, (batch) =>
            {
                batchProcessor(Wrap<ClrTypes.Parent>(batch));
            }, Database);
        }

        public long CountParents()
        {
            return Parent.Count(Database);
        }
    }
}

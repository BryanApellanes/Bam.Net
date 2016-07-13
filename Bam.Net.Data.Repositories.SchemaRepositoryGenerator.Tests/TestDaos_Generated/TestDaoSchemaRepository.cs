using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories.Tests.ClrTypes;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
	[Serializable]
	public class TestDaoSchemaRepository: DaoRepository
	{
		public TestDaoSchemaRepository()
		{
			SchemaName = "TestDaoSchema";
﻿			
			AddType<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>();﻿			
			AddType<Bam.Net.Data.Repositories.Tests.ClrTypes.House>();﻿			
			AddType<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>();﻿			
			AddType<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>();
		}

﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter GetOneDaughterWhere(WhereDelegate<DaughterDaoColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter)DaughterDao.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter OneDaughterWhere(WhereDelegate<DaughterDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter)DaughterDao.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter> TopDaughtersWhere(int count, WhereDelegate<DaughterDaoColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(DaughterDao.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter> DaughtersWhere(WhereDelegate<DaughterDaoColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(where(new DaughterDaoColumns()));
        }
	    
		public long CountDaughters()
        {
            return DaughterDao.Count(Database);
        }

        public long CountDaughtersWhere(WhereDelegate<DaughterDaoColumns> where)
        {
            return DaughterDao.Count(where, Database);
        }
        
        public async Task BatchQueryDaughters(int batchSize, WhereDelegate<DaughterDaoColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>> batchProcessor)
        {
            await DaughterDao.BatchQuery(batchSize, where, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(batch));
                });
            }, Database);
        }

		
        public async Task BatchAllDaughters(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>> batchProcessor)
        {
            await DaughterDao.BatchAll(batchSize, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(batch));
                });
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.House GetOneHouseWhere(WhereDelegate<HouseDaoColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.House>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.House)HouseDao.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.House OneHouseWhere(WhereDelegate<HouseDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.House>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.House)HouseDao.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House> TopHousesWhere(int count, WhereDelegate<HouseDaoColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(HouseDao.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House> HousesWhere(WhereDelegate<HouseDaoColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(where(new HouseDaoColumns()));
        }
	    
		public long CountHouses()
        {
            return HouseDao.Count(Database);
        }

        public long CountHousesWhere(WhereDelegate<HouseDaoColumns> where)
        {
            return HouseDao.Count(where, Database);
        }
        
        public async Task BatchQueryHouses(int batchSize, WhereDelegate<HouseDaoColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House>> batchProcessor)
        {
            await HouseDao.BatchQuery(batchSize, where, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(batch));
                });
            }, Database);
        }

		
        public async Task BatchAllHouses(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House>> batchProcessor)
        {
            await HouseDao.BatchAll(batchSize, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(batch));
                });
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Parent GetOneParentWhere(WhereDelegate<ParentDaoColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Parent)ParentDao.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Parent OneParentWhere(WhereDelegate<ParentDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Parent)ParentDao.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> TopParentsWhere(int count, WhereDelegate<ParentDaoColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(ParentDao.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> ParentsWhere(WhereDelegate<ParentDaoColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(where(new ParentDaoColumns()));
        }
	    
		public long CountParents()
        {
            return ParentDao.Count(Database);
        }

        public long CountParentsWhere(WhereDelegate<ParentDaoColumns> where)
        {
            return ParentDao.Count(where, Database);
        }
        
        public async Task BatchQueryParents(int batchSize, WhereDelegate<ParentDaoColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>> batchProcessor)
        {
            await ParentDao.BatchQuery(batchSize, where, (batch) =>
            {
                batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(batch));
            }, Database);
        }

		
        public async Task BatchAllParents(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>> batchProcessor)
        {
            await ParentDao.BatchAll(batchSize, (batch) =>
            {
                batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(batch));
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Son GetOneSonWhere(WhereDelegate<SonDaoColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Son)SonDao.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Son OneSonWhere(WhereDelegate<SonDaoColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Son)SonDao.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> TopSonsWhere(int count, WhereDelegate<SonDaoColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(SonDao.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> SonsWhere(WhereDelegate<SonDaoColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(where(new SonDaoColumns()));
        }
	    
		public long CountSons()
        {
            return SonDao.Count(Database);
        }

        public long CountSonsWhere(WhereDelegate<SonDaoColumns> where)
        {
            return SonDao.Count(where, Database);
        }
        
        public async Task BatchQuerySons(int batchSize, WhereDelegate<SonDaoColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>> batchProcessor)
        {
            await SonDao.BatchQuery(batchSize, where, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(batch));
                });
            }, Database);
        }

		
        public async Task BatchAllSons(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>> batchProcessor)
        {
            await SonDao.BatchAll(batchSize, (batch) =>
            {
                return Task.Run(() =>
                {
                    batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(batch));
                });
            }, Database);
        }
	}
}																								

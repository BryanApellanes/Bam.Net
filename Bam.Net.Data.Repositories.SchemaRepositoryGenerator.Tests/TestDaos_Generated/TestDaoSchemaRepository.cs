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

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Repository
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
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter GetOneDaughterWhere(WhereDelegate<DaughterColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter OneDaughterWhere(WhereDelegate<DaughterColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter> TopDaughtersWhere(int count, WhereDelegate<DaughterColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter> DaughtersWhere(WhereDelegate<DaughterColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(where(new DaughterColumns()));
        }
	    
		public long CountDaughters()
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Count(Database);
        }

        public long CountDaughtersWhere(WhereDelegate<DaughterColumns> where)
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.Count(where, Database);
        }
        
        public async Task BatchQueryDaughters(int batchSize, WhereDelegate<DaughterColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(batch));
            }, Database);
        }

		
        public async Task BatchAllDaughters(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Daughter.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(batch));
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.House GetOneHouseWhere(WhereDelegate<HouseColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.House>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.House)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.House OneHouseWhere(WhereDelegate<HouseColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.House>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.House)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House> TopHousesWhere(int count, WhereDelegate<HouseColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House> HousesWhere(WhereDelegate<HouseColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(where(new HouseColumns()));
        }
	    
		public long CountHouses()
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Count(Database);
        }

        public long CountHousesWhere(WhereDelegate<HouseColumns> where)
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.Count(where, Database);
        }
        
        public async Task BatchQueryHouses(int batchSize, WhereDelegate<HouseColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(batch));
            }, Database);
        }

		
        public async Task BatchAllHouses(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.House>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.House>(batch));
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Parent GetOneParentWhere(WhereDelegate<ParentColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Parent)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Parent OneParentWhere(WhereDelegate<ParentColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Parent)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> TopParentsWhere(int count, WhereDelegate<ParentColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> ParentsWhere(WhereDelegate<ParentColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(where(new ParentColumns()));
        }
	    
		public long CountParents()
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Count(Database);
        }

        public long CountParentsWhere(WhereDelegate<ParentColumns> where)
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.Count(where, Database);
        }
        
        public async Task BatchQueryParents(int batchSize, WhereDelegate<ParentColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(batch));
            }, Database);
        }

		
        public async Task BatchAllParents(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Parent.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>(batch));
            }, Database);
        }﻿		
		public Bam.Net.Data.Repositories.Tests.ClrTypes.Son GetOneSonWhere(WhereDelegate<SonColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>();
			return (Bam.Net.Data.Repositories.Tests.ClrTypes.Son)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.Data.Repositories.Tests.ClrTypes.Son OneSonWhere(WhereDelegate<SonColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>();
            return (Bam.Net.Data.Repositories.Tests.ClrTypes.Son)Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> TopSonsWhere(int count, WhereDelegate<SonColumns> where)
        {
            return Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> SonsWhere(WhereDelegate<SonColumns> where)
        {
            return Query<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(where(new SonColumns()));
        }
	    
		public long CountSons()
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Count(Database);
        }

        public long CountSonsWhere(WhereDelegate<SonColumns> where)
        {
            return Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.Count(where, Database);
        }
        
        public async Task BatchQuerySons(int batchSize, WhereDelegate<SonColumns> where, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(batch));
            }, Database);
        }

		
        public async Task BatchAllSons(int batchSize, Action<IEnumerable<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>> batchProcessor)
        {
            await Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.Son.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(batch));
            }, Database);
        }
	}
}																								

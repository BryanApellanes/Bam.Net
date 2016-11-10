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
using Bam.Net.Data.Repositories;
using Bam.Net.CoreServices.Data;

namespace Bam.Net.CoreServices.Data.Daos.Repository
{
	[Serializable]
	public class ApplicationRegistryRepository: DaoRepository
	{
		public ApplicationRegistryRepository()
		{
			SchemaName = "ApplicationRegistry";
			Namespace = "Bam.Net.CoreServices.Data.Daos";
﻿			
			AddType<Bam.Net.CoreServices.Data.ApiKey>();﻿			
			AddType<Bam.Net.CoreServices.Data.ApplicationInstance>();﻿			
			AddType<Bam.Net.CoreServices.Data.HostName>();﻿			
			AddType<Bam.Net.CoreServices.Data.Subscription>();﻿			
			AddType<Bam.Net.CoreServices.Data.User>();﻿			
			AddType<Bam.Net.CoreServices.Data.Application>();﻿			
			AddType<Bam.Net.CoreServices.Data.Organization>();
		}
﻿		
		public Bam.Net.CoreServices.Data.ApiKey GetOneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApiKey>();
			return (Bam.Net.CoreServices.Data.ApiKey)Bam.Net.CoreServices.Data.Daos.ApiKey.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.ApiKey OneApiKeyWhere(WhereDelegate<ApiKeyColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApiKey>();
            return (Bam.Net.CoreServices.Data.ApiKey)Bam.Net.CoreServices.Data.Daos.ApiKey.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.ApiKey> TopApiKeysWhere(int count, WhereDelegate<ApiKeyColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ApiKey>(Bam.Net.CoreServices.Data.Daos.ApiKey.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.ApiKey> ApiKeysWhere(WhereDelegate<ApiKeyColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.ApiKey>(where(new ApiKeyColumns()));
        }
	    
		public long CountApiKeys()
        {
            return Bam.Net.CoreServices.Data.Daos.ApiKey.Count(Database);
        }

        public long CountApiKeysWhere(WhereDelegate<ApiKeyColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ApiKey.Count(where, Database);
        }
        
        public async Task BatchQueryApiKeys(int batchSize, WhereDelegate<ApiKeyColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApiKey.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApiKey>(batch));
            }, Database);
        }

		
        public async Task BatchAllApiKeys(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ApiKey>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApiKey.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApiKey>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.ApplicationInstance GetOneApplicationInstanceWhere(WhereDelegate<ApplicationInstanceColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApplicationInstance>();
			return (Bam.Net.CoreServices.Data.ApplicationInstance)Bam.Net.CoreServices.Data.Daos.ApplicationInstance.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.ApplicationInstance OneApplicationInstanceWhere(WhereDelegate<ApplicationInstanceColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ApplicationInstance>();
            return (Bam.Net.CoreServices.Data.ApplicationInstance)Bam.Net.CoreServices.Data.Daos.ApplicationInstance.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.ApplicationInstance> TopApplicationInstancesWhere(int count, WhereDelegate<ApplicationInstanceColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ApplicationInstance>(Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.ApplicationInstance> ApplicationInstancesWhere(WhereDelegate<ApplicationInstanceColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.ApplicationInstance>(where(new ApplicationInstanceColumns()));
        }
	    
		public long CountApplicationInstances()
        {
            return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Count(Database);
        }

        public long CountApplicationInstancesWhere(WhereDelegate<ApplicationInstanceColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ApplicationInstance.Count(where, Database);
        }
        
        public async Task BatchQueryApplicationInstances(int batchSize, WhereDelegate<ApplicationInstanceColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ApplicationInstance>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApplicationInstance.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApplicationInstance>(batch));
            }, Database);
        }

		
        public async Task BatchAllApplicationInstances(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ApplicationInstance>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ApplicationInstance.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ApplicationInstance>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.HostName GetOneHostNameWhere(WhereDelegate<HostNameColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.HostName>();
			return (Bam.Net.CoreServices.Data.HostName)Bam.Net.CoreServices.Data.Daos.HostName.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.HostName OneHostNameWhere(WhereDelegate<HostNameColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.HostName>();
            return (Bam.Net.CoreServices.Data.HostName)Bam.Net.CoreServices.Data.Daos.HostName.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.HostName> TopHostNamesWhere(int count, WhereDelegate<HostNameColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.HostName>(Bam.Net.CoreServices.Data.Daos.HostName.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.HostName> HostNamesWhere(WhereDelegate<HostNameColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.HostName>(where(new HostNameColumns()));
        }
	    
		public long CountHostNames()
        {
            return Bam.Net.CoreServices.Data.Daos.HostName.Count(Database);
        }

        public long CountHostNamesWhere(WhereDelegate<HostNameColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.HostName.Count(where, Database);
        }
        
        public async Task BatchQueryHostNames(int batchSize, WhereDelegate<HostNameColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.HostName>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.HostName.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.HostName>(batch));
            }, Database);
        }

		
        public async Task BatchAllHostNames(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.HostName>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.HostName.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.HostName>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.Subscription GetOneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Subscription>();
			return (Bam.Net.CoreServices.Data.Subscription)Bam.Net.CoreServices.Data.Daos.Subscription.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.Subscription OneSubscriptionWhere(WhereDelegate<SubscriptionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Subscription>();
            return (Bam.Net.CoreServices.Data.Subscription)Bam.Net.CoreServices.Data.Daos.Subscription.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.Subscription> TopSubscriptionsWhere(int count, WhereDelegate<SubscriptionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Subscription>(Bam.Net.CoreServices.Data.Daos.Subscription.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.Subscription> SubscriptionsWhere(WhereDelegate<SubscriptionColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.Subscription>(where(new SubscriptionColumns()));
        }
	    
		public long CountSubscriptions()
        {
            return Bam.Net.CoreServices.Data.Daos.Subscription.Count(Database);
        }

        public long CountSubscriptionsWhere(WhereDelegate<SubscriptionColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.Subscription.Count(where, Database);
        }
        
        public async Task BatchQuerySubscriptions(int batchSize, WhereDelegate<SubscriptionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Subscription.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Subscription>(batch));
            }, Database);
        }

		
        public async Task BatchAllSubscriptions(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Subscription>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Subscription.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Subscription>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.User GetOneUserWhere(WhereDelegate<UserColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.User>();
			return (Bam.Net.CoreServices.Data.User)Bam.Net.CoreServices.Data.Daos.User.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.User OneUserWhere(WhereDelegate<UserColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.User>();
            return (Bam.Net.CoreServices.Data.User)Bam.Net.CoreServices.Data.Daos.User.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.User> TopUsersWhere(int count, WhereDelegate<UserColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.User>(Bam.Net.CoreServices.Data.Daos.User.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.User> UsersWhere(WhereDelegate<UserColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.User>(where(new UserColumns()));
        }
	    
		public long CountUsers()
        {
            return Bam.Net.CoreServices.Data.Daos.User.Count(Database);
        }

        public long CountUsersWhere(WhereDelegate<UserColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.User.Count(where, Database);
        }
        
        public async Task BatchQueryUsers(int batchSize, WhereDelegate<UserColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.User.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.User>(batch));
            }, Database);
        }

		
        public async Task BatchAllUsers(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.User>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.User.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.User>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.Application GetOneApplicationWhere(WhereDelegate<ApplicationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Application>();
			return (Bam.Net.CoreServices.Data.Application)Bam.Net.CoreServices.Data.Daos.Application.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.Application OneApplicationWhere(WhereDelegate<ApplicationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Application>();
            return (Bam.Net.CoreServices.Data.Application)Bam.Net.CoreServices.Data.Daos.Application.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.Application> TopApplicationsWhere(int count, WhereDelegate<ApplicationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Application>(Bam.Net.CoreServices.Data.Daos.Application.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.Application> ApplicationsWhere(WhereDelegate<ApplicationColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.Application>(where(new ApplicationColumns()));
        }
	    
		public long CountApplications()
        {
            return Bam.Net.CoreServices.Data.Daos.Application.Count(Database);
        }

        public long CountApplicationsWhere(WhereDelegate<ApplicationColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.Application.Count(where, Database);
        }
        
        public async Task BatchQueryApplications(int batchSize, WhereDelegate<ApplicationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Application.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Application>(batch));
            }, Database);
        }

		
        public async Task BatchAllApplications(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Application>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Application.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Application>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.Organization GetOneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Organization>();
			return (Bam.Net.CoreServices.Data.Organization)Bam.Net.CoreServices.Data.Daos.Organization.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.Organization OneOrganizationWhere(WhereDelegate<OrganizationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Organization>();
            return (Bam.Net.CoreServices.Data.Organization)Bam.Net.CoreServices.Data.Daos.Organization.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.Organization> TopOrganizationsWhere(int count, WhereDelegate<OrganizationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Organization>(Bam.Net.CoreServices.Data.Daos.Organization.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.Organization> OrganizationsWhere(WhereDelegate<OrganizationColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.Organization>(where(new OrganizationColumns()));
        }
	    
		public long CountOrganizations()
        {
            return Bam.Net.CoreServices.Data.Daos.Organization.Count(Database);
        }

        public long CountOrganizationsWhere(WhereDelegate<OrganizationColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.Organization.Count(where, Database);
        }
        
        public async Task BatchQueryOrganizations(int batchSize, WhereDelegate<OrganizationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Organization.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Organization>(batch));
            }, Database);
        }

		
        public async Task BatchAllOrganizations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Organization>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Organization.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Organization>(batch));
            }, Database);
        }
	}
}																								

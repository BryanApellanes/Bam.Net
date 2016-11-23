/*
This file was generated and should not be modified directly
*/
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
	public class CoreRegistryRepository: DaoRepository
	{
		public CoreRegistryRepository()
		{
			SchemaName = "CoreRegistry";
			Namespace = "Bam.Net.CoreServices.Data.Daos";
﻿			
			AddType<Bam.Net.CoreServices.Data.IpAddress>();﻿			
			AddType<Bam.Net.CoreServices.Data.Configuration>();﻿			
			AddType<Bam.Net.CoreServices.Data.ApiKey>();﻿			
			AddType<Bam.Net.CoreServices.Data.Application>();﻿			
			AddType<Bam.Net.CoreServices.Data.ClientServerConnection>();﻿			
			AddType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.Data.Machine>();﻿			
			AddType<Bam.Net.CoreServices.Data.Organization>();﻿			
			AddType<Bam.Net.CoreServices.Data.ProcessDescriptor>();﻿			
			AddType<Bam.Net.CoreServices.Data.Subscription>();﻿			
			AddType<Bam.Net.CoreServices.Data.User>();
		}

﻿		
		public Bam.Net.CoreServices.Data.IpAddress GetOneIpAddressWhere(WhereDelegate<IpAddressColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.IpAddress>();
			return (Bam.Net.CoreServices.Data.IpAddress)Bam.Net.CoreServices.Data.Daos.IpAddress.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.IpAddress OneIpAddressWhere(WhereDelegate<IpAddressColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.IpAddress>();
            return (Bam.Net.CoreServices.Data.IpAddress)Bam.Net.CoreServices.Data.Daos.IpAddress.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.IpAddress> TopIpAddressesWhere(int count, WhereDelegate<IpAddressColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.IpAddress>(Bam.Net.CoreServices.Data.Daos.IpAddress.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.IpAddress> IpAddressesWhere(WhereDelegate<IpAddressColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.IpAddress>(where(new IpAddressColumns()));
        }
	    
		public long CountIpAddresses()
        {
            return Bam.Net.CoreServices.Data.Daos.IpAddress.Count(Database);
        }

        public long CountIpAddressesWhere(WhereDelegate<IpAddressColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.IpAddress.Count(where, Database);
        }
        
        public async Task BatchQueryIpAddresses(int batchSize, WhereDelegate<IpAddressColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.IpAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.IpAddress.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.IpAddress>(batch));
            }, Database);
        }

		
        public async Task BatchAllIpAddresses(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.IpAddress>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.IpAddress.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.IpAddress>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.Configuration GetOneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Configuration>();
			return (Bam.Net.CoreServices.Data.Configuration)Bam.Net.CoreServices.Data.Daos.Configuration.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.Configuration OneConfigurationWhere(WhereDelegate<ConfigurationColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Configuration>();
            return (Bam.Net.CoreServices.Data.Configuration)Bam.Net.CoreServices.Data.Daos.Configuration.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.Configuration> TopConfigurationsWhere(int count, WhereDelegate<ConfigurationColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Configuration>(Bam.Net.CoreServices.Data.Daos.Configuration.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.Configuration> ConfigurationsWhere(WhereDelegate<ConfigurationColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.Configuration>(where(new ConfigurationColumns()));
        }
	    
		public long CountConfigurations()
        {
            return Bam.Net.CoreServices.Data.Daos.Configuration.Count(Database);
        }

        public long CountConfigurationsWhere(WhereDelegate<ConfigurationColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.Configuration.Count(where, Database);
        }
        
        public async Task BatchQueryConfigurations(int batchSize, WhereDelegate<ConfigurationColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Configuration.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Configuration>(batch));
            }, Database);
        }

		
        public async Task BatchAllConfigurations(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Configuration>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Configuration.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Configuration>(batch));
            }, Database);
        }﻿		
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
		public Bam.Net.CoreServices.Data.ClientServerConnection GetOneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ClientServerConnection>();
			return (Bam.Net.CoreServices.Data.ClientServerConnection)Bam.Net.CoreServices.Data.Daos.ClientServerConnection.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.ClientServerConnection OneClientServerConnectionWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ClientServerConnection>();
            return (Bam.Net.CoreServices.Data.ClientServerConnection)Bam.Net.CoreServices.Data.Daos.ClientServerConnection.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection> TopClientServerConnectionsWhere(int count, WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection> ClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.ClientServerConnection>(where(new ClientServerConnectionColumns()));
        }
	    
		public long CountClientServerConnections()
        {
            return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Count(Database);
        }

        public long CountClientServerConnectionsWhere(WhereDelegate<ClientServerConnectionColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ClientServerConnection.Count(where, Database);
        }
        
        public async Task BatchQueryClientServerConnections(int batchSize, WhereDelegate<ClientServerConnectionColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ClientServerConnection.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(batch));
            }, Database);
        }

		
        public async Task BatchAllClientServerConnections(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ClientServerConnection>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ClientServerConnection.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ClientServerConnection>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor GetOneExternalEventSubscriptionDescriptorWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();
			return (Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor)Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor OneExternalEventSubscriptionDescriptorWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>();
            return (Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor)Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor> TopExternalEventSubscriptionDescriptorsWhere(int count, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor> ExternalEventSubscriptionDescriptorsWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(where(new ExternalEventSubscriptionDescriptorColumns()));
        }
	    
		public long CountExternalEventSubscriptionDescriptors()
        {
            return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Count(Database);
        }

        public long CountExternalEventSubscriptionDescriptorsWhere(WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryExternalEventSubscriptionDescriptors(int batchSize, WhereDelegate<ExternalEventSubscriptionDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(batch));
            }, Database);
        }

		
        public async Task BatchAllExternalEventSubscriptionDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ExternalEventSubscriptionDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ExternalEventSubscriptionDescriptor>(batch));
            }, Database);
        }﻿		
		public Bam.Net.CoreServices.Data.Machine GetOneMachineWhere(WhereDelegate<MachineColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Machine>();
			return (Bam.Net.CoreServices.Data.Machine)Bam.Net.CoreServices.Data.Daos.Machine.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.Machine OneMachineWhere(WhereDelegate<MachineColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.Machine>();
            return (Bam.Net.CoreServices.Data.Machine)Bam.Net.CoreServices.Data.Daos.Machine.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.Machine> TopMachinesWhere(int count, WhereDelegate<MachineColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.Machine>(Bam.Net.CoreServices.Data.Daos.Machine.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.Machine> MachinesWhere(WhereDelegate<MachineColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.Machine>(where(new MachineColumns()));
        }
	    
		public long CountMachines()
        {
            return Bam.Net.CoreServices.Data.Daos.Machine.Count(Database);
        }

        public long CountMachinesWhere(WhereDelegate<MachineColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.Machine.Count(where, Database);
        }
        
        public async Task BatchQueryMachines(int batchSize, WhereDelegate<MachineColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Machine.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Machine>(batch));
            }, Database);
        }

		
        public async Task BatchAllMachines(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.Machine>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.Machine.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.Machine>(batch));
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
        }﻿		
		public Bam.Net.CoreServices.Data.ProcessDescriptor GetOneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
		{
			Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ProcessDescriptor>();
			return (Bam.Net.CoreServices.Data.ProcessDescriptor)Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.GetOneWhere(where, Database).CopyAs(wrapperType, this);
		}

		public Bam.Net.CoreServices.Data.ProcessDescriptor OneProcessDescriptorWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            Type wrapperType = GetWrapperType<Bam.Net.CoreServices.Data.ProcessDescriptor>();
            return (Bam.Net.CoreServices.Data.ProcessDescriptor)Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.OneWhere(where, Database).CopyAs(wrapperType, this);
        }

		public IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor> TopProcessDescriptorsWhere(int count, WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Top(count, where, Database));
        }
		
        public IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor> ProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Query<Bam.Net.CoreServices.Data.ProcessDescriptor>(where(new ProcessDescriptorColumns()));
        }
	    
		public long CountProcessDescriptors()
        {
            return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Count(Database);
        }

        public long CountProcessDescriptorsWhere(WhereDelegate<ProcessDescriptorColumns> where)
        {
            return Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.Count(where, Database);
        }
        
        public async Task BatchQueryProcessDescriptors(int batchSize, WhereDelegate<ProcessDescriptorColumns> where, Action<IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.BatchQuery(batchSize, where, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(batch));
            }, Database);
        }

		
        public async Task BatchAllProcessDescriptors(int batchSize, Action<IEnumerable<Bam.Net.CoreServices.Data.ProcessDescriptor>> batchProcessor)
        {
            await Bam.Net.CoreServices.Data.Daos.ProcessDescriptor.BatchAll(batchSize, (batch) =>
            {
				batchProcessor(Wrap<Bam.Net.CoreServices.Data.ProcessDescriptor>(batch));
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
        }
	}
}																								

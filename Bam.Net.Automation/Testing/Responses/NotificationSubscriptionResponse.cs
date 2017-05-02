/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing.Repository.Data;

namespace Bam.Net.Testing.Repository
{
	public class NotificationSubscriptionResponse: ServiceResponse
    {
		public string Uuid { get; set; }
		public SubscriptionStatus SubscriptionStatus { get; set; }
	}
}

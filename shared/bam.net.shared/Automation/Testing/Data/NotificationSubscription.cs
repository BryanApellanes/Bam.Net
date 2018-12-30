/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Automation.Testing.Data
{
	public class NotificationSubscription: RepoData
	{
		public string EmailAddress { get; set; }
		public bool IsActive { get; set; }
	}
}

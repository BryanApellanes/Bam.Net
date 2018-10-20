/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Logging
{
	public class ObjectRepositoryLogger: RepositoryLogger
	{
		public ObjectRepositoryLogger(ObjectRepository repo) : base(repo) 
		{
			Args.ThrowIfNull(repo);			
		}

		public ObjectRepository ObjectRepository
		{
			get
			{
				return (ObjectRepository)Repository;
			}
		}
	}
}

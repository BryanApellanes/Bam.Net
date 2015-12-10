/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// An abstract base class defining common
	/// properties for any object you may wish to 
	/// save in a Repository
	/// </summary>
	public abstract class RepoData
	{
		public long Id { get; set; }
		public string Uuid { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }
	}
}

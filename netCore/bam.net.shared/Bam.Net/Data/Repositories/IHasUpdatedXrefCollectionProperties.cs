/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// Used internally by Generated Poco's
	/// </summary>
	public interface IHasUpdatedXrefCollectionProperties
	{
		Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }
	}
}

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
	/// Specifies that a property should be used as the 
	/// primary key for an object
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyAttribute: Attribute
	{
	}
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// Defines an event named GenerateDaoAssemblySucceeded
    /// </summary>
	public interface IGeneratesDaoAssembly
	{
		event EventHandler GenerateDaoAssemblySucceeded;
	}
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Bam.Net.Logging
{
	public interface IDaoLogger: ILogger
	{
		Bam.Net.Data.Database Database { get; set; }
	}
}

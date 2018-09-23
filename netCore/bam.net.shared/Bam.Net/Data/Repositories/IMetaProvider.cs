/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Bam.Net.Data.Repositories
{
	public interface IMetaProvider
	{
		Meta GetMeta(object data);
	}
}

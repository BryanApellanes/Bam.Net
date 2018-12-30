/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	public class MetaProvider : Bam.Net.Data.Repositories.IMetaProvider
	{
		public MetaProvider(IObjectPersister objectPersister)
		{
			ObjectPersister = objectPersister;
		}
		
		static IMetaProvider _default;
		static object _defaultLock = new object();
		public static IMetaProvider Default
		{
			get
			{
				return _defaultLock.DoubleCheckLock(ref _default, () => new MetaProvider(ServiceRegistry.Default.Get<IObjectPersister>()));
			}
		}

		public IObjectPersister ObjectPersister { get; set; }

		public virtual Meta GetMeta(object data)
		{
			return new Meta(data, ObjectPersister);
		}
	}
}

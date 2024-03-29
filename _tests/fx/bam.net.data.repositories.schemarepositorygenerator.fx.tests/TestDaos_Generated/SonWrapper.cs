using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Newtonsoft.Json;
using Bam.Net.Data.Repositories.Tests.ClrTypes;
using Bam.Net.Data.Repositories.Tests.ClrTypes.Daos;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Wrappers
{
	// generated
	[Serializable]
	public class SonWrapper: Bam.Net.Data.Repositories.Tests.ClrTypes.Son, IHasUpdatedXrefCollectionProperties
	{
		public SonWrapper(DaoRepository repository)
		{
			this.Repository = repository;
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(!UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}


Bam.Net.Data.Repositories.Tests.ClrTypes.Parent _parent;
		public override Bam.Net.Data.Repositories.Tests.ClrTypes.Parent Parent
		{
			get
			{
				if (_parent == null)
				{
					_parent = (Bam.Net.Data.Repositories.Tests.ClrTypes.Parent)Repository.GetParentPropertyOfChild(this, typeof(Bam.Net.Data.Repositories.Tests.ClrTypes.Parent));
				}
				return _parent;
			}
			set
			{
				_parent = value;
			}
		}

	}
	// -- generated
}																								

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

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
	// generated
	[Serializable]
	public class HousePoco: Bam.Net.Data.Repositories.Tests.ClrTypes.House, IHasUpdatedXrefCollectionProperties
	{
		public HousePoco(DaoRepository repository)
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



// Xref property: Left -> House ; Right -> Parent

		List<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> _parents;
		public override List<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent> Parents
		{
			get
			{
				if(_parents == null)
				{
					 var xref = new XrefDaoCollection<HouseDaoParentDao, ParentDao>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _parents = ((IEnumerable)xref).CopyAs<Bam.Net.Data.Repositories.Tests.ClrTypes.Parent>().ToList();
					 SetUpdatedXrefCollectionProperty("ParentDaos", this.GetType().GetProperty("Parents"));
				}

				return _parents;
			}
			set
			{
				_parents = value;
				SetUpdatedXrefCollectionProperty("ParentDaos", this.GetType().GetProperty("Parents"));
			}
		}
	}
	// -- generated
}																								

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
	public class ParentPoco: Bam.Net.Data.Repositories.Tests.ClrTypes.Parent, IHasUpdatedXrefCollectionProperties
	{
		public ParentPoco(DaoRepository repository)
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

Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter[] _daughters;
		public override Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter[] Daughters
		{
			get
			{
				if (_daughters == null)
				{
					_daughters = Repository.ForeignKeyCollectionLoader<Bam.Net.Data.Repositories.Tests.ClrTypes.Daughter>(this).ToArray();
				}
				return _daughters;
			}
			set
			{
				_daughters = value;
			}
		}System.Collections.Generic.List<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> _sons;
		public override System.Collections.Generic.List<Bam.Net.Data.Repositories.Tests.ClrTypes.Son> Sons
		{
			get
			{
				if (_sons == null)
				{
					_sons = Repository.ForeignKeyCollectionLoader<Bam.Net.Data.Repositories.Tests.ClrTypes.Son>(this).ToList();
				}
				return _sons;
			}
			set
			{
				_sons = value;
			}
		}


// Xref property: Left -> House ; Right -> Parent

		Bam.Net.Data.Repositories.Tests.ClrTypes.House[] _houses;
		public override Bam.Net.Data.Repositories.Tests.ClrTypes.House[] Houses
		{
			get
			{
				if(_houses == null)
				{
					 var xref = new XrefDaoCollection<HouseDaoParentDao, HouseDao>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _houses = ((IEnumerable)xref).CopyAs<Bam.Net.Data.Repositories.Tests.ClrTypes.House>().ToArray();
					 SetUpdatedXrefCollectionProperty("HouseDaos", this.GetType().GetProperty("Houses"));
				}

				return _houses;
			}
			set
			{
				_houses = value;
				SetUpdatedXrefCollectionProperty("HouseDaos", this.GetType().GetProperty("Houses"));
			}
		}	}
	// -- generated
}																								

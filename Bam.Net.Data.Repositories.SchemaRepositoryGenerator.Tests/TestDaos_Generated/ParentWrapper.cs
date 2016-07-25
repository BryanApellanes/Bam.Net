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
	public class ParentWrapper: Bam.Net.Data.Repositories.Tests.ClrTypes.Parent, IHasUpdatedXrefCollectionProperties
	{
		public ParentWrapper(DaoRepository repository)
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
					 var xref = new XrefDaoCollection<Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.HouseParent, Bam.Net.Data.Repositories.Tests.ClrTypes.Daos.House>(Repository.GetDaoInstance(this), false);
					 xref.Load(Repository.Database);
					 _houses = ((IEnumerable)xref).CopyAs<Bam.Net.Data.Repositories.Tests.ClrTypes.House>().ToArray();
					 SetUpdatedXrefCollectionProperty("Houses", this.GetType().GetProperty("Houses"));
				}

				return _houses;
			}
			set
			{
				_houses = value;
				SetUpdatedXrefCollectionProperty("Houses", this.GetType().GetProperty("Houses"));
			}
		}	}
	// -- generated
}																								

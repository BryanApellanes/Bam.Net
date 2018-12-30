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
using Bam.Net.Logging.Http.Data;
using Bam.Net.Logging.Http.Data.Dao;

namespace Bam.Net.Logging.Http.Data.Wrappers
{
	// generated
	[Serializable]
	public class RequestDataWrapper: Bam.Net.Logging.Http.Data.RequestData, IHasUpdatedXrefCollectionProperties
	{
		public RequestDataWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public RequestDataWrapper(DaoRepository repository) : this()
		{
			this.Repository = repository;
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		[JsonIgnore]
		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(UpdatedXrefCollectionProperties != null && !UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else if(UpdatedXrefCollectionProperties != null)
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}

System.Collections.Generic.List<Bam.Net.Logging.Http.Data.CookieData> _cookies;
		public override System.Collections.Generic.List<Bam.Net.Logging.Http.Data.CookieData> Cookies
		{
			get
			{
				if (_cookies == null)
				{
					_cookies = Repository.ForeignKeyCollectionLoader<Bam.Net.Logging.Http.Data.RequestData, Bam.Net.Logging.Http.Data.CookieData>(this).ToList();
				}
				return _cookies;
			}
			set
			{
				_cookies = value;
			}
		}System.Collections.Generic.List<Bam.Net.Logging.Http.Data.HeaderData> _headers;
		public override System.Collections.Generic.List<Bam.Net.Logging.Http.Data.HeaderData> Headers
		{
			get
			{
				if (_headers == null)
				{
					_headers = Repository.ForeignKeyCollectionLoader<Bam.Net.Logging.Http.Data.RequestData, Bam.Net.Logging.Http.Data.HeaderData>(this).ToList();
				}
				return _headers;
			}
			set
			{
				_headers = value;
			}
		}


	}
	// -- generated
}																								

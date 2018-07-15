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
using Bam.Net.CoreServices.WebHooks.Data;
using Bam.Net.CoreServices.WebHooks.Data.Dao;

namespace Bam.Net.CoreServices.WebHooks.Data.Wrappers
{
	// generated
	[Serializable]
	public class WebHookDescriptorWrapper: Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public WebHookDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public WebHookDescriptorWrapper(DaoRepository repository) : this()
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

System.Collections.Generic.List<Bam.Net.CoreServices.WebHooks.Data.WebHookCall> _calls;
		public override System.Collections.Generic.List<Bam.Net.CoreServices.WebHooks.Data.WebHookCall> Calls
		{
			get
			{
				if (_calls == null)
				{
					_calls = Repository.ForeignKeyCollectionLoader<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor, Bam.Net.CoreServices.WebHooks.Data.WebHookCall>(this).ToList();
				}
				return _calls;
			}
			set
			{
				_calls = value;
			}
		}

// Xref property: Left -> WebHookDescriptor ; Right -> WebHookSubscriber

		List<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber> _webHookSubscribers;
		public override List<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber> Subscribers
		{
			get
			{
				if(_webHookSubscribers == null || _webHookSubscribers.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber,  Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookSubscriber>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_webHookSubscribers = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber>().ToList();
					SetUpdatedXrefCollectionProperty("WebHookSubscribers", this.GetType().GetProperty("Subscribers"));					
				}

				return _webHookSubscribers;
			}
			set
			{
				_webHookSubscribers = value;
				SetUpdatedXrefCollectionProperty("WebHookSubscribers", this.GetType().GetProperty("Subscribers"));
			}
		}
	}
	// -- generated
}																								

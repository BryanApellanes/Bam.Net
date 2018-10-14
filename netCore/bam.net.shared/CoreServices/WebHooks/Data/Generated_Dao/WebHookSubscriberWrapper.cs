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
	public class WebHookSubscriberWrapper: Bam.Net.CoreServices.WebHooks.Data.WebHookSubscriber, IHasUpdatedXrefCollectionProperties
	{
		public WebHookSubscriberWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public WebHookSubscriberWrapper(DaoRepository repository) : this()
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




// Xref property: Left -> WebHookDescriptor ; Right -> WebHookSubscriber

		List<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor> _webHookDescriptors;
		public override List<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor> Descriptors
		{
			get
			{
				if(_webHookDescriptors == null || _webHookDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptorWebHookSubscriber, Bam.Net.CoreServices.WebHooks.Data.Dao.WebHookDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_webHookDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.WebHooks.Data.WebHookDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("WebHookDescriptors", this.GetType().GetProperty("Descriptors"));					
				}

				return _webHookDescriptors;
			}
			set
			{
				_webHookDescriptors = value;
				SetUpdatedXrefCollectionProperty("WebHookDescriptors", this.GetType().GetProperty("Descriptors"));
			}
		}	}
	// -- generated
}																								

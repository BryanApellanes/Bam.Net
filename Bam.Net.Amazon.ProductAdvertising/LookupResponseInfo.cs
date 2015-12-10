/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.ProductAdvertising;
using Bam.Net.Data;

namespace Bam.Net.Amazon.ProductAdvertising
{
    public class LookupResponseInfo
    {
        internal LookupResponseInfo(Items info)
        {
            SetInfo(info);
        }

        internal LookupResponseInfo(ItemLookupResponse response)
            : this(response.Items[0])
        {
        }

        internal void SetInfo(ItemLookupResponse response)
        {
            SetInfo(response.Items[0]);
        }

        internal void SetInfo(Items info)
        {
            this.EngineQuery = info.EngineQuery;
            
            this.Items = info;
            this.Request = info.Request.ItemLookupRequest;
            if (info.Request != null &&
                info.Request.Errors != null)
            {
                this.Errors = info.Request.Errors[0];
            }
        }

        internal ItemLookupRequest Request
        {
            get;
            set;
        }

        internal Items Items { get; set; }

        public ErrorsError Errors
        {
            get;
            private set;
        }

        public string EngineQuery { get; set; }
        public int TotalResults { get { return Items.Item.Length; } }
    }
}

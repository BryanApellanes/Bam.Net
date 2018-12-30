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
    public class SearchResponseInfo
    {
        internal SearchResponseInfo(Items info)
        {
            SetInfo(info);
        }

        internal SearchResponseInfo(ItemSearchResponse response)
            : this(response.Items[0])
        {
        }

        internal void SetInfo(ItemSearchResponse response)
        {
            SetInfo(response.Items[0]);
        }

        internal void SetInfo(Items info)
        {
            this.EngineQuery = info.EngineQuery;
            int totalPages = 0;
            int totalResults = 0;

            int.TryParse(info.TotalPages, out totalPages);
            int.TryParse(info.TotalResults, out totalResults);
            this.TotalPages = totalPages;
            this.TotalResults = totalResults;
            this.Items = info;
            this.Request = info.Request.ItemSearchRequest;
            if (info.Request != null &&
                info.Request.Errors != null)
            {
                this.Errors = info.Request.Errors[0];
            }
        }

        internal ItemSearchRequest Request
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
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public int CurrentPage
        {
            get
            {
                return Convert.ToInt32(Items.Request.ItemSearchRequest.ItemPage);
            }
            set
            {
                if (value > TotalPages)
                {
                    throw new InvalidOperationException("Page out of range");
                }

                Items.Request.ItemSearchRequest.ItemPage = value.ToString();
            }
        }

        public string Qid { get; set; }
    }
}

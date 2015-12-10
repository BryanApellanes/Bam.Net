/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.ProductAdvertising;

namespace Bam.Net.Amazon.ProductAdvertising
{
    /// <summary>
    /// Simplifies reading ItemSearchResponse objects
    /// </summary>
    public class SearchResults
    {
        public SearchResults(ItemSearchResponse response)
        {
            this.Info = new SearchResponseInfo(response.Items[0]);
            SetItems();
        }

        private void SetItems()
        {
            List<AmazonItem> items = new List<AmazonItem>();
            if (this.Info.Items.Item != null)
            {
                foreach (AmazonItem item in this.Info.Items.Item)
                {
                    items.Add(item);
                }
            }
            this.Items = items.ToArray();
        }

        public static implicit operator SearchResults(ItemSearchResponse response)
        {
            return new SearchResults(response);
        }
        
        public SearchResponseInfo Info
        {
            get;
            private set;
        }

        public AmazonItem[] Items
        {
            get;
            private set;
        }

        public bool Next(out SearchResults nextPage)
        {
            return AmazonItem.NextPage(this, out nextPage);
        }
    }
}

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
    /// Simplifies reading ItemLookupResponse objects
    /// </summary>
    public class LookupResults
    {
        public LookupResults(ItemLookupResponse response)
        {
            this.Info = new LookupResponseInfo(response.Items[0]);
            SetItems();
        }

        private void SetItems()
        {
            List<AmazonItem> items = new List<AmazonItem>();
            foreach (AmazonItem item in this.Info.Items.Item)
            {
                items.Add(item);
            }
            this.Items = items.ToArray();
        }

        public static implicit operator LookupResults(ItemLookupResponse response)
        {
            return new LookupResults(response);
        }

        public LookupResponseInfo Info
        {
            get;
            private set;
        }

        public AmazonItem[] Items
        {
            get;
            private set;
        }

    }
}

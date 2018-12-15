/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.ProductAdvertising;
using Bam.Net.Configuration;

namespace Bam.Net.Amazon.ProductAdvertising
{
    public class AmazonProductAdvertisingClient: IHasRequiredProperties
    {
        const string DESTINATION = "https://ecs.amazonaws.com/onca/soap?Service=AWSECommerceService";

        public AmazonProductAdvertisingClient()
            : this("hatagi.co-20")
        {
        }

        public AmazonProductAdvertisingClient(string associateTag)
        {
            this.AssociateTag = associateTag;
            DefaultConfiguration.SetProperties(this, true);
        }

        public string AssociateTag
        {
            get;
            private set;
        }

        #region public
        public ItemSearchResponse KeywordSearch(string keywords, SearchIndex searchIndex = SearchIndex.VideoGames, int pageNumber = 1)
        {
            ItemSearchRequest searchRequest = CreateItemSearchRequest(searchIndex);
            searchRequest.ItemPage = pageNumber.ToString();
            searchRequest.Keywords = keywords;

            ItemSearchResponse response = GetSearchResponse(searchRequest);

            return response;
        }

        public ItemSearchResponse Search(ItemSearchRequest request)
        {
            return GetSearchResponse(request);
        }

        public ItemLookupResponse Lookup(params string[] asins)
        {
            ItemLookupRequest lookupRequest = CreateItemLookupRequest();
            lookupRequest.ItemId = asins;

            ItemLookupResponse response = Lookup(lookupRequest);

            return response;
        }

        public ItemLookupResponse Lookup(ItemLookupRequest lookupRequest)
        {
            ItemLookup lookup = CreateItemLookup(lookupRequest);
            AWSECommerceService api = GetService();
            ItemLookupResponse response = api.ItemLookup(lookup);

            return response;
        }

        public ItemSearchResponse GetSearchResponse(ItemSearchRequest searchRequest)
        {
            ItemSearch search = CreateItemSearch(searchRequest);

            AWSECommerceService api = GetService();
            ItemSearchResponse response = api.ItemSearch(search);
            
            return response;
        }
        
        public void GetSearchResponseAsync(ItemSearchRequest searchRequest, ItemSearchCompletedEventHandler callback)
        {
            ItemSearch search = CreateItemSearch(searchRequest);

            AWSECommerceService api = GetService();
            api.ItemSearchCompleted += callback;
            api.ItemSearchAsync(search);
        }
        
        private ItemSearch CreateItemSearch(ItemSearchRequest searchRequest)
        {
            ItemSearch search = new ItemSearch();
            search.AssociateTag = AssociateTag;
            search.Request = new ItemSearchRequest[] { searchRequest };
            return search;
        }

        private ItemLookup CreateItemLookup(ItemLookupRequest lookupRequest)
        {
            ItemLookup lookup = new ItemLookup();
            lookup.AssociateTag = AssociateTag;
            lookup.Request = new ItemLookupRequest[] { lookupRequest };
            return lookup;
        }

        #endregion

        static AmazonProductAdvertisingClient _client;
        public static AmazonProductAdvertisingClient Current
        {
            get
            {
                if (_client == null)
                {
                    _client = new AmazonProductAdvertisingClient();
                }

                return _client;
            }
        }

        protected AWSECommerceService GetService()
        {
            ValidateAmazonSecurity();
            AWSECommerceService api = new AWSECommerceService();
            api.Destination = new Uri(DESTINATION);
            AmazonHmacAssertion amazonHmacAssertion = new AmazonHmacAssertion(AWSKeyID, AWSSecretKey);
            api.SetPolicy(amazonHmacAssertion.Policy());
            return api;
        }
                
        /// <summary>
        /// Create the ItemSearchRequest not to be confused with the ItemSearch itself.
        /// </summary>
        /// <param name="searchIndex"></param>
        /// <returns></returns>
        protected internal ItemSearchRequest CreateItemSearchRequest(SearchIndex searchIndex)
        {
            return CreateItemSearchRequest(searchIndex.ToString());
        }

        /// <summary>
        /// Create the ItemSearchRequest not to be confused with the ItemSearch itself.
        /// </summary>
        /// <param name="searchIndex"></param>
        /// <returns></returns>
        protected static ItemSearchRequest CreateItemSearchRequest(string searchIndex)
        {
            ItemSearchRequest request = new ItemSearchRequest();
            request.SearchIndex = searchIndex;
            request.ResponseGroup = new string[] { "ItemAttributes", "Images" };
            return request;
        }

        protected internal ItemLookupRequest CreateItemLookupRequest()
        {
            return CreateItemLookupRequest(ItemLookupRequestIdType.ASIN, Condition.All);
        }

        protected static ItemLookupRequest CreateItemLookupRequest(ItemLookupRequestIdType idType = ItemLookupRequestIdType.ASIN, Condition condition = Condition.All)
        {
            ItemLookupRequest request = new ItemLookupRequest();
            request.IdType = idType;
            request.Condition = condition;
            request.ResponseGroup = new string[] { "ItemAttributes", "Images" };
            return request;
        }

        protected void ValidateAmazonSecurity()
        {
            if (string.IsNullOrEmpty(AWSKeyID))
            {
                throw new InvalidOperationException("AWSKeyID not set");
            }

            if (string.IsNullOrEmpty(AWSSecretKey))
            {
                throw new InvalidOperationException("AWSSecretKey not set");
            }
        }

        public string AWSKeyID
        {
            get;
            set;
        }

        public string AWSSecretKey
        {
            get;
            set;
        }
        
        public string[] RequiredProperties
        {
            get { return new string[] { "AWSKeyID", "AWSSecretKey" }; }
        }

    }
}

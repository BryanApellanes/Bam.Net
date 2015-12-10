/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Hatagi;
//using BA = Bam.Net.Hatagi.Data;
using APA = Amazon.ProductAdvertising;
using Bam.Net.Data;
using Bam.Net;
using System.Threading.Tasks;

namespace Bam.Net.Amazon.ProductAdvertising
{
    public class AmazonItem//: BA.IItem
    {
        public AmazonItem(APA.Item item)
        {
            this.Item = item;
        }

        public APA.Item Item
        {
            get;
            private set;
        }

        public static implicit operator AmazonItem(APA.Item item)
        {
            return new AmazonItem(item);
        }

        //public static implicit operator BA.Item(AmazonItem item)
        //{
        //    return item.Save();
        //}

        public static AmazonItem Lookup(string asin)
        {
            LookupResults results = Lookup(new string[] { asin });
            if (results.Items.Length > 0)
            {
                return results.Items[0];
            }
            else
            {
                return null;
            }
        }

        public static LookupResults Lookup(params string[] asins)
        {
            AmazonProductAdvertisingClient client = new AmazonProductAdvertisingClient();
            return client.Lookup(asins);
        }

        public static SearchResults Search(string keywords, SearchIndex index = SearchIndex.VideoGames, int pageNumber = 1)
        {
            AmazonProductAdvertisingClient client = new AmazonProductAdvertisingClient();
            return client.KeywordSearch(keywords, index, pageNumber);
        }
        
        /// <summary>
        /// Returns true if there is another page of results and assigns
        /// the next page of SearchResults to the specified nextResults
        /// </summary>
        /// <param name="results"></param>
        /// <param name="nextResults"></param>
        /// <returns></returns>
        public static bool NextPage(SearchResults results, out SearchResults nextResults)
        {
            AmazonProductAdvertisingClient client = new AmazonProductAdvertisingClient();
            string itemPage = results.Info.Request.ItemPage;
            string totalPagesS = results.Info.Items.TotalPages;

            int currentPage = Convert.ToInt32(itemPage);
            currentPage = currentPage == 0 ? 1 : currentPage;
            
            int totalPages = Convert.ToInt32(totalPagesS);
            int nextPage = currentPage + 1;

            results.Info.Request.ItemPage = nextPage.ToString();            

            if (nextPage > 10)
            {
                nextResults = results;
                return false;
            }
            else
            {
                nextResults = client.Search(results.Info.Request);
                return nextPage <= totalPages;
            }
        }



        //public BA.Item Save()
        //{
        //    BA.Item item = BA.Item.WhereAsinEquals(ASIN);
        //    if (item == null)
        //    {
        //        item = new BA.Item();
        //        item.Name = Name;
        //        item.Save();
        //        item.SetItemProperty("ASIN", ASIN);
        //        Task setProperties = new Task(() =>
        //        {
        //            item.SetItemProperty("DetailPage", DetailPage);
        //            item.SetItemProperty("SmallImageURL", SmallImageURL);
        //            item.SetItemProperty("MediumImageURL", MediumImageURL);
        //            item.SetItemProperty("LargeImageURL", LargeImageURL);
        //            item.SetItemProperty("Genre", Genre);
        //            item.SetItemProperty("ESRBRating", ESRBRating);
        //            item.SetItemProperty("Platform", SubTitle);
        //            item.SetItemProperty("ReleaseDate", ReleaseDate);
        //        });
        //        setProperties.Start(TaskScheduler.Default);
        //    }

        //    return item;
        //}

        //#region IItem Members

        public string ASIN
        {
            get { return this.Item.ASIN; }
        }

        //public string Description
        //{
        //    get;
        //    set;
        //}

        //public string DetailPage
        //{
        //    get { return this.Item.DetailPageURL; }
        //}

        //BA.Item item;
        //public long? Id
        //{
        //    get
        //    {
        //        if (item == null)
        //        {
        //            item = BA.Item.WhereAsinEquals(ASIN);
        //        }

        //        long? id = -1;
        //        if (item != null)
        //        {
        //            id = item.Id;
        //        }

        //        return id;
        //    }
        //    set
        //    {
        //        item = BA.Item.OneWhere(c => c.Id == value);
        //    }
        //}

        //public string LargeImageURL
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.LargeImage != null)
        //        {
        //            return Item.LargeImage.URL;
        //        }
        //        return string.Empty;
        //    }
        //}

        //public string MediumImageURL
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.MediumImage != null)
        //        {
        //            return Item.MediumImage.URL;
        //        }

        //        return string.Empty;
        //    }
        //}

        public string Name
        {
            get
            {
                if (Item != null &&
                    Item.ItemAttributes != null)
                {
                    return Item.ItemAttributes.Title;
                }
                return string.Empty;
            }
            set
            {
                if (Item != null &&
                    Item.ItemAttributes != null)
                {
                    Item.ItemAttributes.Title = value;
                }
            }
        }

        //public string SmallImageURL
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.SmallImage != null)
        //        {
        //            return Item.SmallImage.URL;
        //        }
        //        return string.Empty;
        //    }
        //}

        //public string SubTitle
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.ItemAttributes != null &&
        //            Item.ItemAttributes.Platform != null &&
        //            Item.ItemAttributes.Platform.Length > 0)
        //        {
        //            return Item.ItemAttributes.Platform[0];
        //        }

        //        return string.Empty;
        //    }
        //}

        //public string Genre
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.ItemAttributes != null)
        //        {
        //            return Item.ItemAttributes.Genre;
        //        }

        //        return string.Empty;
        //    }
        //}

        //public string ESRBRating
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.ItemAttributes != null)
        //        {
        //            return Item.ItemAttributes.ESRBAgeRating;
        //        }

        //        return string.Empty;
        //    }
        //}

        //public string ReleaseDate
        //{
        //    get
        //    {
        //        if (Item != null &&
        //            Item.ItemAttributes != null)
        //        {
        //            return Item.ItemAttributes.ReleaseDate;
        //        }
        //        return string.Empty;
        //    }
        //}
        //#endregion
    }
}

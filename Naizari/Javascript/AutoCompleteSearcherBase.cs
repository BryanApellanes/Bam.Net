/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using Naizari.Logging;

namespace Naizari.Javascript
{
    public abstract class AutoCompleteSearcherBase: IAutoCompleteSearcher
    {
        #region IAutoCompleteSearcher Members

        public static string[] StaticWhereFilters
        {
            get;
            set;
        }

        public abstract IAutoCompleteSearcher Get();

        [JsonMethod]
        public AutoCompleteItem[] Search(string input)
        {
            input = HttpUtility.UrlDecode(input);
            AutoCompleteItem[] returnValues = null;
            if (HttpContext.Current != null)
            {
                HttpRequest request = HttpContext.Current.Request;
                string key = request.Url.OriginalString;
                Cache c = HttpContext.Current.Cache;

                if (c[key] == null)
                {
                    returnValues = DoSearch(input);
                    c.Add(key, returnValues, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Default, new CacheItemRemovedCallback(SearchCacheItemRemoved));
                }
                else
                {
                    returnValues = (AutoCompleteItem[])c[key];
                }
            }
            else
            {
                returnValues = DoSearch(input);
            }

            return returnValues;
        }

        public void SearchCacheItemRemoved(string key, object value, CacheItemRemovedReason reason)
        {
            LogManager.CurrentLog.AddEntry("Item removed from web cache:\r\n KEY: {0}\r\nREASON: {1}", LogEventType.Information, key, reason.ToString());
        }

        public abstract AutoCompleteItem[] DoSearch(string input);
        #endregion

        protected static string GetStaticWhereClauses()
        {
            string staticWhereClauses = " (";
            if (StaticWhereFilters != null)
            {
                for (int i = 0; i < StaticWhereFilters.Length; i++)
                {
                    staticWhereClauses += string.Format(" {0} ", StaticWhereFilters[i]);
                    if (i != StaticWhereFilters.Length - 1)
                        staticWhereClauses += " AND ";
                }
            }
            staticWhereClauses += ") AND ";

            if (StaticWhereFilters == null ||
                StaticWhereFilters.Length == 0)
            {
                staticWhereClauses = " ";
            }
            return staticWhereClauses;
        }

        public string NoResultsText { get; set; }
        public bool NoResultsSelectable { get; set; }
        public bool DisplayNoResults { get; set; }

        public virtual AutoCompleteItem[] NoResults()
        {
            if (DisplayNoResults)
            {
                string text = string.IsNullOrEmpty(NoResultsText) ? "No results found" : NoResultsText;
                List<AutoCompleteItem> items = new List<AutoCompleteItem>();
                string id = NoResultsSelectable ? "none" : "";
                AutoCompleteItem item = new AutoCompleteItem(id, text, true);
                items.Add(item);
                return items.ToArray();
            }
            else
            {
                return new AutoCompleteItem[] { };
            }
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    /// <summary>
    /// Convenience collection like object for 
    /// paging IEnumerables
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Book<T>
    {
        List<List<T>> allPages;
        List<T> allItems;

        public Book()
            : base()
        {
            allPages = new List<List<T>>();
            allItems = new List<T>();
        }

        /// <summary>
        /// Instantiate a new Book with the specified items, using
        /// a default PageSize of 10
        /// </summary>
        /// <param name="items"></param>
        public Book(IEnumerable<T> items)
            : this()
        {
            this.allItems = new List<T>(items);
            this.PageSize = 10;
        }

        public Book(IEnumerable<T> items, int pageSize)
            : this()
        {
            this.allItems = new List<T>(items);
            this.PageSize = pageSize;
        }

        int pageSize;
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                this.pageSize = value;
                this.Initialize();
            }
        }

        public int PageCount
        {
            get;
            set;
        }

        public int ItemCount
        {
            get
            {
                return this.allItems.Count;
            }
        }

        public void Add(T item)
        {
            this.allItems.Add(item);
            this.Initialize();
        }

        /// <summary>
        /// A list of lists representing the individual
        /// pages
        /// </summary>
        public List<List<T>> AllPages
        {
            get
            {
                return allPages;
            }
        }

        /// <summary>
        /// Retrieve the set of values on the specified
        /// zero based page number
        /// </summary>
        /// <param name="zeroBasedPageNumber"></param>
        /// <returns></returns>
        public List<T> this[int zeroBasedPageNumber]
        {
            get
            {
                List<T> page = allPages.ElementAtOrDefault(zeroBasedPageNumber);
                if (page == null)
                {
                    return new List<T>();
                }
                else
                {
                    return page;
                }
            }
        }

        /// <summary>
        /// Retrieve the set of values on the specified
        /// zero based page number
        /// </summary>
        /// <param name="zeroBasedPageNumber"></param>
        /// <returns></returns>
		public List<T> PageNumber(int zeroBasedPageNumber)
		{
			return this[zeroBasedPageNumber];
		}

        /// <summary>
        /// Convert the book to an array containing all items
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return this.allItems.ToArray();
        }

        private void Initialize()
        {
            this.Initialize(this.allItems);
        }

        private void Initialize(IEnumerable<T> items)
        {
            allPages.Clear();
            int itemCount = items.Count();
            int remainder = itemCount % pageSize;
            int pageCount = itemCount / pageSize;
            if (remainder > 0)
            {
                pageCount++;
            }

            PageCount = pageCount;
            int currentPageNum = 0;
            foreach (T item in items)
            {   
                if (allPages.ElementAtOrDefault(currentPageNum) == null)
                {
                    allPages.Add(new List<T>());
                }

                if (allPages[currentPageNum].Count >= pageSize)
                {
                    allPages.Add(new List<T>());
                    currentPageNum++;
                }
                
                allPages[currentPageNum].Add(item);
            }
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class Book<T>
    {
        List<List<T>> allPages;
        List<T> allItems;

        public Book()
        {
            allPages = new List<List<T>>();
        }

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

        private void Initialize()
        {
            this.Initialize(this.allItems);           
        }

        private void Initialize(IEnumerable<T> items)
        {
            int remainder = items.Count() % pageSize;
            int pageCount = items.Count() / pageSize;
            if (remainder > 0)
                pageCount++;

            PageCount = pageCount;
            foreach (T item in items)
            {
                int currentPage = allPages.Count - 1;
                if (currentPage < 0)
                {
                    currentPage = 0;
                }

                if (allPages.ElementAtOrDefault(currentPage) == null)
                {
                    allPages.Add(new List<T>());
                }

                if (allPages[currentPage].Count >= pageSize)
                {
                    allPages.Add(new List<T>());
                }

                currentPage = allPages.Count - 1;
                allPages[currentPage].Add(item);
            }
        }

        public List<T> this[int zeroBasedPageNumber]
        {
            get
            {
                List<T> page = allPages.ElementAtOrDefault(zeroBasedPageNumber);
                if (page == null)
                {
                    return new List<T>();//return allPages[0];
                }
                else
                {
                    return page;
                }
            }
        }

        public T[] ToArray()
        {
            return this.allItems.ToArray();
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public class DaoObjectCollection<T>: PagedEnumerator<T>, IEnumerable<T> where T: DaoObject, new()
    {
        DaoSearchFilter filter;
        Book<long> pagedIds;
        DatabaseAgent agent;
        OrderBy orderBy = OrderBy.None;
        // public so can be globally overriden if necessary
        public static int DefaultPageSize = 10;

        public DaoObjectCollection()
            : base()
        {
            this.filter = new DaoSearchFilter();
            this.pagedIds = new Book<long>(new List<long>(), DefaultPageSize);
            T proxy = new T();
            this.agent = DaoContext.Get(proxy.DataContextName).DatabaseAgent;
            GetNextPage();
        }

        public DaoObjectCollection(DaoSearchFilter filter)
            : base()
        {
            this.filter = filter;
            this.pagedIds = new Book<long>(new List<long>(), DefaultPageSize);
            T proxy = new T();
            this.agent = DaoContext.Get(proxy.DataContextName).DatabaseAgent;
            this.GetIds();
        }

        public DaoObjectCollection(DaoSearchFilter filter, int pageSize)
            : base()
        {
            this.filter = filter;
            this.PageSize = pageSize;
            this.pagedIds = new Book<long>(new List<long>(), pageSize);
            T proxy = new T();
            this.agent = DaoContext.Get(proxy.DataContextName).DatabaseAgent;
            this.GetIds();           
        }

        public DaoObjectCollection(DaoSearchFilter filter, int pageSize, DatabaseAgent agent)
            : base()
        {
            this.filter = filter;
            this.PageSize = pageSize;
            this.pagedIds = new Book<long>(new List<long>(), pageSize);
            
            this.agent = agent;
            this.GetIds();
        }

        public DaoObjectCollection(DaoSearchFilter filter, int pageSize, OrderBy orderBy, DatabaseAgent agent)
            : base()
        {
            this.orderBy = orderBy;
            this.filter = filter;
            this.PageSize = pageSize;
            this.pagedIds = new Book<long>(new List<long>(), pageSize);

            this.agent = agent;
            this.GetIds();
        }

        public DaoObjectCollection(List<long> ids, int pageSize, DatabaseAgent agent)
        {
            this.PageSize = pageSize;
            this.pagedIds = new Book<long>(ids, pageSize);
            this.agent = agent;
            GetNextPage();
        }

        public DaoObjectCollection(long[] ids, int pageSize, DatabaseAgent agent): this(new List<long>(ids), pageSize, agent)
        {
        }

        public static DaoObjectCollection<DaoType> LoadAll<DaoType>(int pageSize, DatabaseAgent agent) where DaoType : DaoObject, new()
        {
            DaoSearchFilter filter = new DaoSearchFilter();
            DaoType proxy = new DaoType();
            filter.AddParameter(proxy.IdColumnName, null, Comparison.NotEqualTo);
            return new DaoObjectCollection<DaoType>(filter, pageSize, agent);
        }

        public override bool MoveNextPage()
        {
            return GetNextPage();
        }

        public DatabaseAgent DatabaseAgent
        {
            get
            {
                return this.agent;
            }
            protected set
            {
                this.agent = value;
            }
        }

        public Book<long> PagedIds
        {
            get
            {
                return this.pagedIds;
            }
            protected set
            {
                this.pagedIds = value;
            }
        }

        public int PageCount
        {
            get
            {
                return this.pagedIds.PageCount;
            }
        }

        public int ItemCount
        {
            get
            {
                return this.pagedIds.ItemCount;
            }
        }

        private void GetIds()
        {
            T proxy = new T();
            this.pagedIds = new Book<long>(this.agent.SelectPropertyList<T, long>(proxy.IdColumnName, this.filter, this.orderBy), PageSize);
            GetNextPage();
        }

        private bool GetNextPage()
        {
            currentPageIndex++;
            if (currentPageIndex >= this.pagedIds.PageCount)
                return false;

            this.currentPage = new List<T>(this.agent.SelectByIdList<T>(this.pagedIds[currentPageIndex].ToArray(), this.orderBy));
            return true;
        }

        /// <summary>
        /// Get the page of items associated with the specified zero based page index as a
        /// List.
        /// </summary>
        /// <param name="zeroBasedPageIndex"></param>
        /// <returns></returns>
        public List<T> this[int zeroBasedPageIndex]
        {
            get
            {
                if (zeroBasedPageIndex < 0 || zeroBasedPageIndex > this.PageCount)
                {
                    throw new ArgumentException(string.Format("Number must be greater than -1 or less than {0}", this.PageCount));
                }

                return new List<T>(this.agent.SelectByIdList<T>(this.pagedIds[zeroBasedPageIndex].ToArray()));
            }
        }

        /// <summary>
        /// Gets the specified page of items as a list.  One based.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public List<T> GetPage(int pageNumber)
        {
            if (pageNumber > PageCount)
            {
                throw new ArgumentOutOfRangeException(string.Format("pageNumber must be greater than zero and less than PageCount ({0})", pageNumber));
            }

            return this[pageNumber - 1];
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
    }
}

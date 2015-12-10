/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Data
{
    public abstract class PagedEnumerator<T>: IEnumerator<T>
    {
        protected int currentItemIndex;
        protected int currentPageIndex;
        protected List<T> currentPage;
        protected int pageSize;

        public PagedEnumerator()
        {
            pageSize = 10;
            Reset();
        }

        public abstract bool MoveNextPage();

        public int PageSize
        {
            get { return this.pageSize; }
            set { this.pageSize = value; }
        }        

        #region IEnumerator<T> Members

        public T Current
        {
            get { return currentPage[currentItemIndex]; }
        }

        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            this.Reset();
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            currentItemIndex++;
            if (currentItemIndex >= currentPage.Count)
            {
                currentItemIndex = 0;
                return MoveNextPage();
            }
            else
            {
                return true;
            }            
        }

        public virtual void Reset()
        {
            currentItemIndex = -1;
            currentPageIndex = -1;
            currentPage = new List<T>();
            
        }

        #endregion
    }
}

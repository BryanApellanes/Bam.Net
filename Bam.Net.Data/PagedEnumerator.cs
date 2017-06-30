/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public abstract class PagedEnumerator<T> : IEnumerator<T>
    {
        protected int currentItemIndex;
        protected int currentPageIndex;
        protected List<T> currentPage;

        public PagedEnumerator()
        {
            Reset();
        }

        /// <summary>
        /// When implemented by a derived class should set the 
        /// CurrentPage property to the next page.
        /// </summary>
        /// <returns></returns>
        public abstract bool MoveNextPage();

        public List<T> CurrentPage
        {
            get
            {
                return this.currentPage;
            }
            protected set
            {
                this.currentPage = value;
            }
        }

        /// <summary>
        /// Represents the current index of the 
        /// current page.
        /// </summary>
        public int CurrentItemIndex
        {
            get
            {
                return this.currentItemIndex;
            }
            protected set
            {
                this.currentItemIndex = value;
            }
        }

        /// <summary>
        /// Represents the index of the current page.
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                return this.currentPageIndex;
            }
            protected set
            {
                this.currentPageIndex = value;
            }
        }
        
        #region IEnumerator<T> Members

        /// <summary>
        /// Returns the item of the current page at 
        /// the current item index.
        /// </summary>
        public T Current
        {
            get { return CurrentPage != null ? CurrentPage[CurrentItemIndex] : default(T); }
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

        /// <summary>
        /// Reset the current item and page back to the start
        /// </summary>
        public virtual void Reset()
        {
            currentItemIndex = -1;
            currentPageIndex = -1;
            currentPage = new List<T>();
        }
        #endregion
    }
}

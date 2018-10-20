/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bam.Net.Presentation.Html
{
    /// <summary>
    /// Exposes the State property of a given DeferredView.  Passed to content providers
    /// of deferred views to allow progress reporting.
    /// </summary>
    public class DeferredViewState
    {
        public DeferredViewState(DeferredView view, object model = null)
        {
            this.View = view;
            this.Model = model;
        }

        /// <summary>
        /// The value of the current state
        /// </summary>
        public MvcHtmlString Value
        {
            get
            {
                return View.State;
            }
            set
            {
                View.State = value;
            }
        }

        /// <summary>
        /// The DeferredView this state is associated with
        /// </summary>
        protected DeferredView View
        {
            get;
            set;
        }

        protected object Model { get; set; }

        public DeferredViewState Wrap(object model)
        {
            return new DeferredViewState(this.View, model);
        }

        /// <summary>
        /// Casts the Model to the specified generic type
        /// and returns the result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>()
        {
            return (T)Model;
        }

        /// <summary>
        /// Sets the Value of the current instance to the MvcHtmlString specified.  Same
        /// as setting Value directly
        /// </summary>
        /// <param name="value"></param>
        public void SetState(MvcHtmlString value)
        {
            this.Value = value;
        }
    }
}

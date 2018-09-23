/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public class ResponderList: Responder
    {
        List<IResponder> _responders;
        public ResponderList(BamConf conf, IEnumerable<IResponder> responders)
            : base(conf)
        {
            this._responders = new List<IResponder>(responders);
        }

        public void AddResponders(params IResponder[] responder)
        {
            _responders.AddRange(responder);
        }

        public override bool MayRespond(IHttpContext context)
        {
            return true;
        }

        /// <summary>
        /// The responder that handled the request if any
        /// </summary>
        public IResponder HandlingResponder { get; set; }

        #region IResponder Members

        public override bool TryRespond(IHttpContext context)
        {
            bool handled = false;
            foreach (IResponder r in _responders)
            {
                if (r.Respond(context))
                {
                    HandlingResponder = r;
                    handled = true;
                    break;
                }
            }

            return handled;
        }

        #endregion
    }
}

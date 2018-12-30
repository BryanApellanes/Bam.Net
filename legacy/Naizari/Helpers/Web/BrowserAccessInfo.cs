/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Naizari.Helpers.Web
{
    [Serializable]
    public class BrowserAccessInfo
    {
        List<BrowserAccessRule> approvedBrowers;
        public BrowserAccessInfo()
        {
            this.approvedBrowers = new List<BrowserAccessRule>();
        }

        public void AddRule(BrowserAccessRule rule)
        {
            this.approvedBrowers.Add(rule);
        }

        public string ApprovedBrowserXmlPath { get; set; }

        public BrowserAccessRule[] BrowserAccessRules
        {
            get
            {
                return this.approvedBrowers.ToArray();
            }
            set
            {
                this.approvedBrowers = new List<BrowserAccessRule>(value);
            }
        }

        public bool IsApproved
        {
            get
            {
                foreach (BrowserAccessRule rule in this.approvedBrowers)
                {
                    if (rule.Result == BrowserAccessRuleResult.Pass)
                        return true;
                }

                return false;
            }
        }
    }
}

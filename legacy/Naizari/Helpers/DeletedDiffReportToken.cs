/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Naizari.Helpers
{
    [Serializable]
    [XmlInclude(typeof(DiffReportToken))]
    public class DeletedDiffReportToken: DiffReportToken
    {
        public override DiffType Type
        {
            get
            {
                return DiffType.Deleted;
            }
        }
    }
}

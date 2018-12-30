/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bam.Net.Analytics
{
    [Serializable]
    [XmlInclude(typeof(DiffReportToken))]
    public class InsertedDiffReportToken: DiffReportToken
    {
        public override DiffType Type
        {
            get
            {
                return DiffType.Inserted;
            }
        }
    }
}

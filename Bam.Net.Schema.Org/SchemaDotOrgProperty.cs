/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;

namespace Bam.Net.Schema.Org
{
    public class SchemaDotOrgProperty
    {
        public long Id { get; set; }
        public string Name { get; set; }

        string _expectedType;
        public string ExpectedType
        {
            get
            {
                return _expectedType;
            }
            set
            {
                _expectedType = value;
                string[] split = _expectedType.Split(new string[] { " or ", " OR ", "\r\nor\r\n", "\r\nOR\r\n", " ", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                split = split.Select(s => s.PascalCase(!s.IsAllCaps())).ToArray();
                if (split.Length == 2)
                {
                    if(split[0].Trim().Equals("Date") || split[0].Trim().Equals("DateTime"))
                    {
                        _expectedType = "DateTime";
                    }
                    else
                    {
                        _expectedType = string.Format("OneOfThese<{0}, {1}>", split[0], split[1]);
                    }
                }
                else if (split.Length == 3)
                {
                    _expectedType = string.Format("OneOfThese<{0}, {1}, {2}>", split[0], split[1], split[2]);
                }
                else if (split.Length == 4)
                {
                    _expectedType = string.Format("OneOfThese<{0}, {1}, {2}, {3}>", split[0], split[1], split[2], split[3]);
                }
                else if (split.Length == 5)
                {
                    _expectedType = string.Format("OneOfThese<{0}, {1}, {2}, {3}, {4}>", split[0], split[1], split[2], split[3], split[4]);
                }
            }
        }
        public string Description { get; set; }

		public override string ToString()
		{
			return "Name: {Name}, ExpectedType: {ExpectedType}, Description: {Description}".NamedFormat(this);
		}
    }
}

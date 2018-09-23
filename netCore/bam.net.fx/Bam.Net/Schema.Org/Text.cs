/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org.DataTypes
{
    public class Text: DataType
    {
        public static implicit operator string(Text text)
        {
            return text.Value;
        }

        public static implicit operator Text(string text)
        {
            return new Text { Value = text };
        }

        public Text()
        {
            this.Name = "Text";
        }

        public Text(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            set;
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class AssignValue: IParameterInfo
    {
        public AssignValue(string columnName, object value)
        {
            this.ColumnName = string.Format("{0}", columnName);
            this.Value = value;
            this.number = new int?();
			this.ColumnNameFormatter = (c) => c;
			this.ParameterPrefix = "@";
        }

		public Func<string, string> ColumnNameFormatter { get; set; }
		public string ParameterPrefix { get; set; }
        public string ColumnName
        {
            get;
            set;
        }

        int? number;
        public int? Number
        {
            get { return number; }
            set
            {
                number = value;
            }
        }

        public int? SetNumber(int? value)
        {
            number = value;
            return ++value;
        }

        public object Value
        {
            get;
            set;
        }

        public string Operator
        {
            get
            {
                return "=";
            }
            set { }
        }        

        public override string ToString()
        {
            return string.Format("{0} {1} {2} ", ColumnName, this.Operator, string.Format("{0}{1}{2}", ParameterPrefix, ColumnName, Number));
        }
    }
}

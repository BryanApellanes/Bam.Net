/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class SetFormat: FormatPart
    {
		public SetFormat()
		{
			this.ParameterPrefix = "@";
		}
		public string ParameterPrefix { get; set; }

        public void AddAssignment(string columnName, object value)
        {
            AddAssignment(new AssignValue(columnName, value));
        }

        public void AddAssignment(AssignValue value)
        {
            this.AddParameter(value);
        }

        public override string Parse()
        {
            AssignNumbers();
			SetParameterPrefixes();
            return string.Format("SET {0} ", this.Parameters.ToArray().ToDelimited(p => p.ToString()));
        }

        protected void AssignNumbers()
        {
            for (int? i = this.StartNumber; i < this.NextNumber; i++)
            {
                int? index = i - this.StartNumber;
                this.Parameters[index.Value].Number = i;
            }
        }

		protected void SetColumnNameFormatter()
		{
			foreach (IParameterInfo parameter in this.Parameters)
			{
				parameter.ColumnNameFormatter = ColumnNameFormatter;
			}
		}

		protected void SetParameterPrefixes()
		{
			foreach(IParameterInfo parameter in this.Parameters)
			{
				parameter.ParameterPrefix = ParameterPrefix;
			}
		}
    }
}

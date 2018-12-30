using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class ParamColumns: QueryFilter<ParamColumns>, IFilterToken
    {
        public ParamColumns() { }
        public ParamColumns(string columnName)
            : base(columnName)
        { }
		
		public ParamColumns KeyColumn
		{
			get
			{
				return new ParamColumns("Id");
			}
		}	

				
        public ParamColumns Id
        {
            get
            {
                return new ParamColumns("Id");
            }
        }
        public ParamColumns Uuid
        {
            get
            {
                return new ParamColumns("Uuid");
            }
        }
        public ParamColumns Cuid
        {
            get
            {
                return new ParamColumns("Cuid");
            }
        }
        public ParamColumns Position
        {
            get
            {
                return new ParamColumns("Position");
            }
        }
        public ParamColumns Value
        {
            get
            {
                return new ParamColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Param);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
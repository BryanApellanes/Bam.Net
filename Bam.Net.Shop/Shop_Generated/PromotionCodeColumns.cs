using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionCodeColumns: QueryFilter<PromotionCodeColumns>, IFilterToken
    {
        public PromotionCodeColumns() { }
        public PromotionCodeColumns(string columnName)
            : base(columnName)
        { }
		
		public PromotionCodeColumns KeyColumn
		{
			get
			{
				return new PromotionCodeColumns("Id");
			}
		}	

				
        public PromotionCodeColumns Id
        {
            get
            {
                return new PromotionCodeColumns("Id");
            }
        }
        public PromotionCodeColumns Uuid
        {
            get
            {
                return new PromotionCodeColumns("Uuid");
            }
        }
        public PromotionCodeColumns Cuid
        {
            get
            {
                return new PromotionCodeColumns("Cuid");
            }
        }
        public PromotionCodeColumns Value
        {
            get
            {
                return new PromotionCodeColumns("Value");
            }
        }

        public PromotionCodeColumns PromotionId
        {
            get
            {
                return new PromotionCodeColumns("PromotionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PromotionCode);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
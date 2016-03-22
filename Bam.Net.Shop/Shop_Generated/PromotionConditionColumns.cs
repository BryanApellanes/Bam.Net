using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionConditionColumns: QueryFilter<PromotionConditionColumns>, IFilterToken
    {
        public PromotionConditionColumns() { }
        public PromotionConditionColumns(string columnName)
            : base(columnName)
        { }
		
		public PromotionConditionColumns KeyColumn
		{
			get
			{
				return new PromotionConditionColumns("Id");
			}
		}	

				
        public PromotionConditionColumns Id
        {
            get
            {
                return new PromotionConditionColumns("Id");
            }
        }
        public PromotionConditionColumns Uuid
        {
            get
            {
                return new PromotionConditionColumns("Uuid");
            }
        }
        public PromotionConditionColumns Description
        {
            get
            {
                return new PromotionConditionColumns("Description");
            }
        }
        public PromotionConditionColumns Value
        {
            get
            {
                return new PromotionConditionColumns("Value");
            }
        }

        public PromotionConditionColumns PromotionId
        {
            get
            {
                return new PromotionConditionColumns("PromotionId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PromotionCondition);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionEffectsColumns: QueryFilter<PromotionEffectsColumns>, IFilterToken
    {
        public PromotionEffectsColumns() { }
        public PromotionEffectsColumns(string columnName)
            : base(columnName)
        { }
		
		public PromotionEffectsColumns KeyColumn
		{
			get
			{
				return new PromotionEffectsColumns("Id");
			}
		}	

				
        public PromotionEffectsColumns Id
        {
            get
            {
                return new PromotionEffectsColumns("Id");
            }
        }
        public PromotionEffectsColumns Uuid
        {
            get
            {
                return new PromotionEffectsColumns("Uuid");
            }
        }
        public PromotionEffectsColumns Name
        {
            get
            {
                return new PromotionEffectsColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(PromotionEffects);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class PromotionEffectColumns: QueryFilter<PromotionEffectColumns>, IFilterToken
    {
        public PromotionEffectColumns() { }
        public PromotionEffectColumns(string columnName)
            : base(columnName)
        { }
		
		public PromotionEffectColumns KeyColumn
		{
			get
			{
				return new PromotionEffectColumns("Id");
			}
		}	
				
﻿        public PromotionEffectColumns Id
        {
            get
            {
                return new PromotionEffectColumns("Id");
            }
        }
﻿        public PromotionEffectColumns Uuid
        {
            get
            {
                return new PromotionEffectColumns("Uuid");
            }
        }
﻿        public PromotionEffectColumns Value
        {
            get
            {
                return new PromotionEffectColumns("Value");
            }
        }

﻿        public PromotionEffectColumns PromotionId
        {
            get
            {
                return new PromotionEffectColumns("PromotionId");
            }
        }
﻿        public PromotionEffectColumns PromotionEffectsId
        {
            get
            {
                return new PromotionEffectColumns("PromotionEffectsId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PromotionEffect);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class ShoppingListColumns: QueryFilter<ShoppingListColumns>, IFilterToken
    {
        public ShoppingListColumns() { }
        public ShoppingListColumns(string columnName)
            : base(columnName)
        { }
		
		public ShoppingListColumns KeyColumn
		{
			get
			{
				return new ShoppingListColumns("Id");
			}
		}	

				
        public ShoppingListColumns Id
        {
            get
            {
                return new ShoppingListColumns("Id");
            }
        }
        public ShoppingListColumns Uuid
        {
            get
            {
                return new ShoppingListColumns("Uuid");
            }
        }
        public ShoppingListColumns Name
        {
            get
            {
                return new ShoppingListColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ShoppingList);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}
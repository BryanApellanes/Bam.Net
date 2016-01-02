/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    public static class OrderByExtensions
    {
        public static OrderBy ToOrderBy<T>(this Enum field, SortOrder order) where T : DaoObject, new()
        {
            return OrderBy.FromFieldEnum<T>(field, order);
        }
    }

    public class OrderBy
    {
        private bool none;

        public OrderBy(string columnName, SortOrder order)
        {
            ColumnName = columnName;
            Order = order;
        }

        public OrderBy(Enum fieldEnum, SortOrder order)
            : this(fieldEnum.ToString(), order)
        {
        }

        public static OrderBy None
        {
            get { return new OrderBy("", SortOrder.Unspecified); }
        }

        public static OrderBy FromFieldEnum<T>(Enum fieldEnum, SortOrder order) where T : DaoObject, new()
        {
            return new OrderBy(DatabaseAgent.EnumToColumnName<T>(fieldEnum), order);
        }

        public string ColumnName { get; set; }
        public SortOrder Order { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.ColumnName) &&
                this.Order == SortOrder.Unspecified)
            {
                return string.Empty;
            }

            string order = string.Empty;
            if( Order == SortOrder.Descending )
                order = "DESC";
            else
                order = "ASC";

            return string.Format(" ORDER BY {0} {1}", ColumnName, order);
        }
    }
}

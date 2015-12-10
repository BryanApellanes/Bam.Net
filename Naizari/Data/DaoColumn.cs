/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DaoColumn : Attribute
    {
        string columnName;
        int maxLength;
        bool allowNulls;
        public DaoColumn() { }
        public DaoColumn(string columnName)
            : this(columnName, 4000)
        {
        }

        public DaoColumn(string columnName, int maxLength)
        {
            this.columnName = columnName;
            this.maxLength = maxLength;
            this.allowNulls = true;
        }

        public DaoColumn(string columName, int maxLength, bool nullable)
            : this(columName, maxLength)
        {
            this.allowNulls = nullable;
        }

        public string ColumnName
        {
            get { return columnName; }
        }

        public int MaxLength
        {
            get { return maxLength == -1 ? 4000: maxLength; }
        }

        public bool AllowNulls { get { return allowNulls; } }
    }
}

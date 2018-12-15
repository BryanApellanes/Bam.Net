/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Data
{
    public class DaoValueChangedEventArgs: EventArgs
    {
        public DaoValueChangedEventArgs(string columnName, object value)
        {
            ColumnName = columnName;
            Value = value;
        }

        public string ColumnName { get; set; }
        public object Value { get; set; }
    }
}

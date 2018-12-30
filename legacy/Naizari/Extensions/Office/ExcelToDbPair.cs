/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Extensions.Office
{
    [Serializable]
    public class ExcelToDbPair
    {
        public ExcelToDbPair() { }

        public string ExcelColumn { get; set; }
        public string DbColumn { get; set; }

        public override bool Equals(object obj)
        {
            ExcelToDbPair pair = obj as ExcelToDbPair;
            if (pair != null)
            {
                return pair.DbColumn.Equals(DbColumn) && pair.ExcelColumn.Equals(ExcelColumn);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ExcelColumn.GetHashCode() + DbColumn.GetHashCode();
        }

        /// <summary>
        /// Used specifically by the Amex uploads to determine what columns
        /// must have a value.
        /// </summary>
        public bool ValueRequired { get; set; }
    }
}

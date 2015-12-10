/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Extensions.Office
{
    [Serializable]
    public class ExcelToDbColumnMap
    {
        Dictionary<string, ExcelToDbPair> pairs;
        public ExcelToDbColumnMap()
        {
            pairs = new Dictionary<string, ExcelToDbPair>();
        }

        public bool Contains(string excelColumn, string dbColumn)
        {
            ExcelToDbPair pair = new ExcelToDbPair();
            pair.DbColumn = dbColumn;
            pair.ExcelColumn = excelColumn;
            return Contains(pair);
        }

        public bool Contains(ExcelToDbPair pair)
        {
            return pairs.ContainsValue(pair);
        }

        public ExcelToDbPair this[string excelColumnName]
        {
            get
            {
                if( pairs.ContainsKey(excelColumnName) )
                    return pairs[excelColumnName];

                return null;
            }
        }

        public void AddPair(string excelColumn, string dbColumn)
        {
            AddPair(excelColumn, dbColumn, false);
        }

        public void AddPair(string excelColumn, string dbColumn, bool valueRequired)
        {
            ExcelToDbPair p = new ExcelToDbPair();
            p.DbColumn = dbColumn;
            p.ExcelColumn = excelColumn;
            p.ValueRequired = valueRequired;
            AddPair(p);
        }

        public void AddPair(ExcelToDbPair pair)
        {
            pairs.Add(pair.ExcelColumn, pair);
        }

        public ExcelToDbPair[] RequiredValuePairs
        {
            get
            {
                List<ExcelToDbPair> retVal = new List<ExcelToDbPair>();
                foreach (ExcelToDbPair pair in ColumnPairs)
                {
                    if (pair.ValueRequired)
                        retVal.Add(pair);
                }

                return retVal.ToArray();
            }
        }

        public ExcelToDbPair[] ColumnPairs
        {
            get
            {
                ExcelToDbPair[] ret = new ExcelToDbPair[pairs.Values.Count];
                pairs.Values.CopyTo(ret, 0);
                return ret;
            }
            set
            {
                pairs.Clear();
                foreach (ExcelToDbPair pair in value)
                {
                    AddPair(pair);
                }
            }
        }
    }
}

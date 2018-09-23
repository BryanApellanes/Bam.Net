/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class SelectColumn<T>: SqlStringBuilder where T: Dao
    {
        public SelectColumn(string column)
        {
            this.Select(Dao.TableName(typeof(T)), column);            
        }
    }
}

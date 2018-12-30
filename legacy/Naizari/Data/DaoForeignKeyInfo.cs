/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Naizari.Data
{
    /// <summary>
    /// This class is used specifically by the DatabaseAgent to reproduce a schema
    /// from generated DaoObjects.
    /// </summary>
    public class DaoForeignKeyInfo
    {
        public DaoForeignKeyInfo(DaoForeignKeyColumn attribute, string tableName)
        {
            this.DaoForeignKeyColumn = attribute;
            this.TableName = tableName;
        }
        public DaoForeignKeyColumn DaoForeignKeyColumn { get; internal set; }
        public string TableName { get; internal set; }
    }
}

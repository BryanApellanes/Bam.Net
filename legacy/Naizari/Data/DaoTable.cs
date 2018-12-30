/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Data
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DaoTable: Attribute
    {
        string tableName;
        public DaoTable() { }
        public DaoTable(string tableName)
        {
            this.tableName = tableName;
        }

        public string TableName
        {
            get { return tableName; }
        }
    }
}

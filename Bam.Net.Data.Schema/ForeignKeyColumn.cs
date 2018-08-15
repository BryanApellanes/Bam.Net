/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A column that represents a foreign key
    /// </summary>
    public class ForeignKeyColumn: Column
    {
        /// <summary>
        /// Empty constructor provided for deserialization
        /// </summary>
        public ForeignKeyColumn()
        {
            this.ReferencedTable = string.Empty;
            this.DbDataType = string.Empty;
        }
        
        /// <summary>
        /// Instantiate a new ForeignKeyColumn based on the specified column
        /// referencing the specified referencedTable
        /// </summary>
        /// <param name="column"></param>
        /// <param name="referencedTable"></param>
        public ForeignKeyColumn(Column column, string referencedTable)
            : base(column.TableName)
        {
            this.AllowNull = column.AllowNull;
            this.Key = column.Key;
            this.Name = column.Name;
            this.DataType = column.DataType;
            this.ReferencedTable = referencedTable;
            this.DbDataType = column.DbDataType;
        }

        /// <summary>
        /// Instantiate a new ForeignKeyColumn with the specified name
        /// for the specified tableName referencing the specified referencedTable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="referencedTable"></param>
        public ForeignKeyColumn(string name, string tableName, string referencedTable)
            : this(new Column(name, tableName), referencedTable)
        {
        }

        public override DataTypes DataType
        {
            get
            {
                return DataTypes.ULong;
            }
            set
            {
                // always ULong
            }
        }

        string referenceName;
        public string ReferenceName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(referenceName))
                {
                    return string.Format("FK_{0}_{1}", this.TableName, ReferencedTable);
                }
                else
                {
                    return referenceName;
                }
            }
            set
            {
                referenceName = value;
            }
        }

        public string ReferenceNameSuffix
        {
            get;
            private set;
        }

        public string ReferencedKey { get; set; }
        
        public string ReferencedTable { get; set; }

        string _referencedClass;
        public string ReferencedClass
        {
            get
            {
                return string.IsNullOrEmpty(_referencedClass) ? ReferencedTable.PascalCase(true, " ", "_").TrimNonLetters() : _referencedClass;
            }
            set
            {
                _referencedClass = value;
            }
        }

        string _referencingClass;
        public string ReferencingClass
        {
            get
            {
                return string.IsNullOrEmpty(_referencingClass) ? TableName.PascalCase(true, " ", "_").TrimNonLetters() : _referencingClass;
            }
            set
            {
                _referencingClass = value;
            }
        }

        public override string ToString()
        {
            return this.ReferenceName;
        }

        protected internal string RenderClassProperty(int i = -1, string ns = "")
        {
            if (string.IsNullOrEmpty(ReferencedClass.Trim()))
            {
                throw new InvalidOperationException("ReferencedClass cannot be null");
            }
            if (i > 0)
            {
                ReferenceNameSuffix = i.ToString();
            }

            return Render<ForeignKeyColumn>("ForeignKeyProperty.tmpl", ns); 
        }

        protected internal string RenderListProperty()
        {
            return Render<ForeignKeyColumn>("DaoCollectionProperty.tmpl");
        }

        protected internal string RenderAddToChildDaoCollection()
        {
            return Render<ForeignKeyColumn>("ChildDaoCollectionAdd.tmpl");
        }
    }
}

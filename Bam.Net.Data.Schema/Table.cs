/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bam.Net;
using Bam.Net.Data;
//using Bam.Net.ServiceProxy;
//using Bam.Net.ServiceProxy.Js;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A database Table
    /// </summary>
    public class Table
    {
        Dictionary<string, Column> _columns;
        List<ForeignKeyColumn> _referencingForeignKeys;
        Dictionary<string, ForeignKeyColumn> _foreignKeys;

        public Table()
        {
            this._columns = new Dictionary<string, Column>();
            this._referencingForeignKeys = new List<ForeignKeyColumn>();
            this._foreignKeys = new Dictionary<string, ForeignKeyColumn>();
        }

        public Table(string tableName)
            : this()
        {
            this.Name = tableName;
        }

        public Table(string tableName, string connectionName)
            : this(tableName)
        {
            this.ConnectionName = connectionName;
        }

        [Exclude]
        public string ConnectionName { get; set; }

        public void SetPropertyName(string columnName, string propertyName)
        {
            List<Column> columns = new List<Column>(Columns);
            Column toSet = columns.FirstOrDefault(c => c.Name.Equals(columnName));
            if (toSet != null)
            {
                toSet.PropertyName = propertyName;
                Columns = columns.ToArray();
            }
            else
            {
                List<ForeignKeyColumn> fks = new List<ForeignKeyColumn>(ForeignKeys);
                ForeignKeyColumn toSetFk = fks.FirstOrDefault(c => c.Name.Equals(columnName));
                if (toSetFk != null)
                {
                    toSetFk.PropertyName = propertyName;
                    ForeignKeys = fks.ToArray();                    
                }
            }
        }

        public string GetPropertyName(string columnName)
        {
            return this[columnName].PropertyName;
        }

        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = Regex.Replace(value, @"\s", string.Empty);
            }
        }

        string _className;
        public string ClassName
        {
            get
            {
                return _className ?? Name.PascalCase(true, " ", "_").DropLeadingNonLetters();
            }
            set
            {
                _className = value;
            }
        }

        public Column[] Columns
        {
            get
            {
                return this._columns.Values.ToArray();
            }
            set
            {
                _columns.Clear();
                foreach (Column val in value)
                {
                    _columns.Add(val.Name, val);
                }
            }
        }

        public ForeignKeyColumn[] ForeignKeys
        {
            get
            {
                return this._foreignKeys.Values.ToArray();
            }
            set
            {
                _foreignKeys.Clear();
                foreach (ForeignKeyColumn fk in value)
                {
                    _foreignKeys.Add(fk.Name, fk);
                }
            }
        }

        /// <summary>
        /// All ForeignKeyColmns where the current table is referenced.
        /// </summary>
        public ForeignKeyColumn[] ReferencingForeignKeys
        {
            get
            {
                return this._referencingForeignKeys.ToArray();
            }
            set
            {
                this._referencingForeignKeys = new List<ForeignKeyColumn>(value);
            }
        }
        
        [Exclude]
        public Column Key
        {
            get
            {
                Column key = (from col in Columns
                        where (col is KeyColumn || col.Key)
                        select col).FirstOrDefault();
                
                if (key == null)
                {
                    key = KeyColumn.Default;
                }

                return key;
            }
        }

        public void SetKeyColumn(string columnName)
        {
            Column c = (from cl in Columns
                        where cl.Key
                        select cl).FirstOrDefault();
            if (c != null)
            {
                UnsetKeyColumn(c.Name);
            }

            Column col = this[columnName];
            this._columns.Remove(col.Name);
            this.AddColumn(new KeyColumn(col));
        }

        public void SetForeignKeyColumn(string columnName, string referencedColumn, string referencedTable)
        {
            Column c = (from cl in Columns
                        where cl.Name.Equals(columnName)
                        select cl).FirstOrDefault();
            if (c != null)
            {
                RemoveColumn(c);
            }
            this.AddColumn(new ForeignKeyColumn(c, referencedTable));
        }
        
        private void UnsetKeyColumn(string columnName)
        {
            Column col = this[columnName];            
            this._columns.Remove(col.Name);
            this.AddColumn(new Column { 
                AllowNull = col.AllowNull, 
                Name = col.Name, 
                TableName = col.TableName, 
                DataType = col.DataType });
        }

        public void AddColumn(string columnName, DataTypes type, bool allowNull = true)
        {
            AddColumn(new Column { AllowNull = allowNull, Key = false, Name = columnName, TableName = this.Name, DataType = type });
        }

        object _columnLock = new object();
        public void AddColumn(Column column)
        {
            lock (_columnLock)
            {
                column.TableName = this.Name;
                ForeignKeyColumn fk = column as ForeignKeyColumn;
                if (fk != null)
                {
                    if (fk.ReferencedTable.Equals(this.Name))
                    {
                        this._referencingForeignKeys.Add(fk);
                    }

                    if (fk.TableName.Equals(this.Name) && !this._foreignKeys.ContainsKey(fk.Name))
                    {
                        this._foreignKeys.Add(fk.Name, fk);
                    }
                }
                else if (!this._columns.ContainsKey(column.Name))
                {
                    this._columns.Add(column.Name, column);
                }
            }
        }
        
        public void RemoveColumn(Column column)
        {
            RemoveColumn(column.Name);
        }

        public void RemoveColumn(string columnName)
        {
            if (this._columns.ContainsKey(columnName))
            {
                this._columns.Remove(columnName);
            }
        }

        [Exclude]
        public Column this[string columnName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(columnName))
                {
                    throw new ArgumentNullException("columnName");
                }

                if (this._columns.ContainsKey(columnName))
                {
                    return this._columns[columnName];
                }
                else if (this._foreignKeys.ContainsKey(columnName))
                {
                    return this._foreignKeys[columnName];
                }
                else
                {
                    throw new InvalidOperationException(string.Format("The specified column {0} was not found on the table {1}", columnName, this.Name));
                }
            }
        }

        public string RenderContextMethod()
        {
            RazorParser<TableTemplate> razorParser = new RazorParser<TableTemplate>();
            return razorParser.ExecuteResource("ContextMethods.tmpl", new { Model = this });
        }

        public override string ToString()
        {
            return string.Format("{0}.Name={1}::{0}.ClassName={2}", typeof(Table).Name, this.Name, this.ClassName);
        }
    }
}

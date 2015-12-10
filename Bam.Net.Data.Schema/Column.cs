/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Html;
using System.Text.RegularExpressions;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;

namespace Bam.Net.Data.Schema
{
    public class Column
    {
        public Column()
        {
        }

        /// <summary>
        /// Instantiate a column where Type = Long, AllowNull = false, Key = true
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="tableName"></param>
        public Column(string columnName, string tableName)
        {
            this.Name = columnName;
            this.TableName = tableName;
            this.DataType = DataTypes.Long;
            this.AllowNull = false;
            this.Key = true;
        }

        public Column(string columnName, DataTypes dataType, bool allowNull = true, string maxLength = "")
        {
            this.Name = columnName;
            this.DataType = dataType;
            this.AllowNull = allowNull;
            this.MaxLength = maxLength;
        }

        internal Column(string tableName)
        {
            this.TableName = tableName;
        }

        [Exclude]
        public string TableName { get; set; }

        string _tableClassName;
        [Exclude]
        public string TableClassName
        {
            get
            {
                if (string.IsNullOrEmpty(_tableClassName))
                {

                    string val = TableName;
                    if (!string.IsNullOrEmpty(TableName))
                    {
                        val = TableName.PascalCase(true, " ", "_").DropLeadingNonLetters();
                    }
                    _tableClassName = val;
                }
                return _tableClassName;
            }
            set
            {
                _tableClassName = value;
            }
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

        string _propertyName;
        /// <summary>
        /// Gets the value of the PropertyName this Column
        /// will be converted to during code generation
        /// </summary>
        public string PropertyName
        {
            get
            {
                return string.IsNullOrEmpty(_propertyName) ? Name.PascalCase(true, " ", "_").DropLeadingNonLetters() : _propertyName;
            }
            set
            {
                _propertyName = value;
            }
        }

        /// <summary>
        /// The Dao defined DataType of the column
        /// </summary>
        [DropDown(typeof(DataTypes))]
        public virtual DataTypes DataType { get; set; }

        public string MaxLength { get; set; }

        /// <summary>
        /// The string representation of the  Dao defined data type 
        /// translated to its native csharp type equivalent.  Used by
        /// generator and not directly refernced in code
        /// </summary>
        public string NativeType
        {
            get
            {
                switch (DataType)
                {
                    case DataTypes.Boolean:
                        return "bool?";
                    case DataTypes.Int:
                        return "int?";
                    case DataTypes.Long:
                        return "long?";
                    case DataTypes.Decimal:
                        return "decimal?";
                    case DataTypes.String:
                        return "string";
                    case DataTypes.ByteArray:
                        return "byte[]";
                    case DataTypes.DateTime:
                        return "DateTime?";
                    default:
                        return "string";
                }
            }            
        }

        string _dbDataType;
        /// <summary>
        /// The database equivalent of the DataType
        /// </summary>
        public string DbDataType
        {
            get
            {
                if (string.IsNullOrEmpty(_dbDataType))
                {
                    switch (DataType)
                    {
                        case DataTypes.Default:
                            SetDbDataype("VarChar", "4000");
                            break;
                        case DataTypes.Boolean:
                            SetDbDataype("Bit", "1");
                            break;
                        case DataTypes.Int:
                            SetDbDataype("Int", "10");
                            break;
                        case DataTypes.Long:
                            SetDbDataype("BigInt", "19");
                            break;
                        case DataTypes.Decimal:
                            SetDbDataype("Decimal", "28");
                            break;
                        case DataTypes.String:
                            SetDbDataype("VarChar", "4000");
                            break;
                        case DataTypes.ByteArray:
                            SetDbDataype("VarBinary", "8000");
                            break;
                        case DataTypes.DateTime:
                            SetDbDataype("DateTime", "8");
                            break;
                        default:
                            SetDbDataype("VarChar", "4000");
                            break;
                    }
                }

                return _dbDataType;
            }
            set
            {
                _dbDataType = value;
            }
        }

        private void SetDbDataype(string dbDataType, string max = null)
        {
            _dbDataType = dbDataType;
            if (string.IsNullOrEmpty(MaxLength) && !string.IsNullOrEmpty(max))
            {
                MaxLength = max;
            }
        }

        public virtual bool AllowNull
        {
            get;
            set;
        }

        [Exclude]
        public virtual bool Key
        {
            get;
            set;
        }

        protected internal virtual string RenderClassProperty()
        {
            if (this.Key)
            {
                return Render<Column>("KeyProperty.tmpl");
            }
            else
            {
                return Render<Column>("Property.tmpl");
            }
        }

        protected internal virtual string RenderColumnsClassProperty()
        {
            return Render<Column>("ColumnsProperty.tmpl");
        }

        public override int GetHashCode()
        {
            return string.Format("{0}.{1}", this.TableName, this.Name).ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Column col = obj as Column;
            if (col != null)
            {
                return col.GetHashCode() == this.GetHashCode();
            }
            else
            {
                return base.Equals(obj);
            }
        }
        
        protected string Render<T>(string templateName, string ns = "")
        {
            Type type = this.GetType();
            string namespacePath = string.Format("{0}.Templates.", type.Namespace);
            RazorParser<DaoRazorTemplate<T>> razorParser = new RazorParser<DaoRazorTemplate<T>>();
            razorParser.GetDefaultAssembliesToReference = DaoGenerator.GetReferenceAssemblies;
            return razorParser.ExecuteResource(templateName, namespacePath, type.Assembly, new { Model = this, Namespace = ns });
        }
    }
}

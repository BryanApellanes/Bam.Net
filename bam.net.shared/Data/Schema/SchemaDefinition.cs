/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net;
using Bam.Net.Data;
using Newtonsoft.Json;
using Bam.Net.Configuration;

namespace Bam.Net.Data.Schema
{
    public class SchemaDefinition
    {
        Dictionary<string, Table> _tables = new Dictionary<string, Table>();
        Dictionary<string, ColumnAttribute> _columns = new Dictionary<string, ColumnAttribute>();

        public SchemaDefinition()
        {
            this.Name = "Default";
            this.DbType = "UnSpecified";
        }
        public SchemaDefinition(string name): this()
        {
            Name = name;
            File = $"{RuntimeSettings.AppDataFolder}\\{name}_schema_definition.json";
        }
        /// <summary>
        /// Gets or sets the type of the database that this SchemaDefinition was
        /// extracted from.  May be null.
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// Gets or sets the name of the current SchemaDefinition.
        /// </summary>
        public string Name { get; set; }

        FileInfo _file;
        [Exclude]
        public string File
        {
            get
            {
                if (_file != null)
                {
                    return _file.FullName;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _file = new FileInfo(Path.Combine(RuntimeSettings.AppDataFolder, this.Name));
                }
                else
                {
                    _file = new FileInfo(value);
                    if (!_file.Directory.Exists)
                    {
                        _file.Directory.Create();
                    }
                }
            }
        }

        public void RemoveTable(Table table)
        {
            RemoveTable(table.Name);
        }

        public void RemoveTable(string tableName)
        {
            if (this._tables.ContainsKey(tableName))
            {
                this._tables.Remove(tableName);
            }
        }

        public Table[] Tables
        {
            get
            {
				List<Table> tables = new List<Table>();
				tables.AddRange(this._tables.Values);
				return tables.ToArray();
            }
            set
            {
                this._tables.Clear();
                foreach (Table table in value)
                {
                    if (string.IsNullOrEmpty(table.ConnectionName))
                    {
                        table.ConnectionName = this.Name;
                    }
					if (!this._tables.ContainsKey(table.Name))
					{
						this._tables.Add(table.Name, table);
					}
					else
					{
						throw Args.Exception<InvalidOperationException>("Table named {0} defined more than once", table.Name);
					}
                }
            }
        }

        internal Table GetTable(string tableName)
        {
            Table table = null;
			if (this._tables.ContainsKey(tableName))
			{
				table = this._tables[tableName];
			}
            return table;
        }

        List<ForeignKeyColumn> _foreignKeys = new List<ForeignKeyColumn>();
        public ForeignKeyColumn[] ForeignKeys
        {
            get
            {
                return this._foreignKeys.ToArray();
            }
            set
            {
                this._foreignKeys.Clear();
                this._foreignKeys.AddRange(value);
            }
        }

        Dictionary<string, XrefTable> _xrefs = new Dictionary<string, XrefTable>();
        public XrefTable[] Xrefs
        {
            get
            {
                return _xrefs.Values.ToArray();
            }
            set
            {
                this._xrefs = value.ToDictionary<XrefTable, string>(x => x.Name); // to Dictionary key by name
            }
        }

        public XrefInfo[] LeftXrefsFor(string tableName)
        {
            return (from xref in Xrefs
                    where xref.Left.Equals(tableName)
                    select xref).Select(x => new XrefInfo(tableName, x.Name, x.Right)).ToArray();
        }

        public XrefInfo[] RightXrefsFor(string tableName)
        {
            return (from xref in Xrefs
                    where xref.Right.Equals(tableName)
                    select xref).Select(x => new XrefInfo(tableName, x.Name, x.Left)).ToArray();
        }

        internal XrefTable GetXref(string tableName)
        {
            XrefTable result = null;
            if (this._xrefs.ContainsKey(tableName))
            {
                result = this._xrefs[tableName];
            }

            return result;
        }

        public SchemaResult AddXref(XrefTable xref)
        {
            SchemaResult r = new SchemaResult(string.Format("XrefTable {0} was added.", xref.Name));
            try
            {
                xref.ConnectionName = this.Name;
                if (_xrefs.ContainsKey(xref.Name))
                {
                    _xrefs[xref.Name] = xref;
                    r.Message = string.Format("XrefTable {0} was updated.", xref.Name);
                }
                else
                {
                    _xrefs.Add(xref.Name, xref);
                }
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            return r;
        }

        public void RemoveXref(string name)
        {
            if (_xrefs.ContainsKey(name))
            {
                _xrefs.Remove(name);
            }
        }

        public void RemoveXref(XrefTable xrefTable)
        {
            if (_xrefs.ContainsKey(xrefTable.Name))
            {
                _xrefs.Remove(xrefTable.Name);
            }
        }

        public SchemaResult AddTable(Table table)
        {
            SchemaResult r = new SchemaResult(string.Format("Table {0} was added.", table.Name));
            try
            {
                table.ConnectionName = this.Name;
                if (this._tables.ContainsKey(table.Name))
                {
                    this._tables[table.Name] = table;
                    r.Message = string.Format("Table {0} was updated.", table.Name);
                }
                else
                {
                    this._tables.Add(table.Name, table);
                }
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            return r;
        }

        public SchemaResult AddForeignKey(ForeignKeyColumn fk)
        {
            SchemaResult r = new SchemaResult(string.Format("ForeignKey {0} was added.", fk.ReferenceName));
            try
            {
                if (!this._foreignKeys.Contains(fk))
                {
                    this._foreignKeys.Add(fk);
                }
                else
                {
                    ForeignKeyColumn existing = (from efk in this._foreignKeys
                                                 where efk.Equals(fk)
                                                 select efk).FirstOrDefault();

                    existing.AllowNull = fk.AllowNull;
                    existing.DbDataType = fk.DbDataType;
                    existing.Key = fk.Key;
                    existing.MaxLength = fk.MaxLength;
                    existing.Name = fk.Name;
                    existing.ReferencedKey = fk.ReferencedKey;
                    existing.ReferencedTable = fk.ReferencedTable;
                    existing.ReferenceName = fk.ReferenceName;
                    existing.TableName = fk.TableName;                    
                }
            }
            catch (Exception ex)
            {
                SetErrorDetails(r, ex);
            }

            return r;
        }
        
        private void SetErrorDetails(SchemaResult r, Exception ex)
        {
            this.LastException = ex;
            r.Message = ex.Message;
            r.Success = false;
            r.StackTrace = ex.StackTrace;
        }
        
        /// <summary>
        /// The most recent exception that occurred after trying to add a table
        /// or a foreign key
        /// </summary>
        public Exception LastException
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Loads a SchemaDefinition from the specified file, the file
        /// will be created if it doesn't exist.
        /// </summary>
        /// <param name="schemaFile"></param>
        /// <returns></returns>
        public static SchemaDefinition Load(string schemaFile)
        {
            SchemaDefinition schema = new SchemaDefinition();
            schema.File = schemaFile;
            if (System.IO.File.Exists(schemaFile)) 
			{
	            schema = schemaFile.FromJsonFile<SchemaDefinition>();
            }
            else
            {
                Save(schema);
            }
            return schema;
        }

        /// <summary>
        /// Serializes the current SchemaDefinition as json to the
        /// file specified in the File property
        /// </summary>
        public void Save()
        {
            Save(this);
        }

        /// <summary>
        /// Serializes the current SchemaDefinition as json to the
        /// specified filePath
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            this.File = filePath;
            Save(this);
        }

        static object _saveLock = new object();
        private static void Save(SchemaDefinition schema)
        {
            lock (_saveLock)
            {
                schema.ToJsonFile(schema.File);
            }
        }
    }
}

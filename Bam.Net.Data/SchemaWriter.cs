/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.Data
{
    public abstract class SchemaWriter: SqlStringBuilder
    {
        protected static string CreateTableFormat = "CREATE TABLE [{0}] ({1})";
        public SchemaWriter()
        {
            this.KeyColumnFormat = "{0} IDENTITY (1,1) PRIMARY KEY";
            this.AddForeignKeyColumnFormat = "ALTER TABLE [{0}] ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES [{3}] ({4})";
        }

        public event SqlStringBuilderDelegate DropEnabled;

        protected void OnDropEnabled()
        {
            if (DropEnabled != null)
            {
                DropEnabled(this);
            }
        }

        public string KeyColumnFormat
        {
            get;
            protected set;
        }

        public string AddForeignKeyColumnFormat
        {
            get;
            protected set;
        }

        bool _dropEnabled;
        public bool EnableDrop
        {
            get
            {
                return _dropEnabled;
            }
            set
            {
                _dropEnabled = value;
                if (_dropEnabled)
                {
                    OnDropEnabled();
                }
            }
        }

        /// <summary>
        /// Writes the sql script that will recreate the schema associated with the specified
        /// Dao type.  
        /// </summary>
        /// <typeparam name="T">The type to analyse</typeparam>
        /// <returns>False if the Assembly that the specified type 
        /// is defined in has already been analysed, true otherwise</returns>
        public bool WriteSchemaScript<T>() where T: Dao
        {
			return WriteSchemaScript(typeof(T));
        }

        public bool WriteSchemaScript(Type type)
        {
            ForEachTable(type, this.WriteCreateTable);
            this.WriteForeignKeys(type);
            return true;
        }

		public bool WriteSchemaScript(Assembly assembly)
		{
			ForEachTable(assembly, this.WriteCreateTable, t => t.HasCustomAttributeOfType<TableAttribute>());
			this.WriteForeignKeys(assembly, t => t.HasCustomAttributeOfType<TableAttribute>());
			return true;
		}

        private void ForEachTable<T>(Action<Type> action) where T : Dao
        {
            Type type = typeof(T);
            ForEachTable(type, action);
        }

        private void ForEachTable(Type type, Action<Type> action)
        {
            string connectionName = Dao.ConnectionName(type);

			foreach (Type t in type.Assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<TableAttribute>(false)))
			{
				if (Dao.ConnectionName(t).Equals(connectionName))
				{
					action(t);
					Go();
				}
			}
        }

		private void ForEachTable(Assembly assembly, Action<Type> action, Func<Type, bool> typePredicate)
		{
			foreach (Type t in assembly.GetTypes().Where(t => t.HasCustomAttributeOfType<TableAttribute>(false)))
			{
				if (typePredicate(t))
				{
					action(t);
					Go();
				}
			}
		}

        public void DropTable(Type daoType)
        {
            if (!this.EnableDrop)
            {
                throw new DropNotEnabledException();
            }

            WriteDropTable(daoType);
        }

        /// <summary>
        /// Write the necessary script to drop  
        /// all tables associated with the specified type
        /// T.  Throws a DropNotEnabledException if
        /// EnableDrop is false.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void DropAllTables<T>() where T: Dao
        {
            if (!this.EnableDrop)
            {
                throw new DropNotEnabledException();
            }

            ForEachTable<T>(this.WriteDropForeignKeys);
            ForEachTable<T>(this.WriteDropTable);
        }

        protected virtual void WriteCreateTable(Type daoType)
        {
            ColumnAttribute[] columns = GetColumns(daoType);
            
            Builder.AppendFormat(CreateTableFormat,
                Dao.TableName(daoType),
                columns.ToDelimited(c =>
                {
                    if (c is KeyColumnAttribute)
                    {
                        return string.Format(KeyColumnFormat, GetColumnDefinition(c));
                    }
                    else
                    {
                        return GetColumnDefinition(c);
                    }
                }));
        }

        /// <summary>
        /// Gets the text used to declare the specified column in a 
        /// create table sql statement.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public abstract string GetColumnDefinition(ColumnAttribute column);
        
        protected virtual void WriteForeignKeys(Type daoType)
        {
            Args.ThrowIfNull(daoType, "daoType");
			string datasetName = Dao.ConnectionName(daoType);
            foreach (Type type in daoType.Assembly.GetTypes())
            {
                TableAttribute tableAttr = null;
                if (type.HasCustomAttributeOfType<TableAttribute>(out tableAttr))
                {
                    if (Dao.ConnectionName(type).Equals(datasetName))
                    {
                        AppendAddForeignKey(type);
                    }
                }
            }
        }

		protected virtual void WriteForeignKeys(Assembly daoAssembly, Func<Type, bool> typePredicate = null)
		{
			Args.ThrowIfNull(daoAssembly, "daoAssembly");
			typePredicate = typePredicate == null ? t => t.HasCustomAttributeOfType<TableAttribute>() : typePredicate;
			foreach (Type type in daoAssembly.GetTypes())
			{
				if (typePredicate(type))
				{
                    AppendAddForeignKey(type);
				}				
			}
		}

        protected virtual void AppendAddForeignKey(Type type)
        {
            ForeignKeyAttribute[] columns = GetForeignKeys(type);
            foreach (ForeignKeyAttribute fk in columns)
            {
                if (fk != null)
                {
                    // table1Name, fkName, column1Name, table2Name, column2Name
                    // "ALTER TABLE [{0}] ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES [{3}] ({4})";
                    Builder.AppendFormat(AddForeignKeyColumnFormat,
                        fk.Table, fk.ForeignKeyName, fk.Name, fk.ReferencedTable, fk.ReferencedKey);

                    Go();
                }
            }
        }

        protected virtual void WriteDropTable(Type daoType)
        {
            TableAttribute attr = null;
            if (daoType.HasCustomAttributeOfType<TableAttribute>(out attr))
            {
                Builder.AppendFormat("IF OBJECT_ID(N'dbo.{0}') IS NOT NULL\r\nBEGIN\r\nDROP TABLE [{0}]\r\nEND", attr.TableName);
                Go();
            }
        }

        protected virtual void WriteDropForeignKeys(Type daoType)
        {
            TableAttribute table = null;
            if (daoType.HasCustomAttributeOfType<TableAttribute>(out table))
            {
                PropertyInfo[] properties = daoType.GetProperties();
                foreach (PropertyInfo prop in properties)
                {
                    ForeignKeyAttribute fk = null;
                    if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                    {
                        Builder.AppendFormat(@"
IF EXISTS (
    SELECT * 
        FROM SYS.FOREIGN_KEYS
            WHERE object_id = OBJECT_ID(N'dbo.{0}') 
            AND parent_object_id = OBJECT_ID(N'dbo.{1}')
)
    ALTER TABLE [{1}] DROP CONSTRAINT [{0}]", fk.ForeignKeyName, table.TableName);
                        Go();
                    }
                }
            }
        }       

        protected ColumnAttribute[] GetColumns(Type daoType)
        {
            return Db.GetColumns(daoType);
        }

        protected ForeignKeyAttribute[] GetForeignKeys(Type daoType)
        {
            return Db.GetForeignKeys(daoType);
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Naizari.Data;
using Naizari.Configuration;

namespace Naizari.Data
{
    public class DataRelationshipDefinition
    {
        static Dictionary<DatabaseAgent, DataRelationshipDefinition[]> dataRelationships;
        public DataRelationshipDefinition()
        {
            if (dataRelationships == null)
                dataRelationships = new Dictionary<DatabaseAgent, DataRelationshipDefinition[]>();
        }

        internal DataRelationshipDefinition(DataRow row)
            : this()
        {
            //this.row = row;
            DatabaseAgent.FromDataRow(this, row);
        }

        public static DataRelationshipDefinition[] GetDataRelationshipDefinitionsWhereTableIsForeignKey(DatabaseAgent sourcedb, string tableName)
        {
            if (!dataRelationships.ContainsKey(sourcedb))
                GetDataRelationshipDefinitions(sourcedb);

            List<DataRelationshipDefinition> returnValues = new List<DataRelationshipDefinition>();
            foreach (DataRelationshipDefinition definition in dataRelationships[sourcedb])
            {
                if (definition.ForeignKeyTable.Equals(tableName))
                    returnValues.Add(definition);
            }

            return returnValues.ToArray();
        }

        public static DataRelationshipDefinition[] GetDataRelationshipDefinitions(DatabaseAgent sourceDb)
        {
            if (dataRelationships == null)
                dataRelationships = new Dictionary<DatabaseAgent, DataRelationshipDefinition[]>();

            if (!dataRelationships.ContainsKey(sourceDb))
            {
                string sql = @"SELECT FK.constraint_name as ForeignKeyName, 
                FK.table_name as ForeignKeyTable, 
                FKU.column_name as ForeignKeyColumn,
                UK.constraint_name as PrimaryKeyName, 
                UK.table_name as PrimaryKeyTable, 
                UKU.column_name as PrimaryKeyColumn
                FROM Information_Schema.Table_Constraints AS FK
                INNER JOIN
                Information_Schema.Key_Column_Usage AS FKU
                ON FK.constraint_type = 'FOREIGN KEY' AND
                FKU.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Referential_Constraints AS RC
                ON RC.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Table_Constraints AS UK
                ON UK.constraint_name = RC.unique_constraint_name
                INNER JOIN
                Information_Schema.Key_Column_Usage AS UKU
                ON UKU.constraint_name = UK.constraint_name AND
                UKU.ordinal_position =FKU.ordinal_position";

                List<DataRelationshipDefinition> list = new List<DataRelationshipDefinition>();
                DataTable foreignKeyData = sourceDb.GetDataTableFromSql(sql);
                foreach (DataRow row in foreignKeyData.Rows)
                {
                    list.Add(DataRelationshipDefinition.CreateFromDataRow(row));
                }

                DataRelationshipDefinition[] retVal = list.ToArray();
                dataRelationships.Add(sourceDb, retVal);
                return retVal;
            }
            else
            {
                return dataRelationships[sourceDb];
            }
        }

        public static DataRelationshipDefinition CreateFromDataRow(DataRow row)
        {
            if (row["ForeignKeyName"] == null ||
                row["ForeignKeyTable"] == null ||
                row["ForeignKeyColumn"] == null ||
                row["PrimaryKeyName"] == null ||
                row["PrimaryKeyTable"] == null ||
                row["PrimaryKeyColumn"] == null)
            {
                throw new InvalidOperationException("Required information was not contained in the specified DataRow");
            }

            return new DataRelationshipDefinition(row);
            //ForeignKeyName
            //ForeignKeyTable
            //ForeignKeyColumn

            //PrimaryKeyName
            //PrimaryKeyTable
            //PrimaryKeyColumn
        }

        public string ToCsharp(string varName, int indent)
        {
            string indentString = "";
            for (int i = 0; i < indent; i++)
            {
                indentString += "\t";
            }
            StringBuilder code = new StringBuilder();
            code.AppendFormat(indentString + "DataRelationshipDefinition {0} = new DataRelationshipDefinition();\r\n", varName);
            code.AppendFormat(indentString + "{0}.ForeignKeyName = \"{1}\";\r\n", varName, this.ForeignKeyName);
            code.AppendFormat(indentString + "{0}.ForeignKeyTable = \"{1}\";\r\n", varName, this.ForeignKeyTable);
            code.AppendFormat(indentString + "{0}.ForeignKeyColumn = \"{1}\";\r\n", varName, this.ForeignKeyColumn);
            code.AppendFormat(indentString + "{0}.PrimaryKeyName = \"{1}\";\r\n", varName, this.PrimaryKeyName);
            code.AppendFormat(indentString + "{0}.PrimaryKeyTable = \"{1}\";\r\n", varName, this.PrimaryKeyTable);
            code.AppendFormat(indentString + "{0}.PrimaryKeyColumn = \"{1}\";\r\n", varName, this.PrimaryKeyColumn);

            return code.ToString();
        }

        public string ForeignKeyName { get; set; }
        public string ForeignKeyTable { get; set; }
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyName { get; set; }
        public string PrimaryKeyTable { get; set; }
        public string PrimaryKeyColumn { get; set; }

        //public string ForeignKeyName
        //{
        //    get { return row["ForeignKeyName"] as string; }
        //}

        //public string ForeignKeyTable
        //{
        //    get { return row["ForeignKeyTable"] as string; }
        //}

        //public string ForeignKeyColumn
        //{
        //    get { return row["ForeignKeyColumn"] as string; }
        //}

        //public string PrimaryKeyName
        //{
        //    get { return row["PrimaryKeyName"] as string; }
        //}

        //public string PrimaryKeyTable
        //{
        //    get { return row["PrimaryKeyTable"] as string; }
        //}

        //public string PrimaryKeyColumn
        //{
        //    get { return row["PrimaryKeyColumn"] as string; }
        //}

        public override string ToString()
        {
            return string.Format("P Table: {0}, P Column: {1}, F Table: {2}, F Column: {3}", PrimaryKeyTable, PrimaryKeyColumn, ForeignKeyTable, ForeignKeyColumn);
        }
    }
}

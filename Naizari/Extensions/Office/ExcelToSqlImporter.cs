/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Data.Access;
using Naizari.Configuration;
using System.Data.SqlClient;

namespace Naizari.Extensions.Office
{
    public class ExcelToSqlImporter
    {
        public ExcelToDbColumnMap ExcelToDbColumnMap { get; set; }
        public string ExcelLinkedServerName { get; set; }
        public string SheetName { get; set; }
        public string TableName { get; set; }
        public DatabaseUtility DatabaseUtility { get; set; }
        public ExcelToDbStaticValue[] StaticValues { get; set; }

        /// <summary>
        /// Will check that all properties have been properly initialized
        /// and throw an InvalidOperationException if not.
        /// </summary>
        private void CheckProperties()
        {
            if (ExcelToDbColumnMap == null)
                ThrowArgumentException("ExcelToDbColumnMap");
            if (string.IsNullOrEmpty(ExcelLinkedServerName))
                ThrowArgumentException("ExcelLinkedServerName");
            if (string.IsNullOrEmpty(SheetName))
                ThrowArgumentException("SheetName");
            if (DatabaseUtility == null)
                ThrowArgumentException("DatabaseUtility");
            if (string.IsNullOrEmpty(TableName))
                ThrowArgumentException("TableName");
        }

        private void ThrowArgumentException(string argName)
        {
            throw new ArgumentException(string.Format("{0} not properly initialized.", argName));
        }

        /// <summary>
        /// Imports the spreadsheet.  Will throw ArgumentException if
        /// all required properties have not been set.
        /// </summary>
        public void Import()
        {
            CheckProperties();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string sql = string.Format("INSERT INTO {0} SELECT ", TableName);
            //SELECT 1 as loadId, 
            for (int i = 0; i < StaticValues.Length; i++)
            {
                sql += string.Format("@{0} as {0}, ", StaticValues[i].AsName);
                sqlParameters.Add(new SqlParameter("@" + StaticValues[i].AsName, StaticValues[i].Value));
            }

            //[Internet E-mail] as smtpAddress, 
            //[Lotus Notes mail id] as lotusNotesAddress,
            //[full name] as fullName,
            //[title] as title, 
            //[First Name] as firstName,
            //[Last Name] as lastName,
            //[Business Phone] as businessPhone,
            //[Resource Type] as resourceType,
            //[manager] as manager,
            //[business description] as businessDesc,
            //[Cost Center Code] as costCenterCode,
            //[cost center description] as costCenterDescription,
            //[company designation] as companyDesignation,
            //[Department] as department,
            //[Building Code] as buildingCode,
            //[Building Name] as buildingName,
            //[Mail Drop] as mailDrop,
            //[Facility Street Address] as facilityStreetAddress,
            //[City] as city,
            //[State] as state, 
            //[Country] as country,
            //[Postal Code] as postalCode
            int it = 0;
            foreach (ExcelToDbPair pair in ExcelToDbColumnMap.ColumnPairs)
            {
                sql += string.Format("[{0}] as [{1}]", pair.ExcelColumn, pair.DbColumn);
                if (it != ExcelToDbColumnMap.ColumnPairs.Length - 1)
                    sql += ", ";
                it++;
            }

            //FROM AmexExcel...People$
            sql += string.Format(" FROM {0}...{1}", ExcelLinkedServerName, SheetName);

            DatabaseUtility.ExecuteSql(sql, sqlParameters.ToArray());
            
        }
    }
}

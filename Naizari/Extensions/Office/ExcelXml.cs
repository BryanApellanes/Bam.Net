/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using Naizari.Data.Access;
using System.Text;
using System.Reflection;
using System.Data;

namespace Naizari.Extensions.Office
{
    public static class ExcelXml
    {
        public static string ToXMLSS(DataTable data)
        {
            return ToXMLSS(data, string.Empty);
        }

        public static string ToXMLSS(System.Data.DataTable data, string workSheetName)
        {
            if (string.IsNullOrEmpty(workSheetName))
                workSheetName = "Sheet1";

            StringBuilder output = new StringBuilder();
            OpenXMLSS(workSheetName, output);

            if (data.Rows.Count > 0)
            {
                output.AppendLine(MakeHeader(data));

                foreach (DataRow row in data.Rows)
                {
                    List<string> rowValues = new List<string>();
                    foreach (DataColumn column in data.Columns)
                    {
                        rowValues.Add(row[column].ToString());
                    }

                    output.AppendLine(MakeRow(rowValues.ToArray()));
                }
            }
            CloseXMLSS(output);

            return output.ToString();
        }

        /// <summary>
        /// Output data about the objects in the specified array.  All objects should be of the same type
        /// or at least be objects with identical properties to ensure proper column alignment.
        /// </summary>
        /// <param name="args">the array of objects to ouput properties of</param>
        /// <returns>An Excel readable XMLSS string.  When written to a file later versions of 
        /// Excel should natively recognize the xml as a valid Excel readable spreadsheet.</returns>
        public static string ToXMLSS(object[] args)
        {
            return ToXMLSS(args, string.Empty);
        }

        /// <summary>
        /// Output data about the objects in the specified array.  All objects should be of the same type
        /// or at least be objects with identical properties to ensure proper column alignment.
        /// </summary>
        /// <param name="args">the array of objects to ouput properties of</param>
        /// <returns>An Excel readable XMLSS string.  When written to a file later versions of 
        /// Excel should natively recognize the xml as a valid Excel readable spreadsheet.</returns>
        public static string ToXMLSS(object[] args, string workSheetName)
        {
            if (string.IsNullOrEmpty(workSheetName))
                workSheetName = "Sheet1";

            StringBuilder output = new StringBuilder();
            OpenXMLSS(workSheetName, output);

            Type type = null;
            if (args.Length > 0)
            {
                type = args[0].GetType();
                output.AppendLine(MakeHeader(type));

                foreach (object arg in args)
                {
                    ExcelCellWrapper obj = arg as ExcelCellWrapper;
                    object valueHolder = obj == null ? arg : obj.Object;

                    List<string> propValues = new List<string>();
                    List<string> styles = new List<string>();

                    PropertyInfo[] props = type.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetCustomAttributes(typeof(DaoIgnore), true).Length > 0)
                            continue;

                        object propVal = prop.GetValue(valueHolder, null);

                        propValues.Add(propVal == null ? "": propVal.ToString());

                        if( obj != null )
                            styles.Add(obj.GetStyle(prop.Name));
                    }

                    output.AppendLine(MakeRow(propValues.ToArray(), styles.ToArray()));
                }
            }
            CloseXMLSS(output);

            return output.ToString();
        }

        public static void CloseXMLSS(StringBuilder output)
        {
            output.AppendLine("</ss:Table>");
            output.AppendLine("</ss:Worksheet>");
            output.AppendLine("</ss:Workbook>");
        }

        public static void OpenXMLSS(string workSheetName, StringBuilder output)
        {
            output.AppendLine("<?xml version=\"1.0\"?>");
            output.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            output.AppendLine("<ss:Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            output.AppendLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            output.AppendLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            output.AppendLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            output.AppendLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            output.AppendLine("<Styles>");
            output.AppendLine("  <Style ss:ID=\"red\">");
            output.AppendLine("     <Font ss:Color=\"#FFFF00\"/>");
            output.AppendLine("     <Interior ss:Color=\"#FF0000\" ss:Pattern=\"Solid\"/>");
            output.AppendLine("  </Style>");
            output.AppendLine("</Styles>");
            output.AppendFormat("<ss:Worksheet ss:Name=\"{0}\">", workSheetName);
            output.AppendLine("<ss:Table>");
        }

        private static string MakeHeader(DataTable data)
        {
            List<string> headers = new List<string>();
            foreach (DataColumn column in data.Columns)
            {
                headers.Add(column.ColumnName);
            }

            return MakeRow(headers.ToArray());
        }

        public static string MakeHeader(Type type)
        {
            PropertyInfo[] props = type.GetProperties();
            List<string> headers = new List<string>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetCustomAttributes(typeof(DaoIgnore), true).Length > 0)
                    continue;

                headers.Add(prop.Name);
            }

            return MakeRow(headers.ToArray());
        }

        private static string MakeRow(string[] cellContents)
        {
            return MakeRow(cellContents, new string[]{});
        }

        private static string MakeRow(string[] cellContents, string[] styles)
        {
            bool doStyles = cellContents.Length == styles.Length;

            StringBuilder output = new StringBuilder();
            output.AppendLine("<ss:Row>");
            for(int i = 0; i < cellContents.Length; i++)
            {
                if( doStyles )
                    output.AppendLine(GetCell(cellContents[i], styles[i]));
                else
                    output.AppendLine(GetCell(cellContents[i]));
            }
            output.AppendLine("</ss:Row>");

            return output.ToString();
        }

        private static string GetCell(string cell)
        {
            return GetCell(cell, null);
        }

        private static string GetCell(string cell, string styleId)
        {
            string style = string.Empty;
            if (!string.IsNullOrEmpty(styleId))
                style = string.Format(" ss:StyleID=\"{0}\"", styleId);
            return string.Format("<ss:Cell{0}><ss:Data ss:Type=\"String\">{1}</ss:Data></ss:Cell>", style, cell);
        }
    }

}

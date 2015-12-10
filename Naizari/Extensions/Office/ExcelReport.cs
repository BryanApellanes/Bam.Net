/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Naizari.Extensions.Office.Excel;
using Naizari.Data.Access;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Naizari.Extensions.Office
{
    public class ExcelReport
    {
        Dictionary<string, ExcelStyle> styles;
        Dictionary<string, ExcelWorkSheet> worksheets;
        bool isDao;
        bool includeId;
        Dictionary<string, string> ignoreProperties;
        ExcelStyle headStyle;

        public ExcelReport()
        {
            styles = new Dictionary<string, ExcelStyle>();
            worksheets = new Dictionary<string, ExcelWorkSheet>();
            ignoreProperties = new Dictionary<string, string>();
            isDao = true;
            includeId = false;
        }        

        public class ExcelStyle
        {
            public static ExcelStyle New(string fontColor, string interiorColor, string id)
            {
                ExcelStyle ret = new ExcelStyle();
                ret.FontColor = fontColor;
                ret.InteriorColor = interiorColor;
                ret.Id = id;
                return ret;
            }
            public string FontColor;
            public string InteriorColor;
            public string Id;
        }

        public void Ignore(string propertyName)
        {
            this.ignoreProperties.Add(propertyName, propertyName);
        }

        public string[] IgnoreProperties
        {
            get 
            {
                string[] ret = new string[ignoreProperties.Keys.Count];
                ignoreProperties.Keys.CopyTo(ret, 0);
                return ret;
            }
            //set
            //{
            //    foreach (string val in value)
            //    {
            //        ignoreProperties.Add(val, val);
            //    }
            //}
        }

        public bool IsDao
        {
            get { return isDao; }
            set { isDao = value; }
        }

        public bool IncludeId
        {
            get { return includeId; }
            set { includeId = value; }
        }

        public void Save(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(ToXmlSS());
            }
        }

        public string ToXmlSS()
        {
            StringBuilder builder = new StringBuilder();
            OpenXMLSS(builder);
            CloseXMLSS(builder);
            return builder.ToString();
        }

        public ExcelStyle AddStyle(string fontColor, string interiorColor, string id)
        {
            ExcelStyle ret = ExcelStyle.New(fontColor, interiorColor, id);
            styles.Add(id, ret);
            return ret;
        }

        public ExcelStyle SetHeadStyle(string fontColor, string interiorColor)
        {
            headStyle = AddStyle(fontColor, interiorColor, "head");
            return headStyle;
        }

        #region AddWorksheet
        int sheetNum = 1;

        public void AddWorksheet(object[] rows)
        {
            AddWorksheet(ExcelCellWrapper.FromArray(rows));
        }

        public void AddWorksheet(ExcelCellWrapper[] rows)
        {
            AddWorksheet(rows, "Sheet" + sheetNum++.ToString());
        }

        public void AddWorksheet(ExcelCellWrapper[] rows, string worksheetName)
        {
            AddWorksheet(rows, worksheetName, string.Empty, new Dictionary<string,string>());
        }

        public void AddWorksheet(ExcelCellWrapper[] rows, string worksheetName, string headerStyle, Dictionary<string, string> headerMap)
        {
            AddWorksheet(rows, worksheetName, headerStyle, headerMap, true);
        }

        public ExcelWorkSheet AddWorksheet(ExcelCellWrapper[] rows, string worksheetName, string headerStyle, Dictionary<string, string> headerMap, bool isDao)
        {
            foreach (ExcelCellWrapper cell in rows)
            {
                ValidateStyles(cell);
            }

            ExcelWorkSheet worksheet = new ExcelWorkSheet(rows, headerMap);
            worksheet.WorkSheetName = worksheetName;
            worksheet.objects = rows;
            worksheet.headerStyle = headerStyle;
            worksheet.isDao = isDao;
            worksheets.Add(worksheetName, worksheet);
            return worksheet;
        }

        private void ValidateStyles(ExcelCellWrapper item)
        {
            //bool retVal = true;
            PropertyInfo[] properties = item.Object.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string style = item.GetStyle(property.Name);
                if (!string.IsNullOrEmpty(style) &&
                    !this.styles.ContainsKey(style))
                    item.SetStyle(property.Name, string.Empty);
            }

            //return retVal;
        }
        #endregion

        #region ExcelWorkSheet
        public class ExcelWorkSheet
        {
            public string WorkSheetName;
            public ExcelCellWrapper[] objects;
            public bool isDao;
            public bool includeId;
            public string headerStyle;
            public Dictionary<string, string> ignoreProperties;

            public ExcelWorkSheet(ExcelCellWrapper[] objects)
            {
                this.objects = objects;
                isDao = true;
            }

            public ExcelWorkSheet(ExcelCellWrapper[] objects, Dictionary<string, string> headerMap)
                : this(objects)
            {
                this.headerMap = headerMap;
            }

            //private void TermTrace(string message)
            //{
            //    if (WorkSheetName.Equals("AmexTerms"))
            //    {
            //        Logging.LogManager.CurrentLog.AddEntry("TERMTRACE: " + message);
            //    }
            //}

            public void Ignore(string propertyName)
            {
                if (ignoreProperties == null)
                    ignoreProperties = new Dictionary<string, string>();
                ignoreProperties.Add(propertyName, propertyName);
            }

            public string ToXml()
            {
                //TermTrace("ToXml started...");
                if (objects.Length > 0)
                {
                    //TermTrace(string.Format("{0} terms", objects.Length.ToString()));
                    StringBuilder builder = new StringBuilder();
                    Type type = objects[0].Object.GetType();
                    builder.AppendLine(MakeHeader(type));
                    //PropertyInfo[] properties = type.GetProperties();
                    
                        
                    foreach (ExcelCellWrapper cell in objects)
                    {
                        builder.AppendLine(MakeRow(cell));
                    }

                    return builder.ToString();
                }
                else
                    return string.Empty;
            }

            Dictionary<string, string> headerMap;

            public Dictionary<string, string> HeaderMap
            {
                get { return headerMap; }
                set { headerMap = value; }
            }

            private string MakeHeader(Type type)
            {
                return MakeHeader(type, this.headerMap);
            }

            private string MakeHeader(Type type, Dictionary<string, string> headerMap)
            {
                //TermTrace("Type: " + type.ToString());
                PropertyInfo[] props = type.GetProperties();
                List<string> headers = new List<string>();
                List<string> headStyles = new List<string>();
                foreach (PropertyInfo prop in props)
                {
                    //TermTrace("Should Ignore property " + prop.Name + ": " + ShouldIgnoreProperty(prop).ToString());
                    if (ShouldIgnoreProperty(prop))
                        continue;
                    if (headerMap != null && headerMap.ContainsKey(prop.Name))
                        headers.Add(headerMap[prop.Name]);
                    else
                        headers.Add(prop.Name);

                    headStyles.Add(headerStyle);
                }

                return MakeRow(headers.ToArray(), headStyles.ToArray());
            }

            private bool ShouldIgnoreProperty(PropertyInfo prop)
            {
                bool isDaoColumn = prop.GetCustomAttributes(typeof(DaoColumn), true).Length == 1;
                bool isDaoIdColumn = prop.GetCustomAttributes(typeof(DaoIdColumn), true).Length == 1;

                bool retVal = false;
                if (isDao)
                {
                    //TermTrace("isDao was true");
                    if (!isDaoColumn && !isDaoIdColumn)
                        retVal = true;
                    
                    if( isDaoIdColumn && !includeId )
                        retVal = true;
                }

                if (!retVal)
                {
                    if (ignoreProperties.ContainsKey(prop.Name))
                    {
                        //TermTrace(prop.Name + " was in the ignoreProperties");
                        retVal = true;
                    }
                }
                return retVal;
            }

            private string MakeRow(ExcelCellWrapper cell)
            {
                List<string> cellContents = new List<string>();
                List<string> styles = new List<string>();
                foreach (PropertyInfo property in cell.Object.GetType().GetProperties())
                {
                    if (ShouldIgnoreProperty(property))
                        continue;                    

                    object propVal = property.GetValue(cell.Object, null);
                    cellContents.Add(propVal == null ? "": propVal.ToString());//.ToString());
                    styles.Add(cell.GetStyle(property.Name));
                }

                return MakeRow(cellContents.ToArray(), styles.ToArray());
            }

            private string MakeRow(string[] cellContents, string[] styles)
            {
                StringBuilder output = new StringBuilder();
                output.AppendLine("<ss:Row>");
                for (int i = 0; i < cellContents.Length; i++)
                {
                    output.AppendLine(GetCell(cellContents[i], styles[i]));
                }
                output.AppendLine("</ss:Row>");
                return output.ToString();
            }

            private string GetCell(string cell)
            {
                return GetCell(cell, null);
            }

            private string GetCell(string cell, string styleId)
            {
                string style = string.Empty;
                if (!string.IsNullOrEmpty(styleId))
                    style = string.Format(" ss:StyleID=\"{0}\"", styleId);
                return string.Format("<ss:Cell{0}><ss:Data ss:Type=\"String\">{1}</ss:Data></ss:Cell>", style, cell);
            }
        }
        #endregion

        #region XmlRendering
        private void CloseXMLSS(StringBuilder output)
        {
            output.AppendLine("</ss:Workbook>");
        }

        private void OpenXMLSS(StringBuilder output)
        {
            output.AppendLine("<?xml version=\"1.0\"?>");
            output.AppendLine("<?mso-application progid=\"Excel.Sheet\"?>");
            output.AppendLine("<ss:Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            output.AppendLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            output.AppendLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            output.AppendLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            output.AppendLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            output.AppendLine("<Styles>");
            foreach (ExcelStyle style in styles.Values)
            {
                output.AppendFormat("  <Style ss:ID=\"{0}\">\r\n", style.Id);
                output.AppendFormat("     <Font ss:Color=\"{0}\"/>\r\n", style.FontColor);
                output.AppendFormat("     <Interior ss:Color=\"{0}\" ss:Pattern=\"Solid\"/>\r\n", style.InteriorColor);
                output.AppendLine("  </Style>");
            }
            output.AppendLine("</Styles>");

            foreach (ExcelWorkSheet worksheet in worksheets.Values)
            {
                //worksheet.isDao = this.isDao;
                worksheet.includeId = this.includeId;
                if( worksheet.ignoreProperties == null )
                    worksheet.ignoreProperties = this.ignoreProperties;
                if (headStyle != null)
                    worksheet.headerStyle = this.headStyle.Id;
                output.AppendFormat("<ss:Worksheet ss:Name=\"{0}\">", worksheet.WorkSheetName);
                output.AppendLine();
                output.AppendLine("<ss:Table>");
                output.AppendLine(worksheet.ToXml());
                output.AppendLine("</ss:Table>");
                output.AppendLine("</ss:Worksheet>");
            }
            //output.AppendFormat("<ss:Worksheet ss:Name=\"{0}\">", workSheetName);
            //output.AppendLine("<ss:Table>");
        }
        #endregion
    }
}

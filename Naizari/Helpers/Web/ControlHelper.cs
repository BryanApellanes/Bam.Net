/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
//using AjaxControlToolkit;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using Naizari.Testing;
using System.IO;
using Naizari.Extensions;
using System.Reflection;

namespace Naizari.Helpers.Web
{
    public static class ControlHelper
    {
        public static HtmlGenericControl ToScript(this string script)
        {
            return CreateScriptControl(script);
        }

        public static HtmlGenericControl CreateScriptControl(this string script)
        {
            HtmlGenericControl scriptControl = CreateControl("script", script);
            scriptControl.Attributes.Add("type", "text/javascript");
            scriptControl.Attributes.Add("language", "javascript");
            return scriptControl;
        }

        public static HtmlGenericControl CreateControl(string tagName, string literalText)
        {
            return CreateControl(tagName, string.Empty, literalText);
        }

        public static HtmlGenericControl CreateControl(string tagName, string clientId, string literalText)
        {
            HtmlGenericControl retVal = new HtmlGenericControl(tagName);
            if (!string.IsNullOrEmpty(clientId.Trim()))
                retVal.Attributes.Add("id", clientId);
            retVal.Controls.Add(new LiteralControl(literalText));
            return retVal;
        }

        public static HtmlGenericControl CreateControl(string tagName, string clientId, object attributes)
        {
            HtmlGenericControl retVal = new HtmlGenericControl(tagName);
            Type t = attributes.GetType();
            foreach (PropertyInfo prop in t.GetProperties())
            {
                retVal.Attributes.Add(prop.Name, prop.GetValue(attributes, null).ToString());
            }

            return retVal;
        }

        public static HtmlGenericControl CreateImage(string src, string alt)
        {
            HtmlGenericControl retVal = new HtmlGenericControl("img");
            retVal.Attributes.Add("src", src);
            retVal.Attributes.Add("alt", alt);
            return retVal;
        }

        public static HtmlGenericControl Div(this string text)
        {
            return NewDiv(text, "");
        }

        public static HtmlGenericControl Div(this string text, string cssClass)
        {
            return NewDiv(text, cssClass);
        }

        public static HtmlGenericControl NewDiv(Control control, string cssClass)
        {
            HtmlGenericControl div = NewDiv("", cssClass);
            div.Controls.Add(control);
            return div;
        }

        public static HtmlGenericControl NewDiv(string text, string cssClass)
        {
            HtmlGenericControl retVal = NewSpan(text, cssClass);
            retVal.TagName = "div";
            return retVal;
        }

        public static HtmlGenericControl NewSpan(string text, string cssClass)
        {
            HtmlGenericControl span = new HtmlGenericControl();
            span.TagName = "span";
            span.InnerText = text;
            if (!string.IsNullOrEmpty(cssClass))
            {
                span.Attributes.Add("class", cssClass);
            }
            return span;
        }

        public static HtmlGenericControl ToDiv(this string text)
        {
            return ToDiv(text, string.Empty);
        }

        public static HtmlGenericControl ToDiv(this string text, string cssClass)
        {
            return NewDiv(text, cssClass);
        }

        public static HtmlGenericControl ToSpan(this string text)
        {
            return ToSpan(text, string.Empty);
        }

        public static HtmlGenericControl ToSpan(this string text, string cssClass)
        {
            return NewSpan(text, cssClass);
        }

        public static HtmlTable CreateHtmlTable(int rowCount, int columnCount)
        {
            HtmlTable table = new HtmlTable();
            for (int i = 0; i < rowCount; i++)
            {
                HtmlTableRow row = new HtmlTableRow();
                for (int ii = 0; ii < columnCount; ii++)
                {
                    HtmlTableCell cell = new HtmlTableCell();
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static HtmlGenericControl NewDiv(string domId)
        {
            return CreateHtmlDiv(domId);
        }

        public static HtmlGenericControl CreateHtmlDiv(string domId)
        {
            HtmlGenericControl retVal = new HtmlGenericControl("div");
            retVal.Attributes.Add("id", domId);
            return retVal;
        }
        /// <summary>
        /// Used to validate the specified string.  If the specified dimension string ends with "px" or "%"
        /// the input is returned.  If the specified string can be parsed as an int "px" is appended to the string
        /// and returned, otherwise an InvalidOperationException is thrown.
        /// </summary>
        /// <param name="dimensionString">The string to validate</param>
        /// <returns>string</returns>
        public static string GetCssDimension(string dimensionString)
        {
            string retVal;
            if (dimensionString.Trim().EndsWith("px") || dimensionString.Trim().EndsWith("%"))
            {
                retVal = dimensionString;
            }
            else
            {
                int dimension;
                if (int.TryParse(dimensionString.Trim(), out dimension))
                {
                    retVal = dimension + "px";
                }
                else
                {
                    throw ExceptionHelper.CreateException<InvalidOperationException>("Invalid dimension string specified: {0}", dimensionString);
                }
            }
            return retVal;
        }

        /// <summary>
        /// Creates a bare select element without any attributes.  You should specify the id 
        /// after calling this method and add any other attributes as desired.
        /// </summary>
        /// <param name="dictionary">Key value pairs to create the dropdown from</param>
        /// <returns></returns>
        public static HtmlGenericControl CreateSelect(this Dictionary<string, string> dictionary)
        {
            return CreateSelect(dictionary, string.Empty);
        }

        /// <summary>
        /// Creates a bare select element without any attributes.  You should specify the id 
        /// after calling this method and add any other attributes as desired.
        /// </summary>
        /// <param name="dictionary">Key value pairs to create the dropdown from</param>
        /// <param name="selected">A string matching a value in the Dictionary to be the default selected value</param>
        /// <returns></returns>
        public static HtmlGenericControl CreateSelect(this Dictionary<string, string> dictionary, string selected)
        {
            HtmlGenericControl select = new HtmlGenericControl("select");
            foreach (string key in dictionary.Keys)
            {
                HtmlGenericControl option = new HtmlGenericControl("option");
                option.Attributes.Add("value", key);
                option.InnerText = dictionary[key];
                if (dictionary[key].Equals(selected))
                {
                    option.Attributes.Add("selected", "selected");
                }
                select.Controls.Add(option);
            }

            return select;
        }

        public static void AddControlToHtmlTable(HtmlTable tableToAddTo, Control controlToAdd, int row, int column)
        {
            HtmlTableCell tableCell = GetCell(tableToAddTo, row, column);

            tableCell.Controls.Add(controlToAdd);            
        }

        public static void SetCellContent(this HtmlTable tableToAddTo, string content, int row, int column)
        {
            HtmlTableCell tableCell = GetCell(tableToAddTo, row, column);
            tableCell.SetContent(content);
        }

        public static void SetContent(this HtmlTableCell cell, string content)
        {
            cell.InnerHtml = content;
        }

        public static HtmlTableCell GetCell(this HtmlTable tableToAddTo, int row, int column)
        {
            Expect.IsGreaterThanOrEqualTo(tableToAddTo.Rows.Count, row); // verify row validity
            HtmlTableRow tableRow = tableToAddTo.Rows[row - 1];

            Expect.IsGreaterThanOrEqualTo(tableRow.Cells.Count, column); // verify column validity            
            HtmlTableCell tableCell = tableRow.Cells[column - 1];
            return tableCell;
        }

        public static string GetHtmlString(Control control)
        {
            MemoryStream renderedControlText = new MemoryStream();
            StreamWriter sw = new StreamWriter(renderedControlText);
            HtmlTextWriter textWriter = new HtmlTextWriter(sw);

            control.RenderControl(textWriter);
            textWriter.Flush();

            byte[] htmlBytes = new byte[renderedControlText.Length];
            renderedControlText.Seek(0, 0);

            renderedControlText.Read(htmlBytes, 0, (int)renderedControlText.Length);
            return Encoding.UTF8.GetString(htmlBytes);
        }

        /// <summary>
        /// Same as GetHtmlString.  Aliased here as a reminder that even if the control doesn't 
        /// render HTML the text result of the specified controls RenderControl method is 
        /// returned.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static string GetRenderedString(this Control control)
        {
            return GetHtmlString(control);
        }

        public static T AddBreak<T>(this T control) where T : Control
        {
            control.Controls.Add(new LiteralControl("<br />"));
            return control;
        }

        public delegate HtmlGenericControl CreateListDelegate<T>(T item);

        public static HtmlGenericControl CreateBulletList<T>(T[] items, CreateListDelegate<T> itemFormatter)
        {
            return CreateList<T>(items, "ul", itemFormatter);
        }

        public static HtmlGenericControl CreateNumberedList<T>(T[] items, CreateListDelegate<T> itemFormatter)
        {
            return CreateList<T>(items, "ol", itemFormatter);
        }

        private static HtmlGenericControl CreateList<T>(T[] items, string tag, CreateListDelegate<T> itemFormatter)
        {
            HtmlGenericControl retVal = CreateControl(tag, StringExtensions.RandomString(4, false, false), string.Empty);
            foreach (T item in items)
            {
                retVal.Controls.Add(itemFormatter(item));
            }

            return retVal;
        }
    }
}

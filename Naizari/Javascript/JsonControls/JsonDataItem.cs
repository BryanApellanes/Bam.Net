/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

[assembly: TagPrefix("Naizari.Javascript.JsonControls", "json")]
namespace Naizari.Javascript.JsonControls
{
    public class JsonDataItem: JsonControl
    {
        List<JsonDataItem> subMenuItems;
        public JsonDataItem() : base() 
        {
            this.subMenuItems = new List<JsonDataItem>();
        }

        public JsonDataItem(string text, string value)
            : this()
        {
            this.Text = text;
            this.Value = value;
        }

        public JsonDataItem(string text, string value, string html)
            : this(text, value)
        {
            this.Html = html;
        }
        
        public string Text { get; set; }
        public string Value { get; set; }
        public JsonDataItem[] SubMenuItems 
        {
            get
            {
                return this.subMenuItems.ToArray();
            }
        }

        public object UserData { get; set; }
        public string Html { get; set; }
        public void AddSubMenuItem(JsonDataItem menuItem)
        {
            this.subMenuItems.Add(menuItem);
        }
    }
}

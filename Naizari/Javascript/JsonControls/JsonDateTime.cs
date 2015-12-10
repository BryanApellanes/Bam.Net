/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Helpers;
using Naizari.Helpers.Web;
using System.Web.UI.HtmlControls;

namespace Naizari.Javascript.JsonControls
{
    public class JsonDateTime: JsonHiddenInput
    {
        public JsonDateTime()
            : base()
        {
            this.DisplayLocalTime = true;           
            this.Text = string.Empty;
            this.Value = string.Empty;
        }

        public JsonDateTime(DateTime time)
            : this()
        {
            this.Time = time;
        }

        public JsonDateTime(DateTime time, string jsonId)
            : this(time)
        {
            this.JsonId = jsonId;
        }

        public JsonDateTime(DateTime time, string jsonId, string cssClass)
            : this(time, jsonId)
        {
            this.CssClass = cssClass;
        }


        public DateTime Time { get; set; }

        bool local;
        public bool DisplayLocalTime 
        {
            get { return local; }
            set
            {
                local = value;
                utc = !value;
            }
        }
        
        bool utc;
        public bool DisplayUtcTime
        {
            get { return utc; }
            set
            {
                utc = value;
                local = !value;
            }
        }

        public int Month
        {
            get { return this.Time.Month; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(current.Year, value, current.Day, current.Hour, current.Minute, current.Second);
                this.Time = newTime;
            }
        }

        public int DayOfMonth
        {
            get { return this.Time.Day; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(current.Year, current.Month, value, current.Hour, current.Minute, current.Second);
                this.Time = newTime;
            }
        }

        public int Year
        {
            get { return this.Time.Year; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(value, current.Month, current.Day, current.Hour, current.Minute, current.Second);
                this.Time = newTime;
            }
        }

        public int Hour
        {
            get { return this.Time.Hour; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(current.Year, current.Month, current.Day, value, current.Minute, current.Second);
                this.Time = newTime;
            }
        }

        public int Minute
        {
            get { return this.Time.Minute; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(current.Year, current.Month, current.Day, current.Hour, value, current.Second);
                this.Time = newTime;
            }
        }

        public int Second
        {
            get { return this.Time.Second; }
            set
            {
                DateTime current = this.Time;
                DateTime newTime = new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute, value);
                this.Time = newTime;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //ControlHelper.NewSpan("To UTC String:" + this.Time.ToString(
            //ControlHelper.NewSpan("To String:" + this.Time.ToShortDateString() + " " + this.Time.ToLongTimeString(), "label").RenderControl(writer);
            HtmlGenericControl text = ControlHelper.NewSpan("", this.CssClass);
            text.Attributes.Add("id", this.DomId + "_text");
            text.RenderControl(writer);
            base.Render(writer);
            
            //if(this.RenderScripts)
            //    this.RenderConglomerateScript(writer);
        }

    }
}

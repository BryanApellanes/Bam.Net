/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using Naizari.Helpers.Web;

namespace Naizari.Javascript.JsonControls
{
    public class JsonTabStrip: JsonControl
    {
        public JsonTabStrip()
            : base()
        {
            controlToRender.TagName = "div";
            this.tabs = new List<JsonTab>();
            this.BackgroundColor = "#FFFFFF";
            this.TabLineColor = "#000000";
            this.TextPadding = 10;
            this.TabCornerRadius = 15;
            this.TabVerticalMargin = 3;
            this.TabLineWidth = 1;
            this.TabHeight = 20;
            this.TabColor = "#787878";
            this.MouseOverTabColor = "#9A9A9A";
            this.MouseOverLineColor = this.MouseOverTabColor;
            this.SelectedColor = "#FFFFFF";
            this.SelectedIndex = -1;

            this.IE6Width = 675;

            this.clickActions = new List<ClientClickAction>();
        }

        List<JsonTab> tabs;
        public JsonTab[] Tabs
        {
            get
            {
                return this.tabs.ToArray();
            }
        }

        public void AddTab(string headerText)
        {
            this.AddTab(new JsonTab(headerText));
        }

        public void AddTab(JsonTab tab)
        {
            tabs.Add(tab);
        }

        public string TabHeaders { get; set; }
        public string TabColor { get; set; }
        public string TabColors { get; set; }
        public string TabLineColor { get; set; }
        public int TextPadding { get; set; }
        public string TabCssClass { get; set; }
        string tabColorsMouseover;
        /// <summary>
        /// A comma or semi-colon separated list of html hex colors.  The specified
        /// colors will be used as the tab fill color when the mouse hovers over the 
        /// tab.  The same as 
        /// MouseOverTabColors.
        /// </summary>
        public string TabColorsMouseOver 
        {
            get
            {
                return tabColorsMouseover;
            }
            set
            {
                this.tabColorsMouseover = value;
            }
        }

        List<ClientClickAction> clickActions;
        [Description("A comma separated list of colin separated name value pairs representing" +
            " the client click actions for each tab where the name for each pair is either \"Script\" " +
            " or \"Navigate\" and the value is the name of a script function or the root relative path" +
            " of a page to navigate to.")]
        public string ClickActions
        {
            get
            {
                return StringExtensions.ToDelimited((object[])clickActions.ToArray(), ",");
            }
            set
            {
                this.clickActions.Clear();
                foreach (string nameValue in StringExtensions.DelimitSplit(value, ","))
                {
                    this.clickActions.Add(new ClientClickAction(nameValue));
                }
            }
        }
        /// <summary>
        /// A comma or semi-colon separated list of html hex colors.  The specified
        /// colors will be used as the tab fill color when the mouse hovers over the 
        /// tab. The same as TabColorsMouseOver.
        /// </summary>
        public string MouseOverTabColors
        {
            get
            {
                return tabColorsMouseover;
            }
            set
            {
                this.tabColorsMouseover = value;
            }
        }

        public string MouseOverTabColor
        {
            get;
            set;
        }
        public string MouseOverLineColor { get; set; }
        public int TabLineWidth { get; set; }
        public string TabLineWidths { get; set; }
        public string ContentDomIds { get; set; }
        public string BackgroundColor { get; set; }
        public string TabHeights { get; set; }
        public int TabSpacing { get; set; }
        public int TabCornerRadius { get; set; }
        public int SelectedIndex { get; set; }
        public string SelectedColor { get; set; }
        public string SelectedCssClass { get; set; }
        public int TabVerticalMargin { get; set; }
        public int TabHeight { get; set; }
        public int IE6Width { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.WireScriptsAndValidate();
        }

        bool wired;
        public override void WireScriptsAndValidate()
        {
            if (!wired)
            {
                List<string> tabHeaders = new List<string>(); ;
                if (!string.IsNullOrEmpty(this.TabHeaders))
                    tabHeaders = new List<string>(StringExtensions.DelimitSplit(this.TabHeaders, new string[] { ",", ";" }, true));

                List<string> tabColors = new List<string>();
                if (!string.IsNullOrEmpty(this.TabColors))
                    tabColors = new List<string>(StringExtensions.DelimitSplit(this.TabColors, new string[] { ",", ";" }, true));

                List<string> tabLineWidths = new List<string>();
                if (!string.IsNullOrEmpty(this.TabLineWidths))
                    tabLineWidths = new List<string>(StringExtensions.DelimitSplit(this.TabLineWidths, new string[] { ",", ";" }, true));

                List<string> tabMouseOverFillColors = new List<string>();
                if (!string.IsNullOrEmpty(this.TabColorsMouseOver))
                    tabMouseOverFillColors = new List<string>(StringExtensions.DelimitSplit(this.TabColorsMouseOver, new string[] { ",", ";" }, true));

                List<string> tabHeights = new List<string>();
                if (!string.IsNullOrEmpty(this.TabHeights))
                    tabHeights = new List<string>(StringExtensions.DelimitSplit(this.TabHeights, new string[] { ",", ";" }, true));

                for (int i = 0; i < tabHeaders.Count; i++)
                {
                    JsonTab tab = new JsonTab(tabHeaders[i]);
                    tab.TextPadding = this.TextPadding;
                    tab.HorizontalMargin = this.TabSpacing;
                    tab.CornerRadius = this.TabCornerRadius;
                    tab.VerticalMargin = this.TabVerticalMargin;
                    tab.LineColor = this.TabLineColor;
                    tab.MouseOverLineColor = this.MouseOverLineColor;

                    if (!string.IsNullOrEmpty(this.TabCssClass))
                        tab.CssClass = this.TabCssClass;

                    if (this.SelectedIndex == i)
                    {
                        tab.Selected = true;
                        if (!string.IsNullOrEmpty(this.SelectedCssClass))
                            tab.CssClass = this.SelectedCssClass;
                    }

                    tab.MouseOverBackgroundColor = this.BackgroundColor;
                    tab.BackgroundColor = this.BackgroundColor;

                    if (this.clickActions.Count > i)
                    {
                        tab.ClickActionTarget = this.clickActions[i].Target;
                        tab.ClickActionType = this.clickActions[i].ActionType;
                    }

                    if (tabMouseOverFillColors.Count > i)
                    {
                        tab.MouseOverFillColor = tabMouseOverFillColors[i];
                    }
                    else
                    {
                        if (tab.Selected)
                            tab.MouseOverFillColor = this.SelectedColor;
                        else
                            tab.MouseOverFillColor = this.MouseOverTabColor;
                    }

                    if (tabColors.Count > i)
                    {
                        tab.FillColor = tabColors[i];
                    }
                    else
                    {
                        if (tab.Selected)
                            tab.FillColor = this.SelectedColor;
                        else
                            tab.FillColor = this.TabColor;
                    }

                    if (tabLineWidths.Count > i)
                        tab.LineWidth = Convert.ToInt32(tabLineWidths[i]);
                    else
                        tab.LineWidth = this.TabLineWidth;

                    if (tabHeights.Count > i && !tab.Selected)
                    {
                        int height = -1;
                        if (int.TryParse(tabHeights[i], out height))
                        {
                            tab.TabHeight = height;
                        }
                    }
                    else if (tab.Selected)
                    {
                        tab.TabHeight = this.TabHeight + 5;
                    }
                    else
                    {
                        tab.TabHeight = this.TabHeight;
                    }

                    tab.WireScriptsAndValidate();
                    tab.RenderScripts = false;
                    foreach (JsonFunction function in tab.Scripts)
                    {
                        this.AddJsonFunction(function);
                    }

                    this.AddTab(tab);
                }

                //if (this.tabs.Count >= this.SelectedIndex)
                //{
                //    this.tabs[this.SelectedIndex].Selected = true;
                //    this.tabs[this.SelectedIndex].TabHeight += 5;
                //    this.tabs[this.SelectedIndex].FillColor = this.SelectedColor;
                //    this.tabs[this.SelectedIndex].MouseOverFillColor = this.SelectedColor;
                //    this.tabs[this.SelectedIndex].SetMouseOverProperties();
                //}

                for (int i = 1; i < this.tabs.Count; i++)
                {
                    this.tabs[i].IsFirst = false;

                }
                wired = true;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            this.controlToRender.Attributes.Add("id", this.DomId);
            this.controlToRender.Attributes.Add("jsonid", this.JsonId);
            this.controlToRender.Style.Add("white-space", "nowrap");

            if (BrowserAccessHelper.IsIELessThanOrEqualTo(6))
            {
                this.RenderScripts = false;
                HtmlTable table = new HtmlTable();
                table.CellPadding = 0;
                table.CellSpacing = 0;
                HtmlTableRow row = new HtmlTableRow();
                table.Style.Add("width", this.IE6Width + "px");
                table.Style.Add("border", "1px solid " + this.TabLineColor);
                int index = 0;
                bool first = true;
                foreach (JsonTab tab in this.tabs)
                {
                    
                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Style.Add("whitespace", "nowrap");
                    cell.Style.Add("vertical-align", "bottom");
                    cell.Style.Add("padding", this.TabCornerRadius + "px");
                    if (!first)
                        cell.Style.Add("border-left", "1px solid black");
                    
                    HtmlGenericControl text = ControlHelper.NewSpan(tab.Text, "");
                    if (tab.Selected)
                    {
                        cell.Style.Add("background-color", this.SelectedColor);
                    }
                    else
                    {
                        cell.Style.Add("background-color", this.TabColor);
                        cell.Attributes.Add("onmouseover", "this.style.backgroundColor = '" + this.MouseOverTabColor + "'");
                        cell.Attributes.Add("onmouseout", "this.style.backgroundColor = '" + this.TabColor + "'");
                        cell.Style.Add("cursor", "pointer");
                        cell.Style.Add("cursor", "hand");
                        if(this.clickActions.Count > index)
                        {
                            if (this.clickActions[index].ActionType == ClientClickActionType.Navigate)
                            {
                                cell.Attributes.Add("onclick", "window.location = '" + this.clickActions[index].Target + "';");
                            }
                            else
                            {
                                cell.Attributes.Add("onclick", this.clickActions[index].Target);
                            }
                        }
                    }
                    cell.Controls.Add(text);
                    row.Cells.Add(cell);
                    first = false;
                    index++;
                }
                table.Rows.Add(row);
                this.controlToRender.Controls.Add(table);
            }
            else
            {                
                foreach (JsonTab tab in this.tabs)
                {
                    this.controlToRender.Controls.Add(tab);
                }
            }

            this.controlToRender.RenderControl(writer);
            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);        
        }
    }
}

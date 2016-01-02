/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Naizari.Javascript.JsonControls;

namespace Naizari.Javascript.BoxControls
{
    public class JsonContextMenu: Box
    {
        List<MenuItem> menuItems;

        public JsonContextMenu()
            : base()
        {
            this.AutoRegisterScript = true;
            menuItems = new List<MenuItem>();
        }

        public void AddMenuItem(Control control)
        {
            MenuItem item = new MenuItem();
            
            AddMenuItem(item);
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
        }

        public MenuItem[] MenuItems
        {
            get
            {
                return menuItems.ToArray();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {

            controlToRender = JavascriptPage.CreateDiv(this.ID);

            foreach (MenuItem item in this.menuItems)
            {

            }
        }
    }
}

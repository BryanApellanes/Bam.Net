/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Naizari.Extensions;

namespace Naizari.Javascript.JsonControls
{
    public class JsonStyles
    {
        Dictionary<string, string> styles;

        public JsonStyles()
        {
            styles = new Dictionary<string, string>();
        }

        public void Clear()
        {
            styles.Clear();
        }

        public string this[string styleProperty]
        {
            get
            {
                if (styles.ContainsKey(styleProperty))
                    return styles[styleProperty];

                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (styles.ContainsKey(styleProperty))
                        styles[styleProperty] = value;
                    else
                        styles.Add(styleProperty, value);
                }
            }
        }

        public string this[HtmlTextWriterStyle style]
        {
            get
            {
                return this[new StyleConversion(style).CssStyleName];
            }
            set
            {
                this[new StyleConversion(style).CssStyleName] = value;
            }
        }

        public void SetStyle(string style, string value)
        {
            if (styles.ContainsKey(style))
                styles[style] = value;
            else
                styles.Add(style, value);
        }

        /// <summary>
        /// Adds all the styles defined in the current JsonStyles
        /// instance to the specified control's Attributes.CssStyle collection
        /// </summary>
        /// <param name="control"></param>
        internal void AddStyles(HtmlGenericControl control)
        {
            if (styles.Count > 0)
            {
                foreach (string key in styles.Keys)
                {
                    if (!string.IsNullOrEmpty(styles[key].Trim()))
                        control.Attributes.CssStyle.Add(key, styles[key]);
                }
            }
        }

        public override string ToString()
        {
            return DictionaryExtensions.ToDelimited<string, string>(styles, ";", ":");
        }

        public static JsonStyles FromString(string inlineStyles)
        {
            JsonStyles retVal = new JsonStyles();
            retVal.styles = DictionaryExtensions.FromDelimited(inlineStyles, ";", ":");
            return retVal;
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Naizari.Extensions;
using Naizari.Test;
using Naizari.Configuration;

namespace Naizari.Images
{
    [Serializable]
    public class ColorPallette
    {
        [Serializable]
        public class PalletteColor
        {
            public PalletteColor()
            {
            }

            public PalletteColor(string colorName, string htmlHexColor)
            {
                this.ColorName = colorName;
                this.HtmlHexColor = htmlHexColor;
            }

            public string ColorName { get; set; }
            public string HtmlHexColor { get; set; }
        }

        Dictionary<string, PalletteColor> colorsByName;
        public ColorPallette()
        {
            this.colorsByName = new Dictionary<string, PalletteColor>();
            //this.colors = new PalletteColor[] { };
        }

        [XmlIgnore]
        public PalletteColor this[string colorName]
        {
            get
            {
                if (this.colorsByName.ContainsKey(colorName))
                    return this.colorsByName[colorName];
                else
                    return null;
            }
        }

        public PalletteColor[] Colors
        {
            get { return DictionaryExtensions.ValuesToArray<string, PalletteColor>(this.colorsByName); }
            set
            {
                this.colorsByName.Clear();
                foreach (PalletteColor color in value)
                {
                    this.colorsByName.Add(color.ColorName, color);
                }
            }
        }

        public void Add(string colorName, string htmlHexColor)
        {
            Expect.IsTrue(StringExtensions.IsValidHtmlHexColor(htmlHexColor));
            this.colorsByName.Add(colorName, new PalletteColor(colorName, htmlHexColor));
        }

        public void AddMouseOver(string colorName)
        {
            this.AddMouseOver(colorName, 2);
        }

        public void AddMouseOver(string colorName, int shadesBrighter)
        {
            Expect.IsTrue(this.colorsByName.ContainsKey(colorName));
            PalletteColor color = this.colorsByName[colorName];
            string newColorName = colorName + "_mouseover";
            if (this.colorsByName.ContainsKey(newColorName))
                this.colorsByName[newColorName] = new PalletteColor(newColorName, this.Brighten(color.HtmlHexColor));
            else
                this.colorsByName.Add(newColorName, new PalletteColor(newColorName, this.Brighten(color.HtmlHexColor)));
        }

        private static List<char> hexChars = new List<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' });

        public string Brighten(string htmlHexColor)
        {
            return Brighten(htmlHexColor, 2);
        }

        public string Brighten(string htmlHexColor, int shadesBrighter)
        {
            Expect.IsTrue(StringExtensions.IsValidHtmlHexColor(htmlHexColor));
            List<char> input = new List<char>(htmlHexColor.ToCharArray());

            input.RemoveAt(0);
            string retVal = "#";
            foreach (char inputCharacter in input)
            {
                for(int i = 0; i < hexChars.Count; i++)
                {
                    if (hexChars[i].Equals(inputCharacter))
                    {
                        int newIndex = i + shadesBrighter;
                        if (newIndex > hexChars.Count - 1)
                            newIndex = hexChars.Count - 1;

                        retVal += hexChars[newIndex];
                        break;
                    }
                }
            }

            Expect.IsTrue(StringExtensions.IsValidHtmlHexColor(retVal));
            return retVal;
        }
        
        public void Remove(string colorName)
        {
            if (this.colorsByName.ContainsKey(colorName))
                this.colorsByName.Remove(colorName);
        }

        public void Save(string filePath)
        {
            SerializationUtil.XmlSerialize(this, filePath);
        }

        public static ColorPallette Load(string filePath)
        {
            return SerializationUtil.Deserialize<ColorPallette>(filePath);
        }
    }
}

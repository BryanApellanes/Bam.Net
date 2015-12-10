/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Bam.Net;
//using dotless.Core;

namespace Bam.Net.Drawing
{
    [Serializable]
    public class HexColor
    {
        public HexColor()
        {
            this.R = "ff";
            this.G = "ff";
            this.B = "ff";
        }

        public static implicit operator Color(HexColor color)
        {
            return color.ToColor();
        }

        public static explicit operator HexColor(Color color)
        {
            HexColor c = new HexColor();
            byte[] rgb = new byte[] { color.R, color.G, color.B };
            c.Hex = BitConverter.ToString(rgb);
            return c;
        }

        public HexColor(string hex)
            : this()
        {
            this.Hex = hex.Trim().PadRight(7, '0');
        }

        public HexColor(string name, string hex)
            : this(hex)
        {
            this.Name = name;
        }

        public static HexColor Contrast(HexColor color)
        {
            Color sc = (Color)color;
            float yiq = ((sc.R * 299) + (sc.G * 587) + (sc.B * 114)) / 1000;
            return (yiq >= 128) ? Black : White;
        }

        static HexColor _black;
        public static HexColor Black
        {
            get
            {
                if (_black == null)
                {
                    _black = new HexColor("black", "#000000");
                }

                return _black;
            }
        }

        static HexColor _white;
        public static HexColor White
        {
            get
            {
                if (_white == null)
                {
                    _white = new HexColor("white", "#ffffff");
                }

                return _white;
            }
        }

        string _name;
        /// <summary>
        /// Gets or sets the name of the current color.
        /// May be anything useful for remembering this
        /// color and its purpose.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = string.Format("{0}", value.DropLeadingNonLetters());
                if (_name.Length == 0)
                {
                    _name = _name.RandomLetters(5);
                }
            }
        }

        public string LessName()
        {
            return string.Format("@{0}", Name.CamelCase());            
        }

        string _hex;
        public string Hex
        {
            get
            {
                return _hex;
            }
            set
            {
                _hex = value;
                string copy = _hex.Replace("#", "");
                R = copy.Substring(0, 2);
                G = copy.Substring(2, 2);
                B = copy.Substring(4, 2);
            }
        }

        private string R { get; set; }
        private string G { get; set; }
        private string B { get; set; }

        public Color ToColor()
        {
            //remove the # at the front
            string hex = _hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            //convert RGB characters to bytes
            r = Parse(R);
            g = Parse(G);
            b = Parse(B);

            return Color.FromArgb(a, r, g, b);
        }

        private byte Parse(string val)
        {
            return byte.Parse(val, System.Globalization.NumberStyles.HexNumber);
        }
    }
}

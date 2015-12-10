/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Naizari.Extensions
{
    public class SqlSyntaxHighlightMap
    {
        public SqlSyntaxHighlightMap()
        {
            // 1: Keyword (blue)
            // 3: System Function (green)
            // 5: operator (75,75,75)
            // 6: Text (red)
            // 7: Number (cyan)
            // 8: Comment (magenta)
            // 10: Standard Text (black)

            Keyword = Color.Blue;
            SystemFunction = Color.Pink;
            Operator = Color.FromArgb(75,75,75);
            Text = Color.Red;
            Number = Color.Cyan;
            Comment = Color.Magenta;
            StandardText = Color.Black;
        }

        public Color Keyword { get; set; }
        public Color SystemFunction { get; set; }
        public Color Operator { get; set; }
        public Color Text { get; set; }
        public Color Number { get; set; }
        public Color Comment { get; set; }
        public Color StandardText { get; set; }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Presentation.Html
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LabelAttribute: Attribute
    {
        public LabelAttribute(string text)
        {
            this.Text = text;
        }

        public static implicit operator string(LabelAttribute l)
        {
            return l.Text;
        }

        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a br element should be added after the element
        /// </summary>
        public bool PostBreak { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a br element should be added before the element
        /// </summary>
        public bool PreBreak { get; set; }

        public override string ToString()
        {
            return this.Text;
        }
    }
}

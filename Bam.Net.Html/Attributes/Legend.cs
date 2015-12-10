/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Html
{
	/// <summary>
	/// Used to specify the legend to use
	/// when building InputForms or MethodForms
	/// </summary>
    public class Legend: Attribute
    {
        public Legend()
        {
        }

        public string Value { get; set; }
    }
}

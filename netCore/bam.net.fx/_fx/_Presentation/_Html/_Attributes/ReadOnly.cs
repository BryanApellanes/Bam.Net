/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bam.Net.Presentation.Html
{
    public partial class ReadOnly: StringInput
    {
        public override TagBuilder CreateInput()
        {
            return new TagBuilder("span")
                .Name(PropertyName)
                .TextIf(Default != null, (string)Default);
        }
    }
}

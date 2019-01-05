/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;
using System.Web.Mvc;

namespace Bam.Net.Presentation.Html
{
    /// <summary>
    /// Designates that this property when rendered by the FileExtParamsBuilder
    /// should be a drowpdown (select) element.  Must call 
    /// DropDown.SetOptions([nameToUseForDefaultValue], dictionaryOfDefaultValues)
    /// for this to function properly.
    /// </summary>
    public partial class DropDown: StringInput
    {
        public override TagBuilder CreateInput()
        {
            return this.Options.DropDown(Default == null ? string.Empty: Default.ToString());
        }
    }
}

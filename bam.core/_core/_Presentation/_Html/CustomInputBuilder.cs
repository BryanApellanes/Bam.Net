/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bam.Net.Presentation.Html
{
    public delegate TagBuilder CustomInputBuilder(PropertyInfo property);
}

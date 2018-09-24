/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor;
using Bam.Net;
using System.Reflection;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public abstract class TableTemplate : DaoRazorTemplate<Table>
    {
        public SchemaDefinition Schema
        {
            get;
            set;
        }
    }
}

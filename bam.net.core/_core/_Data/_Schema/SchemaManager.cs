using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net;
using Bam.Net.Data;
using System.Web;
using System.CodeDom.Compiler;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Bam.Net.Javascript;
using System.Threading;
using Bam.Net.Razor;
using Bam.Net.Configuration;

namespace Bam.Net.Data.Schema
{
    public partial class SchemaManager // core
    {
        string _rootDir;
        public string RootDir
        {
            get
            {
                return _rootDir ?? SchemaTempPathProvider(CurrentSchema);
            }

            set
            {
                _rootDir = value;
            }
        }
    }
}

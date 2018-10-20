using System;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public partial class DtoModel
    {
        public DtoModel(Dao dao, string nameSpace)
        : this(dao.BuildDynamicType<ColumnAttribute>(), nameSpace)
        { }
    }
}

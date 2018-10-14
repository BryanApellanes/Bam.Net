using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using System.Reflection;
using System.IO;
using System.CodeDom.Compiler;
using Bam.Net.Logging;
using Bam.Net.Configuration;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A generator that will create Dto's from Dao's.
    /// Intended primarily to enable backup of
    /// Daos to an ObjectRepository
    /// </summary>
    public partial class DaoToDtoGenerator // core
    {
        /// <summary>
        /// Write dto source code into the specified namespace placing generated files into the specified directory
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <param name="writeSourceTo"></param>
        public void WriteDtoSource(string nameSpace, string writeSourceTo)
        {
            Args.ThrowIfNull(DaoAssembly, "DaoAssembly");

            foreach (Type daoType in DaoAssembly.GetTypes()
                .Where(t => t.HasCustomAttributeOfType<TableAttribute>()))
            {
                Dto.WriteRenderedDto(nameSpace, writeSourceTo, daoType, pi => pi.HasCustomAttributeOfType<ColumnAttribute>());
            }
        }
    }
}

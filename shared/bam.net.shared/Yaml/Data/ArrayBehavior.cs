using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Yaml.Data
{
    public enum ArrayBehavior
    {
        /// <summary>
        /// If a YamlDataFile is read and has more than one
        /// top level value throw an exception
        /// </summary>
        Throw,
        /// <summary>
        /// If a YamlDataFile is read and has more than one
        /// top level value log a warning
        /// </summary>
        Warn,
        /// <summary>
        /// If a YamlDataFile is read and has more than one
        /// top level value, write each value out to separate
        /// YamlDataFiles
        /// </summary>
        Normalize
    }
}

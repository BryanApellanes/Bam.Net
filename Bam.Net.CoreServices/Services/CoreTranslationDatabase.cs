using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Services
{
    public class CoreTranslationDatabase
    {
        public CoreTranslationDatabase(Database wrapped)
        {
            Database = wrapped;
        }

        public static implicit operator Database(CoreTranslationDatabase ct)
        {
            return ct.Database;
        }

        public Database Database { get; set; }
    }
}

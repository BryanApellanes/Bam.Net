using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.SQLite;

namespace gloo.services
{
    public class ApplicationLogDatabase: SQLiteDatabase
    {
        public ApplicationLogDatabase(string dir) : base(dir, "LocalApplicationLog")
        { }
    }
}

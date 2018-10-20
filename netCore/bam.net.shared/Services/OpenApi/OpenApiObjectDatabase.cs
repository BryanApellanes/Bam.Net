using Bam.Net.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.OpenApi
{
    public class OpenApiObjectDatabase: SQLiteDatabase
    {
        public OpenApiObjectDatabase(): base(".\\OpenApi", "OpenApi")
        {
        }
    }
}

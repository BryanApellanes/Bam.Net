using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;

namespace Bam.Net.Translation
{
    public class LanguageDatabase
    {
        public LanguageDatabase(Database wrapped)
        {
            Database = wrapped;
        }

        public static implicit operator Database(LanguageDatabase ct)
        {
            return ct.Database;
        }

        public Database Database { get; set; }

        static Database _defaultTranslationDb;
        static object _defaultLock = new object();
        public static Database Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _defaultTranslationDb, () => new SQLiteDatabase(".\\", "Language"));
            }
            set
            {
                _defaultTranslationDb = value;
            }
        }
    }
}

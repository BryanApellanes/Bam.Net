using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class TypeInheritanceWriter
    {
        public TypeInheritanceWriter()
        {
            TransactionFormat = @"BEGIN TRANSACTION
{0}
COMMIT";
        }

        public string TransactionFormat { get; set; }
    }
}

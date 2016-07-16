using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Caching
{
    public class DeleteNotSupportedException: Exception
    {
        public DeleteNotSupportedException(string identifier) 
            : base($"CachingRepository does not support the Delete operation, use CachingRepository.SourceRepository.Delete if deleting is required\r\n\t{identifier}")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Files
{
    public interface IChunkable
    {
        IChunk ToChunk();
    }
}

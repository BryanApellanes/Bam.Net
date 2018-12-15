using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Files
{
    public interface IChunkStorage
    {
        IChunk GetChunk(string hash);
        void SetChunk(IChunk chunk);
    }
}

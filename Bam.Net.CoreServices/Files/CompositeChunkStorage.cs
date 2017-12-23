using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.Files
{
    /// <summary>
    /// ChunkStorage composed of a single Primary IChunkStorage
    /// and zero or more Secondary IChunkStorage providers
    /// </summary>
    public class CompositeChunkStorage : IChunkStorage
    {
        public CompositeChunkStorage()
        {
            Primary = new FileSystemChunkStorage();
            Secondary = new HashSet<IChunkStorage>();
        }

        public IChunkStorage Primary { get; set; }
        public HashSet<IChunkStorage> Secondary { get; }
        public void AddStorage(IChunkStorage storage)
        {
            Secondary.Add(storage);
        }
        public IChunk GetChunk(string hash)
        {
            IChunk chunk = Primary.GetChunk(hash);
            if(chunk != null)
            {
                return chunk;
            }
            foreach (IChunkStorage storage in Secondary)
            {
                chunk = storage.GetChunk(hash);
                if (chunk != null)
                {
                    Task.Run(() => Primary.SetChunk(chunk));
                    return chunk;
                }
            }
            return null;
        }

        public void SetChunk(IChunk chunk)
        {
            Primary.SetChunk(chunk);
            foreach(IChunkStorage storage in Secondary)
            {
                Task.Run(() => storage.SetChunk(chunk));
            }
        }
    }
}

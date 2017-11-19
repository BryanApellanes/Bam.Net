using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Services.Chunking;
using System.Threading;

namespace Bam.Net.Server
{
    class Chunker : ServiceExe
    {
        static void Main(string[] args)
        {
            CommandLineInterface.EnsureAdminRights();

            SetInfo(new ServiceInfo("BamFileChunker", "Bam File Chunker", "Bam File Chunker: chunks files and serves chunks"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Chunker>();
            }
        }

        protected override void OnStart(string[] args)
        {
            Server.Start();
        }

        protected override void OnStop()
        {
            Server.Stop();
            Thread.Sleep(1000);
        }

        static ChunkServer _server;
        static object _serverLock = new object();
        public static ChunkServer Server
        {
            get
            {
                return _serverLock.DoubleCheckLock(ref _server, () => new ChunkServer(new FileSystemChunkStorage()));
            }
        }

    }
}

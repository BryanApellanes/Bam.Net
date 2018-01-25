using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    /// <summary>
    /// Handles any initialization after (Post) server initialization
    /// is complete
    /// </summary>
    public class PostServerInitializationHandler : IPostServerInitializationHandler
    {
        public PostServerInitializationHandler()
        {
            InitializationHandlers = new List<IPostServerInitialize>();
        }
        public List<IPostServerInitialize> InitializationHandlers { get; set; }
        public void HandleInitialization(BamServer server)
        {
            foreach(IPostServerInitialize handler in InitializationHandlers)
            {
                handler.Server = server;
                handler.Initialize();
            }
        }
    }
}

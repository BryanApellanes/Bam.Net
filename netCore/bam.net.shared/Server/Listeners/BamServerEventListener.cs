/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    /// <summary>
    /// An abstract base class used to subscribe special handlers
    /// to server events
    /// </summary>
    public abstract class BamServerEventListener
    {
        // consider generating this file: see ...Server.Tests.ConsoleActions.ListServerEventsAndTypes
        public virtual void Initializing(BamServer bryanServer) { }

        public virtual void Initialized(BamServer bryanServer) { }

        public virtual void SchemaInitializing(BamServer bryanServer, SchemaInitializer schemaInitializer) { }

        public virtual void SchemaInitialized(BamServer bryanServer, SchemaInitializer schemaInitializer) { }

        public virtual void SchemasInitializing(BamServer bryanServer) { }

        public virtual void SchemasInitialized(BamServer bryanServer) { }

        public virtual void LoadingConf(BamServer bryanServer, BamConf bryanConf) { }

        public virtual void LoadedConf(BamServer bryanServer, BamConf bryanConf) { }

        public virtual void CreatingApp(BamServer bryanServer, AppConf appConf) { }

        public virtual void CreatedApp(BamServer bryanServer, AppConf appConf) { }

        public virtual void Responded(BamServer bryanServer, IResponder iResponder, IRequest iRequest) { }
        
        public virtual void NotResponded(BamServer bryanServer, IRequest iRequest) { }

        public virtual void ResponderAdded(BamServer bryanServer, IResponder iResponder) { }

        public virtual void SettingConf(BamServer bryanServer, BamConf bryanConf) { }

        public virtual void SettedConf(BamServer bryanServer, BamConf bryanConf) { }

        public virtual void SavedConf(BamServer bryanServer, BamConf bryanConf) { }

        public virtual void Starting(BamServer bryanServer) { }

        public virtual void Started(BamServer bryanServer) { }

        public virtual void Stopping(BamServer bryanServer) { }

        public virtual void Stopped(BamServer bryanServer) { }


    }
}

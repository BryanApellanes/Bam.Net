/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bam.Net;
using Bam.Net.CommandLine;

namespace YourNamespaceHere
{
    class Program: ServiceExe
    {
        static void Main(string[] args)
        {
            
            SetInfo(new ServiceInfo("ServiceName", "Display Name", "This is the description"));

            if (!ProcessCommandLineArgs(args))
            {
                RunService<Program>();
            }
        }

        bool _stop = false;
        AutoResetEvent _reset = new AutoResetEvent(false); 
        // call _reset.Set() to signal that there is work to be done
        protected override void OnStart(string[] args)
        {
            while (!_stop)
            {
                // do work here
                _reset.WaitOne();
            }
        }

        protected override void OnStop()
        {
            _stop = true;
            _reset.Set();
        }        
    }
}

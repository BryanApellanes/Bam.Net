using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Bam.Net;
using System.Diagnostics;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.Threading;

namespace Bam.Net.CommandLine
{
    public abstract partial class CommandLineInterface
    {
        [DebuggerStepThrough]
        protected internal static void InvokeInSeparateAppDomain(MethodInfo method, object instance, object[] ps = null)
        {
            InvokeInSeparateAppDomain(method, instance, null, ps);
        }

        [DebuggerStepThrough]
        protected internal static void InvokeInSeparateAppDomain(MethodInfo method, object instance, object state, object[] ps = null)
        {
            Log.Warn("{0} is not implemented on this platform and will delegate to {1} losing state.", nameof(InvokeInSeparateAppDomain), nameof(InvokeInCurrentAppDomain));
            InvokeInCurrentAppDomain(method, instance, ps);
            
            // TODO: implement this using ExecuteAssembly, possibly generate a wrapper assembly to invoke the method in order to GetData that was set with SetData; see below
            //AppDomain isolationDomain = AppDomain.CreateDomain("TestAppDomain");
            //_methodToInvoke = method;
            //invokeOn = instance;
            //parameters = ps;

            //isolationDomain.SetData("Method", method);
            //isolationDomain.SetData("Instance", instance);
            //isolationDomain.SetData("Parameters", parameters);
            //isolationDomain.SetData("State", state);
            
            //isolationDomain.DoCallBack(InvokeMethod); // this is deprecated
            //AppDomain.Unload(isolationDomain);
        }
    }
}

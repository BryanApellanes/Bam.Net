/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.IO;
using System.Net;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Encryption;
using Newtonsoft.Json;

namespace Bam.Net.ServiceProxy
{
    public partial class ExecutionRequest
    {

        public ExecutionRequest(string className, string methodName, string ext)
        {
            Context = new HttpContextWrapper();
            ViewName = "Default";
            ClassName = className;
            MethodName = methodName;
            Ext = ext;

            IsInitialized = true;
            OnAnyInstanciated(this);
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response)
        {
            Context = new HttpContextWrapper();
            Request = request;
            Response = response;
            OnAnyInstanciated(this);
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response, ProxyAlias[] aliases)
        {
            Context = new HttpContextWrapper();
            Request = request;
            Response = response;
            ProxyAliases = aliases;
            OnAnyInstanciated(this);
        }

        public ExecutionRequest(RequestWrapper request, ResponseWrapper response, ProxyAlias[] aliases, Incubator serviceProvider)
            : this(request, response, aliases)
        {
            Context = new HttpContextWrapper();
            ServiceProvider = serviceProvider;
            OnAnyInstanciated(this);
        }

        public virtual ValidationResult Validate()
        {
            Initialize();
            ValidationResult result = new ValidationResult(this);
            result.Execute(Context, Decrypted);
            return result;
        }
    }
}

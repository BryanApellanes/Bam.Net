using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.Server;
using Bam.Net.Services.Hosts;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using YamlDotNet.Serialization;

namespace Bam.Net.Presentation
{
    public class ViewModel<AP> : ViewModel
    {
        private AP _actionProvider;
        
        /// <summary>
        /// Gets or sets the action provider.  This should be an 
        /// instance of an object whose class definition has the 
        /// ProxyAttribute applied.
        /// </summary>
        /// <value>
        /// The action provider.
        /// </value>
        public new AP ActionProvider
        {
            get => _actionProvider;
            
            set
            {
                _actionProvider = value;
                base.ActionProvider = value;
            }
        }
    }
    
    public class ViewModel
    {
        public string ApplicationName => Application.Name;
        public string OrganizationName => Application.Organization.Name;
        public string Name { get; set; }
        public string Scripts => Page.Scripts;
        public string StyleSheets => Page.StyleSheets;
        public string ViewModelId { get; set; }
        public ViewModelUrl Url { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [YamlIgnore]
        public PageModel Page { get; set; }
        
        public IEnumerable<object> State { get; set; }

        public AppContentResponder AppContentResponder { get; internal set; }

        private ApplicationModel _applicationModel;

        /// <summary>
        /// The ApplicationModel.  This property is set when the current ViewModel
        /// is set as the ViewModel for a PageModel.
        /// </summary>
        public ApplicationModel Application
        {
            get => _applicationModel;
            internal set
            {
                _applicationModel = value;
                Url = new ViewModelUrl
                {
                    OrganizationName = _applicationModel?.Organization?.Name ?? ApplicationDiagnosticInfo.PublicOrganization,
                    ApplicationName = _applicationModel?.Name ?? ApplicationDiagnosticInfo.UnknownApplication,
                    HostName = Machine.Current.DnsName
                };
            }
        }
        
        /// <summary>
        /// Gets or sets the action provider.  This should be an 
        /// instance of an object whose class definition has the 
        /// ProxyAttribute applied.
        /// </summary>
        /// <value>
        /// The action provider.
        /// </value>
        public dynamic ActionProvider { get; set; }

        public void Execute(string methodName)
        {
            Execute(methodName, State.ToArray());
        }
        
        public void Execute(string methodName, params object[] args)
        {
            ActionProvider?.Invoke(methodName, args);
        }

    }
}

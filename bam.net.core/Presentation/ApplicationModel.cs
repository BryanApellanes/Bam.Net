﻿using Bam.Net.ServiceProxy;
using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{
    [Singleton]
    public class ApplicationModel
    {
        public ApplicationModel(ApplicationServiceRegistry applicationServiceRegistry)
        {
            ApplicationServiceRegistry = applicationServiceRegistry;
            WebServiceRegistry = applicationServiceRegistry.Get<WebServiceRegistry>();
            ApplicationName = ApplicationServiceRegistry.Get<IApplicationNameProvider>().GetApplicationName();
            ApplicationServiceRegistry.SetInjectionProperties(this);
        }

        public string ApplicationName { get; set; }

        public ApplicationServiceRegistry ApplicationServiceRegistry { get; set; }
        public WebServiceRegistry WebServiceRegistry { get; set; }

        [Inject]
        public IViewModelProvider ViewModelProvider { get; set; }

        [Inject]
        public IPersistenceModelProvider PersistenceModelProvider { get; set; }

        [Inject]
        public IExecutionRequestResolver ExecutionRequestResolver { get; set; }

        public PersistenceModel GetPersistenceModel(string persistenceModelName)
        {
            if (string.IsNullOrEmpty(persistenceModelName))
            {
                return null;
            }
            return PersistenceModelProvider.GetPersistenceModel(ApplicationName, persistenceModelName);
        }
        
        public ViewModel GetViewModel(string viewModelName, string persistenceModelName = null)
        {
            return ViewModelProvider.GetViewModel(viewModelName, GetPersistenceModel(persistenceModelName));
        }
    }
}

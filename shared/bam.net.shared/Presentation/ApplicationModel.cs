using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{
    public class ApplicationModel
    {
        public ApplicationModel(ApplicationServiceRegistry applicationServiceRegistry)
        {
            ApplicationServiceRegistry = applicationServiceRegistry;
            //ApplicationName = DefaultConfiguration
        }

        public string ApplicationName { get; set; }

        public ApplicationServiceRegistry ApplicationServiceRegistry { get; set; }

        [Inject]
        public IViewModelProvider ViewModelProvider { get; set; }

        [Inject]
        public IPersistenceModelProvider PersistenceModelProvider { get; set; }

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

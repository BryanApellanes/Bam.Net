using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation
{
    public class DefaultViewModelProvider : IViewModelProvider
    {
        public ViewModel GetViewModel(string viewModelName, PersistenceModel persistenceModel = null)
        {
            return new ViewModel();
        }
    }
}

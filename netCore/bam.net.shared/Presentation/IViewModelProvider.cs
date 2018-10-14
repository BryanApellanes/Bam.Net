using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation
{
    public interface IViewModelProvider
    {
        ViewModel GetViewModel(string viewModelName, PersistenceModel persistenceModel = null);
    }
}

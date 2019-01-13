using Bam.Net.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bam.Net.Web.Models
{
    public class ApplicationModel: Bam.Net.Presentation.ApplicationModel
    {
        public ApplicationModel(ApplicationServiceRegistry applicationServiceRegistry) : base(applicationServiceRegistry)
        {
            ApplicationServiceRegistry = applicationServiceRegistry;

            ApplicationServiceRegistry.SetProperties(this);
        }
    }
}

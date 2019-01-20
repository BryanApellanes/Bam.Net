using Bam.Net.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Api
{
    public class V1Model : PageModel
    {
        public V1Model(WebServiceRegistry webservices)
        {
            WebServiceRegistry = webservices;
        }

        public WebServiceRegistry WebServiceRegistry { get; set; }

        public void OnGet()
        {
        }
    }
}
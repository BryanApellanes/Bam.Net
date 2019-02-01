using Bam.Net.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Admin
{
    public class EditorModel : Presentation.BamPageModel
    {
        public EditorModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel) : base(hostingEnvironment, applicationModel)
        {
        }

        public override ActionResult OnGet()
        {
            return Page();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.ErrorHandling
{
    public class ApplicationErrorHandler : ErrorHandler
    {
        public ApplicationErrorHandler(AppContentResponder appResponder)
        {
            AppContentResponder = appResponder;
            FileExtensions = new HashSet<string>()
            {
                ".html"
            };
        }

        public AppContentResponder AppContentResponder { get; set; }
        public HashSet<string> FileExtensions { get; }
        public HashSet<int> SupportedErrors { get; }
        public override Task LoadResponseContent
        {
            get
            {
                return Task.Run(() =>
                {
                    DirectoryInfo dir = AppContentResponder.AppRoot.GetDirectory("~/errors/");
                    dir.GetFiles().ToList().ForEach(fi =>
                    {
                        string name = Path.GetFileNameWithoutExtension(fi.Name);
                        int code = name.TruncateFront(1).ToInt(); // assumes files are named _<code>; for example, _404.html
                        if (code > 0)
                        {
                            ErrorContent.Add(code, new FileErrorContent(fi, code));
                        }
                    });
                });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Server
{
    public class CustomContentHandler: Loggable
    {
        HashSet<string> _paths;
        public CustomContentHandler(string name, Fs fileSystem, params string[] paths)
        {
            _paths = new HashSet<string>(paths);
            Name = name;
            Fs = fileSystem;
            GetContent = (ctx, fs) => new byte[] { };
        }
        public string Name { get; }
        public Fs Fs { get; }
        public void AddPath(string path)
        {
            _paths.Add(path);
        }
        [Verbosity(VerbosityLevel.Information, MessageFormat = "Handl(ING): CustomHandler: {Name}, Uri: {Uri}")]
        public event EventHandler Handling;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "Handl(ED): CustomHandler: {Name}, Uri: {Uri}")]
        public event EventHandler Handled;

        public bool HandleRequest(IHttpContext context, out byte[] content)
        {
            if (!_paths.Contains(context.Request.Url.AbsolutePath))
            {
                content = new byte[] { };
                return false;
            }
            CustomContentEventArgs args = new CustomContentEventArgs { CustomContentHandler = this, Uri = context.Request.Url.ToString() };
            FireEvent(Handling, args);
            content = GetContent(context, Fs);
            FireEvent(Handled, args);
            return content != null;
        }

        public Func<IHttpContext, Fs, byte[]> GetContent { get; set; }
    }
}

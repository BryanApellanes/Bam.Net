using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net
{
    public interface IRenderable
    {
        string Render();
        void Render(Stream output);
        void Render(IStreamRenderer renderer);
        void Render(IStreamRenderer renderer, string templateName, Stream output);
    }
}

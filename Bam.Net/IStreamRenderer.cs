using System.IO;

namespace Bam.Net
{
    public interface IStreamRenderer
    {
        void Render(object toRender, Stream output);
        void Render(string templateName, object toRender, Stream output);
    }
}
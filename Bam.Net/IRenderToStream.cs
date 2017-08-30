using System.IO;

namespace Bam.Net
{
    public interface IRenderToStream
    {
        void Render(object toRender, Stream output);
        void Render(string templateName, object toRender, System.IO.Stream output);
    }
}
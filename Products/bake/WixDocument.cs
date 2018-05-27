using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bam.Net.Application
{
    public class WixDocument
    {
        public const string MergeRedirectFolder = "MergeRedirectFolder";
        public const string TargetDir = "TARGETDIR";

        XDocument _xDocument;
        public WixDocument(string path)
        {
            Path = path;
            _xDocument = XDocument.Load(path);
        }

        public string Path { get; private set; }

        /// <summary>
        /// Sets the target contents to the files and folder found in the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetTargetContents(string path)
        {
            BakeDirectory dir = BakeDirectory.FromPath(path);
            dir.Name = string.Empty;
            dir.Id = MergeRedirectFolder;            
            XElement targetDirElement = _xDocument.Descendants().FirstOrDefault(el => (string)el.Attribute("Id") == TargetDir);
            if(targetDirElement == null)
            {
                throw new InvalidOperationException(string.Format("Directory element with Id \"TARGETDIR\" was not found in the specified document: ({0})", path));
            }
            targetDirElement.RemoveNodes();
            XNamespace defaultNamespace = "http://schemas.microsoft.com/wix/2006/wi";
            XElement components = dir.ToXElement(defaultNamespace);
            targetDirElement.Add(components);
            _xDocument.Save(Path);
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System.IO;
using Microsoft.Web.XmlTransform;

namespace Bam.Net.Automation
{
    public static class Extensions
    {
        public static bool TransformXml(this FileInfo sourceFile, string transformFilePath, string destinationPath)
        {
            return XmlTransform(sourceFile, transformFilePath, destinationPath);
        }

        public static bool TransformXml(this FileInfo sourceFile, FileInfo transformFile, FileInfo destinationFile)
        {
            return XmlTransform(sourceFile, transformFile, destinationFile);
        }

        public static bool XmlTransform(this FileInfo sourceFile, string transformFilePath, string destinationPath)
        {
            return XmlTransform(sourceFile, new FileInfo(transformFilePath), new FileInfo(destinationPath));
        }

        public static bool XmlTransform(this FileInfo sourceFile, FileInfo transformFile, FileInfo destinationFile)
        {
            Args.ThrowIf<FileNotFoundException>(!sourceFile.Exists, "The specified source file does not exist: ({0})"._Format(sourceFile.FullName));
            Args.ThrowIf<FileNotFoundException>(!transformFile.Exists, "The specified transformFile does not exist: ({0})"._Format(transformFile.FullName));

            bool result = false;

            using (XmlTransformableDocument document = new XmlTransformableDocument())
            {
                document.PreserveWhitespace = true;
                document.Load(sourceFile.FullName);

                using (XmlTransformation transform = new XmlTransformation(transformFile.FullName))
                {
                    result = transform.Apply(document);

                    document.Save(destinationFile.FullName);
                }
            }

            return result;
        }
    }
}

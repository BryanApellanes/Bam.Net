using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class CsFile
    {
        public string Root { get; set; }
        public string Path { get; set; }
        
        public CsFile CopyTo(string directoryPath)
        {
            string srcFile = System.IO.Path.Combine(Root, Path);
            DirectoryInfo rootDir = new DirectoryInfo(Root);
            string destFile = System.IO.Path.Combine(directoryPath, rootDir.Name, Path);
            FileInfo destFileInfo = new FileInfo(destFile);
            if (!destFileInfo.Directory.Exists)
            {
                destFileInfo.Directory.Create();
            }
            File.Copy(srcFile, destFile);
            return new CsFile { Root = directoryPath, Path = Path };
        }
    }
}

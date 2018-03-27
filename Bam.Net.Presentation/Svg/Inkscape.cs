using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Bam.Net.CommandLine;

namespace Bam.Net.Presentation.Svg
{
    /// <summary>
    /// Class used to issue commands to inkscape.exe.
    /// </summary>
    public class Inkscape
    {
        static Inkscape()
        {
            ExePath = "C:\\Program Files\\Inkscape\\inkscape.exe";
        }

        public static string ExePath
        {
            get;
            set;
        }

        public static Bitmap ExportSvg(string svgFilePath, int width, int height, string outputPngPath = null)
        {
            FileInfo svgFile = new FileInfo(svgFilePath);
            string fileName = Path.GetFileNameWithoutExtension(svgFile.FullName);
            outputPngPath = outputPngPath ?? Path.Combine(svgFile.Directory.FullName, $"{fileName}.png");
            Bitmap result = null;
            $"{ExePath}".Run($"-z -e {outputPngPath} -w {width} -h {height} {svgFilePath}", (o, a) => result = new Bitmap(outputPngPath), 60000);
            return result;
        }
    }
}

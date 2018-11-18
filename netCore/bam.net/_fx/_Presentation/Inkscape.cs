using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Bam.Net.CommandLine;

namespace Bam.Net.Presentation
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

        /// <summary>
        /// Exports the specified svg file at 3600 x 3600 pixels.
        /// </summary>
        /// <param name="svgFilePath"></param>
        /// <returns></returns>
        public static Bitmap ExportSvg(string svgFilePath)
        {
            return ExportSvg(svgFilePath, 3600, 3600, out FileInfo ignore);
        }

        /// <summary>
        /// Exports the specified svg file at the specified dimensions.
        /// </summary>
        /// <param name="svgFilePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Bitmap ExportSvg(string svgFilePath, int width, int height, out FileInfo output)
        {
            FileInfo svgFile = new FileInfo(svgFilePath);
            string fileName = Path.GetFileNameWithoutExtension(svgFile.FullName);
            output = new FileInfo(Path.Combine(svgFile.Directory.FullName, $"{fileName}.png"));
            Bitmap result = ExportSvg(svgFilePath, width, height, output.FullName);
            return result;
        }

        /// <summary>
        /// Exports the specified svg file at the specified dimensions.
        /// </summary>
        /// <param name="svgFilePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="outputPngPath"></param>
        /// <returns></returns>
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

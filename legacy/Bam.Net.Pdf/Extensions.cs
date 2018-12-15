using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using SautinSoft;

namespace Bam.Net.Pdf
{
    public static class Extensions
    {
        public static List<FileInfo> PdfToHtml(this DirectoryInfo directory)
        {
            return PdfToHtml(directory, directory);
        }
        public static List<FileInfo> PdfToHtml(this DirectoryInfo directory, DirectoryInfo writeToDirectory)
        {
            FileInfo[] pdfFiles = directory.GetFiles("*.pdf");
            List<FileInfo> results = new List<FileInfo>();
            pdfFiles.Each(pdf =>
            {
                FileInfo next;
                pdf.PdfToHtml(writeToDirectory, out next);
                results.Add(next);
            });
            return results;
        }
        public static bool PdfToHtml(this string filePath, out FileInfo resultFile)
        {
            return PdfToHtml(new FileInfo(filePath), out resultFile);
        }
        public static bool PdfToHtml(this FileInfo filePath, out FileInfo resultFile)
        {
            return PdfToHtml(filePath, Log.Default, out resultFile);
        }
        public static bool PdfToHtml(this FileInfo file, ILog logger, out FileInfo resultFile)
        {
            return PdfToHtml(file, file.Directory, logger, out resultFile);
        }
        public static bool PdfToHtml(this string sourceFile, string writeToDirectory)
        {
            return PdfToHtml(new FileInfo(sourceFile), new DirectoryInfo(writeToDirectory));
        }
        public static bool PdfToHtml(this FileInfo file, DirectoryInfo writeTo)
        {
            FileInfo ignore;
            return PdfToHtml(file, writeTo, Log.Default, out ignore);
        }
        public static bool PdfToHtml(this FileInfo file, DirectoryInfo writeToDirectory, out FileInfo resultFile)
        {
            return PdfToHtml(file, writeToDirectory, Log.Default, out resultFile);
        }
        public static bool PdfToHtml(this FileInfo file, DirectoryInfo writeToDirectory, ILog logger, out FileInfo resultFile)
        {
            resultFile = null;
            try
            {
                PdfFocus pdf = new PdfFocus();
                pdf.OpenPdf(file.FullName);
                string fileName = $"{Path.GetFileNameWithoutExtension(file.FullName)}.html";
                resultFile = new FileInfo(Path.Combine(writeToDirectory.FullName, fileName));
                if (resultFile.Exists)
                {
                    resultFile = new FileInfo(resultFile.FullName.GetNextFileName());
                }
                pdf.ToHtml(resultFile.FullName);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error("An exception occurred: {0}\r\n{1}", ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.System
{
    public static class Extensions
    {
        public static void CopyTo(this FileInfo file, string computerName, string userName, string password, string remotePath = null)
        {
            using (RunAsContext runasContext = RunAs.Impersonate(userName, password))
            {
                CopyTo(file, computerName, remotePath);
            }
        }

        public static void CopyTo(this FileInfo file, string computerName, string remotePath = null)
        {
            try
            {
                remotePath = remotePath ?? "C$\\Windows\\Temp";
                string destinationFile = Path.Combine(remotePath, file.Name);
                if(destinationFile.Length >= 2 && destinationFile[1].Equals(":"))
                {
                    StringBuilder df = new StringBuilder(destinationFile);
                    df[1] = '$';
                    destinationFile = df.ToString();
                }
                file.CopyTo($"\\\\{computerName}\\{destinationFile}");
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Exception copying file ({0}) to target computer ({1}), Path={2}", ex, file.FullName, computerName, remotePath);
            }
        }
    }
}

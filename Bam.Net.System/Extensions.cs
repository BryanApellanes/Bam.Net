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

        public static void CopyTo(this DirectoryInfo directory, string computerName, string remoteDirectory)
        {
            List<Task> copyTasks = new List<Task>();
            foreach(FileInfo file in directory.GetFiles())
            {
                copyTasks.Add(Task.Run(() => file.CopyTo(computerName, remoteDirectory)));
            }
            foreach(DirectoryInfo dir in directory.GetDirectories())
            {
                string subPath = dir.FullName.TruncateFront(dir.FullName.Length + 1);
                copyTasks.Add(Task.Run(() => dir.CopyTo(computerName, remoteDirectory + subPath)));
            }
            Task.WaitAll(copyTasks.ToArray());
        }

        public static void CopyTo(this FileInfo file, string computerName, string userName, string password, string remotePath = null)
        {
            using (RunAsContext runasContext = RunAs.Impersonate(userName, password))
            {
                CopyTo(file, computerName, remotePath);
            }
        }

        public static void CopyTo(this FileInfo file, string computerName, string remoteDirectory = null)
        {
            try
            {
                string adminSharePath = GetAdminSharePath(file.Name, computerName, remoteDirectory);
                FileInfo destination = new FileInfo(adminSharePath);
                if (!destination.Directory.Exists)
                {
                    destination.Directory.Create();
                }
                file.CopyTo(adminSharePath, true);
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Exception copying file ({0}) to target computer ({1}), Path={2}", ex, file.FullName, computerName, remoteDirectory);
            }
        }

        public static string GetAdminSharePath(this FileInfo file, string computerName)
        {
            return GetAdminSharePath(file.Name, computerName, file.Directory.FullName);
        }

        /// <summary>
        /// Gets the admin share path.  For C:\windows\temp \\{ComputerName}\C$\Windows\temp is returned.
        /// </summary>
        /// <param name="fileName">The file.</param>
        /// <param name="computerName">Name of the computer.</param>
        /// <param name="remoteDirectory">The remote directory in local notation, for example, C:\windows\temp.</param>
        /// <returns></returns>
        public static string GetAdminSharePath(this string fileName, string computerName, string remoteDirectory)
        {
            remoteDirectory = remoteDirectory ?? "C$\\Windows\\Temp";
            string destinationFile = Path.Combine(remoteDirectory, fileName);
            if (destinationFile.Length >= 2 && destinationFile[1].Equals(':'))
            {
                StringBuilder df = new StringBuilder(destinationFile);
                df[1] = '$';
                destinationFile = df.ToString();
            }
            string adminSharePath = $"\\\\{computerName}\\{destinationFile}";
            return adminSharePath;
        }

        /// <summary>
        /// Gets the admin share directory in the format \\{computerName}\{driveLetter}$\{directoryPath}
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="computerName">Name of the computer.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Specified directoryPath not in expected format: [DriveLetter]:[path]</exception>
        public static DirectoryInfo GetAdminShareDirectory(this string directoryPath, string computerName)
        {
            if(directoryPath.Length >= 2 && directoryPath[1].Equals(':'))
            {
                StringBuilder path = new StringBuilder(directoryPath);
                path[1] = '$';
                return new DirectoryInfo($"\\\\{computerName}\\{path.ToString()}");
            }
            throw new ArgumentException("Specified directoryPath not in expected format: [DriveLetter]:[directoryPath]");
        }
    }
}

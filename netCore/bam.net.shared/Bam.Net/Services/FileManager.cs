using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices.Files;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services
{
    public class FileManager
    {
        public FileManager(IDataDirectoryProvider dataSettings, IFileService fileService)
        {
            FileService = fileService;
        }
        public IDataDirectoryProvider DataSettings { get; set; }
        protected IFileService FileService { get; set; }
        public void StoreFiles(DirectoryInfo directory)
        {
            StoreFiles(directory.GetFiles("*", SearchOption.AllDirectories));
        }

        public void StoreFiles(params FileInfo[] files)
        {
            StoreFiles(string.Empty, files);
        }

        public void StoreFiles(string description, params FileInfo[] files)
        {
            Parallel.ForEach(files, (file) => Task.Run(() => FileService.StoreFileChunks(file, description)));
        }

        public FileInfo RestoreFile(string fileNameOrHash)
        {
            return FileService.WriteFileDataToDirectory(fileNameOrHash, DataSettings.GetFilesDirectory().FullName);
        }
    }
}

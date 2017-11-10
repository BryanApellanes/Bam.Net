using System;
using System.IO;
using System.Reflection;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class FileServiceTests : CommandLineTestInterface
    {
        [UnitTest]
        public void CanCopyLoadedAssembly()
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            FileInfo currentFile = currentAssembly.GetFileInfo();
            FileInfo copyTo = currentFile.CopyFile(nameof(CanCopyLoadedAssembly));
            Expect.IsTrue(copyTo.Exists);
            OutLine(copyTo.FullName, ConsoleColor.Cyan);
            copyTo.Delete();
        }

        [UnitTest]
        public void FileServiceRestoreTest()
        {
            SQLiteDatabase db = new SQLiteDatabase(".\\", nameof(FileServiceRestoreTest));
            CoreFileService fmSvc = new CoreFileService(new DaoRepository(db))
            {
                ChunkLength = 111299
            };
            FileInfo testDataFile = new FileInfo("C:\\Bam\\Data\\Test\\TestDataFile.dll");
            ChunkedFileDescriptor chunkedFile = fmSvc.StoreFileChunksInRepo(testDataFile);
            FileInfo writeTo = new FileInfo($".\\{nameof(FileServiceRestoreTest)}_restored");
            DateTime start = DateTime.UtcNow;
            FileInfo written = fmSvc.RestoreFile(chunkedFile.FileHash, writeTo.FullName, true);
            TimeSpan took = DateTime.UtcNow.Subtract(start);
            OutLine(took.ToString(), ConsoleColor.Cyan);
            Expect.IsTrue(written.Exists);
            Expect.AreEqual(testDataFile.Md5(), written.Md5(), "file content didn't match");
        }

        [UnitTest]
        public void FileServiceRestoreAsyncTest()
        {
            SQLiteDatabase db = new SQLiteDatabase(".\\", nameof(FileServiceRestoreAsyncTest));
            CoreFileService fmSvc = new CoreFileService(new DaoRepository(db));
            fmSvc.ChunkLength = 111299;
            ConsoleLogger logger = new ConsoleLogger();
            logger.AddDetails = false;
            logger.StartLoggingThread();
            FileInfo testDataFile = new FileInfo("C:\\Bam\\Data\\Test\\TestDataFile.dll");
            ChunkedFileDescriptor chunkedFile = fmSvc.StoreFileChunksInRepo(testDataFile);
            FileInfo writeTo = new FileInfo($".\\{nameof(FileServiceRestoreAsyncTest)}_restored.dat");
            DateTime start = DateTime.UtcNow;
            ChunkedFileWriter writer = ChunkedFileWriter.FromFileHash(fmSvc, chunkedFile.FileHash, logger);
            writer.Write(writeTo.FullName).Wait();
            TimeSpan took = DateTime.UtcNow.Subtract(start);
            FileInfo written = new FileInfo(writeTo.FullName);
            OutLine(took.ToString(), ConsoleColor.Cyan);
            Expect.IsTrue(written.Exists);
            Expect.AreEqual(testDataFile.Md5(), written.Md5(), "file content didn't match");
        }
    }
}

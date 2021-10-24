using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.Files.Data;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bam.Net.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {

        public class Primate
        {
        }

        public class Monkey : Primate
        {
        }

        public class Gorilla : Monkey
        {
            public Gorilla(IFruit fruit)
            {
                this.Fruit = fruit;
            }

            public IFruit Fruit { get; private set; }
        }
        public interface IFruit
        { }
        public class Banana : IFruit
        { }
        public class Apple : IFruit
        { }

        interface IVegetable
        { }

        public class Tomato : IFruit
        { }

        public abstract class Eater
        {
            public Eater(IFruit food)
            {
                Food = food;
            }
            public IFruit Food { get; set; }
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet()
        {
            Incubator i = new Incubator();
            i.Set(typeof(Primate), new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult()
        {
            Incubator i = new Incubator();
            Func<Primate> f = () => new Monkey();
            i.Set(typeof(Primate), f);
            Primate m = i.Get<Primate>();
            (m.GetType() == typeof(Monkey)).IsTrue();
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => new Monkey());
            Primate m = i.Get<Primate>();
            (m.GetType() == typeof(Monkey)).IsTrue();
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnByClassName()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => new Monkey());
            object m = i.Get("Primate");
            (m.GetType() == typeof(Monkey)).IsTrue();
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndPopOutSpecifiedType()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => new Monkey());
            object m = i.Get("Primate", out Type type);
            (type == typeof(Primate)).IsTrue();
        }

        [UnitTest]
        public static void FluentIncubatorTest()
        {
            Incubator i = Bam.Net.Incubation.Requesting.A<Primate>().Returns<Monkey>();
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void FluentConstructorTests()
        {
            Incubator withBanana = Incubation.Requesting.A<IFruit>().Returns<Banana>();
            withBanana.AskingFor<Primate>().Returns<Gorilla>();
            Primate p = withBanana.Get<Primate>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Banana));
        }

        [UnitTest]
        public static void FluentConstructorTests2()
        {
            Incubator withApple = Incubation.Requesting.A<IFruit>().Returns<Apple>();
            withApple.AskingFor<Monkey>().Returns<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }

        [UnitTest]
        public static void FluentConstructorTests3()
        {
            Incubator withApple = Incubation.Requesting.A<IFruit>().Returns<Apple>();
            withApple.Bind<Monkey>().To<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }

        class TestLogReader : ILogReader
        {
            public TestLogReader(IFileService fileService)
            {
                FileService = fileService;
            }

            public IFileService FileService { get; set; }

            public ILogger Logger { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public List<LogEntry> GetLogEntries(DateTime from, DateTime to)
            {
                throw new NotImplementedException();
            }
        }

        class TestFileService : IFileService
        {
            public int ChunkDataBatchSize => throw new NotImplementedException();

            public string ChunkDirectory => throw new NotImplementedException();

            public int ChunkLength => throw new NotImplementedException();

            public ChunkData GetChunkData(string chunkHash)
            {
                throw new NotImplementedException();
            }

            public ChunkData GetChunkDataFromFileSystem(string chunkHash)
            {
                throw new NotImplementedException();
            }

            public ChunkData GetChunkDataFromRepository(string chunkHash)
            {
                throw new NotImplementedException();
            }

            public List<FileChunk> GetFileChunks(string fileHash, int fromIndex)
            {
                throw new NotImplementedException();
            }

            public List<FileChunk> GetFileChunks(string fileHash, int fromIndex, int batchSize)
            {
                throw new NotImplementedException();
            }

            public ChunkedFileDescriptor GetFileDescriptor(string fileHashOrName)
            {
                throw new NotImplementedException();
            }

            public ChunkedFileDescriptor GetFileDescriptorByFileHash(string fileHash)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ChunkedFileDescriptor> GetFileDescriptorsByFileName(string fileName, string originalDirectory = null)
            {
                throw new NotImplementedException();
            }

            public ChunkedFileWriter GetFileWriter(string fileHash)
            {
                throw new NotImplementedException();
            }

            public FileInfo RestoreFile(ChunkedFileDescriptor fileDescriptor, string localPath = null)
            {
                throw new NotImplementedException();
            }

            public FileInfo RestoreFile(string fileHash, string localPath, bool overwrite = true)
            {
                throw new NotImplementedException();
            }

            public void SaveChunkData(ChunkData chunk)
            {
                throw new NotImplementedException();
            }

            public ChunkDataDescriptor SaveChunkDataDescriptor(ChunkDataDescriptor xref)
            {
                throw new NotImplementedException();
            }

            public ChunkedFileDescriptor SaveFileDescriptor(ChunkedFileDescriptor fileDescriptor)
            {
                throw new NotImplementedException();
            }

            public ChunkedFileDescriptor StoreFileChunks(FileInfo file, string description = null)
            {
                throw new NotImplementedException();
            }

            public FileInfo WriteFileDataToDirectory(string fileNameOrHash, string directoryPath)
            {
                throw new NotImplementedException();
            }

            public void WriteFileHashToStream(string fileHash, Stream fs)
            {
                throw new NotImplementedException();
            }

            public void WriteFileToStream(string fileNameOrHash, Stream stream)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<FileChunk> YieldFileChunks(string fileHash, int fromIndex, int batchSize)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ChunkedFileDescriptor> ListFiles(string rootPath = ".")
            {
                throw new NotImplementedException();
            }
        }

        [UnitTest]
        public static void CanOverwriteSetting()
        {
            ServiceRegistry reg = CoreServiceRegistryContainer.Create();
            ILogReader logReader = reg.Get<ILogReader>();
            logReader.IsInstanceOfType<SystemLogReaderService>();
            reg.For<ILogReader>().Use<TestLogReader>();
            reg.For<IFileService>().Use<TestFileService>();
            logReader = reg.Get<ILogReader>();
            logReader.IsInstanceOfType<TestLogReader>();
            TestLogReader test = (TestLogReader)logReader;
            test.FileService.IsInstanceOfType<TestFileService>();
        }
    }
}

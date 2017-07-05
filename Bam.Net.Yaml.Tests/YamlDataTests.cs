using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.Yaml.Data;

namespace Bam.Net.Yaml.Tests
{
    public class Data: FsRepoData
    {
        public string Description { get; set; }
        public double Price { get; set; }
    }
    [Serializable]
    public class YamlDataTests : CommandLineTestInterface
    {
        string _singleObjectData = ".\\Data.yaml";
        string _arrayData = ".\\Array.yaml";
        string _normalizeData = ".\\Data\\Normalize.yaml";
        string _dataData = ".\\Data\\Data.yaml";
        // read yaml file
        [UnitTest]
        public void CanReadYaml()
        {
            object[] data = _singleObjectData.FromYamlFile();
            Expect.AreEqual(1, data.Length);
            Expect.IsInstanceOfType<Dictionary<object, object>>(data[0]);
        }

        [UnitTest]
        public void CanReadArray()
        {
            object[] data = _arrayData.FromYamlFile();
            Expect.AreEqual(3, data.Length);
            Expect.IsInstanceOfType<Dictionary<object, object>>(data[0]);
            Expect.IsInstanceOfType<Dictionary<object, object>>(data[1]);
        }
        
        [UnitTest]
        public void WillThrow()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            YamlDataFile data = new YamlDataFile(typeof(Data), _arrayData, logger);
            Expect.Throws(() =>
            {
                Data d = (Data)data.Data;
            }, "Referencing Data didn't throw");
        }

        [UnitTest]
        public void WillWarn()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            bool? fired = false;
            logger.WarnEventOccurred += (o, a) =>
            {
                fired = true;
            };
            YamlDataFile data = new YamlDataFile(typeof(Data), _arrayData, logger);
            data.ArrayBehavior = ArrayBehavior.Warn;
            Expect.IsFalse(fired.Value);
            Data d = (Data)data.Data;
            Expect.IsTrue(fired.Value);
        }

        [UnitTest]
        public void WillNormalize()
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.StartLoggingThread();
            YamlDataFile data = new YamlDataFile(typeof(Data), _normalizeData, logger);
            data.ArrayBehavior = ArrayBehavior.Normalize;
            DirectoryInfo dir = data.File.Directory;
            DeleteFile(Path.Combine(dir.FullName, "Item One.yaml"));
            DeleteFile(Path.Combine(dir.FullName, "Item Two.yaml"));
            DeleteFile(Path.Combine(dir.FullName, "Item Three.yaml"));
            Data d = (Data)data.Data;
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, "Item One.yaml")));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, "Item Two.yaml")));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, "Item Three.yaml")));
        }

        [UnitTest]
        public void FileInfoLastWriteMilliseconds()
        {
            FileInfo txt = new FileInfo(".\\one.txt");            
            10.RandomLetters().SafeWriteToFile(txt.FullName, true);
            txt.Refresh();
            DateTime oneWrite = txt.LastWriteTimeUtc;
            OutLine(oneWrite.Millisecond.ToString());
            8.RandomLetters().SafeWriteToFile(txt.FullName, true);
            txt.Refresh();
            DateTime twoWrite = txt.LastWriteTimeUtc;
            Thread.Sleep(1);
            Expect.IsTrue(twoWrite > oneWrite);
            OutLine(twoWrite.Millisecond.ToString());
        }

        [UnitTest]
        public void CanCreateType()
        {
            object[] data = _singleObjectData.FromYamlFile();
            Type type = ((Dictionary<object, object>)data[0]).ToDynamicType("Data");
            Expect.IsNotNull(type);
            type.GetProperties().Each(p =>
            {
                OutLineFormat("Property: {0}, Type: {1}", p.Name, p.PropertyType.Name);
            });
        }
        
        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}

using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Profiguration.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTool
    {
        [UnitTest]
        public void ShouldBeAbleToUseProfigurationSetToNameProfigurations()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet profSet = new ProfigurationSet(dir);
            string randomName = "Test_".RandomLetters(4);
            Profiguration prof = profSet.Get(randomName);

            Expect.IsNotNull(prof);
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName)));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName + ".key")));
        }

        [UnitTest]
        public void ShouldBeAbleToSaveAProfigurationProvidingAName()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string randomName = "Test_".RandomLetters(4);
            Profiguration prof = new Profiguration();
            prof.AppSettings["test"] = "monkey";

            set.Save(randomName, prof);

            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName)));
            Expect.IsTrue(File.Exists(Path.Combine(dir.FullName, randomName + ".key")));

            Profiguration verify = set.Get(randomName);
            Expect.AreEqual("monkey", verify.AppSettings["test"]);
        }

        [UnitTest]
        public void FileWithNoExtensionShouldReturnEmptyString()
        {
            string withExtension = "file.txt";
            string withoutExtension = "file";

            Expect.AreEqual(".txt", Path.GetExtension(withExtension));
            Expect.AreEqual(string.Empty, Path.GetExtension(withoutExtension));
        }

        [UnitTest]
        public void ExtensionMethodsForCheckingExtensionsShouldWork()
        {
            FileInfo withExtension = new FileInfo("file.txt");
            FileInfo withoutExtension = new FileInfo("file");

            Expect.IsTrue(withExtension.HasExtension(".txt"));
            Expect.IsTrue(withoutExtension.HasNoExtension());
        }

        [UnitTest]
        public void IndexedProfigurationShouldAddName()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            if (dir.Exists)
            {
                dir.Delete(true);
            }

            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Expect.IsTrue(set.ProfigurationNames.Length == 0);

            Profiguration prof = set[name];

            Expect.IsTrue(set.ProfigurationNames.Length == 1);
        }

        [UnitTest]
        public void SettingAProfigurationValueShouldReflectImmediately()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Profiguration prof = set[name];
            prof.AppSettings[key] = value;

            Expect.AreEqual(value, prof.AppSettings[key], "value didn't get set");
        }

        [UnitTest]
        public void ShouldBeAbleToUseIndexedProfiguration()
        {
            DirectoryInfo dir = new DirectoryInfo(MethodBase.GetCurrentMethod().Name);
            ProfigurationSet set = new ProfigurationSet(dir);
            string name = "Test_".RandomLetters(6);
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            Profiguration prof = set[name];
            prof.AppSettings[key] = value;

            Expect.AreEqual(value, prof.AppSettings[key], "value didn't get set");

            set.Save();

            ProfigurationSet validate = new ProfigurationSet(dir);

            Expect.AreEqual(value, validate[name].AppSettings[key]);
        }
    }
}

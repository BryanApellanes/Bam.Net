using Bam.Net.Automation.Nuget;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class NuspecFileTests: CommandLineTestInterface
    {
        [UnitTest]
        public void CanAddPackageDependencies()
        {
            NuspecFile nuspec = new NuspecFile();
            Expect.IsTrue(nuspec.Dependencies.Length == 0);
            nuspec.AddPackageDependencies(".\\test_packages.config");
            Expect.IsTrue(nuspec.Dependencies.Length == 2);
            Expect.IsTrue(nuspec.Dependencies[0].Id.Equals("TestPackage1"));
            Expect.IsTrue(nuspec.Dependencies[0].Version.Equals("1.0.0"));
            Expect.IsTrue(nuspec.Dependencies[1].Id.Equals("TestPackage2"));
            Expect.IsTrue(nuspec.Dependencies[1].Version.Equals("2.0.0"));
        }

        [UnitTest]
        public void NuspecFileShouldHaveValuesAfterInstantiation()
        {
            NuspecFile file = new NuspecFile("test1.nuspec");
            Expect.IsNotNullOrEmpty(file.Version.Value);
            Expect.AreEqual("1", file.Version.Major);
            Expect.AreEqual("0", file.Version.Minor);
            Expect.AreEqual("0", file.Version.Patch);
            Expect.IsNotNullOrEmpty(file.Title);
            Expect.IsNotNullOrEmpty(file.Id);
            Expect.IsNotNullOrEmpty(file.Authors);
            Expect.IsNotNullOrEmpty(file.Owners);
            Expect.IsNotNullOrEmpty(file.ReleaseNotes);
            Expect.IsFalse(file.RequireLicenseAcceptance);
            Expect.IsNotNullOrEmpty(file.Copyright);
            Expect.IsNotNullOrEmpty(file.Description);
            file.AddDependency("monkey", "1.0.0");
            file.AddDependency("test", "[2]");
            file.Version.IncrementPatch();
            file.Save();
            string content = file.Path.SafeReadFile();
            Out(content, ConsoleColor.Cyan);
            File.Delete(file.Path);
        }
    }
}

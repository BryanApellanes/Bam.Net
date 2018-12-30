using Bam.Net.Automation.Nuget;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
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
    }
}

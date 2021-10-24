using System;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class ViewModelTests: CommandLineTool
    {
        [UnitTest]
        [TestGroup("ViewModel")]
        public void CanParseViewModelUrlWithHostName()
        {
            string testOrgName = $"orgName-{12.RandomLetters()}";
            string testAppName = $"appName-{8.RandomLetters()}";
            string testHostName = $"hostName-{6.RandomLetters()}";
            string testRelativeFilePath = $"filePath-{4.RandomLetters()}/{3.RandomLetters()}/{5.RandomLetters()}.bvm";
            string testViewModelUrl = $"bam://{testOrgName}::{testAppName}@{testHostName}/{testRelativeFilePath}";
            ViewModelUrl url = ViewModelUrl.FromString(testViewModelUrl);
            
            Expect.AreEqual(testOrgName, url.OrganizationName);
            Expect.AreEqual(testAppName, url.ApplicationName);
            Expect.AreEqual(testHostName, url.HostName);
            Expect.AreEqual(testRelativeFilePath, url.RelativeFilePath);
            
            Expect.AreEqual(testViewModelUrl, url.ToString());
        }
        
        [UnitTest]
        [TestGroup("ViewModel")]
        public void CanParseViewModelUrlWithoutHostName()
        {
            string testOrgName = 12.RandomLetters();
            string testAppName = 8.RandomLetters();
            string testRelativeFilePath = $"{4.RandomLetters()}/{3.RandomLetters()}/{5.RandomLetters()}.bvm";
            string testViewModelUrl = $"bam://{testOrgName}::{testAppName}/{testRelativeFilePath}";
            ViewModelUrl url = ViewModelUrl.FromString(testViewModelUrl);
            
            Expect.AreEqual(testOrgName, url.OrganizationName);
            Expect.AreEqual(testAppName, url.ApplicationName);
            Expect.IsNullOrEmpty(url.HostName);
            Expect.AreEqual(testRelativeFilePath, url.RelativeFilePath);
            
            Expect.AreEqual(testViewModelUrl, url.ToString());
        }
    }
}
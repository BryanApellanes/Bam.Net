/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Automation;
using Bam.Net.Automation.Nuget;
using System.Collections.ObjectModel;
using Bam.Net.Automation.ContinuousIntegration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Microsoft.Build.BuildEngine;
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using Bam.Net.Automation.ContinuousIntegration.Loggers;
using Bam.Net.Documentation;
using Bam.Net.Testing.Unit;
using Bam.Net.Services;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class UnitTests: CommandLineTestInterface
    {
        [UnitTest]
        public void TestRunListenerEventsShouldFire()
        {
            TestUnitTestRunListener listener = new TestUnitTestRunListener();
            TestTestRunner runner = new TestTestRunner();
            listener.Listen(runner);
            runner.FireAllEventsForTestingPurposes();
            
            Expect.IsTrue(listener.TestFailedRan, "TestFailed didn't run");
            Expect.IsTrue(listener.TestPassedRan, "TestPassed didn't run");
            Expect.IsTrue(listener.TestsStartingRan, "TestsStarting didn't run");
            Expect.IsTrue(listener.TestStartingRan, "TestStarting didn't run");
            Expect.IsTrue(listener.TestsFinishedRan, "TestsFinished didn't run");
            Expect.IsTrue(listener.TestFinishedRan, "TestFinished didn't run");
        }

        [UnitTest]
        public void ShouldBeAbleToSaveAndGetSecurelyWithDefaultOverseer()
        {
            string key = "Key_".RandomLetters(4);
            string value = "Value_".RandomLetters(4);

            JobConductorService.Default.SecureSet(key, value);

            string validate = JobConductorService.Default.SecureGet(key);

            Expect.AreEqual(value, validate);
        }

        [UnitTest]
        public void CsvBuildLoggerActualLoggerShouldBeCorrectType()
        {
            CsvBuildLogger csv = new CsvBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(CsvLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void Dao2BuildLoggerActualLoggerShouldBeCorrectType()
        {
            Dao2BuildLogger csv = new Dao2BuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(DaoLogger2), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void DaoBuildLoggerActualLoggerShouldBeCorrectType()
        {
            DaoBuildLogger csv = new DaoBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(DaoLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void TextFileBuildLoggerActualLoggerShouldBeCorrectType()
        {
            TextFileBuildLogger csv = new TextFileBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(TextFileLogger), csv.ActualLogger.GetType());
        }
        
        [UnitTest]
        public void WindowsBuildLoggerActualLoggerShouldBeCorrectType()
        {
            WindowsBuildLogger csv = new WindowsBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(WindowsLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void XmlBuildLoggerActualLoggerShouldBeCorrectType()
        {
            WindowsBuildLogger csv = new WindowsBuildLogger();
            Expect.IsNotNull(csv.ActualLogger);
            Expect.AreEqual(typeof(WindowsLogger), csv.ActualLogger.GetType());
        }

        [UnitTest]
        public void UnzipResourceTest()
        {
            string extractTo = ".\\Unzip";
            if (Directory.Exists(extractTo))
            {
                Directory.Delete(extractTo, true);
            }
            Expect.IsTrue(Assembly.GetExecutingAssembly().UnzipResource(typeof(UnitTests), "Test.zip", extractTo));
            FileInfo[] files = new DirectoryInfo(extractTo).GetFiles();
            Expect.IsTrue(files.Length > 0);
        }

        [UnitTest]
        public void GetMemberTypeTest()
        {
            string name;
            ClassMemberType type = ClassDocumentation.GetMemberType("M:This.Is.A.Method", out name);
            Expect.IsTrue(type == ClassMemberType.Method);
            Expect.AreEqual("This.Is.A.Method", name);
            type = ClassDocumentation.GetMemberType("F:This.Is.A.Field", out name);
            Expect.IsTrue(type == ClassMemberType.Field);
            Expect.AreEqual("This.Is.A.Field", name);
            type = ClassDocumentation.GetMemberType("T:This.Is.A.Type", out name);
            Expect.IsTrue(type == ClassMemberType.Type);
            Expect.AreEqual("This.Is.A.Type", name);
            type = ClassDocumentation.GetMemberType("P:This.Is.A.Property", out name);
            Expect.IsTrue(type == ClassMemberType.Property);
            Expect.AreEqual("This.Is.A.Property", name);
        }

        [UnitTest]
        public void ReadXmlDocs()
        {
            doc doc = new FileInfo("./TestDoc.xml").FromXmlFile<doc>();
            OutLineFormat("Assembly Name: {0}", doc.assembly.name);
            if (doc.members == null)
            {
                OutLine("doc.members == null", ConsoleColor.Cyan);
            }
            else if (doc.members.Items == null)
            {
                OutLine("doc.members.Items == null", ConsoleColor.Yellow);
            }
            else
            {                
                OutLineFormat("Iterating on {0}", ConsoleColor.Cyan, "doc.members.Items");
                doc.members.Items.Each(member =>
                {
					OutLineFormat("member.name={0}", ConsoleColor.Yellow, member.name);
                    if (member.Items == null)
                    {
						OutLineFormat("member.Items == null");
                    }
                    else
                    {
						OutLineFormat("Iterating on {0}", ConsoleColor.Cyan, "member.Items");
                        member.Items.Each(item =>
                        {
                            Type itemType = item.GetType();
                            OutFormat("\tItem type = {0}", ConsoleColor.Yellow, itemType.FullName);
                            summary summary = item as summary;
                            if (summary != null)
                            {
                                HandleSummary(summary);
                            }
                        });
                    }
                });
            }
        }

        [UnitTest]
        public void DocInfoFromXmlFileShouldHaveDeclaringTypeName()
        {
            Dictionary<string, List<ClassDocumentation>> infos = ClassDocumentation.FromXmlFile("./TestBuildProject.xml");
            infos.Keys.Each(s =>
            {
                OutLine(s, ConsoleColor.Cyan);

                infos[s].Each(info =>
                {
                    OutputInfo(info);
                });
            });
        }

        /// <summary>
        /// This is the xml summary
        /// </summary>
        [ClassDocumentation(@"This class is for testing documentation
and whatever")]
        class DocumentedClassTest
        {
            [ClassDocumentation(@"This is a method that takes no 
arguments")]
            public void TestMethod()
            {

            }

            [ClassDocumentation(@"This method returns 
an empty string")]
            public string ReturnsStringMethod()
            {
                return string.Empty;
            }

            [ClassDocumentation(@"This method takes arguments", "the reason this is funny")]
            public string ReturnFunnyString(string reason)
            {
                return reason + " funny";
            }
        }

        [UnitTest]
        public void DocInfoFromAttributeShouldHaveDeclaringTypeName()
        {
            Dictionary<string, List<ClassDocumentation>> infos = ClassDocumentation.FromDocAttributes(typeof(DocumentedClassTest));
            Expect.IsGreaterThan(infos.Count, 0);

            infos.Keys.Each(type =>
            {
                infos[type].Each(info =>
                {
                    Out("From: ");
                    OutLine(info.From.ToString(), ConsoleColor.Cyan);

                    if (info.From == ClassDocumentationFrom.Reflection)
                    {
                        Expect.IsFalse(string.IsNullOrEmpty(info.DeclaringTypeName));
                    }
                    OutputInfo(info);
                });
            });
        }

        [UnitTest]
        public void ShouldBeAbleToInferDocs()
        {
            Dictionary<string, List<ClassDocumentation>> infos = ClassDocumentation.Infer(Assembly.GetExecutingAssembly());
            Expect.IsGreaterThan(infos.Count, 0);

            infos.Keys.Each(type =>
            {
                infos[type].Each(info =>
                {
                    Out("From: ", ConsoleColor.Cyan);
                    ConsoleColor fromColor = info.From == ClassDocumentationFrom.Reflection ? ConsoleColor.Blue : ConsoleColor.Yellow;
                    OutLine(info.From.ToString(), fromColor);

                    if (info.From == ClassDocumentationFrom.Reflection)
                    {
                        Expect.IsFalse(string.IsNullOrEmpty(info.DeclaringTypeName));
                    }

                    OutputInfo(info);
                });
            });
        }

        private static void OutputInfo(ClassDocumentation info)
        {
            OutLineFormat("Summary:\r\n{0}", ConsoleColor.Blue, info.Summary);
            OutLineFormat("DeclaringTypName: {0}", ConsoleColor.Yellow, info.DeclaringTypeName);
            OutLineFormat("MemberType: {0}", ConsoleColor.Magenta, info.MemberType.ToString());
            OutLineFormat("MemberName: {0}", ConsoleColor.Yellow, info.MemberName);

            if (info.MemberType == ClassMemberType.Method)
            {
                info.ParamInfos.Each(p =>
                {
                    OutLineFormat("Parameter: {0}, Description: {1}", p.Name, p.Description);
                });
            }
            if (info.HasExtraItems)
            {
                OutLine("Extra Items", ConsoleColor.Yellow);
                info.Items().Each(o =>
                {
                    if (o != null)
                    {
                        OutLineFormat("\tType: {0}", ConsoleColor.Yellow, o.GetType());
                        OutLineFormat("\t{0}", o.ValuePropertiesToDynamic().PropertiesToString());
                    }
                });
            }
        }

        class TestCsvLogger: CsvLogger
        {
            public void Run()
            {
                LogEvent evt = CreateInfoEvent("this is a test");
                string msg = GetLogText(evt);
                Out(msg);
            }
        }

        [UnitTest]
        public void CsvLoggerTest()
        {
            TestCsvLogger test = new TestCsvLogger();
            test.Run();
        }
        
        [UnitTest]
        public void BuildConfShouldSave()
        {
            WorkerConf conf = CreateTestConf();

            conf.Save();
            string fileName = ".\\{0}_WorkerConf.json"._Format(conf.Name);
            Expect.IsTrue(File.Exists(fileName));
            File.Delete(fileName);
        }

        private static WorkerConf CreateTestConf()
        {
            WorkerConf conf = new WorkerConf();
            conf.Name = "Test_".RandomLetters(4);
            return conf;
        }

        [UnitTest]
        public void WhenWorkerConfSavesShouldSetWorkerTypeName()
        {
            string filePath = "{0}.json"._Format(MethodBase.GetCurrentMethod().Name);
            AllProjectsBuildWorker worker = new AllProjectsBuildWorker("monkey");
            worker.SaveConf(filePath);
            WorkerConf conf = WorkerConf.Load(filePath);
            Expect.IsNotNull(conf.WorkerTypeName);
            Expect.AreEqual(typeof(AllProjectsBuildWorker).AssemblyQualifiedName, conf.WorkerTypeName);
        }

        [UnitTest]
        public void JobConfShouldCreateValidJob()
        {
            DirectoryInfo dir = new DirectoryInfo(".\\{0}"._Format(MethodBase.GetCurrentMethod().Name));
            if (dir.Exists)
            {
                dir.Delete(true);
            }

            JobConf jobConf = new JobConf();
            jobConf.JobDirectory = dir.FullName;

            GitGetSourceWorker worker = new GitGetSourceWorker("monkey");
            jobConf.AddWorker(worker);
            string filePath = jobConf.Save();

            JobConf check = JobConf.Load(filePath);
            Job job = check.CreateJob();
            IWorker checkWork = job["monkey"];
            Expect.IsNotNull(checkWork);
            Expect.AreEqual(typeof(GitGetSourceWorker), checkWork.GetType());

            GitGetSourceWorker checkWorker = job.GetWorker<GitGetSourceWorker>("monkey");
            Expect.AreEqual("Git", checkWorker.SourceControlType);
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
        
        private void HandleSummary(summary summary)
        {
            OutLine("Summary line by line");
            summary.Text.Each(text =>
            {
                OutLineFormat("{0}", text);
            });
            if (summary.Items != null)
            {
                OutLine("Iterating over summary.Items");
                summary.Items.Each(o =>
                {
                    Type itemType = o.GetType();
                    OutLineFormat("\tItem type = {0}", ConsoleColor.Yellow, itemType.FullName);
                });
            }
            else
            {
                OutLine("summary.Items was null");
            }

            if (summary.Items1 != null)
            {
                OutLine("Iterating over summary.Items1");
                summary.Items1.Each(o =>
                {
                    Type itemType = o.GetType();
                    OutLineFormat("\tItem type = {0}", ConsoleColor.Blue, itemType.FullName);
                });
            }
            else
            {
                OutLine("summary.Items1 was null");
            }
        }

        private void OutFormat(int tabCount, string format, ConsoleColor color, params string[] args)
        {
            StringBuilder tabs = new StringBuilder();
            tabCount.Times(i => tabs.Append("\t"));
            OutLineFormat("{0}{1}", color, tabs, format._Format(args));
        }
    }
}

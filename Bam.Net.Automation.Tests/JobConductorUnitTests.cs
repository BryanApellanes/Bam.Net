/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Automation;
using System.IO;
using System.Reflection;
using System.Threading;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Microsoft.Build.Framework;
using M = Microsoft.Build.Logging;
using Microsoft.Build.Execution;
using Bam.Net.Testing.Unit;
using Bam.Net.Services;
using System.CodeDom.Compiler;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class JobConductorUnitTests : CommandLineTestInterface
    {
        [UnitTest("JobConductor:: Verify Jobs Folder in AppDataFolder for new JobConductor")]
        public void JobsDirectoryShouldBeInAppDataFolder()
        {
            JobConductorService jobConductor = new JobConductorService();
            Expect.AreEqual(Path.Combine(RuntimeSettings.AppDataFolder, "Jobs"), jobConductor.JobsDirectory);
        }

        public void DeleteJobsDirectory()
        {
            JobConductorService svc = new JobConductorService();
            string dir = svc.JobsDirectory;
            if (Directory.Exists(dir))
            {
                Directory.Delete(svc.JobsDirectory, true);
            }
        }

        [UnitTest("JobConductor:: Should Create JobConf")]
        public void JobConductorShouldCreaetJobConf()
        {
            JobConductorService jc = new JobConductorService()
            {
                JobsDirectory = new DirectoryInfo(MethodBase.GetCurrentMethod().Name).FullName
            };
            string name = "JobConfTest_".RandomLetters(4);
            JobConf conf = jc.CreateJob(name);
            string path = Path.Combine(jc.JobsDirectory, conf.Name, conf.Name + ".job");
            Expect.IsTrue(File.Exists(path));
        }

        [UnitTest("JobConductor:: JobExists should be true after create")]
        public void ExistsShouldBeTrueAfterCreate()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string testJobName = name + "_JobName_".RandomLetters(4);
            jc.CreateJob(testJobName);
            Expect.IsTrue(jc.JobExists(testJobName));
        }
        
        [UnitTest("Func Compiler Test")]
        public void FuncCompilerTest()
        {
            string code = @"
using System;
public class FuncProvider
{ 
    public Func<bool> GetFunc()
    {
        return (Func<bool>)(() => $Code$);
    }
}";
            
            CompilerResults results = AdHocCSharpCompiler.CompileSource(code.Replace("$Code$", "true"), "TestAssembly");
            Func<bool> o = (Func<bool>)results.CompiledAssembly.GetType("FuncProvider").Construct().Invoke("GetFunc");
            Expect.IsTrue(o());
        }

        private static JobConductorService GetTestJobConductor(string jobConductorName)
        {
            DirectoryInfo dir = new DirectoryInfo("JobConductor_" + jobConductorName);
            if (dir.Exists)
            {
                dir.Delete(true);
            }
            JobConductorService jc = new JobConductorService
            {
                JobsDirectory = dir.FullName
            };
            return jc;
        }

        [UnitTest("JobConductor:: CreateJob should throw an exception if it already exists")]
        public void CreateJobShouldThrowExceptionIfItExists()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string testJobName = name + "_JobName_".RandomLetters(4);
            jc.CreateJob(testJobName);
            Expect.IsTrue(jc.JobExists(testJobName));
            Expect.Throws(() => jc.CreateJob(testJobName), "Should have thrown an exception but didn't");
        }


        [UnitTest("JobConductor:: JobDirectory should be set on JobConf")]
        public void JobConductorShouldCreateJobConfWithJobDirectorySet()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService foreman = GetTestJobConductor(name);
            JobConf conf = foreman.CreateJobConf(name);
            Expect.AreEqual(name, conf.Name);
            Expect.IsTrue(conf.JobDirectory.StartsWith(foreman.JobsDirectory), "conf directory wasn't set correctly");
        }

        [UnitTest("JobConductor:: GetJob should create new job")]
        public void GetJobShouldCreateNewJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            Expect.IsFalse(jc.JobExists(name));
            JobConf validate = jc.GetJob(name);

            Expect.IsNotNull(validate);
            Expect.AreEqual(name, validate.Name);
            Expect.IsTrue(jc.JobExists(validate.Name));
            Expect.IsTrue(File.Exists(validate.GetFilePath()));
        }

        [UnitTest("JobConductor:: GetJob should return existingJob")]
        public void GetJobShouldReturnExistingJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            Expect.IsFalse(jc.JobExists(name));
            JobConf conf = jc.CreateJob(name);
            Expect.IsTrue(jc.JobExists(name));

            JobConf validate = jc.GetJobConf(name);
            Expect.IsNotNull(validate);
            Expect.AreEqual(name, validate.Name);
        }

        [UnitTest("JobConductor:: Job should run if job runner thread is running")]
        public void JobShouldRunIfRunnerThreadIsRunning()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            jc.StopJobRunnerThread();
            jc.JobQueue.Clear();
            jc.StartJobRunnerThread();

            JobConf conf = jc.CreateJob(name);
            TestWorker.ValueToCheck = false;
            TestWorker worker = new TestWorker();
            conf.AddWorker(worker);
            Expect.IsFalse(TestWorker.ValueToCheck);

            bool? finished = false;
            AutoResetEvent signal = new AutoResetEvent(false);
            jc.WorkerFinished += (o, a) =>
            {
                Expect.IsTrue(TestWorker.ValueToCheck);
                finished = true;
                signal.Set();
            };

            jc.EnqueueJob(conf);
            signal.WaitOne(10000);
            Expect.IsTrue(finished == true);
        }

        [UnitTest("JobConductor:: Add Worker to non existent job should create new job")]
        public void AddWorkerShouldCreateJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string jobName = "Job_".RandomLetters(4);
            Expect.IsFalse(jc.JobExists(jobName));

            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, "worker");

            Expect.IsTrue(jc.JobExists(jobName));
        }

        [UnitTest("JobConductor:: AddWorker should throw ArgumentNullException if type not found")]
        public void AddWorkerShouldThrowArgumentNullException()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            Expect.Throws(() =>
            {
                jc.AddWorker("JobName", "noTypeByThisNameShouldBeFound".RandomLetters(4), "work_" + name);
            }, (ex) =>
            {
                ex.IsInstanceOfType<ArgumentNullException>("Exception wasn't the right type");
            }, "Should have thrown an exception but didn't");
        }

        [UnitTest("JobConductor:: AddWorker should create worker")]
        public void AddWorkerShouldSetWorkerName()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string workerName = "worker_" + name;
            string jobName = "Job_" + name;
            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, workerName);
            Expect.IsTrue(jc.WorkerExists(jobName, workerName));
        }

        [UnitTest("JobConductor:: AddWorker should create worker and Job should know")]
        public void ShouldBeAbleToAddWorker()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string jobName = "Job_" + name;
            string workerName = "worker_1";
            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, workerName);

            JobConf job = jc.GetJob(jobName);

            Expect.IsTrue(job.WorkerExists(workerName));
        }

        [UnitTest("JobConductor:: After AddWorker job create should have expected worker count")]
        public void AfterAddWorkerCreateJobShouldHaveCorrectWorkers()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService jc = GetTestJobConductor(name);
            string jobName = "Job_" + name;
            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, "one");
            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, "two");

            JobConf conf = jc.GetJob(jobName);
            Job job = conf.CreateJob();

            Expect.IsTrue(job.WorkerNames.Length == 2);
            Expect.IsNotNull(job["one"]);
            Expect.IsNotNull(job["two"]);
            Expect.AreEqual("one", job["one"].Name);
            Expect.AreEqual("two", job["two"].Name);
        }

        [UnitTest("JobConductor:: Should be able to run job with specified (0) based step number")]
        public void ShouldBeAbleToRunJobWithSpecifiedStepNumber()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            TestWorker.ValueToCheck = false;
            StepTestWorker.ValueToCheck = false;
            JobConductorService jc = GetTestJobConductor(name);
            string jobName = "Job_" + name;

            jc.AddWorker(jobName, typeof(TestWorker).AssemblyQualifiedName, "TestWorker");
            jc.AddWorker(jobName, typeof(StepTestWorker).AssemblyQualifiedName, "StepTestWorker");

            Expect.IsFalse(TestWorker.ValueToCheck);
            Expect.IsFalse(StepTestWorker.ValueToCheck);
            bool? finished = false;
            AutoResetEvent signal = new AutoResetEvent(false);
            jc.JobFinished += (o, a) =>
            {
                Expect.IsFalse(TestWorker.ValueToCheck, "testworker value should have been false after job finished");
                Expect.IsTrue(StepTestWorker.ValueToCheck, "Step test worker value should have been true after job finished");
                finished = true;
                signal.Set();
            };

            JobConf conf = jc.GetJob(jobName);
            jc.RunJob(conf.CreateJob(), 1);
            signal.WaitOne(10000);
            Expect.IsTrue(finished.Value, "finished value should have been set");
        }

        class TestWorker : Worker
        {
            public static bool ValueToCheck { get; set; }
            protected override WorkState Do(WorkState currentWorkState)
            {
                ValueToCheck = true;
                return new WorkState(this, "success") { PreviousWorkState = currentWorkState };
            }

            public override string[] RequiredProperties
            {
                get { return new string[] { }; }
            }
        }

        class StepTestWorker : Worker
        {
            public static bool ValueToCheck
            {
                get;
                set;
            }

            protected override WorkState Do(WorkState currentWorkState)
            {
                WorkState state = new WorkState(this) { PreviousWorkState = currentWorkState };
                ValueToCheck = true;
                return state;
            }

            public override string[] RequiredProperties
            {
                get { return new string[] { }; }
            }
        }
    }
}

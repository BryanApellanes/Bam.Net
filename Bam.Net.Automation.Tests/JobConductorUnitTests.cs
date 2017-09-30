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
using Bam.Net.Automation.ContinuousIntegration;
using System.IO;
using System.Reflection;
using System.Threading;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Automation.ContinuousIntegration.Loggers;
using Microsoft.Build.Framework;
using M = Microsoft.Build.Logging;
using Microsoft.Build.Execution;
using Bam.Net.Testing.Unit;
using Bam.Net.Services;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class JobConductorUnitTests : CommandLineTestInterface
    {
        [UnitTest("JobConductor:: Verify Jobs Folder in AppDataFolder for new JobConductor")]
        public void JobsDirectoryShouldBeInAppDataFolder()
        {
            JobConductorService orchestrator = new JobConductorService();
            Expect.AreEqual(Path.Combine(RuntimeSettings.AppDataFolder, "Jobs"), orchestrator.JobsDirectory);
        }

        public void DeleteJobsDirectory()
        {
            string dir = JobConductorService.Default.JobsDirectory;
            if (Directory.Exists(dir))
            {
                Directory.Delete(JobConductorService.Default.JobsDirectory, true);
            }
        }

        [UnitTest("JobConductor:: Should Create JobConf")]
        public void JobConductorShouldCreaetJobConf()
        {
            JobConductorService.Default.JobsDirectory = new DirectoryInfo(MethodBase.GetCurrentMethod().Name).FullName;
            JobConductorService fm = JobConductorService.Default;
            string name = "JobConfTest_".RandomLetters(4);
            JobConf conf = fm.CreateJob(name);
            string path = Path.Combine(fm.JobsDirectory, conf.Name, conf.Name + ".job");
            Expect.IsTrue(File.Exists(path));
        }

        [UnitTest("JobConductor:: JobExists should be true after create")]
        public void ExistsShouldBeTrueAfterCreate()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string testJobName = name + "_JobName_".RandomLetters(4);
            fm.CreateJob(testJobName);
            Expect.IsTrue(fm.JobExists(testJobName));
        }

        private static JobConductorService GetTestJobConductor(string jobConductorName)
        {
            DirectoryInfo dir = new DirectoryInfo("JobConductor_" + jobConductorName);
            if (dir.Exists)
            {
                dir.Delete(true);
            }
            JobConductorService fm = new JobConductorService();
            fm.JobsDirectory = dir.FullName;
            return fm;
        }

        [UnitTest("JobConductor:: CreateJob should throw an exception if it already exists")]
        public void CreateJobShouldThrowExceptionIfItExists()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string testJobName = name + "_JobName_".RandomLetters(4);
            fm.CreateJob(testJobName);
            Expect.IsTrue(fm.JobExists(testJobName));
            Expect.Throws(() =>
            {
                fm.CreateJob(testJobName);
            }, "Should have thrown an exception but didn't");
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

        class TestWorker : Worker
        {
            public static bool ValueToCheck { get; set; }
            protected override WorkState Do()
            {
                ValueToCheck = true;
                return new WorkState(this, "success");
            }

            public override string[] RequiredProperties
            {
                get { return new string[] { }; }
            }
        }

        [UnitTest("JobConductor:: GetJob should create new job")]
        public void GetJobShouldCreateNewJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            Expect.IsFalse(fm.JobExists(name));
            JobConf validate = fm.GetJob(name);

            Expect.IsNotNull(validate);
            Expect.AreEqual(name, validate.Name);
            Expect.IsTrue(fm.JobExists(validate.Name));
            Expect.IsTrue(File.Exists(validate.GetFilePath()));
        }

        [UnitTest("JobConductor:: GetJob should return existingJob")]
        public void GetJobShouldReturnExistingJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService orch = GetTestJobConductor(name);
            Expect.IsFalse(orch.JobExists(name));
            JobConf conf = orch.CreateJob(name);
            Expect.IsTrue(orch.JobExists(name));

            JobConf validate = orch.GetJobConf(name);
            Expect.IsNotNull(validate);
            Expect.AreEqual(name, validate.Name);
        }

        [UnitTest("JobConductor:: Job should run if job runner thread is running")]
        public void JobShouldRunIfRunnerThreadIsRunning()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            fm.StopJobRunnerThread();
            fm.JobQueue.Clear();
            fm.StartJobRunnerThread();

            JobConf conf = fm.CreateJob(name);
            TestWorker.ValueToCheck = false;
            TestWorker worker = new TestWorker();
            conf.AddWorker(worker);
            Expect.IsFalse(TestWorker.ValueToCheck);

            bool? finished = false;
            AutoResetEvent signal = new AutoResetEvent(false);
            fm.WorkerFinished += (o, a) =>
            {
                Expect.IsTrue(TestWorker.ValueToCheck);
                finished = true;
                signal.Set();
            };

            fm.EnqueueJob(conf);
            signal.WaitOne(10000);
            Expect.IsTrue(finished == true);
        }

        [UnitTest("JobConductor:: Add Worker to non existent job should create new job")]
        public void AddWorkerShouldCreateJob()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string jobName = "Job_".RandomLetters(4);
            Expect.IsFalse(fm.JobExists(jobName));

            fm.AddWorker(typeof(TestWorker).AssemblyQualifiedName, "worker", jobName);

            Expect.IsTrue(fm.JobExists(jobName));
        }

        [UnitTest("JobConductor:: AddWorker should throw ArgumentNullException if type not found")]
        public void AddWorkerShouldThrowArgumentNullException()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            Expect.Throws(() =>
            {
                fm.AddWorker("noTypeByThisNameShouldBeFound".RandomLetters(4), "work_" + name, "JobName");
            }, (ex) =>
            {
                ex.IsInstanceOfType<ArgumentNullException>("Exception wasn't the right type");
            }, "Should have thrown an exception but didn't");
        }

        [UnitTest("JobConductor:: AddWorker should create worker")]
        public void AddWorkerShouldSetWorkerName()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string workerName = "worker_" + name;
            string jobName = "Job_" + name;
            fm.AddWorker(typeof(TestWorker).AssemblyQualifiedName, workerName, jobName);
            Expect.IsTrue(fm.WorkerExists(jobName, workerName));
        }

        [UnitTest("JobConductor:: AddWorker should create worker and Job should know")]
        public void ShouldBeAbleToAddWorker()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string jobName = "Job_" + name;
            string workerName = "worker_1";
            fm.AddWorker(typeof(TestWorker).AssemblyQualifiedName, workerName, jobName);

            JobConf job = fm.GetJob(jobName);

            Expect.IsTrue(job.WorkerExists(workerName));
        }

        [UnitTest("JobConductor:: After AddWorker job create should have expected worker count")]
        public void AfterAddWorkerCreateJobShouldHaveCorrectWorkers()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            JobConductorService fm = GetTestJobConductor(name);
            string jobName = "Job_" + name;
            fm.AddWorker(typeof(TestWorker).AssemblyQualifiedName, "one", jobName);
            fm.AddWorker(typeof(TestWorker).AssemblyQualifiedName, "two", jobName);

            JobConf conf = fm.GetJob(jobName);
            Job job = conf.CreateJob();

            Expect.IsTrue(job.WorkerNames.Length == 2);
            Expect.IsNotNull(job["one"]);
            Expect.IsNotNull(job["two"]);
            Expect.AreEqual("one", job["one"].Name);
            Expect.AreEqual("two", job["two"].Name);
        }

        class StepTestWorker : Worker
        {
            public static bool ValueToCheck
            {
                get;
                set;
            }

            protected override WorkState Do()
            {
                WorkState state = new WorkState(this);
                ValueToCheck = true;
                return state;
            }

            public override string[] RequiredProperties
            {
                get { return new string[] { }; }
            }
        }
        [UnitTest("JobConductor:: Should be able to run job with specified (0) based step number")]
        public void ShouldBeAbleToRunJobWithSpecifiedStepNumber()
        {
            string name = MethodBase.GetCurrentMethod().Name;
            TestWorker.ValueToCheck = false;
            StepTestWorker.ValueToCheck = false;
            JobConductorService jc = GetTestJobConductor(name);
            string jobName = "Job_" + name;

            jc.AddWorker(typeof(TestWorker).AssemblyQualifiedName, "TestWorker", jobName);
            jc.AddWorker(typeof(StepTestWorker).AssemblyQualifiedName, "StepTestWorker", jobName);

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
    }
}

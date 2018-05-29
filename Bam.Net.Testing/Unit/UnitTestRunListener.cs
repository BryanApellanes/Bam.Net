using Bam.Net.Data.Repositories;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Testing.Unit
{
    /// <summary>
    /// A local test run listener for basic 
    /// reporting of test results
    /// </summary>
    public class UnitTestRunListener : TestRunListener<UnitTestMethod>
    {
        public UnitTestRunListener()
        {
        }
        public UnitTestRunListener(string resultDirectory, string resultFileName)
        {
            DaoRepository = new DaoRepository(new SQLiteDatabase(resultDirectory, resultFileName));
            DaoRepository.AddType(typeof(TestResult));
            DaoRepository.EnsureDaoAssemblyAndSchema();            
        }
        public DaoRepository DaoRepository { get; set; }

        public override void TestFailed(object sender, TestExceptionEventArgs args)
        {
            TestResult result = new TestResult(args);
            DaoRepository.Save(result);
        }

        public override void TestPassed(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestResult result = new TestResult(args.Test);
            DaoRepository.Save(result);
        }
    }
}

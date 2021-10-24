using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;
using Bam.Net.ServiceProxy;
using Bam.Net.Data.Repositories;
using Bam.Net.Data;
using Bam.Net.Configuration;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net.Testing.Data;
using Bam.Net.Automation.Testing;
using Bam.Net.Automation.Testing.Data;
using Bam.Net.Automation.Testing.Data.Dao.Repository;
using Bam.Net.CoreServices;

namespace Bam.Net.Automation.Testing
{
    public partial class TestReportService
    {
        /// <summary>
        /// Get an existing SuiteDefinition with the specified title or
        /// create it if none exists
        /// </summary>
        /// <param name="suiteTitle"></param>
        /// <returns></returns>
        public virtual GetSuiteDefinitionResponse GetSuiteDefinition(string suiteTitle)
        {
            try
            {
                TestSuiteDefinition result = GetOrCreateSuiteDefinition(new TestSuiteDefinition { Title = suiteTitle }, out CreateStatus createStatus).ToDynamicData().CopyAs<TestSuiteDefinition>();
                return new GetSuiteDefinitionResponse { Success = true, Data = result, CreateStatus = createStatus };
            }
            catch (Exception ex)
            {
                return new GetSuiteDefinitionResponse { Success = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Get an existing TestDefinition for the specified title and definition
        /// or create it if none exists
        /// </summary>
        /// <param name="suiteTitle"></param>
        /// <param name="testDefinition"></param>
        /// <returns></returns>
        public virtual GetTestDefinitionResponse GetTestDefinition(string suiteTitle, TestDefinition testDefinition)
        {
            try
            {
                TestDefinition result = GetOrCreateTestDefinition(suiteTitle, testDefinition, out CreateStatus createStatus);
                result = result.ToDynamicData().CopyAs<TestDefinition>();
                return new GetTestDefinitionResponse { Success = true, Data = result, CreateStatus = createStatus };
            }
            catch (Exception ex)
            {
                return new GetTestDefinitionResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual SaveTestSuiteExecutionSummaryResponse SaveTestSuiteExecutionSummary(TestSuiteExecutionSummary toCreate = null)
        {
            try
            {
                toCreate = toCreate ?? new TestSuiteExecutionSummary();
                Meta.SetAuditFields(toCreate);
                TestSuiteExecutionSummary sum = TestingRepository.Save(toCreate).ToDynamicData().CopyAs<TestSuiteExecutionSummary>();
                return new SaveTestSuiteExecutionSummaryResponse { Success = true, Data = sum, CreateStatus = toCreate.Id > 0 ? CreateStatus.Existing : CreateStatus.Created };
            }
            catch (Exception ex)
            {
                return new SaveTestSuiteExecutionSummaryResponse { Success = false, Message = ex.Message };
            }
        }
    }
}

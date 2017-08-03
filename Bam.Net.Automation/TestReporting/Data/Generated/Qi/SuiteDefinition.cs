/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net;
using System.Web.Mvc;
using Bam.Net.Data;
using Bam.Net.Data.Qi;
using Bam.Net.Automation.TestReporting.Data.Dao;

namespace Bam.Net.Automation.TestReporting.Data.Dao.Qi
{
    public class SuiteDefinitionController : DaoController
    {	
		public ActionResult Save(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition[] values)
		{
			try
			{
				SuiteDefinitionCollection saver = new SuiteDefinitionCollection();
				saver.AddRange(values);
				saver.Save();
				return Json(new { Success = true, Message = "", Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult Create(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition dao)
		{
			return Update(dao);
		}

		public ActionResult Retrieve(long id)
        {
			try
			{
				object value = Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
        }

		public ActionResult Update(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition dao)
        {
			try
			{
				dao.Save();
				return Json(new { Success = true, Message = "", Dao = dao.ToJsonSafe() });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}            
        }

		public ActionResult Delete(long id)
		{
			try
			{
				string msg = "";
				Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition dao = Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.OneWhere(c => c.KeyColumn == id);				
				if(dao != null)
				{
					dao.Delete();	
				}
				else
				{
					msg = string.Format("The specified id ({0}) was not found in the table (SuiteDefinition)", id);
				}
				return Json(new { Success = true, Message = msg, Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult OneWhere(QiQuery query)
		{
			try
			{
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition));
				object value = Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.OneWhere(query).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}	 			
		}

		public ActionResult Where(QiQuery query)
		{
			try
			{
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition));
				object[] value = Bam.Net.Automation.TestReporting.Data.Dao.SuiteDefinition.Where(query).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		private ActionResult GetErrorResult(Exception ex)
		{
			return Json(new { Success = false, Message = ex.Message });
		}
	}
}
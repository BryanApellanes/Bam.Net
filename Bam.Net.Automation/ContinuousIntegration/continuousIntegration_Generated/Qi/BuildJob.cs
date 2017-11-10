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
using Bam.Net.Automation.ContinuousIntegration.Data;

namespace Bam.Net.Automation.ContinuousIntegration.Data.Qi
{
    public class BuildJobController : DaoController
    {	
		public ActionResult Save(Bam.Net.Automation.ContinuousIntegration.Data.BuildJob[] values)
		{
			try
			{
				BuildJobCollection saver = new BuildJobCollection();
				saver.AddRange(values);
				saver.Save();
				return Json(new { Success = true, Message = "", Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult Create(Bam.Net.Automation.ContinuousIntegration.Data.BuildJob dao)
		{
			return Update(dao);
		}

		public ActionResult Retrieve(long id)
        {
			try
			{
				object value = Bam.Net.Automation.ContinuousIntegration.Data.BuildJob.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
        }

		public ActionResult Update(Bam.Net.Automation.ContinuousIntegration.Data.BuildJob dao)
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
				Bam.Net.Automation.ContinuousIntegration.Data.BuildJob dao = Bam.Net.Automation.ContinuousIntegration.Data.BuildJob.OneWhere(c => c.KeyColumn == id);				
				if(dao != null)
				{
					dao.Delete();	
				}
				else
				{
					msg = string.Format("The specified id ({0}) was not found in the table (BuildJob)", id);
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
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Automation.ContinuousIntegration.Data.BuildJob));
				object value = Bam.Net.Automation.ContinuousIntegration.Data.BuildJob.OneWhere(query).ToJsonSafe();
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
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Automation.ContinuousIntegration.Data.BuildJob));
				object[] value = Bam.Net.Automation.ContinuousIntegration.Data.BuildJob.Where(query).ToJsonSafe();
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
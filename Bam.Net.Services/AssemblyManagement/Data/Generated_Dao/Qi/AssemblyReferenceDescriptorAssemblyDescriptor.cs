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
using Bam.Net.Services.AssemblyManagement.Data.Dao;

namespace Bam.Net.Services.AssemblyManagement.Data.Dao.Qi
{
    public class AssemblyReferenceDescriptorAssemblyDescriptorController : DaoController
    {	
		public ActionResult Save(Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor[] values)
		{
			try
			{
				AssemblyReferenceDescriptorAssemblyDescriptorCollection saver = new AssemblyReferenceDescriptorAssemblyDescriptorCollection();
				saver.AddRange(values);
				saver.Save();
				return Json(new { Success = true, Message = "", Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult Create(Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor dao)
		{
			return Update(dao);
		}

		public ActionResult Retrieve(long id)
        {
			try
			{
				object value = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
        }

		public ActionResult Update(Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor dao)
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
				Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor dao = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.OneWhere(c => c.KeyColumn == id);				
				if(dao != null)
				{
					dao.Delete();	
				}
				else
				{
					msg = string.Format("The specified id ({0}) was not found in the table (AssemblyReferenceDescriptorAssemblyDescriptor)", id);
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
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor));
				object value = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.OneWhere(query).ToJsonSafe();
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
				query.table = Bam.Net.Data.Dao.TableName(typeof(Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor));
				object[] value = Bam.Net.Services.AssemblyManagement.Data.Dao.AssemblyReferenceDescriptorAssemblyDescriptor.Where(query).ToJsonSafe();
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
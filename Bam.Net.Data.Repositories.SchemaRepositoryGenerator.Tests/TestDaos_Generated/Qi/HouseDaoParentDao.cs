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
using Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_;

namespace Qi
{
    public class HouseDaoParentDaoController : DaoController
    {	
		public ActionResult Save(Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao[] values)
		{
			try
			{
				HouseDaoParentDaoCollection saver = new HouseDaoParentDaoCollection();
				saver.AddRange(values);
				saver.Save();
				return Json(new { Success = true, Message = "", Dao = "" });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
		}

		public ActionResult Create(Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao dao)
		{
			return Update(dao);
		}

		public ActionResult Retrieve(long id)
        {
			try
			{
				object value = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
				return Json(new { Success = true, Message = "", Dao = value });
			}
			catch(Exception ex)
			{
				return GetErrorResult(ex);
			}
        }

		public ActionResult Update(Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao dao)
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
				Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao dao = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.OneWhere(c => c.KeyColumn == id);				
				if(dao != null)
				{
					dao.Delete();	
				}
				else
				{
					msg = string.Format("The specified id ({0}) was not found in the table (HouseDaoParentDao)", id);
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
				query.table = Dao.TableName(typeof(HouseDaoParentDao));
				object value = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.OneWhere(query).ToJsonSafe();
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
				query.table = Dao.TableName(typeof(HouseDaoParentDao));
				object[] value = Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_.HouseDaoParentDao.Where(query).ToJsonSafe();
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
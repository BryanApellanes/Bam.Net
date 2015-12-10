/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema; 
using Bam.Net.Data.Qi;
using Alsdobo.Models;

namespace Alsdobo.Controllers
{
    [Proxy("userRole")]
    public partial class UserRole
    {
        
        public object Create(Alsdobo.Models.UserRole userrole)
        {
            return Update(userrole);
        }

        public object Retrieve(long id)
        {
            return Alsdobo.Models.UserRole.OneWhere(c => c.KeyColumn == id).ToJsonSafe();
        }

        public object Update(Alsdobo.Models.UserRole userrole)
        {
            userrole.Save();
            return userrole.ToJsonSafe();
        }
        
        public void Delete(Alsdobo.Models.UserRole userrole)
        {
            userrole.Delete();            
        }

        public object[] Search(QiQuery query)
        {
            return new Alsdobo.Models.Qi.UserRole().Where(query);
        }

        [Exclude]
        public static Type GetModelType()
        {
            return typeof(Alsdobo.Models.UserRole);          
        }

    }
}
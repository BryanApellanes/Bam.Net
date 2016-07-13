using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.Configuration
{
    public static class RuntimeSettings
    {
        public static string AppDataFolder
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                }
                else
                {
                    return HttpContext.Current.Server.MapPath("~/App_Data/");
                }
            }
        }
    }
}

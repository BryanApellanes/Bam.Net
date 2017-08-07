using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.Configuration
{
    public static class RuntimeSettings
    {
        static string _appDataFolder;
        static object _appDataFolderLock = new object();
        
        public static string AppDataFolder
        {
            get
            {
                return _appDataFolderLock.DoubleCheckLock(ref _appDataFolder, () =>
                {
                    StringBuilder path = new StringBuilder();
                    if (HttpContext.Current == null)
                    {
                        path.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                        if (!path.ToString().EndsWith("\\"))
                        {
                            path.Append("\\");
                        }

                        path.Append(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKNOWN") + "\\");
                        FileInfo fileInfo = new FileInfo(path.ToString());
                        if (!Directory.Exists(fileInfo.Directory.FullName))
                        {
                            Directory.CreateDirectory(fileInfo.Directory.FullName);
                        }
                    }
                    else
                    {
                        path.Append(HttpContext.Current.Server.MapPath("~/App_Data/"));                        
                    }
                    _appDataFolder = path.ToString();
                    return _appDataFolder;
                });
            }
        }
        
        public static Func<Type, bool> ClrTypeFilter
        {
            get
            {
                return (t) => !t.IsAbstract && !t.HasCustomAttributeOfType<CompilerGeneratedAttribute>()
                              && t.Attributes != (
                                      TypeAttributes.NestedPrivate |
                                      TypeAttributes.Sealed |
                                      TypeAttributes.Serializable |
                                      TypeAttributes.BeforeFieldInit
                                  );
            }
        } 
    }
}

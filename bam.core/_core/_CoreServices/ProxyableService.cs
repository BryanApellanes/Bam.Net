using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public abstract partial class ProxyableService
    {
        [RoleRequired("/", "Admin", "Diagnoser")]
        public virtual Dictionary<string, string> GetSettings()
        {
            object userDatabase = UserManager?.Property("Database", false);

            Dictionary<string, string> settings = new Dictionary<string, string>
            {
                { "DiagnosticName", DiagnosticName },
                { "UserManager.Database.ConnectionName", userDatabase?.Property<string>("ConnectionName") },
                { "UserManager.Database.ConnectionString", userDatabase?.Property<string>("ConnectionString") },
                { "ApplicationName", ApplicationName },
                { "ProcessMode", ProcessMode.Current.ToString() },
                { "AppConfJson", AppConf.ToJson(true) },
                { "DaoRepository.Database.ConnectionName", DaoRepository?.Database?.Property<string>("ConnectionName") },
                { "DaoRepository.Database.ConnectionString", DaoRepository?.Database?.Property<string>("ConnectionString") },
                { "IRepository.StorableTypes", string.Join("\r\n", Repository?.StorableTypes.Select(t => t.FullName).ToArray() ?? Array.Empty<string>()) }
            };
            return settings;
        }
    }
}

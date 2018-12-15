using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public interface IVaultService
    {
        Dictionary<string, string> ExportValues();
        Dictionary<string, string> ExportUserValues();
        void ImportValues(Dictionary<string, string> values);
        void ImportUserValues(Dictionary<string, string> values);
        string GetValue(string keyName);
        void SetValue(string keyName, string value);
        string GetUserValue(string keyName);
        void SetUserValue(string keyName, string value);
    }
}

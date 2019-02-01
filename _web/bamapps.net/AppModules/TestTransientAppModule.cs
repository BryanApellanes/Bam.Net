using Bam.Net.Services;

namespace Bam.Net.Web.AppModules
{
    [Transient]
    public class TestTransientAppModule
    {
        public TestTransientAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}

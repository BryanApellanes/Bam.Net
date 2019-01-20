using Bam.Net.Services;

namespace Bam.Net.Web.AppModules
{
    [Singleton]
    public class TestSingletonAppModule
    {
        public TestSingletonAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}

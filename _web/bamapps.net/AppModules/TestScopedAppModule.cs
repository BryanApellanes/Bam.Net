using Bam.Net.Services;

namespace Bam.Net.Web.AppModules
{
    [Scoped]
    public class TestScopedAppModule
    {
        public TestScopedAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}

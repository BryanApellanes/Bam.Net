using Bam.Net.Services;

namespace Bam.Net.Web.AppModules
{
    [AppModule]
    public class TestAppModule
    {
        public TestAppModule()
        {
        }

        public string GetTestString(string input)
        {
            return $"{GetType().Name}: {input}";
        }
    }
}

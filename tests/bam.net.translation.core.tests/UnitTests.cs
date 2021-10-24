using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Translation.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTool
    {
        [UnitTest]
        public void DefaultTranslationDatabaseHasLanguages()
        {
            Database db = LanguageDatabase.Default;
            LanguageCollection languages = Language.LoadAll(db);
            Expect.IsTrue(languages.Count > 0);
            languages.Each(l => Message.PrintLine("{0}, {1}: {2}", l.ISO6392, l.ISO6391, l.EnglishName));
        }
    }
}

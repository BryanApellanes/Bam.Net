using Bam.Net.Analytics.EnglishDictionary;
using Bam.Net.CommandLine;
using Bam.Net.Data.SQLite;
using Bam.Net.Testing;
using Bam.Net.Web;
using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCuid;
using System.Threading;

namespace Bam.Net.Analytics.Tests
{
    [Serializable]
    public class ConsoleActions : CommandLineTestInterface
    {
        [ConsoleAction]
        public void PopulateDictionary()
        {
            SQLiteDatabase db = new SQLiteDatabase("c:\\temp", "EnglishDictionary");
            db.TryEnsureSchema<Word>();
            string urlTemplate = "http://www.mso.anu.edu.au/~ralph/OPTED/v003/wb1913_{0}.html";
            Parallel.ForEach(Bam.Net.Extensions.LowerCaseLetters, (letter) =>
            {
                Console.WriteLine("Getting letter: {0}", letter);
                string html = Http.Get(string.Format(urlTemplate, letter));
                CQ cq = CQ.Create(html);
                var paragraphs = cq["p", cq];
                Parallel.ForEach(paragraphs, (p) =>
                {
                    string word = cq["b", p].Text();
                    string wordType = cq["i", p].Text();
                    string definition = p.InnerText.TrimNonLetters();
                    Word wordEntry = GetWordEntry(word, db);
                    Definition definitionEntry = wordEntry.DefinitionsByWordId.AddNew();
                    definitionEntry.Cuid = Cuid.Generate();
                    definitionEntry.WordType = wordType;
                    definitionEntry.Value = definition;
                    definitionEntry.Save(db);
                    Console.WriteLine("Saved definition for {0} {1}", word, wordType);
                    Console.WriteLine("*** {0}", definition);
                });
            });
            Thread.Sleep(5000);
        }

        private Word GetWordEntry(string word, SQLiteDatabase db)
        {
            Word wordEntry = Word.FirstOneWhere(w => w.Value == word, db);
            if (wordEntry == null)
            {
                wordEntry = new Word { Value = word, Cuid = Cuid.Generate() };
                wordEntry.Save(db);
            }
            return wordEntry;
        }    
    }
}

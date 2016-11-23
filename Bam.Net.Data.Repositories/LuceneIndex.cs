/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Lucene.Net;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using System.Reflection;
using LuceneSearch = Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace Bam.Net.Data.Repositories
{
    public class LuceneIndex
    {
        /// <summary>
        /// Instantiate a new LuceneIndex
        /// </summary>
        /// <param name="indexDirectoryPath"></param>
        public LuceneIndex(string indexDirectoryPath, Lucene.Net.Util.Version luceneVersion = Lucene.Net.Util.Version.LUCENE_30)
        {
            IndexDirectoryPath = indexDirectoryPath;
            DirectoryProvider = () => FSDirectory.Open(IndexDirectoryPath);
            AnalyzerProvider = ()=> new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion);
            LuceneVersion = luceneVersion;
            ReaderProvider = () => IndexReader.Open(DirectoryProvider(), true);
        }
        
        public Lucene.Net.Util.Version LuceneVersion { get; set; }

        public string IndexDirectoryPath { get; private set; }
        public Func<Directory> DirectoryProvider { get; set; }        
        
        public Func<Analyzer> AnalyzerProvider { get; set; }        
        
        public void Index(params object[] instances)
        {
            Index(instances.Select(o => o.ToDocument()).ToArray());
        }
        
        public void Index(params Document[] documents)
        {
            using(IndexWriter idxWriter = new IndexWriter(DirectoryProvider(), AnalyzerProvider(), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach(Document doc in documents)
                {
                    idxWriter.AddDocument(doc);
                }
                idxWriter.Optimize();
                idxWriter.Flush(true, true, true);
            }
        }

        public IEnumerable<T> Search<T>(string propertyNameToSearch, string searchTerm, int topCount = int.MaxValue)
        {
            LuceneSearch.IndexSearcher searcher = new LuceneSearch.IndexSearcher(DirectoryProvider());
            QueryParser parser = new QueryParser(LuceneVersion, propertyNameToSearch, AnalyzerProvider());
            LuceneSearch.Query query = parser.Parse(searchTerm);
            LuceneSearch.TopDocs hits = searcher.Search(query, topCount);
            IndexReader reader = ReaderProvider();
            
            for (int i = 0; i < hits.ScoreDocs.Length; i++)
            {
                LuceneSearch.ScoreDoc scoreDoc = hits.ScoreDocs[i];
                Document doc = reader.Document(scoreDoc.Doc);
                yield return doc.FromDocument<T>();
            }
        }
        protected Func<IndexReader> ReaderProvider { get; set; }
    }
}

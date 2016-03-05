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
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace Bam.Net.Data.Repositories
{
    public class LuceneIndex
    {
        /// <summary>
        /// Instantiate a new LuceneIndex
        /// </summary>
        /// <param name="indexDirectoryPath"></param>
        public LuceneIndex(string indexDirectoryPath)
        {
            IndexDirectoryPath = indexDirectoryPath;
            DirectoryProvider = () => FSDirectory.Open(IndexDirectoryPath);
            AnalyzerProvider = ()=> new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
        }
        
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

        public void Search(string searchTerm)
        {
            IndexSearcher searcher = new IndexSearcher(DirectoryProvider());
            throw new NotImplementedException();
        }
    }
}

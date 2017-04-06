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
using System.IO;

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
            //DirectoryProvider = () => FSDirectory.Open(IndexDirectoryPath);
            AnalyzerProvider = ()=> new Lucene.Net.Analysis.Standard.StandardAnalyzer(luceneVersion);
            LuceneVersion = luceneVersion;
            //ReaderProvider = () => IndexReader.Open(DirectoryProvider(), true);
        }
        
        public Lucene.Net.Util.Version LuceneVersion { get; set; }

        public string IndexDirectoryPath { get; private set; }
        //public Func<Lucene.Net.Store.Directory> DirectoryProvider { get; set; }        
        FSDirectory _indexDirectory;
        public FSDirectory IndexDirectory
        {
            get
            {
                if (!System.IO.Directory.Exists(IndexDirectoryPath))
                {
                    System.IO.Directory.CreateDirectory(IndexDirectoryPath);
                }
                if (_indexDirectory == null)
                {
                    _indexDirectory = FSDirectory.Open(IndexDirectoryPath);
                }
                if (IndexWriter.IsLocked(_indexDirectory))
                {
                    IndexWriter.Unlock(_indexDirectory);
                }
                string lockFile = Path.Combine(IndexDirectoryPath, "write.lock");
                if (File.Exists(lockFile))
                {
                    File.Delete(lockFile);
                }

                return _indexDirectory;
            }
        }
        public Func<Analyzer> AnalyzerProvider { get; set; }        
        
        public void Index(params object[] instances)
        {
            Index(instances.Select(o => o.ToDocument()).ToArray());
        }
        public void UnIndex(params object[] instances)
        {
            UnIndex(instances.Select(o => o.ToTerm()).ToArray());
        }
        public Task UnIndexAsync(params Term[] terms)
        {
            return Task.Run(() => UnIndex(terms));
        }
        public void UnIndex(params Term[] terms)
        {
            using (IndexWriter idxWriter = new IndexWriter(IndexDirectory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(LuceneVersion), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                idxWriter.DeleteDocuments(terms);
            }
        }
        public Task IndexAsync(params Document[] documents)
        {
            return Task.Run(() => Index(documents));
        }

        public void Index(params Document[] documents)
        {
            using (IndexWriter idxWriter = new IndexWriter(IndexDirectory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(LuceneVersion), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (Document doc in documents)
                {
                    idxWriter.AddDocument(doc);
                }
                idxWriter.Optimize();
                idxWriter.Flush(true, true, true);
            }
        }
        public List<T> Search<T>(string propertyNameToSearch, string searchTerm, int topCount = int.MaxValue)
        {
            List<T> results = new List<T>();
            foreach(Document doc in Search(propertyNameToSearch, searchTerm, topCount))
            {
                results.Add(doc.ToInstance<T>());
            }
            return results;
        }

        public IEnumerable<Document> Search(string propertyNameToSearch, string searchTerm, int topCount = int.MaxValue)
        {
            LuceneSearch.IndexSearcher searcher = new LuceneSearch.IndexSearcher(IndexDirectory);
            QueryParser parser = new QueryParser(LuceneVersion, propertyNameToSearch, AnalyzerProvider());
            LuceneSearch.Query query = parser.Parse(searchTerm);
            LuceneSearch.TopDocs hits = searcher.Search(query, topCount);
            IndexReader reader = IndexReader.Open(IndexDirectory, true);
            
            for (int i = 0; i < hits.ScoreDocs.Length; i++)
            {
                LuceneSearch.ScoreDoc scoreDoc = hits.ScoreDocs[i];
                Document doc = reader.Document(scoreDoc.Doc);
                yield return doc;
            }
        }
        
        //private IndexWriter GetIndexWriter()
        //{
            
        //    return new IndexWriter(IndexDirectory, AnalyzerProvider(), create, IndexWriter.MaxFieldLength.UNLIMITED);
        //}

    }
}

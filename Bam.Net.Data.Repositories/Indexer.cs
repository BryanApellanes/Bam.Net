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

namespace Bam.Net.Data.Repositories
{
    public class Indexer
    {
        public Indexer(string indexDirectoryPath)
        {
            this.Directory = FSDirectory.Open(indexDirectoryPath);
            this.Analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            this.IndexWriter = new IndexWriter(this.Directory, this.Analyzer, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public Directory Directory { get; set; }
        public IndexWriter IndexWriter { get; set; }
        public Analyzer Analyzer { get; set; }

        public void Index(params object[] instance)
        {
            instance.Each(o =>
            {
                IndexWriter.AddDocument(o.ToDocument());
            });
            IndexWriter.Optimize();
            IndexWriter.Flush(true, true, true);
            IndexWriter.Close();
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
    public static class IndexExtensions
    {
        public static Document ToDocument(this object instance)
        {
            Document document = new Document();
            Type type = instance.GetType();
            type.GetProperties().Each(prop =>
            {
                document.Add(CreateField(instance, prop));
            });
            return document;
        }

        private static Field CreateField(object instance, PropertyInfo prop)
        {
            return new Field(prop.Name, prop.GetValue(instance).ToString(), Field.Store.YES, Field.Index.ANALYZED);            
        }
    }
}

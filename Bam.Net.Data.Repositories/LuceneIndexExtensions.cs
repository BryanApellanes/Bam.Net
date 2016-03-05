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
    public static class LuceneIndexExtensions
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

        public static T FromDocument<T>(this Document doc)
        {
            return (T)doc.FromDocument(typeof(T));
        }

        public static object FromDocument(this Document doc, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            object result = type.Construct();
            foreach (PropertyInfo property in properties)
            {
                result.Property(property.Name, Convert.ChangeType(doc.Get(property.Name), property.PropertyType));
            }
            return result;
        }

        private static Field CreateField(object instance, PropertyInfo prop)
        {
            return new Field(prop.Name, prop.GetValue(instance).ToString(), Field.Store.YES, Field.Index.ANALYZED);            
        }
    }
}

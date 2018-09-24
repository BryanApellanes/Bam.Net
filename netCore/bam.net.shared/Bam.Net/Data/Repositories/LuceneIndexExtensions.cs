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
using Lucene.Net.Index;

namespace Bam.Net.Data.Repositories
{
    public static class LuceneIndexExtensions
    {
        public static Document ToDocument(this object instance, Func<object, PropertyInfo, Field> fieldCreator = null)
        {
            fieldCreator = fieldCreator ?? CreateField;
            Document document = new Document();
            Type type = instance.GetType();
            document.Add(new Field("Type", $"{type.Namespace}.{type.Name}", Field.Store.YES, Field.Index.ANALYZED));
            type.GetProperties().Each(prop =>
            {
                document.Add(fieldCreator(instance, prop));
            });
            return document;
        }

        public static IEnumerable<Term> ToTerm(this object instance, Func<object, PropertyInfo, Term> termCreator = null)
        {
            termCreator = termCreator ?? CreateTerm;
            Type type = instance.GetType();
            yield return new Term("Type", $"{type.Namespace}.{type.Name}");
            foreach(PropertyInfo prop in instance.GetType().GetProperties())
            {
                yield return termCreator(instance, prop);
            }
        }


#if NET472
        public static object ToDynamicInstance(this Document doc, string typeName)
        {
            Type type;
            return ToDynamicInstance(doc, typeName, out type);
        }

        public static object ToDynamicInstance(this Document doc, string typeName, out Type dynamicType)
        {
            Dictionary<object, object> dictionary;
            dynamicType = ToDynamicType(doc, typeName, out dictionary);
            object instance = dynamicType.Construct();
            foreach (object key in dictionary.Keys)
            {
                instance.Property((string)key, dictionary[key]);
            }
            return instance;
        }

        public static Type ToDynamicType(Document doc, string typeName)
        {
            Dictionary<object, object> typeDef;
            return ToDynamicType(doc, typeName, out typeDef);
        }

        public static Type ToDynamicType(Document doc, string typeName, out Dictionary<object, object> typeDefinition)
        {
            typeDefinition = new Dictionary<object, object>();
            foreach (IFieldable field in doc.GetFields())
            {
                typeDefinition.AddMissing(field.Name, doc.Get(field.Name));
            }
            Type dynamicType = typeDefinition.ToDynamicType(typeName);
            return dynamicType;
        }
#endif

        public static T ToInstance<T>(this Document doc)
        {
            return (T)doc.ToInstance(typeof(T));
        }

        public static object ToInstance(this Document doc, Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            object result = type.Construct();
            foreach (PropertyInfo property in properties)
            {
                result.Property(property.Name, Convert.ChangeType(doc.Get(property.Name), property.PropertyType));
            }
            return result;
        }

        public static Dictionary<string, object> ToSearchTerms(this QueryFilter filter)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach(IParameterInfo paramInfo in filter.Parameters)
            {                
                result.Add(paramInfo.ColumnName, paramInfo.Value);
            }
            return result;
        }

        private static Field CreateField(object instance, PropertyInfo prop)
        {
            string name = prop.Name;
            object valueObj = prop.GetValue(instance);
            string value = valueObj == null ? "[null]" : valueObj.ToString();
            return new Field(name, value, Field.Store.YES, Field.Index.ANALYZED);            
        }

        private static Term CreateTerm(object instance, PropertyInfo prop)
        {
            string name = prop.Name;
            object valueObj = prop.GetValue(instance);
            string value = valueObj == null ? "[null]" : valueObj.ToString();
            return new Term(name, value);
        }
    }
}

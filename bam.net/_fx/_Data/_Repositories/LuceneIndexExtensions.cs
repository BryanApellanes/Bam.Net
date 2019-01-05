using Bam.Net;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public static class LuceneIndexExtensionsFx
    {
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
    }
}

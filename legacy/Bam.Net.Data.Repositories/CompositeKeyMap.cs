using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
    public class CompositeKeyMap: RepoData
    {
        public CompositeKeyMap() { }
        public CompositeKeyMap(IHasKeyHash instance)
        {
            Instance = instance;
            LongCompositeKey = instance.GetLongKeyHash();
            IntCompositeKey = instance.GetIntKeyHash();
            ReferencedCuid = instance.Property<string>("Cuid", true);
        }
        protected IHasKeyHash Instance { get; set; }
        public long LongCompositeKey { get; set; }
        public int IntCompositeKey { get; set; }
        public string ReferencedCuid { get; set; }

        /// <summary>
        /// The AssemblyQualifiedName of the type
        /// </summary>
        public string TypeName { get; set; }

        public Type GetReferencedType()
        {
            return Type.GetType(TypeName);
        }
    }
}

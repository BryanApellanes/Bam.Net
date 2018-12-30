using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class TypeXrefModel: TypeXref
    {
        public TypeXrefModel() : base()
        { }

        public string DaoNamespace { get; set; }
        public static TypeXrefModel FromTypeXref(TypeXref xref, string daoNamespace)
        {
            TypeXrefModel model = new TypeXrefModel();
            model.CopyProperties(xref);
            model.DaoNamespace = daoNamespace;
            return model;
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.Razor;

namespace Bam.Net.Data.Repositories
{
	public abstract class PocoTemplate: RazorTemplate<PocoModel>
	{
		public void WriteChildPrimaryKeyProperty(TypeFk fk)
		{
			MethodInfo method = fk.ChildParentProperty.GetGetMethod();
			if(method != null && method.IsVirtual)
			{
				Write(Render<TypeFk>("ChildPrimaryKeyProperty.tmpl", new { Model = fk }));
			}
		}

		public void WriteForeignKeyProperty(TypeFk fk) // used by razor templates
		{
			if(fk.CollectionProperty.GetGetMethod().IsVirtual)
			{
				Write(Render<TypeFk>("ForeignKeyProperty.tmpl", new { Model = fk }));
			}
		}

		public void WriteLeftXrefProperty(TypeXref xref) // used by razor templates
        {
			MethodInfo method = xref.LeftCollectionProperty.GetGetMethod();
			if(method != null && method.IsVirtual)
			{
				Write(Render<TypeXref>("XrefLeftProperty.tmpl", new { Model = xref }));
			}
		}

		public void WriteRightXrefProperty(TypeXref xref) // used by razor templates
        {
			MethodInfo method = xref.LeftCollectionProperty.GetGetMethod();
			if (method != null && method.IsVirtual)
			{
				Write(Render<TypeXref>("XrefRightProperty.tmpl", new { Model = xref }));
			}
		}

		private string Render<T>(string templateName, object options)
		{
            List<Assembly> referenceAssemblies = new List<Assembly>{
                    typeof(DaoGenerator).Assembly,
                    typeof(ServiceProxyController).Assembly,
                    typeof(Args).Assembly,
                    typeof(DaoRepository).Assembly};
            
			RazorParser<RazorTemplate<T>> parser = new RazorParser<RazorTemplate<T>>();
			string result = parser.ExecuteResource(templateName, "Bam.Net.Data.Repositories.Templates.", typeof(PocoTemplate).Assembly, options, referenceAssemblies.ToArray()).Trim();
			return result;
		}
	}
}

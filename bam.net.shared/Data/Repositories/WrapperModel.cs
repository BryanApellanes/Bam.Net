/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
	public class WrapperModel
	{
		public WrapperModel(Type pocoType, TypeSchema schema, string wrapperNamespace = "TypeWrappers", string daoNameSpace = "Daos")
		{
			BaseType = pocoType;
			WrapperNamespace = wrapperNamespace;
            DaoNamespace = daoNameSpace;
			TypeNamespace = pocoType.Namespace;
            TypeName = pocoType.Name.TrimNonLetters();
            WrapperTypeName = pocoType.ToTypeString(false).Replace(TypeName, $"{TypeName}Wrapper");
            BaseTypeName = pocoType.ToTypeString(false);
			ForeignKeys = schema.ForeignKeys.Where(fk => fk.PrimaryKeyType.Equals(pocoType)).ToArray();
			ChildPrimaryKeys = schema.ForeignKeys.Where(fk => fk.ForeignKeyType.Equals(pocoType)).ToArray();
            LeftXrefs = schema.Xrefs.Where(xref => xref.Left.Equals(pocoType)).Select(xref => TypeXrefModel.FromTypeXref(xref, daoNameSpace)).ToArray();
            RightXrefs = schema.Xrefs.Where(xref => xref.Right.Equals(pocoType)).Select(xref => TypeXrefModel.FromTypeXref(xref, daoNameSpace)).ToArray();
		}

		public string WrapperNamespace { get; set; }

		public string TypeNamespace { get; set; }
        public string DaoNamespace { get; set; }
        public string TypeName { get; set; }
		public string WrapperTypeName { get; set; }
        public string BaseTypeName { get; set; }

		public TypeFk[] ForeignKeys { get; set; }

		public TypeFk[] ChildPrimaryKeys { get; set; }

		/// <summary>
		/// Xrefs where the current DtoType is the left
		/// side of the cross reference table
		/// </summary>
		public TypeXrefModel[] LeftXrefs { get; set; }

		/// <summary>
		/// Xrefs where the current DtoType is the Right
		/// side of the cross reference table
		/// </summary>
		public TypeXrefModel[] RightXrefs { get; set; }
		
		private Type BaseType { get; set; }

		public string Render()
		{
			List<Assembly> references = new List<Assembly>(GetDefaultAssembliesToReference());
			references.Add(BaseType.Assembly);
			RazorParser<WrapperTemplate> parser = new RazorParser<WrapperTemplate>(RazorBaseTemplate.DefaultInspector);
			string output = parser.ExecuteResource("Wrapper.tmpl", RepositoryTemplateResources.Path, typeof(WrapperGenerator).Assembly,
				new { Model = this }, references.ToArray());

			return output;
		}

        private static Assembly[] GetDefaultAssembliesToReference()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(WrapperTemplate).Assembly, 
					typeof(DaoGenerator).Assembly,
					typeof(ServiceProxySystem).Assembly, 
					typeof(Resolver).Assembly};
            return assembliesToReference;
        }
	}
}

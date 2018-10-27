/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Razor;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Data.Repositories
{
    public partial class DtoModel
	{
		public DtoModel(Type dynamicDtoType, string nameSpace)
		{
			TypeName = dynamicDtoType.Name;
			List<string> properties = new System.Collections.Generic.List<string>();
			foreach(PropertyInfo p in dynamicDtoType.GetProperties())
			{
				Type type = (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? Nullable.GetUnderlyingType(p.PropertyType) : p.PropertyType;
				properties.Add("\t\tpublic {0} {1} {{get; set;}}\r\n"._Format(type.Name, p.Name));
			}
			Properties = properties.ToArray();
			DtoType = dynamicDtoType;
			Namespace = nameSpace;
		}

        public DtoModel(string nameSpace, string typeName, params DtoPropertyModel[] propertyModels)
        {
            List<string> properties = new List<string>();
            foreach(DtoPropertyModel p in propertyModels)
            {
                properties.Add("\t\tpublic {0} {1} {{get; set;}}"._Format(p.PropertyType, p.PropertyName));
            }
            TypeName = typeName;
            Properties = properties.ToArray();
            Namespace = nameSpace;
        }

		public string TypeName { get; set; }
		public string Namespace { get; set; }
		public string[] Properties { get; set; }

		public Type DtoType { get; set; }

		public string Render()
		{
            List<Assembly> references = new List<Assembly>(GetDefaultAssembliesToReference())
            {
                typeof(PropertyInfo).Assembly
            };
            RazorParser<DtoTemplate> parser = new RazorParser<DtoTemplate>(RazorBaseTemplate.DefaultInspector);
			string output = parser.ExecuteResource("Dto.tmpl", RepositoryTemplateResources.Path, typeof(DtoTemplate).Assembly,
				new { Model = this }, references.ToArray());

			return output;
		}

        private static Assembly[] GetDefaultAssembliesToReference()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(DtoTemplate).Assembly, 
					typeof(DaoGenerator).Assembly,
					typeof(ServiceProxySystem).Assembly, 
					typeof(Resolver).Assembly};
            return assembliesToReference;
        }
	}
}

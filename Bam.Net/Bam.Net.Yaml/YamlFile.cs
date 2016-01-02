/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Yaml
{
	public class YamlFile
	{
		public YamlFile(FileInfo file)
		{
			this.File = file;
		}

		public FileInfo File { get; private set; }

		List<Type> _dynamicTypes;
		object _dynamicTypeLock = new object();
		public List<Type> DynamicTypes
		{
			get
			{
				return _dynamicTypeLock.DoubleCheckLock(ref _dynamicTypes, () =>
				{
					List<Type> types = new List<Type>();
					object[] instances = this.File.FromYamlFile();
					for (int i = 0; i < instances.Length; i++)
					{
						object o = instances[i];
						string typeName = "{0}{1}"._Format(Path.GetFileNameWithoutExtension(File.Name).PascalCase(true, " "), i > 0 ? i.ToString() : "");
						Dictionary<object, object> dict = o as Dictionary<object, object>;
						if (dict != null)
						{
							dict.ToDynamicType(typeName, types);
						}
						else
						{
							Type dynamicType = o.ToDynamicType(typeName, (p) => p.PropertyType.IsArray || p.PropertyType.IsValueType || p.PropertyType.IsPrimitive);
							types.Add(dynamicType);
						}
					}

					return types;
				});
			}
		}

		protected internal YamlSchema Schema
		{
			get;
			set;
		}
		protected internal string Namespace
		{
			get
			{
				if (Schema != null)
				{
					return Schema.GetNamespace(File);
				}

				return "Yaml";
			}
		}
	}
}

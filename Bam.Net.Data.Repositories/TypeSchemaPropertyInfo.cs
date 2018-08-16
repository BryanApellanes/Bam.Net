/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// Represents information about a missing primary key/id column or missing foreign key.
    /// </summary>
    /// <seealso cref="System.Reflection.PropertyInfo" />
    public class TypeSchemaPropertyInfo: PropertyInfo
	{
		public TypeSchemaPropertyInfo(string name, Type decrlaringType, ITypeTableNameProvider tableNameProvier)
		{
			this._name = name;
			this.SetDeclaringType(decrlaringType);
            this.TableNameProvider = tableNameProvier;
		}

		public TypeSchemaPropertyInfo(string name, Type declaringType, Type foreignKeyTableType, ITypeTableNameProvider tableNameProvider) 
		{
			this._name = name;
			this.SetDeclaringType(declaringType);
			this._foreignKeyTableType = foreignKeyTableType;
            this.TableNameProvider = tableNameProvider;
		}

        protected ITypeTableNameProvider TableNameProvider
        {
            get;
            set;
        }

		public override PropertyAttributes Attributes
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanRead
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanWrite
		{
			get { throw new NotImplementedException(); }
		}

		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return null;
		}

		public override System.Reflection.ParameterInfo[] GetIndexParameters()
		{
			throw new NotImplementedException();
		}

		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override Type PropertyType
		{
			get { throw new NotImplementedException(); }
		}

		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		Type _declaringType;
		public override Type DeclaringType
		{
			get { return _declaringType; }
		}

		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		string _name;
		public override string Name
		{
			get { return _name; }
		}

		public override Type ReflectedType
		{
			get { throw new NotImplementedException(); }
		}

		
		protected internal void SetDeclaringType(Type type) 
		{
			_declaringType = type;
		}

        public KeyColumn ToKeyColumn()
        {
            return ToKeyColumn(TableNameProvider);
        }

		public KeyColumn ToKeyColumn(ITypeTableNameProvider tableNameProvider) {
			string name = "Id";
			if (DeclaringType != null) 
			{
				PropertyInfo keyProperty = TypeSchemaGenerator.GetKeyProperty(DeclaringType);
				if (keyProperty != null) 
				{
					name = keyProperty.Name;
				}
			}
			return new KeyColumn 
			{
				TableName = tableNameProvider.GetTableName(DeclaringType),
				Name = name,
				DataType = DataTypes.ULong
			};
		}

		Type _foreignKeyTableType;
		public ForeignKeyColumn ToForeignKeyColumn(ITypeTableNameProvider tableNameProvider = null) {
            ForeignKeyColumn result = new ForeignKeyColumn(Name, TypeSchemaGenerator.GetTableNameForType(_foreignKeyTableType),
                TypeSchemaGenerator.GetTableNameForType(DeclaringType, tableNameProvider))
            {
                DataType = DataTypes.ULong
            };

            return result;
		}
	}
}

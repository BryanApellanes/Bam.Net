/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Bam.Net.Data;
using Bam.Net.Js;
using Bam.Net.ServiceProxy;
using System.ComponentModel;

namespace Bam.Net.Html
{
    /// <summary>
    /// Used to build input forms for specified 
    /// types, methods and their parameters
    /// </summary>
    public class InputFormBuilder
    {
        Type invocationType;
        Dictionary<Type, CustomInputBuilder> customAttributeBuilders;
        public InputFormBuilder()
        {
            this.LabelCssClass = "label";
            this.LabelFormat = "{0}";
            this.customAttributeBuilders = new Dictionary<Type, CustomInputBuilder>();
            this.RecursionLimit = 5;
            this.AddLabels = true;
        }

        public InputFormBuilder(Type invocationTarget)
            : this()
        {
            this.invocationType = invocationTarget;
        }

        public Type InvocationType
        {
            get
            {
                return this.invocationType;
            }
            set
            {
                this.invocationType = value;
            }
        }
        
        TagBuilderDelegate numberOrStringBuilder;
        public TagBuilderDelegate NumberBuilder
        {
            get
            {
                if (numberOrStringBuilder == null)
                {
                    numberOrStringBuilder = (p) =>
                    {
                        TagBuilder c = CreateInput("number", p);
                        return c;
                    };
                }

                return numberOrStringBuilder;
            }
            set
            {
                numberOrStringBuilder = value;
            }
        }

        TagBuilderDelegate stringBuilder;
        public TagBuilderDelegate StringBuilder
        {
            get
            {
                if (stringBuilder == null)
                {
                    stringBuilder = (p) =>
                    {
                        TagBuilder c = CreateInput("text", p);
                        return c;
                    };
                }

                return stringBuilder;
            }
            set
            {
                stringBuilder = value;
            }
        }

        TagBuilderDelegate dateTimeBuilder;
        public TagBuilderDelegate DateTimeBuilder
        {
            get
            {
                if (dateTimeBuilder == null)
                {
                    dateTimeBuilder = (name) =>
                    {
                        TagBuilder datepicker = CreateInput("text", name)
                            .DataSet("plugin", "datepicker");

                        return datepicker;
                    };
                }
                return dateTimeBuilder;
            }
            set
            {
                dateTimeBuilder = value;
            }
        }

        TagBuilderDelegate boolBuilder;
        public TagBuilderDelegate BooleanBuilder
        {
            get
            {
                if (boolBuilder == null)
                {
                    boolBuilder = (name) =>
                    {
                        return CreateInput("checkbox", name);
                    };
                }

                return boolBuilder;
            }

            set
            {
                boolBuilder = value;
            }
        }

        /// <summary>
        /// Used to register the specified InputBuilder for the specified attribute T.
        /// If a property is addorned with the specified Attribute T the registered 
        /// InputBuilder will be used to build the input html for that property.
        /// </summary> 
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public void RegisterAttributeBuilder<T>(CustomInputBuilder builder) where T : Attribute
        {
            if (!this.customAttributeBuilders.ContainsKey(typeof(T)))
            {
                this.customAttributeBuilders.Add(typeof(T), builder);
            }
            else
            {
                this.customAttributeBuilders[typeof(T)] = builder;
            }
        }

        public string LabelCssClass
        {
            get;
            set;
        }

        /// <summary>
        /// The format used for the labels rendered by the current ParametersBuilder.
        /// The default is "{0}".
        /// </summary>
        public string LabelFormat
        {
            get;
            set;
        }

        ParameterLayouts layout;
        public ParameterLayouts Layout
        {
            get
            {
                return layout;
            }
            set
            {
                if (value == ParameterLayouts.BreakAfterLabels)
                {
                    this.AddLabels = true;
                }
                this.layout = value;
            }
        }

        public bool AddLabels
        {
            get;
            set;
        }

        public TagBuilder MethodForm(string methodName, Dictionary<string, object> defaults)
        {
            int ignore = -1;
            return MethodForm(methodName, defaults, out ignore);
        }


        public TagBuilder MethodForm(string methodName, Dictionary<string, object> defaults, out int paramCount)
        {
            return MethodForm("fieldset", methodName, defaults, out paramCount);
        }

        public TagBuilder MethodForm(string wrapperTagName, string methodName, Dictionary<string, object> defaults)
        {
            int ignore = -1;
            return MethodForm(wrapperTagName, methodName, defaults, out ignore);
        }

        public TagBuilder MethodForm(string wrapperTagName, string methodName, Dictionary<string, object> defaults, out int paramCount)
        {
            Args.ThrowIfNull(invocationType, "InvocationType");
            return MethodForm(invocationType, wrapperTagName, methodName, defaults, out paramCount);
        }

		public TagBuilder MethodForm(Type type, string methodName)
		{
			int ignore = -1;
			return MethodForm(type, "fieldset", methodName, null, out ignore);
		}

		public TagBuilder MethodForm(Type type, string wrapperTagName, string methodName, Dictionary<string, object> defaults, out int paramCount)
		{
			return MethodForm(type, wrapperTagName, methodName, defaults, false, out paramCount);
		}
        /// <summary>
        /// Build a form to be used as parameters for the specified method
        /// </summary>
        /// <param name="wrapperTagName"></param>
        /// <param name="methodName"></param>
        /// <param name="defaults"></param>
        /// <param name="paramCount"></param>
        /// <returns></returns>
        public TagBuilder MethodForm(Type type, string wrapperTagName, string methodName, Dictionary<string, object> defaults, bool registerProxy, out int paramCount)
        {
            Args.ThrowIfNull(type, "InvocationType");
			if(registerProxy)
			{
				ServiceProxySystem.Register(type);
			}

            MethodInfo method = type.GetMethod(methodName);
        
            // Prevent NullReferenceException
            if (defaults == null)
            {
                defaults = new Dictionary<string, object>();
            }

            ParameterInfo[] parameters = method.GetParameters();
            paramCount = parameters.Length;
            TagBuilder form = new TagBuilder(wrapperTagName);

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                object defaultValue = defaults.ContainsKey(parameter.Name) ? defaults[parameter.Name] : null;
                string defaultString = defaultValue == null ? string.Empty : defaultValue.ToString();
                
                TagBuilder label = new TagBuilder("label")
                    .Html(string.Format(this.LabelFormat, parameter.Name.PascalSplit(" ")))
                    .Css(this.LabelCssClass);
                
                bool addLabel = this.AddLabels;
                bool addValue = true;
                bool wasObject = false;
                bool handled = false;
                TagBuilder toAdd;
                Type paramType;
                TryBuildPrimitiveInput(parameter, defaultValue, ref addValue, ref handled, out toAdd, out paramType);

                if(!handled)
                {
                    string legend = GetLegend(paramType);
                    toAdd = FieldsetFor(paramType, defaultValue, legend);
                    addLabel = false;
                    addValue = false;
                    wasObject = true;
                }

                toAdd.DataSet("parameter-name", parameter.Name)
                    .DataSetIf(!wasObject, "type", parameter.ParameterType.Name.ToLowerInvariant())
                    .ValueIf(!string.IsNullOrEmpty(defaultString) && addValue, defaultString);

                if (addLabel)
                {
                    form.Child(label).BrIf(this.Layout == ParameterLayouts.BreakAfterLabels);
                }

                form.Child(toAdd).BrIf(
                    this.Layout != ParameterLayouts.NoBreaks && 
                    i != parameters.Length - 1 &&
                    !wasObject);
            }

            return form.DataSet("method", methodName)
                .FirstChildIf(wrapperTagName.Equals("fieldset"), new TagBuilder("legend")
                .Text(GetLegend(method)));
        }

		private string GetLegend(MethodInfo method)
		{
			Legend legend;
			if(method.HasCustomAttributeOfType<Legend>(out legend))
			{
				return legend.Value;
			}
			else
			{
				return method.Name.PascalSplit(" ");
			}
		}

        private string GetLegend(Type type)
        {
            Legend legend;
            if (type.HasCustomAttributeOfType<Legend>(out legend))
            {
                return legend.Value;
            }
            else
            {
                return type.Name.PascalSplit(" ");
            }
        }

        private void TryBuildPrimitiveInput(ParameterInfo parameter, object defaultValue, ref bool addValue, ref bool handled, out TagBuilder toAdd, out Type paramType)
        {
            toAdd = null;

            paramType = parameter.ParameterType;
            // if the parameter type is a primitive then use the appropriate html input
            // string - text 
            if (paramType == typeof(int) || paramType == typeof(long))
            {
                toAdd = NumberBuilder(parameter.Name);
                handled = true;
            }
            else if (paramType == typeof(string))
            {
                toAdd = StringBuilder(parameter.Name);
                handled = true;
            }
            // DateTime - text with jQuery datepicker    
            else if (paramType == typeof(DateTime))
            {
                addValue = defaultValue != null;
                toAdd = DateTimeBuilder(parameter.Name)
                    .ValueIf(addValue, ((DateTime)defaultValue).ToShortDateString());

                handled = true;
            }// bool - checkbox
            else if (paramType == typeof(bool))
            {
                toAdd = BooleanBuilder(parameter.Name)
                    .CheckedIf(defaultValue != null && (bool)defaultValue);

                handled = true;
            }// enum - radio list
            else if (paramType.IsEnum)
            {
                toAdd = RadioList.FromEnum(paramType, defaultValue);
                addValue = false;

                handled = true;
            }

        }

        public TagBuilder FieldsetFor(Type paramType, object defaultValues, string name = null)
        {
            return FieldsetFor(paramType, defaultValues, name, 0);
        }

		/// <summary>
		/// Get a form to input the properties of the specified instance
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <returns></returns>
        public TagBuilder FieldsetFor(object param, string name = null)
        {
            if (param is Type)
            {
                return FieldsetFor((Type)param, null, name, 0);
            }
            else
            {
                return FieldsetFor(param.GetType(), param, name);
            }
        }

        public TagBuilder FieldsetFor(Type paramType, object defaultValues, string name = null, int recursionThusFar = 0)
        {
            TagBuilder container = GetFieldset(name);

            AppendInputsFor(paramType, defaultValues, container, recursionThusFar);

            return container;
        }

        public TagBuilder FieldsetForDynamic(dynamic obj, string name = null, bool setValues = false, int recursionThusFar = 0)
        {
            TagBuilder container = GetFieldset(name);

            AppendInputsForDynamic(obj, container, setValues, 0);
            return container;
        }

        private static TagBuilder GetFieldset(string name)
        {
            string legend = string.IsNullOrEmpty(name) ? "" : name.PascalSplit(" ");
            TagBuilder container = new TagBuilder("fieldset")
                                    .Attr("itemscope", "itemscope")
                                    .Attr("itemtype", "http://schema.org/Thing")
                                    .DataSet("type", "object")
                                    .ChildIf(!string.IsNullOrEmpty(name),
                                        new TagBuilder("legend")
                                            .TextIf(!string.IsNullOrEmpty(name), legend)
                                    );
            return container;
        }

        Func<PropertyInfo, bool> _propertyInclusionPredicate;
        /// <summary>
        /// Used to determine whether to include specific properties of
        /// object instances.
        /// </summary>
        public Func<PropertyInfo, bool> PropertyInclusionPredicate
        {
            get
            {
                if (_propertyInclusionPredicate == null)
                {
                    _propertyInclusionPredicate = (p) =>
                    {
                        return !p.HasCustomAttributeOfType<ExcludeAttribute>();
                    };
                }

                return _propertyInclusionPredicate;
            }
            set
            {
                _propertyInclusionPredicate = value;
            }
        }

        /// <summary>
        /// Limit the depth to which Types will be analyzed
        /// </summary>
        public int RecursionLimit
        {
            get;
            private set;
        }

        internal protected void AppendInputsForDynamic(dynamic target, TagBuilder container, bool setValues, int recursionThusFar)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(target);//new List<PropertyDescriptor>(target.GetProperties());
            string paramTypeName = target.Name;

            foreach (PropertyDescriptor property in properties)
            {
                //LabelAttribute labelAttr = property.GetCustomAttributeOfType<LabelAttribute>();
                string labelText = string.Format(this.LabelFormat, property.Name.PascalSplit(" "));
                string id = "{0}_{1}"._Format(paramTypeName, property.Name);

                TagBuilder label = new TagBuilder("label")
                    .Html(labelText)
                    .Attr("for", id)
                    .Css(LabelCssClass);

                bool addLabel = this.AddLabels;
                bool breakAfterLabel = this.Layout == ParameterLayouts.BreakAfterLabels;
                object defaultValue = property.GetValue(target);

                bool addValue = true;

                Type propType = property.PropertyType;

                bool isArray = propType.IsArray;
                if (isArray)
                {
                    propType = propType.GetElementType();
                }
                else if (propType.IsGenericType &&
                    propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = propType.GetGenericArguments()[0];
                }

                bool isHidden = false;
                bool wasObject = false;
                bool typeAdded = false;

                TagBuilder toAdd = null;

                if (propType == typeof(int) || propType == typeof(long))
                {
                    toAdd = NumberBuilder(property.Name);
                }
                else if (propType == typeof(string))
                {
                    toAdd = StringBuilder(property.Name);
                }
                else if (propType == typeof(DateTime))
                {
                    DateTime defaultDate = defaultValue == null ? DateTime.MinValue : (DateTime)defaultValue;
                    addValue = defaultDate > DateTime.MinValue;
                    toAdd = DateTimeBuilder(property.Name)
                        .ValueIf(addValue, defaultDate.ToShortDateString());

                    addValue = false;
                }
                else if (propType == typeof(bool))
                {
                    
                    toAdd = BooleanBuilder(property.Name);
                    if (defaultValue != null)
                    {
                        bool bVal;
                        Boolean.TryParse(defaultValue.ToString(), out bVal);
                        toAdd.CheckedIf(defaultValue != null && bVal);
                    }
                }
                else if (propType.IsEnum)
                {
                    toAdd = RadioList.FromEnum(propType, defaultValue)
                        .DataSet("type", "enum");
                    typeAdded = true;
                    addValue = false;
                    wasObject = true; // prevent new br
                }
                else if (recursionThusFar < RecursionLimit)
                {
                    toAdd = FieldsetFor(propType, defaultValue, property.Name, ++recursionThusFar);
                    addLabel = false;
                    wasObject = true;
                    typeAdded = true;
                }

                if (toAdd != null)
                {
                    toAdd.Id(id)
                        .Attr("name", property.Name)
                        .Attr("itemprop", property.Name) // schema.org
                        .DataSetIf(!typeAdded, "type", GetDataSetType(propType))
                        .ValueIf(addValue && defaultValue != null, (defaultValue as string))
                        .DataSetIf(isArray, "array", "true");

                    container
                        .ChildIf(addLabel, label)
                        .BrIf(addLabel && breakAfterLabel)
                        .Child(toAdd, toAdd.TagName.ToLowerInvariant().Equals("input") ? TagRenderMode.SelfClosing : TagRenderMode.Normal)
                        .BrIf(!isHidden && !wasObject &&
                        (
                            this.Layout == ParameterLayouts.Default ||
                            this.Layout == ParameterLayouts.BreakAfterLabels
                        )
                    );
                }
            }
        }

        internal protected void AppendInputsFor(object defaultValues, TagBuilder container)
        {
            AppendInputsFor(defaultValues.GetType(), defaultValues, container);
        }

        internal protected void AppendInputsFor(Type paramType, object defaultValues, TagBuilder container)
        {
            AppendInputsFor(paramType, defaultValues, container, 0);
        }

        internal protected void AppendInputsFor(Type paramType, object defaultValues, TagBuilder container, int recursionThusFar)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>(paramType.GetProperties());
            properties.Sort((l, r) => l.MetadataToken.CompareTo(r.MetadataToken));
            string paramTypeName = paramType.Name;

            foreach (PropertyInfo property in properties)
            {
                if (!PropertyInclusionPredicate(property))
                {
                    continue;
                }

                LabelAttribute labelAttr = property.GetCustomAttributeOfType<LabelAttribute>();
                string labelText = labelAttr ?? string.Format(this.LabelFormat, property.Name.PascalSplit(" "));
                string id = "{0}_{1}"._Format(paramTypeName, property.Name);

                TagBuilder label = new TagBuilder("label")
                    .Html(labelText)
                    .Attr("for", id)
                    .Css(LabelCssClass);

                bool addLabel = this.AddLabels;
                bool breakAfterLabel = (this.Layout == ParameterLayouts.BreakAfterLabels) || (labelAttr != null && labelAttr.PostBreak);
                object defaultValue = null;

                if (defaultValues != null)
                {
                    defaultValue = GetDefaultValue(defaultValues, property, defaultValue);
                }

                bool addValue = true;

                Type propType = property.PropertyType;

                bool isArray = propType.IsArray;
                if (isArray)
                {
                    propType = propType.GetElementType();
                }
                else if (propType.IsGenericType &&
                    propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = propType.GetGenericArguments()[0];
                }

                StringInput attr;
                bool isHidden = false;
                bool wasObject = false;
                bool typeAdded = false;

                TagBuilder toAdd = null;

                if (property.HasCustomAttributeOfType<StringInput>(true, out attr))
                {
                    attr.Default = attr.Default == null ? defaultValue : attr.Default;
                    attr.PropertyName = property.Name;
                    toAdd = attr.CreateInput().DataSetIf(propType.IsEnum, "type", "string");
                    typeAdded = propType.IsEnum;
                    addLabel = attr.AddLabel.HasValue ? attr.AddLabel.Value : addLabel;
                    isHidden = attr.IsHidden.HasValue ? attr.IsHidden.Value : isHidden;
                    breakAfterLabel = attr.BreakAfterLabel.HasValue ? attr.BreakAfterLabel.Value : breakAfterLabel;
                    addValue = attr.AddValue.HasValue ? attr.AddValue.Value : addValue;
                }
                else if (propType == typeof(int) || propType == typeof(long))
                {
                    toAdd = NumberBuilder(property.Name);

                }
                else if (propType == typeof(string))
                {
                    toAdd = StringBuilder(property.Name);
                }
                else if (propType == typeof(DateTime))
                {
                    DateTime defaultDate = defaultValue == null ? DateTime.MinValue : (DateTime)defaultValue;
                    addValue = defaultDate > DateTime.MinValue;
                    toAdd = DateTimeBuilder(property.Name)
                        .ValueIf(addValue, defaultDate.ToShortDateString());
                    
                    addValue = false;
                }
                else if (propType == typeof(bool))
                {
                    toAdd = BooleanBuilder(property.Name)
                        .CheckedIf(defaultValue != null && (bool)defaultValue);
                }
                else if (propType.IsEnum)
                {
                    toAdd = RadioList.FromEnum(propType, defaultValue)
                        .DataSet("type", "enum");
                    typeAdded = true;
                    addValue = false;
                    wasObject = true; // prevent new br
                }
                else if (recursionThusFar < RecursionLimit)
                {
                    toAdd = FieldsetFor(propType, defaultValue, property.Name, ++recursionThusFar);
                    addLabel = false;
                    wasObject = true;
                    typeAdded = true;
                }

                if (toAdd != null)
                {
                    toAdd.Id(id)
                        .Attr("name", property.Name)
                        .Attr("itemprop", property.Name) // schema.org
                        .DataSetIf(!typeAdded, "type", GetDataSetType(propType))
                        .ValueIf(addValue && defaultValue != null, (defaultValue as string))
                        .DataSetIf(isArray, "array", "true");

                    container
                        .BrIf(labelAttr != null && labelAttr.PreBreak && addLabel)
                        .ChildIf(addLabel, label)
                        .BrIf(addLabel && breakAfterLabel)
                        .Child(toAdd, toAdd.TagName.ToLowerInvariant().Equals("input") ? TagRenderMode.SelfClosing: TagRenderMode.Normal)
                        .BrIf(!isHidden && !wasObject &&
                        (
                            this.Layout == ParameterLayouts.Default ||
                            this.Layout == ParameterLayouts.BreakAfterLabels
                        )
                    );
                }
            }
        }

        private static string GetDataSetType(Type propType)
        {
            if (propType == typeof(int) || propType == typeof(long) || propType == typeof(decimal) || propType == typeof(float))
            {
                return "number";
            }

            return propType.Name.ToLowerInvariant();
        }

        private static object GetDefaultValue(object defaultValues, PropertyInfo property, object defaultValue)
        {
            Type defaultsType = defaultValues.GetType();
            PropertyInfo defaultsProperty = defaultsType.GetProperty(property.Name);
            if (defaultsProperty != null)
            {
                defaultValue = defaultValues == null ? null : defaultsProperty.GetValue(defaultValues, null);
            }
            return defaultValue;
        }

       
        internal static TagBuilder CreateInput(string type, string name)
        {
            return new TagBuilder("input")
                .Type(type)
                .Name(name);            
        }
    }
}

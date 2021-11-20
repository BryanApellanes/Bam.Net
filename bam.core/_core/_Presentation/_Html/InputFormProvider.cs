using System;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.Html.Js;
using Bam.Net.ServiceProxy;
using System.ComponentModel;

namespace Bam.Net.Presentation.Html
{
    /// <summary>
    /// Used to build input forms for specified 
    /// types, methods and their parameters. 
    /// </summary>
    public partial class InputFormProvider: InputProvider
    {
        Type invocationType;
        Dictionary<Type, Func<Type, Tag>> customAttributeBuilders;
        public InputFormProvider()
        {
            this.LabelCssClass = "label";
            this.LabelFormat = "{0}";
            this.customAttributeBuilders = new Dictionary<Type, Func<Type, Tag>>();
            this.RecursionLimit = 5;
            this.AddLabels = true;
        }

        public InputFormProvider(Type invocationTarget)
            : this()
        {
            this.invocationType = invocationTarget;
        }
        
        public override Tag CreateInput(PropertyInfo propertyInfo, object data = null)
        {
            return InputProvider.CreateInput(InputTypes.Text, propertyInfo, data);
        }
        
        public Type InvocationType
        {
            get => this.invocationType;
            set => this.invocationType = value;
        }
        
        Func<ParameterInfo, Tag> _numberTagProvider;
        public Func<ParameterInfo, Tag> NumberTagProvider
        {
            get
            {
                return _numberTagProvider ?? (_numberTagProvider = (p) => InputProvider.CreateInput(InputTypes.Number, p));
            }
            set => _numberTagProvider = value;
        }

        Func<ParameterInfo, Tag> _textTagProvider;
        public Func<ParameterInfo, Tag> TextTagProvider
        {
            get
            {
                return _textTagProvider ?? (_textTagProvider = (pi) => InputProvider.CreateInput(InputTypes.Text, pi));
            }
            set => _textTagProvider = value;
        }

        Func<ParameterInfo, Tag> _dateTimeTagProvider;
        public Func<ParameterInfo, Tag> DateTimeTagProvider
        {
            get
            {
                return _dateTimeTagProvider ?? (_dateTimeTagProvider = (pi) => InputProvider
                           .CreateInput(InputTypes.Text, pi)
                           .DataSet("plugin", "datepicker"));
            }
            set => _dateTimeTagProvider = value;
        }

        Func<ParameterInfo, Tag> _booleanTagProvider;
        public Func<ParameterInfo, Tag> BooleanTagProvider
        {
            get
            {
                return _booleanTagProvider ?? (_booleanTagProvider = (pi) => InputProvider.CreateInput(InputTypes.Checkbox, pi));
            }

            set => _booleanTagProvider = value;
        }

        public string LabelCssClass
        {
            get;
            set;
        }

        /// <summary>
        /// The format used for the labels rendered by the current InputFormBuilder.
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
            get => layout;
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

        public Tag MethodForm(string methodName, Dictionary<string, object> defaults)
        {
            int ignore = -1;
            return MethodForm(methodName, defaults, out ignore);
        }

        public Tag MethodForm(string methodName, Dictionary<string, object> defaults, out int paramCount)
        {
            return MethodForm("fieldset", methodName, defaults, out paramCount);
        }

        public Tag MethodForm(string wrapperTagName, string methodName, Dictionary<string, object> defaults)
        {
            return MethodForm(wrapperTagName, methodName, defaults, out int ignore);
        }

        public Tag MethodForm(string wrapperTagName, string methodName, Dictionary<string, object> defaults, out int paramCount)
        {
            Args.ThrowIfNull(invocationType, "InvocationType");
            return MethodForm(invocationType, wrapperTagName, methodName, defaults, out paramCount);
        }

		public Tag MethodForm(Type type, string methodName)
		{
			return MethodForm(type, "fieldset", methodName, null, out int ignore);
		}

		public Tag MethodForm(Type type, string wrapperTagName, string methodName, Dictionary<string, object> defaults, out int paramCount)
		{
			return MethodForm(type, wrapperTagName, methodName, defaults, false, out paramCount);
		}

        /// <summary>
        /// Build a form to be used as parameters for the specified method
        /// </summary>
        /// <param name="type"></param>
        /// <param name="wrapperTagName"></param>
        /// <param name="methodName"></param>
        /// <param name="defaults"></param>
        /// <param name="registerProxy"></param>
        /// <param name="paramCount"></param>
        /// <returns></returns>
        public Tag MethodForm(Type type, string wrapperTagName, string methodName, Dictionary<string, object> defaults, bool registerProxy, out int paramCount)
        {
            Args.ThrowIfNull(type, "InvocationType");
			if(registerProxy)
			{
				ServiceProxySystem.Register(type);
			}

            MethodInfo method = type.GetMethod(methodName);
            defaults = defaults ?? new Dictionary<string, object>();

            System.Reflection.ParameterInfo[] parameters = method.GetParameters();
            paramCount = parameters.Length;
            Tag form = Tag.Of(wrapperTagName);

            for (int i = 0; i < parameters.Length; i++)
            {
                System.Reflection.ParameterInfo parameter = parameters[i];
                object defaultValue = defaults.ContainsKey(parameter.Name) ? defaults[parameter.Name] : null;
                string defaultString = defaultValue == null ? string.Empty : defaultValue.ToString();

                Tag label = Tag.Of("label", null, string.Format(this.LabelFormat, parameter.Name.PascalSplit(" ")))
                    .AddClass(this.LabelCssClass);
                
                bool addLabel = this.AddLabels;
                bool addValue = true;
                bool wasObject = false;
                bool handled = false;
                TryBuildPrimitiveInput(parameter, defaultValue, ref addValue, ref handled, out Tag toAdd, out Type paramType);

                if (!handled)
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
                    form.Child(label).BreakIf(this.Layout == ParameterLayouts.BreakAfterLabels);
                }

                form.Child(toAdd).BreakIf(
                    this.Layout != ParameterLayouts.NoBreaks && 
                    i != parameters.Length - 1 &&
                    !wasObject);
            }

            return form.DataSet("method", methodName)
                .FirstChildIf(wrapperTagName.Equals("fieldset"), Tags.Legend()
                .Text(GetLegend(method)));
        }

		private string GetLegend(MethodInfo method)
		{
			LegendAttribute legendAttribute;
			if(method.HasCustomAttributeOfType<LegendAttribute>(out legendAttribute))
			{
				return legendAttribute.Value;
			}
			else
			{
				return method.Name.PascalSplit(" ");
			}
		}

        private string GetLegend(Type type)
        {
            LegendAttribute legendAttribute;
            if (type.HasCustomAttributeOfType<LegendAttribute>(out legendAttribute))
            {
                return legendAttribute.Value;
            }
            else
            {
                return type.Name.PascalSplit(" ");
            }
        }

        private void TryBuildPrimitiveInput(System.Reflection.ParameterInfo parameter, object defaultValue, ref bool addValue, ref bool handled, out Tag toAdd, out Type paramType)
        {
            toAdd = null;

            paramType = parameter.ParameterType;
            // if the parameter type is a primitive then use the appropriate html input
            // string - text 
            if (paramType == typeof(int) || paramType == typeof(long))
            {
                toAdd = NumberTagProvider(parameter);
                handled = true;
            }
            else if (paramType == typeof(string))
            {
                toAdd = TextTagProvider(parameter);
                handled = true;
            }
            // DateTime - text with jQuery datepicker    
            else if (paramType == typeof(DateTime))
            {
                addValue = defaultValue != null;
                toAdd = DateTimeTagProvider(parameter)
                    .ValueIf(addValue, ((DateTime)defaultValue).ToShortDateString());

                handled = true;
            }// bool - checkbox
            else if (paramType == typeof(bool))
            {
                toAdd = BooleanTagProvider(parameter)
                    .CheckedIf(defaultValue != null && (bool)defaultValue);

                handled = true;
            }// enum - radio list
            else if (paramType.IsEnum)
            {
                toAdd = Tag.RadioList(paramType, defaultValue);
                addValue = false;

                handled = true;
            }

        }

        public Tag FieldsetFor(Type paramType, object defaultValues, string name = null)
        {
            return FieldsetFor(paramType, defaultValues, name, 0);
        }

		/// <summary>
		/// Get a form to input the properties of the specified instance
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <returns></returns>
        public Tag FieldsetFor(object param, string name = null)
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

        public Tag FieldsetFor(Type paramType, object defaultValues, string name = null, int recursionThusFar = 0)
        {
            Tag container = GetFieldset(name);

            AppendInputsFor(paramType, defaultValues, container, recursionThusFar);

            return container;
        }

        public Tag FieldsetForDynamic(dynamic obj, string name = null, bool setValues = false, int recursionThusFar = 0)
        {
            Tag container = GetFieldset(name);

            AppendInputsForDynamic(obj, container, setValues, 0);
            return container;
        }

        private static Tag GetFieldset(string name)
        {
            string legend = string.IsNullOrEmpty(name) ? "" : name.PascalSplit(" ");
            Tag container = Tags.Fieldset()
                .Attr("itemscope", "itemscope")
                .Attr("itemtype", "http://schema.org/Thing")
                .DataSet("type", "object")
                .ChildIf(!string.IsNullOrEmpty(name), Tags.Legend().TextIf(!string.IsNullOrEmpty(name), legend));
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
                return _propertyInclusionPredicate ?? (_propertyInclusionPredicate = (p) => !p.HasCustomAttributeOfType<ExcludeAttribute>());
            }
            set => _propertyInclusionPredicate = value;
        }

        /// <summary>
        /// Limit the depth to which Types will be analyzed
        /// </summary>
        public int RecursionLimit
        {
            get;
            private set;
        }

        internal protected void AppendInputsForDynamic(dynamic target, Tag container, bool setValues, int recursionThusFar)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(target);
            string paramTypeName = target.Name;

            foreach (PropertyDescriptor property in properties)
            {                
                string labelText = string.Format(this.LabelFormat, property.Name.PascalSplit(" "));
                string id = "{0}_{1}"._Format(paramTypeName, property.Name);

                Tag label = Tags.Label()
                    .Text(labelText)
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

                Tag toAdd = null;

                if (propType == typeof(int) || propType == typeof(long))
                {
                    toAdd = InputProvider.CreateInput(InputTypes.Number, property.Name);
                }
                else if (propType == typeof(string))
                {
                    toAdd = InputProvider.CreateInput(InputTypes.Text, property.Name);
                }
                else if (propType == typeof(DateTime))
                {
                    DateTime defaultDate = (DateTime?) defaultValue ?? DateTime.MinValue;
                    addValue = defaultDate > DateTime.MinValue;
                    toAdd = InputProvider.CreateInput(InputTypes.Text, property.Name)
                        .DataSet("plugin", "datepicker"); 

                    addValue = false;
                }
                else if (propType == typeof(bool))
                {

                    toAdd = InputProvider.CreateInput(InputTypes.Checkbox, property.Name);
                    if (defaultValue != null)
                    {
                        bool bVal;
                        Boolean.TryParse(defaultValue.ToString(), out bVal);
                        toAdd.CheckedIf(defaultValue != null && bVal);
                    }
                }
                else if (propType.IsEnum)
                {
                    toAdd = Tag.RadioList(propType, defaultValue).DataSet("type", "enum"); 
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
                        .BreakIf(addLabel && breakAfterLabel)
                        .Child(toAdd)
                        .BreakIf(!isHidden && !wasObject &&
                        (
                            this.Layout == ParameterLayouts.Default ||
                            this.Layout == ParameterLayouts.BreakAfterLabels
                        )
                    );
                }
            }
        }

        internal protected void AppendInputsFor(object defaultValues, Tag container)
        {
            AppendInputsFor(defaultValues.GetType(), defaultValues, container);
        }

        internal protected void AppendInputsFor(Type paramType, object defaultValues, Tag container)
        {
            AppendInputsFor(paramType, defaultValues, container, 0);
        }

        internal protected void AppendInputsFor(Type paramType, object defaultValues, Tag container, int recursionThusFar)
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

                Tag label = Tags.Label()
                    .Text(labelText)
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

                StringInputAttribute attr;
                bool isHidden = false;
                bool wasObject = false;
                bool typeAdded = false;

                Tag toAdd = null;

                if (property.HasCustomAttributeOfType<StringInputAttribute>(true, out attr))
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
                    toAdd = InputProvider.CreateInput(InputTypes.Number, property.Name);

                }
                else if (propType == typeof(string))
                {
                    toAdd = InputProvider.CreateInput(InputTypes.Text, property.Name);
                }
                else if (propType == typeof(DateTime))
                {
                    DateTime defaultDate = defaultValue == null ? DateTime.MinValue : (DateTime)defaultValue;
                    addValue = defaultDate > DateTime.MinValue;
                    toAdd = InputProvider.CreateInput(InputTypes.Text, property.Name)
                        .DataSet("plugin", "datepicker");
                    
                    addValue = false;
                }
                else if (propType == typeof(bool))
                {
                    toAdd = InputProvider.CreateInput(InputTypes.Checkbox, property.Name)
                        .CheckedIf(defaultValue != null && (bool)defaultValue);
                }
                else if (propType.IsEnum)
                {
                    toAdd = Tag.RadioList(propType, defaultValue).DataSet("type", "enum");
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
                        .BreakIf(labelAttr != null && labelAttr.PreBreak && addLabel)
                        .ChildIf(addLabel, label)
                        .BreakIf(addLabel && breakAfterLabel)
                        .Child(toAdd)
                        .BreakIf(!isHidden && !wasObject &&
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
    }
}
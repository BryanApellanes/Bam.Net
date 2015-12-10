/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using Naizari.Helpers;
using Naizari.Extensions;
using Naizari.Test;

namespace Naizari.Javascript.BoxControls
{
    public class BoxVariable
    {
        object topLevel;
        object currentlyReading;

        public BoxVariable(string variableName, object topLevel)
        {
            this.IsValid = true;
            this.VariableName = variableName;
            this.topLevel = topLevel;
            this.currentlyReading = topLevel;
            this.Value = string.Empty;
            this.SetValue();
        }

        private void SetValue()
        {
            Expect.IsTrue(this.VariableName.StartsWith(BoxServer.VariablePrefix), "Invalid variableName specified");
            Expect.IsTrue(this.VariableName.EndsWith(BoxServer.VariableSuffix), "Invalid variableName specified");

            string property = VariableName.Replace(BoxServer.VariablePrefix, string.Empty).Replace(BoxServer.VariableSuffix, string.Empty);
            string[] split = property.Split('.');
            foreach (string propertyName in split)
            {
                PropertyInfo propInfo = this.currentlyReading.GetType().GetProperty(propertyName);
                if (propInfo != null)
                {
                    if (propInfo.PropertyType == typeof(string) ||
                        propInfo.PropertyType == typeof(int) ||
                        propInfo.PropertyType == typeof(long) ||
                        propInfo.PropertyType == typeof(decimal) ||
                        propInfo.PropertyType == typeof(DateTime) ||
                        propInfo.PropertyType == typeof(bool))
                    {
                        object val = propInfo.GetValue(this.currentlyReading, null);
                        this.Value = val != null ? val.ToString(): string.Empty;
                        if (propInfo.PropertyType == typeof(bool))
                            this.Value = this.Value.ToLower();
                    }
                    else
                    {
                        this.currentlyReading = propInfo.GetValue(this.currentlyReading, null);
                    }
                }
                else
                {
                    this.IsValid = false;
                    return;
                }
            }
        }

        public static string[] GetPropertyVariables(Type typeToGetVariablesFor)
        {
            List<string> retVal = new List<string>();
            foreach (PropertyInfo propInfo in typeToGetVariablesFor.GetProperties())
            {
                if (propInfo.PropertyType == typeof(string) ||
                        propInfo.PropertyType == typeof(int) ||
                        propInfo.PropertyType == typeof(long) ||
                        propInfo.PropertyType == typeof(decimal) ||
                        propInfo.PropertyType == typeof(DateTime) ||
                        propInfo.PropertyType == typeof(bool) ||
                        propInfo.PropertyType.IsEnum)
                {
                    retVal.Add(string.Format("{0}{1}{2}", BoxServer.VariablePrefix, propInfo.Name, BoxServer.VariableSuffix));
                }
                else if (CustomAttributeExtension.HasCustomAttributeOfType<BoxVar>(propInfo))
                {
                    string prefix = string.Format("{0}{1}.", BoxServer.VariablePrefix, propInfo.Name);
                    string[] variables = GetPropertyVariables(propInfo.PropertyType);
                    foreach (string variable in variables)
                    {
                        string varName = variable.Replace(BoxServer.VariablePrefix, string.Empty).Replace(BoxServer.VariableSuffix, string.Empty);
                        retVal.Add(string.Format("{0}{1}{2}", prefix, varName, BoxServer.VariableSuffix));
                    }
                }
            }
            return retVal.ToArray();
        }
        public bool IsValid { get; private set; }

        public string Value { get; private set; }
        public string VariableName { get; private set; }
    }
}

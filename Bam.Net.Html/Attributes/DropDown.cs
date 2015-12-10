/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;
using System.Web.Mvc;

namespace Bam.Net.Html
{
    /// <summary>
    /// Designates that this property when rendered by the FileExtParamsBuilder
    /// should be a drowpdown (select) element.  Must call 
    /// DropDown.SetOptions([nameToUseForDefaultValue], dictionaryOfDefaultValues)
    /// for this to function properly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DropDown: StringInput
    {
        static DropDown()
        {
            OptionLookup = new Dictionary<string, Dictionary<string, string>>();
        }
        
        string key;
        public DropDown(string optionKey)
        {
            this.key = optionKey;
        }

        public DropDown(Type enumType)
        {
            this.key = enumType.Name;
            DropDown.SetOptions(this.key, enumType);
        }

        public Dictionary<string, string> Options
        {
            get
            {
                if (OptionLookup.ContainsKey(key))
                {
                    return OptionLookup[key];
                }

                return new Dictionary<string, string>();
            }
        }

        public static void SetOptions(string key, Dictionary<string, string> options)
        {
            if (!OptionLookup.ContainsKey(key))
            {
                OptionLookup.Add(key, new Dictionary<string, string>());
            }

            OptionLookup[key] = options;
        }

        public static void SetOptions(string key, Type enumType)
        {
            Dictionary<string, string> options = DictionaryFromEnum(enumType);

            SetOptions(key, options);
        }

        internal static Dictionary<string, string> DictionaryFromEnum(Type enumType)
        {
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            Dictionary<string, string> options = new Dictionary<string, string>();
            foreach (FieldInfo field in fields)
            {
                object enumValue = field.GetRawConstantValue();
                string enumString = field.Name.PascalSplit(" ");
                options.Add(enumValue.ToString(), enumString);
            }
            return options;
        }

        /// <summary>
        /// A static set of application level default values.  Used by instances
        /// of DropDown to find their default values.
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> OptionLookup
        {
            get;
            set;
        }

        public override bool? BreakAfterLabel
        {
            get;
            set;
        }

        public override bool? AddValue
        {
            get
            {
                return false;
            }
            set
            {
                // always false
            }
        }

        public override bool? IsHidden
        {
            get;
            set;
        }

        public override bool? AddLabel
        {
            get;
            set;
        }

        public override TagBuilder CreateInput()
        {
            return this.Options.DropDown(Default == null ? string.Empty: Default.ToString());
        }
    }
}

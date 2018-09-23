/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Reflection;
using Bam.Net;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Presentation.Html
{
    public static class RadioList
    {
        /// <summary>
        /// Builds a ul element with radio buttons inside of li elements
        /// </summary>
        /// <param name="enumType">The typeof(enum)</param>
        /// <param name="selected">The enum.value to select</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TagBuilder EnumRadios(this TagBuilder builder, Type enumType, object selected, string id = null)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException(string.Format("The specified type ({0}) is not an enum.", enumType.Name));
            }

            TagBuilder list = FromEnum(enumType, selected, id);

            return builder.Child(list);
        }

        public static TagBuilder FromEnum(Type enumType, object selected, string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                id = "".RandomString(5);
            }

            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            TagBuilder list = new TagBuilder("ul")
                .Attr("id", id)
                .Css("list-style", "none");

            bool first = true;
            foreach (FieldInfo field in fields)
            {
                object enumValue = field.GetRawConstantValue();
                string enumString = field.Name.PascalSplit(" ");
                string radioId = new StringBuilder(field.Name).A("_".RandomString(4)).ToString();

                bool selectedCondition = selected != null ? field.Name.Equals(selected.ToString()) : first;
                TagBuilder li = new TagBuilder("li")
                    .Child(
                        new TagBuilder("input")
                        .Radio()
                        .Id(radioId)
                        .Name(enumType.Name)
                        .Value(enumValue.ToString())
                        .Attr("data-text", field.Name)
                        .CheckedIf(selectedCondition)
                    ).Label(radioId, enumString);

                list.Child(li);
                first = false;
            }
            return list;
        }

    }
}

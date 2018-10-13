/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Html;
using Bam.Net.Js;
using Bam.Net;
using System.Web.Mvc;
using System.Web;
using Bam.Net.Presentation.Html;
using Bam.Net.ServiceProxy;
using System.Reflection;
using Newtonsoft.Json;

namespace Bam.Net.Presentation.Html
{
    public static class HtmlHelperExtensions
    {
        public static Tag AjaxPartial(this HtmlHelper helper, string viewName, string controller = "home")
        {
            return new Tag("div").Data("view-name", viewName).Data("controller", controller);
        }

        public static Tag Tag(this HtmlHelper helper, string tagName, object attributes = null)
        {
            return new Tag(tagName, attributes);
        }

        public static MvcHtmlString ServiceProxyPartial(this HtmlHelper helper, string viewName, object model)
        {
            Type type = model.GetType();
            string view = string.Format("{0}/{1}", type.Name, viewName);
            if (!ServiceProxySystem.ServiceProxyPartialExists(type, viewName))
            {
                ServiceProxySystem.WriteServiceProxyPartial(type, viewName);
            }

            return helper.Partial(view, model);
        }

        public static MvcHtmlString FriendlyDate(this HtmlHelper helper, DateTime utcValue, string tagName = "span", object htmlAttrs = null)
        {
            return helper.Date(utcValue, tagName, htmlAttrs);
        }

        public static MvcHtmlString Date(this HtmlHelper helper, DateTime utcValue, string tagName = "span", object htmlAttrs = null)
        {
            //month = dataset.MM,
            //    day = dataset.DD,
            //    year = dataset.YYYY,
            //    hour = dataset.hh,
            //    minute = dataset.mm,
            //    second = dataset.ss,
            //    millisecond = dataset.ms
            return new TagBuilder(tagName)
                .DataSet("mo", (utcValue.Month - 1).ToString())
                .DataSet("dd", utcValue.Day.ToString())
                .DataSet("yyyy", utcValue.Year.ToString())
                .DataSet("hh", utcValue.Hour.ToString())
                .DataSet("mm", utcValue.Minute.ToString())
                .DataSet("ss", utcValue.Second.ToString())
                .DataSet("ms", utcValue.Millisecond.ToString())
                .DataSet("plugin", "friendlyDate")
                .AttrsIf(htmlAttrs != null, htmlAttrs)
                .ToHtml();
        }

        public static MvcHtmlString Deferred(this HtmlHelper helper, Func<DeferredViewState, MvcHtmlString> content, int millisecondsToRender = 300)
        {
            return new DeferredView(
                "html_deferred_".RandomLetters(4),
                (o)=> content(new DeferredViewState((DeferredView)o)), 
                millisecondsToRender)
            .Render();
        }

        public static MvcHtmlString Deferred(this HtmlHelper helper, Func<DeferredViewState, MvcHtmlString> content, Func<MvcHtmlString> initial, int millisecondsToRender = 300)
        {
            return new DeferredView(
                "html_deferred_".RandomLetters(4),  
                (o)=> content(new DeferredViewState((DeferredView)o)),
                initial, 
                millisecondsToRender)
            .Render();
        }

        public static MvcHtmlString Deferred(this HtmlHelper helper, Func<DeferredViewState, MvcHtmlString> content, MvcHtmlString initial, int millisecondsToRender = 300)
        {
            return new DeferredView(
                "html_deferred_".RandomLetters(4), 
                (o)=> content(new DeferredViewState((DeferredView)o)), 
                initial, 
                millisecondsToRender)
            .Render();
        }

        public static MvcHtmlString Deferred(this HtmlHelper helper, Func<DeferredViewState, MvcHtmlString> content, MvcHtmlString initial, object attributes, int millisecondsToRender = 300)
        {
            return new DeferredView(
                "html_deferred_".RandomLetters(4),
                (o) => content(new DeferredViewState((DeferredView)o)),
                initial,
                attributes,
                millisecondsToRender)
            .Render();
        }
        
        public static MvcHtmlString FieldsetFor(this HtmlHelper helper, string json, string legendText = null, object wrapperAttrs = null, bool setValues = false)
        {
            object obj = JsonConvert.DeserializeObject(json);
            return FieldsetFor(helper, obj, legendText, wrapperAttrs, setValues);
        }

        public static MvcHtmlString FieldsetFor(this HtmlHelper helper, dynamic obj, string legendText = null, object wrapperAttrs = null, bool setValues = false)
        {
            InputFormBuilder builder = new InputFormBuilder();
            TagBuilder tag = builder.FieldsetForDynamic(obj, legendText, setValues);
            tag.AttrsIf(wrapperAttrs != null, wrapperAttrs);
            return tag.ToMvcHtml();
        }

        public static MvcHtmlString FieldSetFor(this HtmlHelper helper, Type type, object defaults = null, string legendText = null, object wrapperAttrs = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            TagBuilder tag = builder.FieldsetFor(type, defaults, legendText);
            tag.AttrsIf(wrapperAttrs != null, wrapperAttrs);
            return tag.ToMvcHtml();
        }

        public static MvcHtmlString InputsFor(this HtmlHelper helper, Type type, object defaults = null, object wrapperAttrs = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            TagBuilder tag = new TagBuilder("span");
            builder.AppendInputsFor(type, defaults, tag);
            tag.AttrsIf(wrapperAttrs != null, wrapperAttrs);
            return tag.ToMvcHtml();
        }

        /// <summary>
        /// Creates a form representing the parameters for the specified 
        /// method of the specified type.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="buttonAttributes"></param>
        /// <param name="extraButtons"></param>
        /// <param name="defaults"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MethodCall(this HtmlHelper helper,
            string className,
            string methodName,
            object buttonAttributes = null,
            object[] extraButtons = null,
            Dictionary<string, object> defaults = null,
            object htmlAttributes = null)
        {
            return helper.MethodCall(ServiceProxySystem.Incubator[className], methodName, buttonAttributes, extraButtons, defaults, htmlAttributes);
        }

        /// <summary>
        /// Creates a form representing the parameters for the specified 
        /// method of the specified type.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="buttonAttributes"></param>
        /// <param name="extraButtons"></param>
        /// <param name="defaults"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MethodCall(this HtmlHelper helper,
            Type type,
            string methodName,
            object buttonAttributes = null,
            object[] extraButtons = null,
            Dictionary<string, object> defaults = null,
            object htmlAttributes = null,
            string wrapperTagName = "fieldset"
        )
        {
            int paramCount = -1;
            string id = string.Format("{0}_{1}", type.Name, methodName);
            TagBuilder builder = GetParamsBuilder(type, methodName, wrapperTagName, defaults, htmlAttributes, out paramCount)
                .IdIfNone(id)
                .Br();

            builder.MethodButton(
                type.Name,
                methodName,
                paramCount > 0 ? builder.Id() : null, buttonAttributes);

            if (extraButtons != null)
            {
                foreach (object buttonInfo in extraButtons)
                {
                    builder.Child(new TagBuilder("span").Button(buttonInfo));
                }
            }

            string formName = "form_" + id;
            StringBuilder final = new StringBuilder(
                    builder
                    .Parent("form")
                    .Name(formName)
                    .Id(formName)
                    .Attr("action", "#").ToString())
                .A(new TagBuilder("div").Id(string.Format("{0}_results", methodName)).ToString());

            return MvcHtmlString.Create(final.ToString());
        }

        public static TagBuilder InputFor(this HtmlHelper helper, Type type, object defaultValues = null, string name = null, string labelClass = null)
        {
            InputFormBuilder builder = CreateBuilder(type, labelClass);
            return builder.FieldsetFor(type, defaultValues, name);
        }

        public static MvcHtmlString InputFor(this HtmlHelper helper,
            Type type,
            object buttonAttributes = null,
            object wrapperAttributes = null,
            object defaultValues = null,
            string name = null,
            string labelClass = null)
        {
            InputFormBuilder builder = CreateBuilder(type, labelClass);

            return builder.FieldsetFor(type, defaultValues, name)
                    .AttrsIf(wrapperAttributes != null, wrapperAttributes)
                    .ChildIf(buttonAttributes != null, new TagBuilder("span").Button(buttonAttributes))
                    .ToHtml();
        }

        private static InputFormBuilder CreateBuilder(Type type, string labelClass)
        {
            InputFormBuilder builder = new InputFormBuilder();
            if (!string.IsNullOrWhiteSpace(labelClass))
            {
                builder.LabelCssClass = labelClass;
            }
            return builder;
        }

        /// <summary>
        /// Build an input fieldset element for the specified method of the specified
        /// class.
        /// </summary>
        /// <param name="helper">The FileExtHelper</param>
        /// <param name="className">The name of the class that whose method will be called</param>
        /// <param name="methodName">The name of the method that will be called</param>
        /// <param name="defaults">A dictionary of default values to fill into the resulting
        /// form keyed by the names of the method parameters</param>
        /// <returns>MvchtmlString</returns>
        public static MvcHtmlString ParamsFor(this HtmlHelper helper,
            string className,
            string methodName,
            Dictionary<string, object> defaults = null)
        {
            return helper.ParamsFor(ServiceProxySystem.Incubator[className], methodName, defaults);
        }

        public static MvcHtmlString ParamsFor(this HtmlHelper helper,
            Type type,
            string methodName,
            Dictionary<string, object> defaults = null,
            object htmlAttributes = null)
        {
            return ParamsFor(helper, type, methodName, "fieldset", defaults, htmlAttributes);
        }

        /// <summary>
        /// Build a fieldset element for the specified method of the specified
        /// class.
        /// </summary>
        /// <param name="helper">The FileExtHelper</param>
        /// <param name="type">The Type whose method will be called</param>
        /// <param name="methodName">The name of the method that will be called</param>
        /// <param name="defaults">A dictionary of default values to fill into the resulting
        /// form keyed by the names of the method parameters</param>
        /// <returns>MvchtmlString</returns>
        public static MvcHtmlString ParamsFor(this HtmlHelper helper,
            Type type,
            string methodName,
            string wrapperTagName = "fieldset",
            Dictionary<string, object> defaults = null,
            object htmlAttributes = null)
        {
            if (ServiceProxySystem.Incubator[type] == null)
            {
                ServiceProxySystem.Register(type);
            }
            int ignore = -1;
            TagBuilder form = GetParamsBuilder(type, methodName, wrapperTagName, defaults, htmlAttributes, out ignore);

            return MvcHtmlString.Create(form.ToString());
        }

        private static TagBuilder GetParamsBuilder(Type type, string methodName, string wrapperTagName, Dictionary<string, object> defaults, object htmlAttributes, out int paramCount)
        {
            InputFormBuilder builder = new InputFormBuilder(type);
            builder.AddLabels = true;
            TagBuilder form = builder.MethodForm(wrapperTagName, methodName, defaults, out paramCount)
                .AttrsIf(htmlAttributes != null, htmlAttributes);

            return form;
        }

        private static void ThrowIfNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Specifies default values to use when creating ServiceProxy Method forms for the 
        /// specified method.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="method"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> AddDefault(this Dictionary<string, object> target, MethodInfo method, object value)
        {
            ThrowIfNull(target, "target");
            ThrowIfNull(method, "method");
            ThrowIfNull(value, "value");

            string parameterName = string.Empty;
            IEnumerable<ParameterInfo> parameters = (from p in method.GetParameters()
                                                     where p.ParameterType == value.GetType()
                                                     select p);

            int count = parameters.Count();
            if (count > 1)
            {
                throw new AmbiguousMatchException(string.Format("The specified method ({0}) takes more than one parameter of type ({1}).", method.Name, value.GetType().Name));
            }
            else if (count == 0)
            {
                return target;
            }
            else
            {
                return target.AddDefault(parameters.First().Name, value);
            }
        }

        /// <summary>
        /// Add the specified value to represent the default values for the
        /// specified parameterName.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<string, object> AddDefault(this Dictionary<string, object> target, string parameterName, object value)
        {
            target.Add(parameterName, value);
            return target;
        }

        /// <summary>
        /// Creates a dictionary of the specified object's properties. 
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="defaults">The value</param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary<T>(this T defaults)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (PropertyInfo prop in defaults.GetType().GetProperties())
            {
                if (!prop.PropertyType.IsArray)
                {
                    result.Add(prop.Name, prop.GetValue(defaults, null));
                }
            }

            return result;
        }

        /// <summary>
        /// Write a span to the page with attribute role=button that will
        /// cause the execution of the specified method on the specified 
        /// class when clicked.  Requires DataSet.js 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="className"></param>
        /// <param name="method"></param>
        /// <param name="parametersId"></param>
        /// <param name="text"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MethodButton(this HtmlHelper helper, string className, string method, string parametersId = null, string text = null, object htmlAttributes = null)
        {
            TagBuilder builder = new TagBuilder("span");
            builder.MethodButton(className, method, parametersId, text);
            return MvcHtmlString.Create(builder.ToString());
        }

        /// <summary>
        /// Write a javascript element block to the page containing
        /// the specified script.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public static MvcHtmlString Script(this HtmlHelper helper, string script)
        {
            return new TagBuilder("script")
                .Attr("type", "text/javascript")
                .Attr("language", "javascript")
                .Html(script)
                .ToHtml();
        }

        public static MvcHtmlString DropDown(this HtmlHelper helper, Type enumType, string selected = null, object htmlAttributes = null)
        {
            return enumType.DropDown(selected, htmlAttributes).ToHtml();
        }
    
    }
}

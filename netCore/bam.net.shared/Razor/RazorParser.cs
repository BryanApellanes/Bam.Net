/*
	Copyright © Bryan Apellanes 2015  
*/
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Razor;

namespace Bam.Net.Razor
{
    public class RazorParser<TBaseTemplate> where TBaseTemplate: RazorBaseTemplate
    {
        RazorTemplateEngine _engine;
        public RazorParser()
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name))
        {
        }

        public RazorParser(object options = null)
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name), options)
        {
        }

        public RazorParser(Action<string> resultInspector)
            : this(typeof(TBaseTemplate).Namespace, string.Format("{0}Template", typeof(TBaseTemplate).Name))
        {
            this.ResultInspector = resultInspector;
        }

        static RazorParser()
        {
            DefaultRazorInspector = RazorBaseTemplate.DefaultInspector;
        }

        /// <summary>
        /// The default inspector used by any RazorParser that hasn't been assigned one
        /// </summary>
        public static Action<string> DefaultRazorInspector
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultNamespace"></param>
        /// <param name="defaultClassName"></param>
        /// <param name="options">Applied to the GeneratedClassContext</param>
        public RazorParser(string defaultNamespace = "RazorOutput", string defaultClassName = "Template", object options = null)
        {
            if (this.ResultInspector == null)
            {
                this.ResultInspector = DefaultRazorInspector;
            }

            GetDefaultAssembliesToReference = () => new[] { typeof(Args).Assembly };

            // Set up the hosting environment

            // a. Use the C# language (you could detect this based on the file extension if you want to)
            RazorEngineHost host = new RazorEngineHost(new CSharpRazorCodeLanguage());

            // b. Set the base class
            host.DefaultBaseClass = typeof(TBaseTemplate).FullName;

            // c. Set the output namespace and type name
            host.DefaultNamespace = "RazorOutput";
            host.DefaultClassName = "Template";

            // d. Add default imports
            host.NamespaceImports.Add("System");
            host.NamespaceImports.Add("Bam.Net");
            host.NamespaceImports.Add("Bam.Net.ServiceProxy");
            host.NamespaceImports.Add(defaultNamespace);
            
            if(options != null)
            {
                Type contextType = host.GeneratedClassContext.GetType();
                foreach (PropertyInfo prop in options.GetType().GetProperties())
                {
                    PropertyInfo contextProp = contextType.GetProperty(prop.Name);
                    if (contextProp != null)
                    {
                        object valueToSet = prop.GetValue(options, null);
                        contextProp.SetValue(host.GeneratedClassContext, valueToSet, null);
                    }
                }
            }
            // Create the template engine using this host
            _engine = new RazorTemplateEngine(host);
        }
        
        public Action<string> ResultInspector
        {
            get;
            set;
        }

		/// <summary>
		/// Execute the specified razor resource template
		/// </summary>
		/// <param name="templateName">The name of the embedded resource template</param>
		/// <param name="options">Additional information to pass to the template engine including 
		/// the Model</param>
		/// <returns></returns>
        public string ExecuteResource(string templateName, object options = null)
        {
            Type currentType = this.GetType();
			return ExecuteResource(templateName, currentType, options);
        }

        public string ExecuteResource(string templateName, Type nameSpaceExampleAndResourceContainer, object options = null, params Assembly[] assembliesToReference)
        {
            string namespacePath = string.Format("{0}.Templates.", nameSpaceExampleAndResourceContainer.Namespace);
            return ExecuteResource(templateName, namespacePath, nameSpaceExampleAndResourceContainer.Assembly, options, assembliesToReference);
        }

		public string ExecuteResource(string templateName, string resourceNamePrefix, Assembly resourceContainer, object options = null, params Assembly[] assembliesToReference)
		{
            string[] manifestResourceNames = resourceContainer.GetManifestResourceNames();
            string resourcePath = manifestResourceNames.FirstOrDefault(fullPath => fullPath.Substring(resourceNamePrefix.Length, fullPath.Length - resourceNamePrefix.Length).Equals(templateName));
			
			if (string.IsNullOrEmpty(resourcePath))
			{
				throw new InvalidOperationException(string.Format("The specified resource template was not found: {0}", templateName));
			}

			using (StreamReader resourceTemplate = new StreamReader(resourceContainer.GetManifestResourceStream(resourcePath)))
			{
				string hashKey = resourceTemplate.ReadToEnd().Md5();
				resourceTemplate.BaseStream.Position = 0;
				return Execute(resourceTemplate, hashKey, options, ResultInspector, assembliesToReference);
			}
		}

		public string Execute(TextReader input, object options = null, Action<string> inspector = null)
		{
			return Execute(input, null, options, inspector);
		}


        /// <summary>
        /// Executes the specified input and returns the resulting output
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hashKey"></param>
        /// <param name="options">Arguments to pass to the template engine including the Model</param>
        /// <param name="inspector"></param>
        /// <param name="assembliesToReference"></param>
        /// <returns></returns>
        public string Execute(TextReader input, string hashKey = null, object options = null, Action<string> inspector = null, params Assembly[] assembliesToReference)
		{
			Assembly templateAssembly;
			if (!string.IsNullOrEmpty(hashKey) && CompiledTemplates.ContainsKey(hashKey))
			{
				templateAssembly = CompiledTemplates[hashKey];
			}
			else
			{
				if(assembliesToReference == null || assembliesToReference.Length == 0)
				{
					assembliesToReference = GetDefaultAssembliesToReference();
				}
                templateAssembly = GetTemplateAssembly(input, hashKey, assembliesToReference, out CSharpCodeProvider codeProvider, out GeneratorResults results);
                OutputToInspector(inspector, codeProvider, results);
			}
			
			return GetRazorTemplateResult(options, templateAssembly);
		}

        public Func<Assembly[]> GetDefaultAssembliesToReference
        {
            get;
            set;
        }

		private Assembly GetTemplateAssembly(TextReader input, string hashKey, Assembly[] assembliesToReference, out CSharpCodeProvider codeProvider, out GeneratorResults results)
		{
			Assembly templateAssembly;
			codeProvider = new CSharpCodeProvider();
			results = _engine.GenerateCode(input);
			CompilerResults compilerResults = codeProvider.CompileAssemblyFromDom(
				GetCompilerParameters(assembliesToReference), results.GeneratedCode);

			if (compilerResults.Errors.HasErrors)
			{
				throw new Exception(compilerResults.Errors[0].ErrorText);
			}
			templateAssembly = compilerResults.CompiledAssembly;
			if (!string.IsNullOrEmpty(hashKey))
			{
				CompiledTemplates[hashKey] = templateAssembly;
			}
			return templateAssembly;
		}

		private CompilerParameters GetCompilerParameters(params Assembly[] assembliesToReference)
		{
			string[] referencePaths = assembliesToReference.Select(a => a.GetFilePath()).ToArray();
			return new CompilerParameters(referencePaths);
		}

		private static void OutputToInspector(Action<string> inspector, CSharpCodeProvider codeProvider, GeneratorResults results)
		{
			if (inspector != null)
			{
				using (StringWriter sw = new StringWriter())
				{
					codeProvider.GenerateCodeFromCompileUnit(results.GeneratedCode, sw, new CodeGeneratorOptions());
					inspector(sw.GetStringBuilder().ToString());
				}
			}
		}

		private string GetRazorTemplateResult(object options, Assembly templateAssembly)
		{
			Type templateType = templateAssembly.GetType(string.Format("{0}.{1}", _engine.Host.DefaultNamespace, _engine.Host.DefaultClassName));
			ConstructorInfo ctor = templateType.GetConstructor(Type.EmptyTypes);
			object templateInstance = ctor.Invoke(null);

			if (options != null)
			{
				Type setType = options.GetType();
				foreach (PropertyInfo prop in setType.GetProperties())
				{
					templateType.GetProperty(prop.Name).SetValue(templateInstance, prop.GetValue(options, null), null);
				}
			}

			templateType.GetMethod("Execute").Invoke(templateInstance, null);

			return ((StringBuilder)templateType.GetProperty("Generated").GetValue(templateInstance, null)).ToString();
		}

		static Dictionary<string, Assembly> _compiledTemplates;
		private static Dictionary<string, Assembly> CompiledTemplates
		{
			get
			{
				if (_compiledTemplates == null)
				{
					_compiledTemplates = new Dictionary<string, Assembly>();
				}

				return _compiledTemplates;
			}
		}
    }
}

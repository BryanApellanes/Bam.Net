/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Bam.Net.ServiceProxy;
using Bam.Net.Razor;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A code generator that writes Dao code for a SchemaDefinition
    /// </summary>
    public class DaoGenerator
    {
        List<Stream> _resultStreams = new List<Stream>();
        public DaoGenerator()
        {
            this.DisposeOnComplete = true;
            this.SubscribeToEvents();
			
            this.Namespace = "DaoGenerated";
        }

        public static List<string> DefaultReferenceAssemblies
        {
            get
            {
				return new List<string>(AdHocCSharpCompiler.DefaultReferenceAssemblies);
            }
        }

        private void SubscribeToEvents()
        {
            this.BeforeWriteClass += (c, s) =>
            {
                _resultStreams.Add(s);
            };

            this.BeforeWriteCollection += (c, s) =>
            {
                _resultStreams.Add(s);
            };

            this.BeforeWriteColumnsClass += (c, s) =>
            {
                _resultStreams.Add(s);
            };

            this.BeforeWriteQiClass += (c, s) =>
            {
                _resultStreams.Add(s);
            };

            this.GenerateComplete += (g, schema) =>
            {
                if (DisposeOnComplete)
                {
                    foreach (Stream s in this._resultStreams)
                    {
                        s.Dispose();
                    }
                }

                this._resultStreams.Clear();
            };
        }
        
        public DaoGenerator(string nameSpace)
            : this()
        {
            this.Namespace = nameSpace;
        }

        public DaoGenerator(string ns, Action<string> resultInspector)
            : this(ns)
        {
            this.RazorResultInspector = resultInspector;
        }

        /// <summary>
        /// Gets or sets a value indicating whether dispose will
        /// be called on the output streams after code generation.
        /// </summary>
        public bool DisposeOnComplete { get; set; }

        public Action<string> RazorResultInspector { get; set; }
        #region events
        public event TargetTableEventDelegate BeforeClassParse;
        public event TargetTableEventDelegate AfterClassParse;

        public event TargetTableEventDelegate BeforePartialParse;
        public event TargetTableEventDelegate AfterPartialParse;

        public event TargetTableEventDelegate BeforeQueryClassParse;
        public event TargetTableEventDelegate AfterQueryClassParse;
        
        public event TargetTableEventDelegate BeforePagedQueryClassParse;
        public event TargetTableEventDelegate AfterPagedQueryClassParse;

        public event TargetTableEventDelegate BeforeQiClassParse;
        public event TargetTableEventDelegate AfterQiClassParse;

        public event TargetTableEventDelegate BeforeCollectionParse;
        public event TargetTableEventDelegate AfterCollectionParse;

        public event TargetTableEventDelegate BeforeColumnsClassParse;
        public event TargetTableEventDelegate AfterColumnsClassParse;


        public event GeneratorEventDelegate BeforeContextClassParse;
        public event GeneratorEventDelegate AfterContextClassParse;

        public event GeneratorEventDelegate BeforeContextStreamResolved;
        public event GeneratorEventDelegate AfterContextStreamResolved;

        /// <summary>
        /// The event that fires prior to code generation
        /// </summary>
        public event GeneratorEventDelegate GenerateStarted;

        /// <summary>
        /// The event that fires when code generation is complete
        /// </summary>
        public event GeneratorEventDelegate GenerateComplete;

        /// <summary>
        /// The event that fires when compilation starts
        /// </summary>
        public event GeneratorEventDelegate CompileStarted;

        /// <summary>
        /// The event that fires when compilation is complete
        /// </summary>
        public event GeneratorEventDelegate CompileComplete;

        /// <summary>
        /// The event that fires prior to resolving the target 
        /// stream for a table
        /// </summary>
        public event TargetTableEventDelegate BeforeClassStreamResolved;

        public event TargetTableEventDelegate BeforeQiClassStreamResolved;

        public event TargetTableEventDelegate BeforeQueryClassStreamResolved;
        public event TargetTableEventDelegate AfterQueryClassStreamResolved;

        public event TargetTableEventDelegate BeforePagedQueryClassStreamResolved;
        public event TargetTableEventDelegate AfterPagedQueryClassStreamResolved;
        /// <summary>
        /// The event that fires after the target stream is resolved for
        /// a table
        /// </summary>
        public event TargetTableEventDelegate AfterClassStreamResolved;

        public event TargetTableEventDelegate AfterQiClassStreamResolved;

        /// <summary>
        /// The event that fires before resolving the stream to write each
        /// collection to
        /// </summary>
        public event TargetTableEventDelegate BeforeCollectionStreamResolved;

        /// <summary>
        /// The event that fires after resolving the stream to write each ccollection to
        /// </summary>
        public event TargetTableEventDelegate AfterCollectionStreamResolved;

        public event TargetTableEventDelegate BeforeColumnsClassStreamResolved;

        public event TargetTableEventDelegate AfterColumnsClassStreamResolved;


        public event ResultStreamEventDelegate BeforeWriteContext;
        public event ResultStreamEventDelegate AfterWriteContext;
        /// <summary>
        /// The event that fires before writing code to the target stream
        /// </summary>
        public event ResultStreamEventDelegate BeforeWriteClass;

        public event ResultStreamEventDelegate BeforeWriteQueryClass;
        public event ResultStreamEventDelegate BeforeWritePagedQueryClass;

        public event ResultStreamEventDelegate BeforeWriteQiClass;

        public event ResultStreamEventDelegate BeforeWritePartial;
        public event ResultStreamEventDelegate AfterWritePartial;

        /// <summary>
        /// The event that fires after writing code to the target stream
        /// </summary>
        public event ResultStreamEventDelegate AfterWriteClass;

        public event ResultStreamEventDelegate AfterWriteQueryClass;
        public event ResultStreamEventDelegate AfterWriteQiClass;

        public event ResultStreamEventDelegate BeforeWriteCollection;

        public event ResultStreamEventDelegate AfterWriteCollection;

        public event ResultStreamEventDelegate BeforeWriteColumnsClass;

        public event ResultStreamEventDelegate AfterWriteColumnsClass;

        protected void OnBeforeColumnsClassParse(string ns, Table table)
        {
            if (BeforeColumnsClassParse != null)
            {
                BeforeColumnsClassParse(ns, table);
            }
        }

        protected void OnAfterColumnsClassParse(string ns, Table table)
        {
            if (AfterColumnsClassParse != null)
            {
                AfterColumnsClassParse(ns, table);
            }
        }

        protected void OnBeforeColumnsClassStreamResolved(string ns, Table table)
        {
            if (BeforeColumnsClassStreamResolved != null)
            {
                BeforeColumnsClassStreamResolved(ns, table);
            }
        }

        protected void OnAfterColumnsClassStreamResolved(string ns, Table table)
        {
            if (AfterColumnsClassStreamResolved != null)
            {
                AfterColumnsClassStreamResolved(ns, table);
            }
        }

        protected void OnBeforeWriteColumnsClass(string code, Stream s)
        {
            if (BeforeWriteColumnsClass != null)
            {
                BeforeWriteColumnsClass(code, s);
            }
        }

        protected void OnAfterWriteColumnsClass(string code, Stream s)
        {
            if (AfterWriteColumnsClass != null)
            {
                AfterWriteColumnsClass(code, s);
            }
        }

        protected void OnBeforeWriteCollection(string code, Stream s)
        {
            if (BeforeWriteCollection != null)
            {
                BeforeWriteCollection(code, s);
            }
        }

        protected void OnAfterWriteCollection(string code, Stream s)
        {
            if (AfterWriteCollection != null)
            {
                AfterWriteCollection(code, s);

            }
        }

        protected void OnBeforeCollectionParse(string ns, Table table)
        {
            if (BeforeCollectionParse != null)
            {
                BeforeCollectionParse(ns, table);
            }
        }

        protected void OnAfterCollectionParse(string ns, Table table)
        {
            if (AfterCollectionParse != null)
            {
                AfterCollectionParse(ns, table);
            }
        }

        protected void OnBeforeCollectionStreamResolved(string ns, Table table)
        {
            if (BeforeCollectionStreamResolved != null)
            {
                BeforeCollectionStreamResolved(ns, table);
            }
        }

        protected void OnAfterCollectionStreamResolved(string ns, Table table)
        {
            if (AfterCollectionStreamResolved != null)
            {
                AfterCollectionStreamResolved(ns, table);
            }
        }
        protected void OnBeforePagedQueryClassParse(string ns, Table table)
        {
            if (BeforePagedQueryClassParse != null)
            {
                BeforePagedQueryClassParse(ns, table);
            }
        }

        protected void OnAfterPagedQueryClassParse(string ns, Table table)
        {
            if (AfterPagedQueryClassParse != null)
            {
                AfterPagedQueryClassParse(ns, table);
            }
        }
        protected void OnBeforeQueryClassParse(string ns, Table table)
        {
            if (BeforeQueryClassParse != null)
            {
                BeforeQueryClassParse(ns, table);
            }
        }

        protected void OnAfterQueryClassParse(string ns, Table table)
        {
            if (AfterQueryClassParse != null)
            {
                AfterQueryClassParse(ns, table);
            }
        }

        protected void OnBeforeQiClassParse(string ns, Table table)
        {
            if (BeforeQiClassParse != null)
            {
                BeforeQiClassParse(ns, table);
            }
        }

        protected void OnAfterQiClassParse(string ns, Table table)
        {
            if (AfterQiClassParse != null)
            {
                AfterQiClassParse(ns, table);
            }
        }

        protected void OnAfterQiClassStreamResolved(string ns, Table table)
        {
            if (AfterQiClassStreamResolved != null)
            {
                AfterQiClassStreamResolved(ns, table);
            }
        }
        protected void OnBeforePagedQueryClassStreamResolved(string ns, Table table)
        {
            if (BeforePagedQueryClassStreamResolved != null)
            {
                BeforePagedQueryClassStreamResolved(ns, table);
            }
        }
        protected void OnAfterPagedQueryClassStreamResolved(string ns, Table table)
        {
            if (AfterPagedQueryClassStreamResolved != null)
            {
                AfterPagedQueryClassStreamResolved(ns, table);
            }
        } 
        protected void OnBeforeQueryClassStreamResolved(string ns, Table table)
        {
            if (BeforeQueryClassStreamResolved != null)
            {
                BeforeQueryClassStreamResolved(ns, table);
            }
        }
        protected void OnAfterQueryClassStreamResolved(string ns, Table table)
        {
            if (AfterQueryClassStreamResolved != null)
            {
                AfterQueryClassStreamResolved(ns, table);
            }
        } 

        protected void OnBeforeQiClassStreamResolved(string ns, Table table)
        {
            if (BeforeQiClassStreamResolved != null)
            {
                BeforeQiClassStreamResolved(ns, table);
            }
        }        

        protected void OnBeforeClassParse(string ns, Table table)
        {
            if (BeforeClassParse != null)
            {
                BeforeClassParse(ns, table);
            }
        }

        protected void OnAfterClassParse(string ns, Table table)
        {
            if (AfterClassParse != null)
            {
                AfterClassParse(ns, table);
            }
        }

        protected void OnBeforePartialParse(string ns, Table table)
        {
            if (BeforePartialParse != null)
            {
                BeforePartialParse(ns, table);
            }
        }

        protected void OnAfterPartialParse(string ns, Table table)
        {
            if (AfterPartialParse != null)
            {
                AfterPartialParse(ns, table);
            }
        }
        protected void OnBeforeWriteQueryClass(string code, Stream stream)
        {
            if (BeforeWriteQueryClass != null)
            {
                BeforeWriteQueryClass(code, stream);
            }
        }

        protected void OnAfterWriteQueryClass(string code, Stream stream)
        {
            if (AfterWriteQueryClass != null)
            {
                AfterWriteQueryClass(code, stream);
            }
        }
        protected void OnBeforeWriteQiClass(string code, Stream stream)
        {
            if (BeforeWriteQiClass != null)
            {
                BeforeWriteQiClass(code, stream);
            }
        }

        protected void OnAfterWriteQiClass(string code, Stream stream)
        {
            if (AfterWriteQiClass != null)
            {
                AfterWriteQiClass(code, stream);
            }
        }

        protected void OnBeforeWriteClass(string code, Stream stream)
        {
            if (BeforeWriteClass != null)
            {
                BeforeWriteClass(code, stream);
            }
        }

        protected void OnBeforeWritePartial(string code, Stream stream)
        {
            if (BeforeWritePartial != null)
            {
                BeforeWritePartial(code, stream);
            }
        }

        protected void OnAfterWritePartial(string code, Stream stream)
        {
            if (AfterWritePartial != null)
            {
                AfterWritePartial(code, stream);
            }
        }

        protected void OnBeforeWriteContext(string code, Stream stream)
        {
            if (BeforeWriteContext != null)
            {
                BeforeWriteContext(code, stream);
            }
        }

        protected void OnAfterWriteContext(string code, Stream stream)
        {
            if (AfterWriteContext != null)
            {
                AfterWriteContext(code, stream);
            }
        }

        protected void OnBeforeContextStreamResolved(SchemaDefinition schema)
        {
            if (BeforeContextStreamResolved != null)
            {
                BeforeContextStreamResolved(this, schema);
            }
        }

        protected void OnAfterContextStreamResolved(SchemaDefinition schema)
        {
            if (AfterContextStreamResolved != null)
            {
                AfterContextStreamResolved(this, schema);
            }
        }

        protected void OnAfterWriteClass(string code, Stream stream)
        {
            if (AfterWriteClass != null)
            {
                AfterWriteClass(code, stream);
            }
        }

        protected void OnBeforeClassStreamResolved(string ns, Table table)
        {
            if (BeforeClassStreamResolved != null)
            {
                BeforeClassStreamResolved(ns, table);
            }
        }

        protected void OnAfterClassStreamResolved(string ns, Table table)
        {
            if (AfterClassStreamResolved != null)
            {
                AfterClassStreamResolved(ns, table);
            }
        }

        protected void OnBeforeContextClassParse(SchemaDefinition schema)
        {
            if (BeforeContextClassParse != null)
            {
                BeforeContextClassParse(this, schema);
            }
        }

        protected void OnAfterContextClassParse(SchemaDefinition schema)
        {
            if (AfterContextClassParse != null)
            {
                AfterContextClassParse(this, schema);
            }
        }

        protected void OnGenerateStarted(SchemaDefinition schema)
        {
            if (GenerateStarted != null)
            {
                GenerateStarted(this, schema);
            }
        }

        protected void OnGenerateComplete(SchemaDefinition schema)
        {
            if (GenerateComplete != null)
            {
                GenerateComplete(this, schema);
            }
        }

        protected void OnCompileStarted(SchemaDefinition schema)
        {
            if (CompileStarted != null)
            {
                CompileStarted(this, schema);
            }
        }

        protected void OnCompileComplete(SchemaDefinition schema)
        {
            if (CompileComplete != null)
            {
                CompileComplete(this, schema);
            }
        }
        #endregion events

		/// <summary>
		/// If the generator compiled generated files, this will be the FileInfo 
		/// representing the compiled assembly
		/// </summary>
		public FileInfo DaoAssemblyFile { get; set; }

        public string Namespace { get; set; }

        public CompilerResults Compile(string directoryPath)
        {
            return Compile(new DirectoryInfo(directoryPath));
        }

        public CompilerResults Compile(DirectoryInfo directory, string assemblyFileName = null)
        {
            assemblyFileName = assemblyFileName ?? directory.Name;
			return Compile(directory, assemblyFileName, DefaultReferenceAssemblies.ToArray());
        }

        public CompilerResults Compile(DirectoryInfo[] directories, string assemblyFileName)
        {
			return Compile(directories, assemblyFileName, DefaultReferenceAssemblies.ToArray());
        }

        public CompilerResults Compile(DirectoryInfo directory, string assemblyFileName, string[] referenceAssemblies = null, bool executable = false)
        {
            return Compile(new DirectoryInfo[] { directory }, assemblyFileName, referenceAssemblies, executable);
        }

        public CompilerResults Compile(DirectoryInfo[] directories, string assemblyFileName, string[] referenceAssemblies = null, bool executable = false)
        {
            if (referenceAssemblies == null)
            {
				referenceAssemblies = DefaultReferenceAssemblies.ToArray();
            }
            return AdHocCSharpCompiler.CompileDirectories(directories, assemblyFileName, referenceAssemblies, executable);        
        }
        
        public CompilerResults Compile(string[] sources, string assemblyFileName, string[] referenceAssemblies = null, bool executable = false)
        {
            if (referenceAssemblies == null)
            {
                referenceAssemblies = new string[0];
            }
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters parameters = AdHocCSharpCompiler.GetCompilerParameters(assemblyFileName, referenceAssemblies, executable);

            return codeProvider.CompileAssemblyFromSource(parameters, sources);
        }
        
        public void Generate(SchemaDefinition schema)
        {
            Generate(schema, ".\\");
        }

        /// <summary>
        /// Generate code for the specified schema
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="root"></param>
        public void Generate(SchemaDefinition schema, string root)
        {
            Generate(schema, null, root, null);
        }

        public void Generate(SchemaDefinition schema, string root, string partialsDir)
        {
            Generate(schema, null, root, partialsDir);
        }

        /// <summary>
        /// Generate code for the specified schema
        /// </summary>
        /// <param name="schema">The schema to generate code for</param>
        /// <param name="targetResolver">If specified, generated code will be 
        /// written to the stream returned by this function</param>
        /// <param name="root">The root file path to use if no target resolver is specified</param>
        public void Generate(SchemaDefinition schema, Func<string, Stream> targetResolver = null, string root = ".\\", string partialsDir = null)
        {
            if (string.IsNullOrEmpty(Namespace))
            {
                throw new NamespaceNotSpecifiedException();
            }

            OnGenerateStarted(schema);            
            
            WriteContext(schema, targetResolver, root);
            
            bool writePartial = !string.IsNullOrEmpty(partialsDir);
            if (writePartial)
            {
                EnsurePartialsDir(partialsDir);
            }

            foreach (Table table in schema.Tables)
            {
                if (writePartial)
                {
                    WritePartial(schema, partialsDir, table);
                }
                WriteClass(schema, targetResolver, root, table);
                WriteQueryClass(schema, targetResolver, root, table);
                WritePagedQueryClass(schema, targetResolver, root, table);
                WriteQiClass(schema, targetResolver, root, table);
                WriteCollection(schema, targetResolver, root, table);
                WriteColumnsClass(schema, targetResolver, root, table);
            }

            OnGenerateComplete(schema);
        }

        private static void EnsurePartialsDir(string partialsDir)
        {
            DirectoryInfo partials = new DirectoryInfo(partialsDir);
            if (!partials.Exists)
            {
                partials.Create();
            }
        }

        private void WriteColumnsClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector)
            {
                GetDefaultAssembliesToReference = GetReferenceAssemblies
            };
            Stream s = null;

            OnBeforeColumnsClassParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("ColumnsClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterColumnsClassParse(Namespace, table);

            OnBeforeColumnsClassStreamResolved(Namespace, table);
            s = GetTargetColumnsClassStream(targetResolver, root, table, s);
            OnAfterColumnsClassStreamResolved(Namespace, table);

            WriteColumnsClassToStream(result, s);
        }

        private void WriteCollection(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforeCollectionParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("Collection.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterCollectionParse(Namespace, table);

            OnBeforeCollectionStreamResolved(Namespace, table);
            s = GetTargetCollectionStream(targetResolver, root, table, s);
            OnAfterCollectionStreamResolved(Namespace, table);

            WriteCollectionToStream(result, s);
        }

        public static Assembly[] GetReferenceAssemblies()
        {
            Assembly[] assembliesToReference = new Assembly[]{typeof(SchemaTemplate).Assembly, 
					typeof(DaoGenerator).Assembly,
					typeof(ServiceProxySystem).Assembly, 
                    typeof(DataTypes).Assembly,
					typeof(Resolver).Assembly};
            return assembliesToReference;
        }

        private void WriteContext(SchemaDefinition schema, Func<string, Stream> targetResolver, string root)
        {
            RazorParser<SchemaTemplate> parser = new RazorParser<SchemaTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforeContextClassParse(schema);
            Type type = this.GetType();            
            string result = parser.ExecuteResource("Context.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = schema, Namespace = Namespace });
            OnAfterContextClassParse(schema);

            OnBeforeContextStreamResolved(schema);
            s = GetTargetContextStream(targetResolver, root, schema, s);
            OnAfterContextStreamResolved(schema);

            WriteContextToStream(result, s);
        }

        private void WriteClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforeClassParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("Class.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterClassParse(Namespace, table);

            OnBeforeClassStreamResolved(Namespace, table);
            s = GetTargetClassStream(targetResolver, root, table, s);
            OnAfterClassStreamResolved(Namespace, table);

            WriteClassToStream(result, s);
        }

        private void WritePartial(SchemaDefinition schema, string partialsDir, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforePartialParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("Partial.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterPartialParse(Namespace, table);

            FileInfo partial = new FileInfo(Path.Combine(partialsDir, "{0}.cs"._Format(table.Name)));
            if (!partial.Exists)
            {
                s = partial.OpenWrite();
                WritePartialToStream(result, s);
            }
        }

        private void WritePagedQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforePagedQueryClassParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("PagedQueryClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterPagedQueryClassParse(Namespace, table);

            OnBeforePagedQueryClassStreamResolved(Namespace, table);
            s = GetTargetPagedQueryClassStream(targetResolver, root, table, s);
            OnAfterPagedQueryClassStreamResolved(Namespace, table);

            WriteQueryClassToStream(result, s);
        }

        private void WriteQueryClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforeQueryClassParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("QueryClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterQueryClassParse(Namespace, table);

            OnBeforeQueryClassStreamResolved(Namespace, table);
            s = GetTargetQueryClassStream(targetResolver, root, table, s);
            OnAfterQueryClassStreamResolved(Namespace, table);

            WriteQueryClassToStream(result, s);
        }

        private void WriteQiClass(SchemaDefinition schema, Func<string, Stream> targetResolver, string root, Table table)
        {
            RazorParser<TableTemplate> parser = new RazorParser<TableTemplate>(RazorResultInspector);
            parser.GetDefaultAssembliesToReference = GetReferenceAssemblies;
            Stream s = null;

            OnBeforeQiClassParse(Namespace, table);
            Type type = this.GetType();
            string result = parser.ExecuteResource("QiClass.tmpl", SchemaTemplateResources.Path, type.Assembly, new { Model = table, Schema = schema, Namespace = Namespace });
            OnAfterQiClassParse(Namespace, table);

            OnBeforeQiClassStreamResolved(Namespace, table);
            s = GetTargetQiClassStream(targetResolver, root, table, s);
            OnAfterQiClassStreamResolved(Namespace, table);

            WriteQiClassToStream(result, s);
        }

        private static Stream GetTargetContextStream(Func<string, Stream> targetResolver, string root, SchemaDefinition schema, Stream s)
        {
            string parameterValue = string.Format("{0}Context", schema.Name);
            return GetTargetStream(targetResolver, root, s, parameterValue);
        }

        private static Stream GetTargetClassStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string parameterValue = table.ClassName;
            return GetTargetStream(targetResolver, root, s, parameterValue);
        }
        private static Stream GetTargetQueryClassStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string paramaterValue = string.Format("{0}Query", table.ClassName);
            return GetTargetStream(targetResolver, root, s, paramaterValue);
        }
        private static Stream GetTargetPagedQueryClassStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string paramaterValue = string.Format("{0}PagedQuery", table.ClassName);
            return GetTargetStream(targetResolver, root, s, paramaterValue);
        }
        private static Stream GetTargetQiClassStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string paramaterValue = string.Format("Qi/{0}", table.ClassName);
            return GetTargetStream(targetResolver, root, s, paramaterValue);
        }
        private static Stream GetTargetCollectionStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string parameterValue = string.Format("{0}Collection", table.ClassName);
            return GetTargetStream(targetResolver, root, s, parameterValue);
        }

        private static Stream GetTargetColumnsClassStream(Func<string, Stream> targetResolver, string root, Table table, Stream s)
        {
            string parameterValue = string.Format("{0}Columns", table.ClassName);
            return GetTargetStream(targetResolver, root, s, parameterValue);
        }

        private static Stream GetTargetStream(Func<string, Stream> targetResolver, string root, Stream s, string parameterValue)
        {
            if (targetResolver != null)
            {
                s = targetResolver(parameterValue);
            }
            else
            {
                string path = Path.Combine(root, string.Format("{0}.cs", parameterValue));
                FileInfo f = new FileInfo(path);
                if (!f.Directory.Exists)
                {
                    f.Directory.Create();
                }
                s = f.OpenWrite();
            }
            return s;
        }
        protected virtual void WriteQueryClassToStream(string code, Stream s)
        {
            OnBeforeWriteQueryClass(code, s);
            WriteToStream(code, s);
            OnAfterWriteQueryClass(code, s);
        }
        protected virtual void WriteQiClassToStream(string code, Stream s)
        {
            OnBeforeWriteQiClass(code, s);
            WriteToStream(code, s);
            OnAfterWriteQiClass(code, s);
        }

        protected virtual void WriteContextToStream(string code, Stream s)
        {
            OnBeforeWriteContext(code, s);
            WriteToStream(code, s);
            OnAfterWriteContext(code, s);
        }

        protected virtual void WritePartialToStream(string code, Stream s)
        {
            OnBeforeWritePartial(code, s);
            WriteToStream(code, s);
            OnAfterWritePartial(code, s);
        }

        protected virtual void WriteClassToStream(string code, Stream s)
        {
            OnBeforeWriteClass(code, s);
            WriteToStream(code, s);
            OnAfterWriteClass(code, s);
        }

        private static void WriteToStream(string text, Stream s)
        {
            using (StreamWriter sw = new StreamWriter(s))
            {
                sw.Write(text);
                sw.Flush();
            }
        }

        protected virtual void WriteCollectionToStream(string code, Stream s)
        {
            OnBeforeWriteCollection(code, s);
            WriteToStream(code, s);
            OnAfterWriteCollection(code, s);
        }

        protected virtual void WriteColumnsClassToStream(string code, Stream s)
        {
            OnBeforeWriteColumnsClass(code, s);
            WriteToStream(code, s);
            OnAfterWriteColumnsClass(code, s);
        }
    }
}

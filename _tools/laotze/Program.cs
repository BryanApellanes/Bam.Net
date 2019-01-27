/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Data.MsSql;
using Bam.Net.Data.Oracle;
using Bam.Net.Data.SQLite;
using Bam.Net.Incubation;
using Bam.Net.CommandLine;
using Bam.Net.Razor;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;

namespace laotze
{
    class Program: CommandLineInterface
    {
        static TargetTableEventDelegate BeforeTableHandler = (ns, t) =>
        {
            OutLineFormat("Writing {0}.{1}", ConsoleColor.Yellow, ns, t.ClassName);
        };

        static TargetTableEventDelegate AfterTableHandler = (ns, t) =>
        {
            OutLineFormat("Done Writing {0}.{1}", ConsoleColor.Green, ns, t.ClassName);
        };

        static void Main(string[] args)
        {
            SetArguments(args);

            if (Arguments.Contains("?"))
            {
                Usage(Assembly.GetExecutingAssembly());
                return;
            }
            else if (Arguments.Contains("examples"))
            {
                Out("For extraction:\r\n");
                Out("LaoTze /f:<file> /conn:<connectionNameFromConfig> /gen:<dirPath> /ns:<defaultNamespace> /dll:<assemblyName> [/v|/s]");
                Out("\r\n or To generate from *.db.js\r\n");
                Out("LaoTze /root:<project_root_to_search_for_database.db.js>\r\n");
                return;
            }

            if (Arguments.Contains("pause"))
            {
                Pause("Press a key to continue...");
            }

            if (Arguments.Contains("root"))
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(Arguments["root"]);
                if (!rootDirectory.Exists)
                {
                    OutLineFormat("Specified root directory does not exist: {0}", ConsoleColor.Red, rootDirectory.FullName);
                    Pause();
                    Environment.Exit(1);
                }

                FileInfo[] dbjs = rootDirectory.GetFiles("*.db.js", SearchOption.AllDirectories);
                if (dbjs.Length > 0)
                {
                    if (dbjs.Length > 1)
                    {
                        OutLine("Multiple *.db.js files found", ConsoleColor.Red);
                        if (!Arguments.Contains("s"))
                        {
                            OutLineFormat("{0}", ConsoleColor.Yellow, dbjs.ToDelimited<FileInfo>(f => f.FullName, "\r\n"));
                            string answer = Prompt("Process each? [y N]", ConsoleColor.Yellow);
                            if (!answer.ToLowerInvariant().Equals("y"))
                            {
                                Exit(1);
                            }
                        }
                        else
                        {
                            OutLineFormat("Processing each: {0}", ConsoleColor.Yellow, dbjs.ToDelimited<FileInfo>(f => f.FullName, "\r\n\t"));
                        }
                    }

                    foreach (FileInfo file in dbjs)
                    {
                        try
                        {
                            OutLineFormat("Processing {0}...", ConsoleColor.Yellow, file.FullName);
							CuidSchemaManager manager = new CuidSchemaManager();                        

                            DirectoryInfo fileParent = file.Directory;
                            DirectoryInfo genToDir = GetTargetDirectory(file);

                            bool keep = Arguments.Contains("keep");

                            DirectoryInfo partialsDir = GetPartialsDir(genToDir);

							SchemaResult result = null;
							if (!Arguments.Contains("dll"))
							{
								bool compile = !keep;
								result = manager.GenerateDao(file, compile, keep, genToDir.FullName, partialsDir.FullName);
							}
							else
							{
								result = manager.GenerateDao(file, new DirectoryInfo(Arguments["dll"]), keep, genToDir.FullName, partialsDir.FullName);
							}

                            if (!result.Success)
                            {
                                throw new Exception(result.Message);
                            }

							if (Arguments.Contains("sql"))
							{
								WriteSqlFile(result);
							}

                            OutLine(result.Message, ConsoleColor.Green);
							if (result.DaoAssembly != null)
							{
								OutLineFormat("Compiled to: {0}", result.DaoAssembly.FullName, ConsoleColor.Yellow);
							}
                        }
                        catch (Exception ex)
                        {
                            OutLineFormat("{0}\r\n\r\n***\r\n{1}", ConsoleColor.Red, ex.Message, ex.StackTrace ?? "");
                            Pause("Press enter to exit\r\n");
                            Exit(1);
                        }
                    }

                    Pause("Press enter to exit...\r\n");
                }
                else
                {
                    OutLine("No *.db.js files were found", ConsoleColor.Yellow);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Arguments["conn"]))
                {
                    OutLine("Please specify a connection name from the config or a directory to search", ConsoleColor.Yellow);
                    Pause();
                }
                else
                {
                    Extract();
                }
            }
        }

        private static void SetArguments(string[] args)
        {
            AddValidArgument("f", false, description: "The output schema file name");
            AddValidArgument("conn", false, description: "The name of the connection in the config file to use");
            AddValidArgument("gen", false, description: "The directory to write generated files to");
            AddValidArgument("ns", false, description: "The namespace to place generated code into");
            AddValidArgument("dll", false, description: "When generating from an existing database, if specified the code will be compiled to the dll specified");
            AddValidArgument("examples", true, description: "Original example usage output");
            AddValidArgument("p", false, description: "Partial folder for custom code");
            AddValidArgument("pause", true, description: "Prompt for a keypress before processing");
            AddValidArgument("v", true, description: "Enable verbose mode, outputs generated code to the console");
            AddValidArgument("s", true, description: "Enable silent mode, limited output");
            AddValidArgument("root", false, description: "Specifies the root directory to search when generating from *.db.js files");
            AddValidArgument("keep", true, description: "If not specified when generating from a *.db.js file the code will be compiled to the dll specified by /dll and the source will be deleted");
			AddValidArgument("sql", false, description: "The name of the sql txt file to output the schema creation script to");
			AddValidArgument("dialect", false, description: "The sql dialect to use, one of: SQLite, Ms or Oracle");
			AddValidArgument("?", true, description: "Usage");

            ParseArgs(args);
        }

		private static void WriteSqlFile(SchemaResult result)
		{
			if (result.DaoAssembly == null)
			{
				OutLine("Unable to locate Dao assembly for sql schema generation, specify dll argument", ConsoleColor.Red);
			}
			else
			{
				FileInfo sqlFile = new FileInfo(Arguments["sql"]);
				SqlDialect dialect = SqlDialect.MsSql;
				if (Arguments.Contains("dialect"))
				{
					dialect = (SqlDialect)Enum.Parse(typeof(SqlDialect), Arguments["dialect"]);
				}
				WriteSqlFile(result.DaoAssembly, sqlFile, dialect);
				OutLineFormat("Sql script written: {0}", sqlFile.FullName);
			}
		}

		private static void WriteSqlFile(FileInfo daoFile, FileInfo sqlFile, SqlDialect dialect)
		{
			Assembly daoAssembly = Assembly.LoadFrom(daoFile.FullName);
			SchemaWriter schemaWriter = SchemaWriters[dialect]();
			schemaWriter.WriteSchemaScript(daoAssembly);
			schemaWriter.ToString().SafeWriteToFile(sqlFile.FullName);
		}

		static Dictionary<SqlDialect, Func<SchemaWriter>> _schemaWriters;
		static object _schemaWriterLock = new object();
		private static Dictionary<SqlDialect, Func<SchemaWriter>> SchemaWriters
		{
			get
			{
				return _schemaWriterLock.DoubleCheckLock(ref _schemaWriters, () =>
				{
                    Dictionary<SqlDialect, Func<SchemaWriter>> result = new Dictionary<SqlDialect, Func<SchemaWriter>>
                    {
                        { SqlDialect.Invalid, () => new MsSqlSqlStringBuilder() },
                        { SqlDialect.Ms, () => new MsSqlSqlStringBuilder() },
                        { SqlDialect.MsSql, () => new MsSqlSqlStringBuilder() },
                        { SqlDialect.My, () => new MySqlSqlStringBuilder() },
                        { SqlDialect.MySql, () => new MySqlSqlStringBuilder() },
                        { SqlDialect.Oracle, () => new OracleSqlStringBuilder() },
                        { SqlDialect.SQLite, () => new SQLiteSqlStringBuilder() },
                        { SqlDialect.Postgres, ()=> new NpgsqlSqlStringBuilder() }
                    };
                    return result;
				});
			}
		}

        private static DirectoryInfo GetTargetDirectory(FileInfo file)
        {
            string genTo = Arguments.Contains("gen") ? Arguments["gen"] : Path.Combine(file.Directory.FullName, "{0}_Generated"._Format(file.Name.Truncate(6)));
            if (Directory.Exists(genTo))
            {
                Directory.Move(genTo, Path.Combine(genTo, "{0}_{1}"._Format(genTo, DateTime.Now.ToJulianDate().ToString())));
            }
            DirectoryInfo genToDir = new DirectoryInfo(genTo);
            return genToDir;
        }

        private static DirectoryInfo GetPartialsDir(DirectoryInfo genToDir)
        {
            string partialsDir = Arguments["p"] ?? "*";
            if (partialsDir.Equals("*"))
            {
                DirectoryInfo genToParent = genToDir.Parent;
                DirectoryInfo partials = new DirectoryInfo(Path.Combine(genToParent.FullName, "Partials"));
                partialsDir = partials.FullName;
            }
            return new DirectoryInfo(partialsDir);
        }

        private static void Extract()
        {
            Action<string> inspector = (s) => { };
            if (Arguments.Contains("v"))
            {
                inspector = (s) => { Out(s, ConsoleColor.Cyan); };
            }

            string connectionName = "Default";
            string filePath = "Schema.json";
            bool gen = !string.IsNullOrEmpty(Arguments["gen"]);
            bool compile = !string.IsNullOrEmpty(Arguments["dll"]);
            bool silent = Arguments.Contains("s");

            if (!string.IsNullOrEmpty(Arguments["f"]))
            {
                filePath = Arguments["f"];
            }

            if (!string.IsNullOrEmpty(Arguments["conn"]))
            {
                connectionName = Arguments["conn"];
            }

            OutLineFormat("Extracting schema using the connection ({0})", connectionName);
            SchemaDefinition schema = ExtractSchema(connectionName, filePath);
            OutLine("Extraction complete...");

            if (gen)
            {
                RazorParser<RazorBaseTemplate>.DefaultRazorInspector = inspector;
                OutLineFormat("Generating csharp for ({0})", schema.File);
                Generate(schema, inspector, silent);
                OutLine("Generation complete...");
                if (compile)
                {
                    DirectoryInfo dir = new DirectoryInfo(Arguments["gen"]);
                    List<DirectoryInfo> dirs = new List<DirectoryInfo>();
                    dirs.Add(dir);
                    if (!string.IsNullOrEmpty(Arguments["p"]))
                    {
                        dirs.Add(GetPartialsDir(dir));
                    }

                    FileInfo file = new FileInfo(Arguments["dll"]);

                    OutLineFormat("Compiling sources in ({0})", dir.FullName);
                    Compile(dirs.ToArray(), file);
                    OutLineFormat("Compilation complete...");
                }
            }
        }

        private static void Compile(DirectoryInfo[] dirs, FileInfo file)
        {
            DaoGenerator generator = new DaoGenerator(GetNamespace());
            CompilerResults results = generator.Compile(dirs, file.FullName);
            OutputCompilerErrors(results);
        }

        private static void OutputCompilerErrors(CompilerResults results)
        {
            foreach (CompilerError error in results.Errors)
            {
                OutLineFormat("File=>{0}", ConsoleColor.Yellow, error.FileName);
                OutLineFormat("Line {0}, Column {1}::{2}", error.Line, error.Column, error.ErrorText);
                Out();
            }
        }

        private static void Generate(SchemaDefinition schema, Action<string> resultInspector = null, bool silent = false)
        {
            DirectoryInfo dir = new DirectoryInfo(Arguments["gen"]);
            if (!dir.Exists)
            {
                dir.Create();
            }

            string ns = GetNamespace();
            if (resultInspector == null)
            {
                resultInspector = (s) => { };
            }
            DaoGenerator generator = new DaoGenerator(ns, resultInspector);
            if (!silent)
            {
                generator.BeforeClassParse += BeforeTableHandler;
                generator.AfterClassParse += AfterTableHandler;
            }
            generator.Generate(schema, dir.FullName);
        }

        private static string GetNamespace()
        {
            string ns = Arguments["ns"];
            if (string.IsNullOrEmpty(ns))
            {
                ns = "Dao";
            }
            return ns;
        }

        private static SchemaDefinition ExtractSchema(string connectionName, string filePath)
        {
            ISchemaExtractor extractor = Incubator.Default.Get<ISchemaExtractor>(new MsSqlSmoSchemaExtractor(connectionName));
            SchemaDefinition schema = extractor.Extract();
            schema.Save(filePath);
            return schema;
        }
    }
}

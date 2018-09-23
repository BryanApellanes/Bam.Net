using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using System.CodeDom.Compiler;
using Google.Protobuf;

namespace Bam.Net.CoreServices.ProtoBuf
{
    /// <summary>
    /// A class used to generate source and/or an assembly
    /// containing CSharp protocol buffer classes
    /// </summary>
    public class ProtocolBuffersAssemblyGenerator: IAssemblyGenerator
    {
        public ProtocolBuffersAssemblyGenerator()
            : this("Bam.Net.Generated.ProtocolBuffers")
        { }

        public ProtocolBuffersAssemblyGenerator(string assemblyName, params Type[] types)
            :this(new ProtoFileGenerator())
        {
            AssemblyName = assemblyName;
            if(types.Length > 0)
            {
                AddTypes(types);
            }
        }

        public ProtocolBuffersAssemblyGenerator(ProtoFileGenerator protoFileGenerator, Func<PropertyInfo, bool> propertyFilter = null)
        {
            CompilerPath = ".\\protoc.exe";
            ProtoFileGenerator = protoFileGenerator;
            CsFileDirectory = ".\\Generated_Protobuf_Cs";
            if(propertyFilter != null)
            {
                PropertyFilter = propertyFilter;
            }
            Types = new HashSet<Type>();
        }

        protected ProtoFileGenerator ProtoFileGenerator { get; set; }
        public string CompilerPath { get; set; }
        public string CsFileDirectory { get; set; }
        public string AssemblyName { get; set; }
        public HashSet<Type> Types { get; set; }
        public void AddTypes(IEnumerable<Type> types)
        {
            types.Each(t => Types.Add(t));
        }
        public string ProtoFileDirectory
        {
            get
            {
                return ProtoFileGenerator.OutputDirectory;
            }
            set
            {
                ProtoFileGenerator.OutputDirectory = value;
            }
        }
        public Func<PropertyInfo, bool> PropertyFilter
        {
            get
            {
                return ProtoFileGenerator.PropertyFilter;
            }
            set
            {
                ProtoFileGenerator.PropertyFilter = value;
            }
        }        
        public Assembly GetAssembly()
        {
            return GeneratedAssemblyInfo.GetGeneratedAssembly(AssemblyName, this);
        }
        public DirectoryInfo GenerateCsFiles()
        {
            return GenerateCsFiles(Types.ToArray());
        }
        public DirectoryInfo GenerateCsFiles(params Type[] types)
        {
            return GenerateCsFiles(types, (o, a) => { });
        }
        public DirectoryInfo GenerateCsFiles(IRepository repo)
        {
            return GenerateCsFiles(repo.StorableTypes);
        }
        public DirectoryInfo GenerateCsFiles(IEnumerable<Type> clrTypes, EventHandler onComplete)
        {
            return GenerateCsFiles(clrTypes, CsFileDirectory, onComplete);
        }
        public DirectoryInfo GenerateCsFiles(IEnumerable<Type> clrTypes, string csFileDirectory = null, EventHandler onComplete = null)
        {
            CsFileDirectory = csFileDirectory ?? CsFileDirectory;
            HashSet<Type> types = new HashSet<Type>();
            if (clrTypes != null)
            {
                clrTypes.Each(t => types.Add(t));
            }
            Types = types;
            DirectoryInfo dir = GenerateCsFiles(onComplete);
            return dir;
        }

        public DirectoryInfo GenerateCsFiles(EventHandler onComplete = null)
        {
            string protoFilePath = ProtoFileGenerator.GenerateProtoFile(Types);
            DirectoryInfo dir = new DirectoryInfo(CsFileDirectory);
            if (Directory.Exists(dir.FullName))
            {
                Directory.Move(dir.FullName, dir.FullName.GetNextDirectoryName());
            }
            dir.Create();
            FileInfo compiler = new FileInfo(CompilerPath);
            string command = $"{compiler.FullName} -I={compiler.Directory.FullName} --csharp_out={dir.FullName} {protoFilePath}";
            ProcessOutput output = null;
            AutoResetEvent wait = new AutoResetEvent(false);
            output = command.Run((o, a) =>
            {
                onComplete?.Invoke(o, a);
                wait.Set();
            });
            wait.WaitOne();
            return dir;
        }

        public GeneratedAssemblyInfo GenerateAssembly()
        {
            Args.ThrowIfNullOrEmpty(AssemblyName, nameof(AssemblyName));

            GeneratedAssemblyInfo result = null;
            AutoResetEvent wait = new AutoResetEvent(false);
            GenerateCsFiles(Types, (o, a) =>
            {
                CompilerResults compilerResults = AdHocCSharpCompiler.CompileDirectory(new DirectoryInfo(CsFileDirectory), AssemblyName, new Assembly[] { typeof(IMessage).Assembly });
                result = new GeneratedAssemblyInfo(AssemblyName, compilerResults);
                wait.Set();
            });
            wait.WaitOne();
            return result;
        }

        public void WriteSource(string writeSourceDir)
        {
            CsFileDirectory = writeSourceDir;
            GenerateCsFiles();
        }
    }
}

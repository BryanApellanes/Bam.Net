/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Bam.Net.Presentation;

namespace Bam.Net.Server.Renderers
{
    /// <summary>
    /// Represents the compiled result of a dust tempalte.  Dust.js will 
    /// compile dust templates to javascript.  The compilation process
    /// can take time depending on the size of the template.  This class
    /// saves the result in a json file along with a hash of the original
    /// source to determine if compilation is necessary.
    /// </summary>
    public class CompiledDustTemplate : ICompiledTemplate
    {
        public static implicit operator string(CompiledDustTemplate result)
        {
            return result.Compiled;
        }

        public CompiledDustTemplate() { }

        public CompiledDustTemplate(string sourceFilePath, string templateName)
        {
            Name = templateName;
            SourceFilePath = sourceFilePath;
            Source = sourceFilePath.SafeReadFile();
            Load();
        }

        public CompiledDustTemplate(FileInfo file) : this(file.FullName, Path.GetFileNameWithoutExtension(file.Name))
        {
        }

        private void Load()
        {
            // find our file 
            CompiledDustTemplate temp = null;
            FileInfo saveTo = new FileInfo(GetSaveToPath());
            if (saveTo.Exists)
            {
                // load it
                temp = saveTo.FromJsonFile<CompiledDustTemplate>();
            }

            // compare the hashes
            if (temp != null && !string.IsNullOrEmpty(temp.Source) && temp.SourceHash.Equals(this.SourceHash) && temp.Name.Equals(this.Name))
            {
                // if they match set our compiled to that of temp
                this.Compiled = temp.Compiled;
            }
            else
            {
                Compile();
            }
        }

        private void Compile()
        {
            Args.ThrowIfNullOrEmpty(SourceFilePath, "SourceFilePath");           
            string source = File.ReadAllText(SourceFilePath);
            this.Compiled = DustScript.Compile(source, this.Name);
            this.ToJsonFile(GetSaveToPath());
        }
        
        private string GetSaveToPath()
        {
            Args.ThrowIfNullOrEmpty(SourceFilePath, "SourceFilePath");
            FileInfo sourceFile = new FileInfo(SourceFilePath);
            DirectoryInfo parent = sourceFile.Directory;
            return Path.Combine(parent.FullName, Path.GetFileNameWithoutExtension(sourceFile.Name) + ".dust.json");            
        }       
        
        public string SourceFilePath
        {
            get;
            set;
        }
        
        public string Name
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public string SourceHash
        {
            get
            {
                return Source.Sha1();
            }
        }

        public string UnescapedCompiled
        {
            get
            {
                return Regex.Unescape(Compiled);
            }
        }

        public string Compiled
        {
            get;
            set;
        }
    }
}

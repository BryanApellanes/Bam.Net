/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Server.Renderers
{
    /// <summary>
    /// Represents the compiled result of a dust tempalte.  Dust.js will 
    /// compile dust templates to javascript.  The compilation process
    /// can take time depending on the size of the template.  This class
    /// saves the result in a json file along with a hash of the original
    /// source to determine if compilation is necessary.
    /// </summary>
    public class CompiledDustTemplate
    {
        public static implicit operator string(CompiledDustTemplate result)
        {
            return result.CompiledTemplate;
        }

        public CompiledDustTemplate() { }

        public CompiledDustTemplate(string sourceFilePath, string templateName)
        {
            this.TemplateName = templateName;
            this.SourceFilePath = sourceFilePath;
            this.SourceHash = new FileInfo(sourceFilePath).Sha1();
            this.Load();
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
            if (temp != null && temp.SourceHash.Equals(this.SourceHash) && temp.TemplateName.Equals(this.TemplateName))
            {
                // if they match set our compiled to that of temp
                this.CompiledTemplate = temp.CompiledTemplate;
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
            this.CompiledTemplate = DustScript.Compile(source, this.TemplateName);
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
        
        public string TemplateName
        {
            get;
            set;
        }

        public string SourceHash
        {
            get;
            set;
        }

        public string CompiledTemplate
        {
            get;
            set;
        }
    }
}

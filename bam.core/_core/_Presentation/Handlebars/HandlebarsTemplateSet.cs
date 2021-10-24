using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Schema;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsTemplateSet
    {
        public const string DefaultPath = "./Handlebars";
        private readonly HandlebarsTemplateRenderer _renderer;
        public HandlebarsTemplateSet()
        {
            HandlebarsDirectories = new HashSet<HandlebarsDirectory>();
            HandlebarsEmbeddedResources = new HandlebarsEmbeddedResources(Assembly.GetExecutingAssembly());
            _renderer = new HandlebarsTemplateRenderer(HandlebarsEmbeddedResources, HandlebarsDirectories.ToArray());
        }

        public HandlebarsTemplateSet(string directoryPath)
        {
            HandlebarsDirectories = new HashSet<HandlebarsDirectory>();
            HandlebarsDirectories.Add(new HandlebarsDirectory(directoryPath));
            HandlebarsEmbeddedResources = new HandlebarsEmbeddedResources(Assembly.GetExecutingAssembly());
            _renderer = new HandlebarsTemplateRenderer(HandlebarsEmbeddedResources, HandlebarsDirectories.ToArray());
        }

        public HandlebarsTemplateSet(Assembly embeddedResourceContainer)
        {
            HandlebarsDirectories = new HashSet<HandlebarsDirectory>();
            HandlebarsEmbeddedResources = new HandlebarsEmbeddedResources(embeddedResourceContainer);
            _renderer = new HandlebarsTemplateRenderer(HandlebarsEmbeddedResources, HandlebarsDirectories.ToArray());
        }
        
        public HashSet<HandlebarsDirectory> HandlebarsDirectories { get; set; }
        public HandlebarsEmbeddedResources HandlebarsEmbeddedResources { get; set; }

        public string Render(string templateName, object modelData)
        {
            return ToRenderer().Render(templateName, modelData);
        }

        public ITemplateRenderer ToRenderer()
        {
            return new HandlebarsTemplateRenderer(HandlebarsEmbeddedResources, HandlebarsDirectories.ToArray());
        }
    }
}
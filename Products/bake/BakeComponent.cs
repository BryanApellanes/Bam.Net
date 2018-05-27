using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Bam.Net.Application
{
    public class BakeComponent
    {
        public BakeComponent(string sourceDirRelativeFilePath)
        {
            string stringValue = 32.RandomString().ToUpperInvariant();
            Id = "owc" + stringValue;
            Guid = System.Guid.NewGuid().ToString();
            Win64 = "yes";
            File = new BakeFile { Id = "owf" + stringValue, Source = "$(var.SourceDir)\\" + sourceDirRelativeFilePath, KeyPath = "yes" };
        }
        public string Id { get; set; }
        public string Guid { get; set; }

        string _win64;
        public string Win64
        {
            get { return _win64.IsAffirmative() ? "yes" : "no"; }
            set { _win64 = value; }
        }

        public BakeFile File { get; set; }

        public override string ToString()
        {
            return ToXml();
        }
        
        public string ToXml()
        {
            return ToXElement().ToString();
        }

        public XElement ToXElement(XNamespace ns = null)
        {
            XElement component = ns == null ? new XElement("Component") : new XElement(ns + "Component");
            component.SetAttributeValue("Id", Id);
            component.SetAttributeValue("Guid", Guid);
            component.SetAttributeValue("Win64", Win64);
            XElement file = ns == null ? new XElement("File") : new XElement(ns + "File");
            file.SetAttributeValue("Id", File.Id);
            file.SetAttributeValue("Source", File.Source);
            file.SetAttributeValue("KeyPath", File.KeyPath);
            component.Add(file);
            return component;
        }
    }
}

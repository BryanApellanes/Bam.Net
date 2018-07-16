using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Bam.Net.Application
{
    public class WixDirectory
    {
        public WixDirectory()
        {
            string stringValue = 32.RandomString().ToUpperInvariant();
            Id = "owc" + stringValue;
            _components = new List<WixComponent>();
            _directories = new List<WixDirectory>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        List<WixComponent> _components;
        public WixComponent[] Components
        {
            get { return _components.ToArray(); }
            set { _components = value.ToList(); }
        }

        List<WixDirectory> _directories;
        public WixDirectory[] Directories
        {
            get { return _directories.ToArray(); }
            set { _directories.ToList(); }
        }

        public void AddComponent(string sourceDirRelativeFilePath)
        {
            _components.Add(new WixComponent(sourceDirRelativeFilePath));
        }

        public void AddComponent(WixComponent component)
        {
            _components.Add(component);
        }

        public void AddDirectory(WixDirectory directory)
        {
            _directories.Add(directory);
        }
        
        public string ToXml()
        {
            return ToXElement().ToString();
        }

        public XElement ToXElement (XNamespace ns = null)
        {
            XElement directory = ns == null ? new XElement("Directory") : new XElement(ns + "Directory");
            directory.SetAttributeValue("Id", Id);
            if (!string.IsNullOrEmpty(Name))
            {
                directory.SetAttributeValue("Name", Name);
            }
            foreach(WixComponent component in Components)
            {
                directory.Add(component.ToXElement(ns));
            }
            foreach(WixDirectory subDir in Directories)
            {
                directory.Add(subDir.ToXElement(ns));
            }
            return directory;
        }

        public static WixDirectory FromPath(string id, string directoryPath)
        {
            WixDirectory result = FromPath(directoryPath);
            result.Id = id;
            return result;
        }

        public static WixDirectory FromPath(string directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            return Traverse(dir, directoryPath);
        }

        private static WixDirectory Traverse(DirectoryInfo dir, string root = null)
        {
            root = root ?? dir.FullName;
            if (!root.EndsWith("\\"))
            {
                root += "\\";
            }
            WixDirectory current = new WixDirectory { Name = dir.Name };
            foreach (FileInfo file in dir.GetFiles())
            {
                current.AddComponent(file.FullName.Replace(root, ""));
            }
            foreach(DirectoryInfo subDir in dir.GetDirectories())
            {
                current.AddDirectory(Traverse(subDir, root));
            }
            return current;
        }
    }
}

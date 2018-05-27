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
    public class BakeDirectory
    {
        public BakeDirectory()
        {
            string stringValue = 32.RandomString().ToUpperInvariant();
            Id = "owc" + stringValue;
            _components = new List<BakeComponent>();
            _directories = new List<BakeDirectory>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        List<BakeComponent> _components;
        public BakeComponent[] Components
        {
            get { return _components.ToArray(); }
            set { _components = value.ToList(); }
        }

        List<BakeDirectory> _directories;
        public BakeDirectory[] Directories
        {
            get { return _directories.ToArray(); }
            set { _directories.ToList(); }
        }

        public void AddComponent(string sourceDirRelativeFilePath)
        {
            _components.Add(new BakeComponent(sourceDirRelativeFilePath));
        }

        public void AddComponent(BakeComponent component)
        {
            _components.Add(component);
        }

        public void AddDirectory(BakeDirectory directory)
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
            foreach(BakeComponent component in Components)
            {
                directory.Add(component.ToXElement(ns));
            }
            foreach(BakeDirectory subDir in Directories)
            {
                directory.Add(subDir.ToXElement(ns));
            }
            return directory;
        }

        public static BakeDirectory FromPath(string id, string directoryPath)
        {
            BakeDirectory result = FromPath(directoryPath);
            result.Id = id;
            return result;
        }

        public static BakeDirectory FromPath(string directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            return Traverse(dir, directoryPath);
        }

        private static BakeDirectory Traverse(DirectoryInfo dir, string root = null)
        {
            root = root ?? dir.FullName;
            if (!root.EndsWith("\\"))
            {
                root += "\\";
            }
            BakeDirectory current = new BakeDirectory { Name = dir.Name };
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

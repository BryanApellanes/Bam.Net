/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Configuration;
using IO = System.IO;

namespace Bam.Net.Automation.Nuget
{
    public class NuspecFile
    {
        public NuspecFile(string path)
        {
            this.Path = path;
        }

        public NuspecFile()
        {
            this.Path = ".\\nuspecfile.nuspec";

        }

        string _path;
        public string Path
        {
            get
            {
                if (!IO.File.Exists(_path) && _package == null)
                {
                    InitializePackage();
                }

                return _path;
            }
            set
            {
                _path = value;
                if (!IO.File.Exists(_path) && _package == null)
                {
                    InitializePackage();
                }
                else
                {
                    LoadPackage();
                }
            }
        }

        private void LoadPackage()
        {
            _package = _path.FromXmlFile<package>();
            Version = new PackageVersion(_package.metadata);
        }

        private void InitializePackage()
        {
            _package = new package();
            packageMetadata meta = new packageMetadata();
            _package.metadata = meta;
            _package.metadata.dependencies = new packageMetadataDependency[] { };

            Version = new PackageVersion("1.0.0");
            Version.MetaData = meta;

            Title = "Package Title";
            Id = "PackageId";
            Authors = "Package Authors";
            Owners = "Package Owners";
            RequireLicenseAcceptance = false;
            Description = "Package Description";
            ReleaseNotes = "Package Release Notes";
            Copyright = "Copyright {0}"._Format(DateTime.Now.Year);
        }

        public void Save()
        {
            WriteTo(Path);
        }

        public void WriteTo(string path)
        {
            if (_package.files == null)
            {
                conventionbasedpackage cpackage = new conventionbasedpackage();
                cpackage.CopyProperties(_package);
                cpackage.ToXmlFile(path);
            }
            else
            {
                _package.ToXmlFile(path);
            }
        }

        package _package;
        object _packageLock = new object();
        public package Package
        {
            get
            {
                return _packageLock.DoubleCheckLock(ref _package, () =>
                {
                    return Path.FromXmlFile<package>();
                });
            }
        }

        packageMetadata _metaData;
        object _metaDataLock = new object();
        public packageMetadata MetaData
        {
            get
            {
                return _metaDataLock.DoubleCheckLock(ref _metaData, () =>
                {
                    return Package.metadata;
                });
            }
        }

        PackageVersion _version;
        object _versionLock = new object();
        public PackageVersion Version
        {
            get
            {
                return _versionLock.DoubleCheckLock(ref _version, () => new PackageVersion(MetaData));
            }
            private set
            {
                _version = value;
                _version.MetaData = MetaData;
            }
        }

        public string Title
        {
            get
            {
                return MetaData.title;
            }
            set
            {
                MetaData.title = value;
            }
        }

        public string Id
        {
            get
            {
                return MetaData.id;
            }
            set
            {
                MetaData.id = value.PascalCase();
            }
        }

        public string Authors
        {
            get
            {
                return MetaData.authors;
            }
            set
            {
                MetaData.authors = value;
            }
        }

        public string Owners
        {
            get
            {
                return MetaData.owners;
            }
            set
            {
                MetaData.owners = value;
            }
        }
        public string LicenseUrl
        {
            get
            {
                return MetaData.licenseUrl;
            }
            set
            {
                MetaData.licenseUrl = value;
            }
        }
        public string ProjectUrl
        {
            get
            {
                return MetaData.projectUrl;
            }
            set
            {
                MetaData.projectUrl = value;
            }
        }
        public bool RequireLicenseAcceptance
        {
            get
            {
                return MetaData.requireLicenseAcceptance;
            }
            set
            {
                MetaData.requireLicenseAcceptance = value;
            }
        }

        public string Description
        {
            get
            {
                return MetaData.description;
            }
            set
            {
                MetaData.description = value;
            }
        }

        public string ReleaseNotes
        {
            get
            {
                return MetaData.releaseNotes;
            }
            set
            {
                MetaData.releaseNotes = value;
            }
        }

        public string Copyright
        {
            get
            {
                return MetaData.copyright;
            }
            set
            {
                MetaData.copyright = value;
            }
        }

        public NugetPackageIdentifier[] Dependencies
        {
            get
            {
                return MetaData.dependencies.Select(d => new NugetPackageIdentifier(d.id, d.version)).ToArray();
            }
            set
            {
                MetaData.dependencies = value.Select(npi => new packageMetadataDependency { id = npi.Id, version = npi.Version }).ToArray();
            }
        }

        public void AddDependency(string id, string version)
        {
            NugetPackageIdentifier[] current = Dependencies;
            NugetPackageIdentifier[] next = new NugetPackageIdentifier[current.Length + 1];
            current.CopyTo(next, 0);
            next[next.Length - 1] = new NugetPackageIdentifier(id, version);
            Dependencies = next;
        }
    }
}

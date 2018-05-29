/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Automation.MSBuild;
using Bam.Net.Automation.SourceControl;
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

            Version = new PackageVersion("1.0.0")
            {
                MetaData = meta
            };

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

        /// <summary>
        /// Updates the release notes by reading git commits that are prefixed with the name (Id) of the
        /// nuget package.
        /// </summary>
        /// <param name="gitRepoPath">The git repo path.</param>
        public void UpdateReleaseNotes(string gitRepoPath)
        {
            GitReleaseNotes releaseNotes = GitReleaseNotes.SinceLatestRelease(Id, gitRepoPath, out string latestRelease);
            if (releaseNotes.CommitCount > 0)
            {
                releaseNotes.Summary = $"Version {Version.Value}\r\nUpdates since {latestRelease}:";
            }
            else
            {
                releaseNotes.Summary = $"Version {Version.Value}";
            }
            ReleaseNotes = releaseNotes.Value;
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
            set
            {
                _version = value;
                MetaData.version = value.Value;
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
                if(MetaData != null &&
                    MetaData.dependencies != null)
                {
                    return MetaData.dependencies.Select(d => new NugetPackageIdentifier(d.id, d.version)).ToArray();
                }
                return new NugetPackageIdentifier[] { };
            }
            set
            {
                MetaData.dependencies = new HashSet<NugetPackageIdentifier>(value).Select(npi => new packageMetadataDependency { id = npi.Id, version = npi.Version }).ToArray();
            }
        }

        public void UpdateProjectDependencyVersions(string version, Predicate<string> whereProjectName)
        {
            FileInfo projectFile = GetProjectFile();
            if (projectFile.Exists)
            {
                UpdateProjectDependencyVersions(projectFile, version, whereProjectName);
            }
        }

        public void UpdateProjectDependencyVersions(FileInfo projectFile, string version, Predicate<string> whereProjectName)
        {
            foreach(ProjectReference projectReference in projectFile.GetProjectReferences())
            {
                if (whereProjectName(projectReference.Name))
                {
                    UpdateDependencyVersion(projectReference.Name, version);
                }
            }
        }

        /// <summary>
        /// Adds the project dependencies.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <param name="whereProjectName">Name of the where project.</param>
        public void AddProjectDependencies(string version, Predicate<string> whereProjectName)
        {
            FileInfo projectFile = GetProjectFile();
            if (projectFile.Exists)
            {
                AddProjectDependencies(projectFile, version, whereProjectName);
            }
        }

        /// <summary>
        /// Adds the project dependencies.
        /// </summary>
        /// <param name="projectFile">The project file.</param>
        /// <param name="version">The version.</param>
        /// <param name="whereProjectName">Name of the where project.</param>
        public void AddProjectDependencies(FileInfo projectFile, string version, Predicate<string> whereProjectName)
        {
            foreach (ProjectReference projectReference in projectFile.GetProjectReferences())
            {
                if (whereProjectName(projectReference.Name))
                {
                    AddDependency(projectReference.Name, version);
                }
            }
        }

        /// <summary>
        /// Adds package dependencies from the package.config file found next to the nuspec file if it exists.
        /// </summary>
        public void AddPackageDependencies()
        {
            FileInfo nuspecFile = new FileInfo(Path);
            FileInfo packageConfig = new FileInfo(IO.Path.Combine(nuspecFile.Directory.FullName, "package.config"));
            if (packageConfig.Exists)
            {
                AddPackageDependencies(packageConfig);
            }
        }

        /// <summary>
        /// Adds package dependencies from the specified package.config file.
        /// </summary>
        /// <param name="packageConfigPath">The package configuration path.</param>
        public void AddPackageDependencies(string packageConfigPath)
        {
            AddPackageDependencies(new FileInfo(packageConfigPath));
        }

        /// <summary>
        /// Adds package dependencies from the specified package.config file.
        /// </summary>
        /// <param name="packageConfig">The package configuration.</param>
        public void AddPackageDependencies(FileInfo packageConfig)
        {
            packages package = packageConfig.FromXmlFile<packages>();
            if (package.Items != null)
            {
                foreach (packagesPackage item in package.Items)
                {
                    AddDependency(item.id, item.version);
                }
            }
        }

        public void SetPackageDependencies(FileInfo packageConfig)
        {
            packages package = packageConfig.FromXmlFile<packages>();
            List<NugetPackageIdentifier> dependencies = new List<NugetPackageIdentifier>();
            if(package.Items != null)
            {
                foreach(packagesPackage item in package.Items)
                {
                    dependencies.Add(new NugetPackageIdentifier(item.id, item.version));
                }
            }
            Dependencies = dependencies.ToArray();
        }

        public void RemoveDependency(string id)
        {
            Dependencies = Dependencies.Where(npi => !npi.Id.Equals(id)).ToArray();
        }

        /// <summary>
        /// Adds a dependency. Will not add the dependency if the specified id
        /// is already a dependency regardless of version, use UpdateDependencyVersion 
        /// in that case.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public void AddDependency(string id, string version)
        {
            NugetPackageIdentifier[] current = Dependencies.Where(npi=> !npi.Id.Equals(id)).ToArray();
            NugetPackageIdentifier[] next = new NugetPackageIdentifier[current.Length + 1];
            current.CopyTo(next, 0);
            next[next.Length - 1] = new NugetPackageIdentifier(id, version);
            Dependencies = next;
        }

        /// <summary>
        /// Updates the dependency version.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        public void UpdateDependencyVersion(string id, string version)
        {
            HashSet<NugetPackageIdentifier> current = new HashSet<NugetPackageIdentifier>(Dependencies);
            HashSet<NugetPackageIdentifier> updated = new HashSet<NugetPackageIdentifier>();
            foreach(NugetPackageIdentifier identifier in current)
            {
                if (identifier.Id.Equals(id))
                {
                    updated.Add(new NugetPackageIdentifier { Id = id, Version = version });
                }
                else
                {
                    updated.Add(identifier);
                }
            }
            Dependencies = updated.ToArray();
        }

        /// <summary>
        /// Gets a nuspec file for the specified file.  The nuspec file
        /// path is a sibling to the specified fileInfo and the name is set
        /// to the name of the specified fileInfo with the extension replaced
        /// with nuspec.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns></returns>
        public static NuspecFile ForFile(FileInfo fileInfo)
        {
            string nuspecName = IO.Path.GetFileNameWithoutExtension(fileInfo.Name);
            return new NuspecFile(IO.Path.Combine(fileInfo.Directory.Name, $"{nuspecName}.nuspec"));
        }

        private FileInfo GetProjectFile()
        {
            FileInfo nuspecFile = new FileInfo(Path);
            string projectName = IO.Path.GetFileNameWithoutExtension(nuspecFile.Name);
            string projectFilePath = IO.Path.Combine(nuspecFile.Directory.FullName, $"{projectName}.csproj");
            FileInfo projectFile = new FileInfo(projectFilePath);
            return projectFile;
        }
    }
}

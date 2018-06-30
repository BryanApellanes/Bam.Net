# Bake

Bake.exe is a tool used to build the BamToolkit and Bam.Net nuget packages.

## Init
When init mode is selected, the repositoryPath and globalPackagesFolder nuget config settings
are set by making calls to nuget.exe.  The values are from the app.config file (RepositoryPath and
GlobalPackagesFolder), or, if the values are not present in app.config a prompt is shown.

Example:
```
bake /init
```

## Clean
When clean mode is selected, the local nuget caches are cleared and any "-Dev-latest" packages
are deleted.

Example:
```
bake /clean
```

## Build
When build mode is selected, a build is run using the specified build config.  The build config is
a json serialized BakeBuildConfig object.  The BakeBuildConfig definition has the same properties
defined as the bambot configs.  Bambot configs will properly deserialize as BakeBuildConfig instances.

Example
```
bake /build:.\BakeBuildConfig.json
```

## Deploy

Example:
```
bake /deploy:.\BakeDeployConfig.json
```

When deploy mode is selected, the Bam based Windows services and Daemon executables defined in the 
specified deploy config are deployed.  The json in the specified deploy config should deserizlize
to a DeployInfo instance.

Each Windows service defined in the deploy config is an executable that defines a ServiceExe
derivative in such a way that ServiceExe.ProcessCommandLineArgs is called for each of the command line
switches defined therein.

Each Daemon defined in the deploy config is a long running process but is not necessarily a Windows service.
Daemons are executed and monitored by the Bam Daemon, see [bamd](../bamd/).

## Test

## Latest

Example:
```
bake /latest
```

When latest mode is selected, the binaries are expected to exist in the path 
{Builds}{Platform}{FrameworkVersion}\Debug\_**{latest}**, where **{latest}** is
the commit hash read from {Builds}\latest and each remaining variable
value is specified in the config file.  The resulting nuget packages will have 
the suffix "-Dev-latest".

All binaries are also copied to C:\bam\latest so any projects referencing the binaries
are automatically updated to reference the latest binaries when they are rebuilt.

## Commit
Example:
```
bake /commit:0b815
```

When commit mode is selected, **argument** is the commit hash of the build to pack.  The argument specified can
be the first X number of characters of the commit hash used to uniquely identify a specific commit.  The binaries
are expected to exist in the path {Builds}{Platform}{FrameworkVersion}\Debug\_**{argument}**, where each variable
value is specified in the config file.  The resulting nuget packages will have the suffix 
"-Dev-**first five characters of commit hash**".

## Dev
Example:
```
bake /dev:C:\bam\BuildOutput
```

When dev mode is selected, **argument** is the path to where the Bam.Net binaries are found.  The 
version number used is the latest release version with the commit hash appended in the format -Dev.{commitHash}.  

## Release 
Example
```
bake /release:C:\bam\src\Bam.Net
```

When release mode is selected, **argument** is the path to the root directory where the Bam.Net source
files are found.  If no version is specified, the patch level is incremented and the resulting version is used.

The bake tool will initiate a release build before creating the nuget packages.
As part of the release build the version is set in all AssemblyInfo.cs files. If no version is specified 
the patch number is incremented and the result is used as the version.  You may specify any combination 
of the command line switches, /major, /minor or /patch to increment the associated version part.  Additionally, 
an msi is created as part of the release by prompting for the location of wix merge module and project files.

## Publish
Example
```
bake /publish:internal
```
or
```
bake /publish:public
```

When publish mode is slected, **argument** is the kind of nuget source to publish to.  If _internal_ is specified
the nuget packages are added to the nuget repository specified by NugetInternalSource in the app.config file.  If _public_
is specified the nuget packages are pused to the nuget repository specified by NugetPublicSource in the app.config file.
Additionally, if _public_ is selcted the version update is committed and tagged.

## What's next?
- [Run unit and integration tests](../bamtestrunner/) using bamtestrunner.exe
- [Define BAM managed services](../bamd/) using bamd.exe
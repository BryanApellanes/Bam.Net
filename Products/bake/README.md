# bake

Bake.exe is a tool used to build the BamToolkit and Bam.Net nuget packages.

## Latest
When latest mode is selected, the binaries are expected to exist in the path 
{Builds}{Platform}{FrameworkVersion}\Debug\_**{latest}**, where **{latest}** is
the commit hash read from {Builds}\latest and each remaining variable
value is specified in the config file.  The resulting nuget packages will have 
the suffix "-Dev-latest";

Example:
```
bake /latest
```

## Commit
When commit mode is selected, **argument** is the commit hash of the build to pack.  The argument specified can
be the first X number of characters of the commit hash used to uniquely identify a specific commit.  The binaries
are expected to exist in the path {Builds}{Platform}{FrameworkVersion}\Debug\_**{argument}**, where each variable
value is specified in the config file.  The resulting nuget packages will have the suffix 
"-Dev-**first five characters of commit hash**".

Example:
```
bake /commit:0b815
```

## Dev
When dev mode is selected, **argument** is the path to where the Bam.Net binaries are found.  The 
version number used is the latest release version with the commit hash appended in the format -Dev.{commitHash}.  

Example:
```
bake /dev:C:\bam\BuildOutput
```

## Release 

When release mode is selected, **argument** is the path to the root directory where the Bam.Net source
files are found.  If no version is specified, the patch level is incremented and the resulting version is used.

The bake tool will initiate a release build before creating the nuget packages.
As part of the release build the version is set in all AssemblyInfo.cs files. If no version is specified 
the patch number is incremented and the result is used as the version.  You may specify any combination 
of the command line switches, /major, /minor or /patch to increment the associated version part.  Additionally, 
an msi is created as part of the release by prompting for the location of wix merge module and project files.

Example
```
bake /release:C:\bam\src\Bam.Net
```


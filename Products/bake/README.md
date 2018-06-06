# bake

Bake.exe is a tool used to build the BamToolkit and Bam.Net nuget packages.

## Mode: Release

- bake /**[mode]**:**]DirectoryPath]**

Where **mode** is one of **dev** or **release**.

When release mode is selected, **DirectoryPath** is the path to the root directory where the Bam.Net source
files are found.  If no version is specified, the patch level is incremented and the resulting version is used.

The bake tool will initiate a release build before creating the nuget packages.
As part of the release build the version is set in all AssemblyInfo.cs files. If no version is specified 
the patch number is incremented and the result is used as the version.  You may specify any combination 
of the command line switches, /major, /minor or /patch to increment the associated version part.  Additionally, 
an msi is created as part of the release by prompting for the location of wix merge module and project files.

When dev mode is selected, **DirectoryPath** is the path to where the Bam.Net binaries are found.  The 
version number used is the latest release version with the build number appended in the format -Dev.{0}.  

Example:
```
bake /dev:C:\bam\BuildOutput
```


Example
```
bake /release:C:\bam\src\Bam.Net
```
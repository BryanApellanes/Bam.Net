# bake

Bake.exe is a tool used to build the BamToolkit and Bam.Net nuget packages.

## Usage

- bake /**[mode]**:**]DirectoryPath]**

Where **mode** is one of **dev** or **release**.

When release mode is selected **DirectoryPath** is the path to where the Bam.Net source directory is found. 
The bake tool will initiate a release build before packing and adding the packages to the nuget repository.  
As part of the release build the version is set in all AssemblyInfo.cs files. If no version is specified 
the patch number is incremented and the result is used as the version.  Additionally, an msi is created
as part of the release by prompting for the location of wix merge module and project files.

When dev mode is selected **DirectoryPath** is the path to where the Bam.Net binaries are found.  The version number used is the latest release version with the build number appended in the format -Dev.{0}.  The build number is tracked in a file in the current directory named **buildnum**.

Example:
```
bake /dev:C:\bam\BuildOutput
```

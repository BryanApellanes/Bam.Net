/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Bam.Net.Server")]
[assembly: AssemblyDescription("Core Bam server allowing applications to self host outside of 	IIS.  Simplifies distributed computing")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Bryan Apellanes")]
[assembly: AssemblyProduct("Bam.Net.Server")]
[assembly: AssemblyCopyright("Copyright © 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("75b9b151-b2af-43b7-a5a8-40f614309424")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: InternalsVisibleTo("Bam.Net.Server.Tests")]
[assembly: InternalsVisibleTo("Bam.Net.CoreServices")]
[assembly: InternalsVisibleTo("Bam.Net.Services")]

[assembly: AssemblyVersion("1.7.0")]
[assembly: AssemblyFileVersion("1.7.0")]

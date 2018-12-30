/*
	Copyright © Bryan Apellanes 2015  
*/
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyDescription("Automation tools")]
[assembly: AssemblyCopyright("Copyright © Bryan Apellanes 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("621b9e4e-4cd2-48bd-b2ce-6f7e3a5186e4")]

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

[assembly: InternalsVisibleTo("bam.net.automation.core.tests")]
[assembly: InternalsVisibleTo("bam.net.coreservices.core.tests")]
[assembly: InternalsVisibleTo("bam.net.serviceproxy.core.tests")]
[assembly: InternalsVisibleTo("bam.net.data.repositories.core.tests")]
[assembly: InternalsVisibleTo("bam.net.encryption.core.tests")]
[assembly: InternalsVisibleTo("bam.net.profiguration.core.tests")]
[assembly: InternalsVisibleTo("bam.net.server.core.tests")]
[assembly: InternalsVisibleTo("bam.net.services.core.tests")]
[assembly: InternalsVisibleTo("bake")]

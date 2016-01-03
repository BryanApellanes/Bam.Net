# Bam.Net 
Bam.Net is a framework and set of tools for rapid web application development.  
It offers data access code generation, web service generation, logging and 
unit testing tools.  Bam.Net is built on top of ASP.net MVC and was designed 
to bridge the gap between JavaScript, C# and SQL with an emphasis on allowing great 
JavaScript developer's to collaborate, as peers, with .Net developer's while still 
utilizing a relational database system like SQL Server.

## Why Bam.Net?
Bam.Net consists of reusable code components that I've written over the years 
that were used to contribute to numerous projects in a variety of professional 
roles I've held.  While there is functional overlap between Bam.Net and other 
more popular systems today, at the time of its creation either no equivalent 
existed or the options did not provide the specific functionality and capabilities 
that I was looking for.

## No, I Mean Why Is It Called Bam.Net?
Bam.Net is a re-branding of what was Brevitee.  The underlying toolkit of Brevitee made heavy use of the acronym B.A.M short for Brevitee Application Management which now is short for Bam Application Management.  The word brevity is defined as; concise and exact use of words in writing or speech.  In the same sense Bam.Net is intended, in the spirit of jQuery, to allow one to write less and do more through the use of Bam.Net's extensive .Net extension method library, jQuery plugins and other productivity enhancing JavaScripts.  BAM and you're done!

## Data Access Objects (Dao)
The generation of data access objects can be done by extracting objects from an 
existing SQL Server database or by processing a *.db.js file.  Due to its use 
primarily as a greenfield technology, code generation from an existing SQL Server 
database does not account for what I refer to as “silliness in the database” such 
as tables with no primary keys or tables with compound or composite primary keys.

### Dao From LaoTze
The generation of data access objects is most easily done by creating a *.db.js 
file and processing that file with the command line tool LaoTze.exe.  A *.db.js 
file is a special JavaScript file that contains a single JavaScript literal object 
named “database” that defines a database schema.  An example database schema object
is shown below.

```javascript
var database = {
	nameSpace: “The.Namespace.That.Generated.Objects.Will.Be.Placed.In”,
	schemaName: “UsedAsTheConnectionStringNameInTheConfig”,
	xrefs: [ // An array of arrays; 
		// each entry defines a many to many relationship between the table 					
		// names specified
		[“LeftTable”, “RightTable”]
	],
	tables: [
		{
			name: “TheNameOfTheTable”,
			fks: [ 	// An array of foreign key definitions where the key is 
				// the column name and the value is the name of the table 
				// that the foreign key references
				{ ColumnName1: “ReferencedTable1” },
				{ ColumnName2: “ReferencedTable2” }
			],
			cols: [ 	// An array of column definitions
				{ ColumnName: “DataType”, Null: false || true } // 
			]
		},
		{
			name: “TableOne”,
			cols: [ 	
				{ Name: “String”, Null: false },
				{ Description: "String", Null: true }
			]
		},
		{
			name: "TableTwo",
			fks: [
				{ TableOneId: "TableOne" }
			],
			cols: [
				{ Name: "String", Null: false },
				{ DescriptionTwo: "String", Null: true }
			]
		},
		{
			name: "LeftTable",
			cols: [
				{ LeftName: "String"}
			]
		},
		{
			name: "RightTable",
			cols: [
				{ RightName: "String"}
			]
		}
		{		
			// … another table like above and so on
		}		
	]
}
```


### Dao From LaoTzu
Another way of generating data access objects is by extracting those objects 
from an existing SQL Server using LaoTzu.exe.  To reiterate, code generation from 
an existing SQL Server database does not account for what I refer to as 
“silliness in the database” such as tables with no primary keys or tables with 
compound or composite primary keys so your mileage with this technique may vary.

```
// TODO: show screenshot of LaoTzu
```

LaoTze and LaoTzu both generate .Net (C#) code that can be used to quickly perform all 
database CRUD (Create, Retrieve, Update, Delete) operations.

```c#
// Create
TableOne one = new TableOne();
one.Name = "TableOneName";
one.Description = "TableOne Description";
one.Save();

// Retrieve
TableOne retrieved = TableOne.OneWhere(c => c.Name == "TableOneName");
// or
TableOneCollection retrieved = TableOne.Where(c => Name == "TableOneName");

// Update
retrieved.Description = "The description updated";
retrieved.Save();

// Delete
retrieved.Delete();
```

## Web Services Using ServiceProxySystem
Creating and exposing web services with Bam.Net is as simple as defining 
a .Net class and registering that class with the ServiceProxySystem early in the 
application life-cycle, typically in the global.asax file or a custom AppStart Config
class.

### Web Service Server 
```c#
// Echo.cs
public class Echo
{
	public string Test(string value)
	{
		return value;
	}
}

// Application_Start in global.asax
ServiceProxySystem.Initialize();
ServiceProxySystem.Register<Echo>();
```

Alternatively if the Echo class were adorned with the Proxy attribute...
```c#
[Proxy]
public class Echo
{
}

// ...it could then be registered like so;
ServiceProxySystem.RegisterBinProviders();
```
Keep in mind that using the RegisterBinProviders method will likely 
incur a performance hit on startup of the application as the 
ServiceProxySystem will "scour" the bin directory looking for classes 
with the Proxy attribute.

### Web Service Endpoints
The web service endpoints or endpoint urls that are defined for each registered 
class will conform to the following route signature:

```
/{VERB}/{ClassName}/{MethodName}.{ext}
```

Where VERB is one of GET or POST.

### Web Service Clients
In addition to automatically exposing any class that you choose as a
web service, the ServiceProxySystem will also automatically generate clients
on your behalf.

#### C# Clients
To obtain C# client code simply download the code from a running ServiceProxySystem
installation using the following path:

```
/ServiceProxy/CSharpProxies
```

You may also specify an optional namespace that the clients will be defined in

```
/ServiceProxy/CSharpProxies?namespace=My.Name.Space
```

#### JavaScript Clients
The ServiceProxySystem also generates JavaScript clients as well which
can be downloaded in a similar fashion as the C# clients.  But, the recommended way
of acquiring JavaScript clients would be to include a script tag in your pages
with the src attribute set to the JavaScript proxies path:

```xml
<script src="/ServiceProxy/JSProxies"></script>
```

## Logging
Logging using Bam.Net is done through the static convenience class
Log and it's various AddEntry methods.

Configuring logging with Bam.Net is as simple as adding an entry to the 
appSettings section of the app.config or web.config file.

```xml
<add key="LogType" value="Text" />
```

Additionally, you are also encouraged to add an entry identifying the application by name like
so:

```xml
<add key="ApplicationName" value="MyApplicationName" />
```

There are a number of Loggers included with Bam.Net, they are:
- Text
- Csv
- Xml
- Windows

The logging implementation contained in Bam.Net uses a single background commit thread
to ensure that logging operations do not block the main application thread.  Because of this
the logging system should be started early in the application life-cycle by calling Log.Start().
This is typically done in the global.asax file or a custom AppStart Config class.

### Defining a Custom Logger
If the included loggers do not meet your needs or you would otherwise like to define a logger of 
your own that commits events to a custom store or other location you need only to extend the 
base Logger class and implement the CommitLogEvent method.

```c#
public class CustomLogger: Logger
{
	public void CommitLogEvent(LogEvent event)
	{
		/// your logic here
	}
}
```

To configure the application to use a custom logger not included in Bam.Net you 
will need to specify the assembly qualified name in the app.config or web.config file
as the value for the LogType entry.




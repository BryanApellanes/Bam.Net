# Gloo
Gloo is a ServiceProxy server

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




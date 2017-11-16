# Logging
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




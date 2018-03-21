# Gloo
Using Gloo you can create and expose a web service simply by creating a class
and serving that class with gloo.exe.


# TL;DR
Serve services:	

gloo /serve:[className] /assemblySearchPattern:[seatchPattern]

or

gloo /registries:[commaSeparatedListOfRegistryNames] /assemlbySearchPattern:[searchPattern]


### Web Service Class
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

```html
<script src="/ServiceProxy/JSProxies"></script>
```


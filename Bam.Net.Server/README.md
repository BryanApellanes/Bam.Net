BAM (Bam Application Manager)
=========

BAM is a single page application manager for JavaScript, designed for use with a BamServer or ASP.Net MVC.

BamConf (~/BamConf.[json | yaml])
---
BamConf is the server level configuration file for a BamServer instance.  The BamConf can contain the following values and is only used when using BAM within the context of a BamServer:

 - **GenerateDao (bool)** - If true causes the BamServer to generate Data Access Objects from any &ast;.db.js or &ast;.db.json files that are found recursively in the ~/dao directory
 - **InitializeFileSystemFrom (enum)** - Can be one of 'Resource' or 'ZipPath'.  If 'Resource' is specified the file system will be initialized from the embedded resource that comes with the BamServer.  If 'ZipPath' is specified the file system will be initialized from the path specified by the value specified in the ZipPath property
 - **ZipPath (string)** - The path to a zip file containing the default structure for the ContentRoot of the current BamServer
 - **InitializeTemplates (bool)** - If true will cause the BamServer to call TemplateInitializer.Initialize at the end of its own Initialization sequence
 - **UseCache (bool)** - If true will cause the content responder to cache the contents of the include.js file reading it once and retrieving it from the cache on subsequent requests.  Otherwise, the contents of the include.js file will be read from disk for every request
 - **DaoSearchPattern (string)** - The file search pattern used to to filter assemblies for Dao registration
 - **ServiceSearchPattern (string)** - The file search pattern used to to filter assemblies for ServiceProxy registration
 - **ContentRoot (string)** - The root path of the BamServer
 - **ProxyAliases (array of ProxyAlias)** - A ProxyAlias has an Alias property and a ClassName property.  Used to specify alternative names that can be used in an URL to reference a class
 - **LoggerPaths (array of string)** - Direcotry paths to search for ILogger implementations 
 - **LoggerSearchPattern (string)** - The file search pattern used to load assemblies that contain ILogger implementations
 - **LoggerName (string)** - The name of the logger to use for the current BamServer instance
 - **MaxThreads (int)** - The maximum number of responder threads
 - **Port (int)** - The port that the BamServer will listen on

AppConf (~/apps/[applicationName]/AppConf.[json | yaml])
---
AppConf is the application level configuration file for each application in a BamServer instance.  The AppConf can contain the following values and is only used when using BAM within the context of a BamServer:

 - **Name** - The name of the application; this should match the name of the folder in the 'apps' directory where the AppConf is located
 - **DefaultLayout** - The name of the layout file to use for this application; layout files are found in ~/dust/layouts
 - **GenerateDao** - If true causes the BamServer to generate Data Access Objects from any &ast;.db.js or &ast;.db.json files that found recursively in the ~/apps/[applicationName]/dao directory
 - **CheckDaoHashes** - If true Data Access Objects will only be generated if the database definition files (&ast;.db.js or &ast;.db.json) have been modified
 - **ExtractBaseApp** - If true causes the BamServer to extract files from the compiled resource placing them into the application; will not overwrite existing files

Common Include.js (~/apps/include.js)
---
The Include.js file found in the 'apps' folder in the 'ContentRoot' of the server is used to specify common .css and javascript files that will be included at the layout level of all applications.  The Include.js file should contain one object literal called include with the following properties:
- **css (array of string)** - Css file paths to include
- **scripts (array of string)** - Script file paths to include; root is relative to the root of the server

Application Include.js (~/apps/[applicationName]/include.js)
---
The same as the common Include.js file except is only valid within the context of a specific application.  The Include.js file should contain one object literal called include with the following properties:
- **css (array of string)** - Css file paths to include
- **scripts (array of string)** - Script file paths to include; root is relative to the root of the application

Init.js (~/apps/[applicationName]/init.js
---
The init.js file is included automatically in the layout of an application and is used to define page activation handlers and page transition filters.

```js
$(document).ready(function () {
    bam.app("localhost", "[data-app=localhost]").setPageTransitionFilter("current", "next", function (tx, d) {
        // tx is the transitionHandler which looks like this
        // {
        //      name: <string>,
        //      from: <string>, // the name of the page the transition is from
        //      to: <string>, // the name of the page the transition is to
        //      play: function(data), // plays the transition passing in optional data
        //      also triggers start and end events before and after play
        // }
        // analyze the data d to determine if the transition will be allowed or
        // directly analyze the state of the dom.
        // return false to stop the transition from current to next page
    })
    .pageActivated("contentPage", function (page) {
        page.setStateTransitionFilter("other", "monkey", function (tx, d) {
            $("#errors").text("put state monkey");
            return "error"; // return a different state or boolean indicating whether to allow the transition
        });
        page.setStateHelloEffect("error", "shake");
    })
    .pageActivated("instructions", function (page) {
        bam.app("localhost").view("sample.test", { Title: "Dust Step", Details: "These are the details" }, ".dustTarget");
    })
    .run($("[data-app=localhost]").attr("data-start") || "start");
});
```


















    
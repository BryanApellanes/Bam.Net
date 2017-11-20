# BAMvvm
### File structure (bam sub folders)
  -  3rdParty (folder) - required and optional 3rd party dependencies
  -  apps (folder) - contains each bam application as subdirectories	
    -  <application name>
      -  dust (folder) - contains dust templates available to the current application
      - js - intended to contain common js files specific to the current application
      - pages - contains all developer created html pages 
      - viewModels - contains application specific client side JavaScript view models
  -  css - intended to contain common css files, provided for convenience
  -  dust - contains dust templates available to all bam applications
  -  js - contains bam specific JavaScript files	
  
### Data-Opts
Can be json (data-opts=’{prop1: “val1”, prop2: “val2”}’) or multiple data- entries (data-opts-prop1=”val1” data-opts-prop2=”val2”); each will result in the same object after parsing with $.dataSetOptions()

### Data-view

### Data-view-model

### Getting Started
bam.app(<appName> [, <selector>])

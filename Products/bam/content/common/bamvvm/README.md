# BAMvvm
BAM application model, view, view-model framework

# Models
All models in BAMvvm are plain and simply JavaScript objects.  They may be observed by using observer.observe
 which will return an observable object if an object is specified or an observable collection if an array
 is specified; see observer

# Views
Views in BAMvvm are html files or dust templates.  Html has always been the language used to express views
on the web and is ubiquitous.  Dynamism in a view is achieved by the use of viewModels.

# ViewModels
ViewModels in BAMvvm are the "W" in the MVW or the "\*" in MV*.  BAMvvm implements the MVVM pattern in the browser.
ViewModels define what actions can be taken on a view and provides the wiring between models and views.

# Data attributes

## data-opts-[property-name]
Used to specify options for plugins and other bamvvm events such as navigation.  Parsed by $.dataSetOptions and
returns an object with properties mirroring those specified with the "data-opts" portion removed and the 
remainder camel cased.

## data-navigate-to
Used to specify an element that causes bamvvm navigation when clicked

## data-opts-target-state
If specified, used to specify state the target of data-navigate-to should go to upon activation

## data-action
Used to specify an action on a viewModel to execute on click or the event specified by data-action-on

## data-action-on
Used to specify the event an action should be executed in response to, used in conjunction with data-action

# Observer (observer.js)
Used to observe an object or collection.  The result of calling observer.observe can be likened to instantiating
a backbone model or collection only the resulting object won't suck.

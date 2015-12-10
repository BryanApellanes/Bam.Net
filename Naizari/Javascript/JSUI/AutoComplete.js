if(!JSUI)
    alert("The core JSUI.js file was not loaded");    
    
if( window.AutoComplete == null ||
    window.AutoComplete == 'undefined'){

window.AutoComplete = {};
AutoComplete.Instances = [];

AutoComplete.AttachTo = function(textAreaElementOrId, searcherObj){
    for(strAutoCompleteId in AutoComplete.Instances){
        if(AutoComplete.Instances[strAutoCompleteId].textBoxId == textAreaElementOrId)
            return AutoComplete.Instances[strAutoCompleteId];  
    }
    
    var targetElement = JSUI.GetElement(textAreaElementOrId);
    targetElement.setAttribute("autocomplete", "off");
    var randomId = JSUI.GetRandomId(8);
    var autocomplete = new AutoCompleteClass(randomId, targetElement.id, searcherObj);
    return autocomplete;
}



function AutoCompleteClass(strResultsDivId, strInputTextAreaId, searcher)
{
    AutoComplete.Instances[strResultsDivId] = this;
    
    this.textBoxId = strInputTextAreaId;
    this.Id = strResultsDivId; // div used to show results
    this.SearchResultsHash = []; // the current set of results hashed by displayName
    this.SearchResultsIndexed = []; // the current set of results indexed
    this.SearchResultsIdHash = [];
    this.ItemSelectedListeners = []; // array of functions that will be called when an item is selected
    this.NonResultItemSelectedListeners = [];
    this.SearchStartedListeners = [];
    this.SearchQueuedListeners = [];
    this.SearchCompletedListeners = [];
        
    this.SearchDelay = 1;
     // the current set of items that are selected.
     // Each item in this array will have a boolean property
     // named "Selected".
    this.SelectedItems = [];
    this.HighlightedItem = {};
    this.HighlightedItem.IsNull = true;
    this.HighlightedIndex = 0;
    this.Searcher = searcher;
    this.OptionHeight = 13;
    this.OptionPadding = 3;
    this.UnhighlightedColor = "#FFEEBB";
    this.HighlightedColor = "#FFCC99";
    this.BackgroundColor = "#FFEEBB";
    this.FontColor = "#000000";
    this.FontFamily = "Verdana";
    this.FontWeight = "600";
    this.Width = "400px";
    this.FontSize = "10px";
    this.CurrentInput = "";
    this.IsSearching = false;
//    this.Sync = 0;	// used to synchronize the number of searches so that the results for the 
                    // last input is displayed and not the previous.  this actually provides
                    // no guarantee since the request is asyncronous but it helps...   
    this.ViewableOptionCount = 5;

    var iFrame = document.createElement("iframe");
    iFrame.style.position = "absolute";
    iFrame.style.display = "none";
    document.body.appendChild(iFrame);
        
    var id = this.Id;	    
    var textBox = document.getElementById(strInputTextAreaId);
    textBox.setAttribute("autocomplete", "off");
    var div = document.getElementById(strResultsDivId);
    if( div == null || div == 'undefined')
    {
        div = document.createElement("div");
        div.style.width = this.Width;//textBox.style.width;
        iFrame.style.width = this.Width;
        
        div.setAttribute("id", id);

        div.style.border = "1px solid black";
        document.body.appendChild(div);
    }
    
    div.style.textAlign = "left";
    this.Reposition = function(strDisplay){
        try{
            var textBoxTopLeft = JSUI.GetElementPosition(textBox);
            var textBoxHeight = JSUI.GetElementHeight(strInputTextAreaId);
            var newTop = ((textBoxTopLeft.Top * 1) + textBoxHeight) + "px";
            var newLeft = textBoxTopLeft.Left + "px"; 
            div.style.top = newTop;
            div.style.left = newLeft;
            div.style.display = strDisplay ? strDisplay : "none";
            div.style.overflow = "auto";
            div.style.overflowX = "hidden";
            div.style.position = "absolute";
            
            iFrame.style.top = newTop;
            iFrame.style.left = newLeft;
        }catch(e){
                        
        }
    }
    
    this.Reposition("none");
    var thisObj = this;

    this.OnSearchStarted = function(strInput){
        for(var i = 0; i < thisObj.SearchStartedListeners.length; i++){
            thisObj.SearchStartedListeners[i](strInput);
        }
    }

    this.OnSearchQueued = function(strInput){
        for(var i = 0; i < thisObj.SearchQueuedListeners.length; i++){
            thisObj.SearchQueuedListeners[i](strInput);            
        }
    }

    this.OnItemSelected = function(dataItem){
        for(var i = 0; i < thisObj.ItemSelectedListeners.length; i++){
            thisObj.ItemSelectedListeners[i](dataItem);
        }
        
        if(dataItem.IsNonResult)
            thisObj.OnNonResultItemSelected(dataItem);
    }
    
    this.OnNonResultItemSelected = function(dataItem){
        for(var i = 0; i < thisObj.NonResultItemSelectedListeners.length; i++){
            thisObj.NonResultItemSelectedListeners[i](dataItem);
        }
    }
    
    this.OnSearchCompleted = function(dataItemArray){
        for(var i = 0; i < thisObj.SearchCompletedListeners.length; i++){
            thisObj.SearchCompletedListeners[i](dataItemArray);
        }
    }
    
    this.AddItemSelectedListener = function(funcPointer){
        JSUI.IsFunctionOrDie(funcPointer);
        
        for(var i = 0; i < thisObj.ItemSelectedListeners.length; i++){
            if( thisObj.ItemSelectedListeners[i] == funcPointer ){
                return;
            }
        }
        thisObj.ItemSelectedListeners.push(funcPointer);
    }
    
    this.AddNonResultItemSelectedListener = function(funcPointer){
        JSUI.IsFunctionOrDie(funcPointer);
        
        if(!JSUI.ArrayContains(thisObj.NonResultItemSelectedListeners, funcPointer)){
            thisObj.NonResultItemSelectedListeners.push(funcPointer);
        }
    }
    
    this.AddSearchCompletedListener = function(funcPointer){
        JSUI.IsFunctionOrDie(funcPointer);
        
        for(var i = 0; i <  thisObj.SearchCompletedListeners.length; i++){
            if( thisObj.SearchCompletedListeners[i] == funcPointer){
                return;
            }
        }
        thisObj.SearchCompletedListeners.push(funcPointer);
    }
    
    this.Unhighlight = function(){
        if( !thisObj.HighlightedItem.IsNull )
        {
            var itemDiv = document.getElementById(thisObj.HighlightedItem.DivId);
            if( itemDiv )
                itemDiv.style.backgroundColor = thisObj.UnhighlightedColor;//"#FFFFFF";
        }
    }

    this.RefreshSelected = function(){
        var textBox = document.getElementById(thisObj.textBoxId);
        for(displayName in thisObj.SelectedItems)
        {
            if( textBox.value.indexOf(displayName) == -1 )
                thisObj.SelectedItems[displayName].Selected = false;
        }
    }

    this.ClearSelected = function(){
        thisObj.SelectedItems = [];
    }

//    this.GetScrollDistance = function(index){
//        return (1.5 * (((this.OptionHeight * (index + 1)) + (this.OptionPadding * 2)) + 2));
//    }

    this.Reset = function(){
        thisObj.SearchResultsHash = [];
        thisObj.SearchResultsIndexed = [];
        thisObj.HighlightedItem = {};
        thisObj.HighlightedItem.IsNull = true;
        thisObj.HighlightedIndex = 0;
        thisObj.Clear();
        thisObj.RefreshSelected();

        var divObj = document.getElementById(thisObj.Id);
        divObj.style.display = 'none';
        divObj.style.height = "15px";
        divObj.style.backgroundColor = thisObj.BackgroundColor;
        
        iFrame.style.display = 'none';
    }

    this.Highlight = function(strDataId){
        thisObj.Unhighlight();
        var itemDiv = document.getElementById(strDataId);//.style.backgroundColor = "#FFCC99";
        if( itemDiv ){
            itemDiv.style.backgroundColor = thisObj.HighlightedColor;
            var index = itemDiv.getAttribute("index");
            index = index * 1;  // convert to int
            thisObj.HighlightedIndex = index;
            thisObj.HighlightedItem.IsNull = false;
            thisObj.HighlightedItem = thisObj.SearchResultsIndexed[index];            
        }
    }

    this.EnsureViewable = function(){
        var div = document.getElementById(thisObj.Id);
        var page = Math.floor(thisObj.HighlightedIndex / thisObj.ViewableOptionCount);
        var scrollTop = 0;
        if( page > -1 ){
            var elementHeight = JSUI.GetElementHeight(thisObj.Id);
            scrollTop = elementHeight * page;
//            for(var i = 0; i < page; i++)
//            {
//                scrollTop += JSUI.GetElementHeight(id);//thisObj.MaxHeight;                    
//            }
            div.scrollTop = scrollTop;
        }	
    }

    this.UpArrowKey = function(){
        if( thisObj.HighlightedIndex > 0 ){
            thisObj.HighlightedIndex--;	
            var divId = thisObj.SearchResultsIndexed[thisObj.HighlightedIndex].DivId;        
            thisObj.Highlight(divId);

        thisObj.EnsureViewable();
        }
    }

    this.SelectHighlighted = function(){
        if( !thisObj.HighlightedItem.IsNull ){
            var displayName = thisObj.HighlightedItem.toString(); 
            thisObj.SelectedItems[displayName] = thisObj.HighlightedItem;
            thisObj.SelectedItems[displayName].Selected = true;

            var textBox = document.getElementById(thisObj.textBoxId);
            textBox.value = "";
            for(displayName in thisObj.SelectedItems)
            {
                if( displayName != "Extends" && displayName != "InvokeMethod" &&
                    thisObj.SelectedItems[displayName].Selected)
                {
                    textBox.value += displayName + "; ";
                }
            }
           // textBox.value = textBox.value.replace(/\n\n/g,"\n");
            textBox.scrollTop = textBox.scrollHeight;
            thisObj.OnItemSelected(thisObj.HighlightedItem);
            thisObj.Reset();                                     
        }
    }

    this.ToTildeSeparatedIds = function(){
        var returnString = "";
        for(displayName in thisObj.SelectedItems)
        {
            if( displayName != "Extends" && displayName != "InvokeMethod" )
            {
                var item = thisObj.SelectedItems[displayName];
                if( item.Selected )
                    returnString += item.DataId + "~";                    
            }
        }
        return returnString;
    }

    this.ToTildeSeparatedIdAndText = function(){
        var returnString = "";
        for(displayName in thisObj.SelectedItems)
        {
            if( displayName != "Extends" && displayName != "InvokeMethod" )
            {
                var item = thisObj.SelectedItems[displayName];
                if( item.Selected )
                    returnString += item.DataId + "~" + item.DisplayName + "~~";
            }
        }
        return returnString;
    }
    
    this.EnterKey = function(){
        thisObj.SelectHighlighted();	
        return false;        
    }



    this.DownArrowKey = function(){
        if( thisObj.HighlightedIndex < thisObj.SearchResultsIndexed.length - 1 )
        {
            thisObj.HighlightedIndex++;
            var divId = thisObj.SearchResultsIndexed[thisObj.HighlightedIndex].DivId;
            thisObj.Highlight(divId);

            thisObj.EnsureViewable();
       }
    }

   
    this.StartSearch = function(strInput){
        thisObj.Reset();
        strInput = strInput.replace(/\r/g, "").replace(/\n/g, "").replace(/;/g, "").replace(/,/g, "");
        for(displayName in thisObj.SelectedItems)
        {                               
            strInput = strInput.replace(displayName, "").trim();                
        }
        if( strInput != "" )
        {
//            thisObj.Sync++;
            thisObj.CurrentInput = strInput;
            thisObj.IsSearching = true;
            thisObj.Searcher.Search(strInput);            
            thisObj.OnSearchStarted(strInput);
        }
    }
    
    var searchQueueTimer;
    this.QueueSearch = function(strInput){
        if(thisObj.CurrentInput == strInput && thisObj.IsSearching){  // this will catch a backspace to prevent a new search on identical input
            if(searchQueueTimer)
                clearTimeout(searchQueueTimer); // if a search is queued for the same input as what is already being searched for, clear the queue timer
            return;
        }
        thisObj.CurrentInput = strInput; // the new input is different so set current to the new input
        if(searchQueueTimer)
            clearTimeout(searchQueueTimer);
            
        searchQueueTimer = setTimeout("AutoComplete.Instances['" + thisObj.Id + "'].StartSearch('" + encodeURIComponent(thisObj.CurrentInput) + "')", thisObj.SearchDelay * 1000);
        thisObj.OnSearchQueued(strInput);
    }

    var connectKeyDown = function(){
        JSUI.AddEventHandler(JSUI.GetElement(thisObj.textBoxId), //textBox,
                                 function(evt){
                                        var e = evt;
                                        if( window.event )
                                        {
                                            e = window.event;
                                        }

                                        // enter key
                                        if( e.keyCode == 13 )
                                        {
                                            thisObj.EnterKey();
                                            if( e.preventDefault )
                                                e.preventDefault();
                                            return false;
                                        }else if( e.keyCode == 40 ) //down arrow
                                        {
                                            thisObj.DownArrowKey();
                                            return false;

                                        }else if (e.keyCode == 38) // up arrow
                                        {
                                            thisObj.UpArrowKey();
                                            return false;
                                        }

                                        return true;
                                 },
                                 "keydown"				     
            )
     }

    connectKeyDown();
    
    var connectKeyUp = function(){
        JSUI.AddEventHandler(JSUI.GetElement(thisObj.textBoxId), //textBox,
                    function(evt){
                        var e = evt;
                        if( window.event)
                        {
                            e  = window.event;
                        }

                        // enter key
                        if( e.keyCode == 13 )
                        {
                            thisObj.EnterKey();
                            return false;
                        }else if( e.keyCode == 40 ) //down arrow
                        {
                            return false;

                        }else if (e.keyCode == 38) // up arrow
                        {
                            return false;
                        }else if( e.keyCode == 27) // reset on escape key
                        {
                            thisObj.Reset();
                            return false;
                        }

                        if( textBox.value != "" && textBox.value != thisObj.CurrentInput)
                            thisObj.QueueSearch(textBox.value);
                        else
                        {
                            for(displayName in thisObj.SelectedItems)
                            {
                                if( displayName != "Extends" && displayName != "InvokeMethod" )
                                {
                                    var item = thisObj.SelectedItems[displayName];
                                    item.Selected = false;                   
                                }
                            }
                        }

                        return true;
                    },
                    "keyup"
        )
     }
     
     connectKeyUp();
     
     var connectBlur = function(){
        JSUI.AddEventHandler(JSUI.GetElement(thisObj.textBoxId), //textBox, 
              function(){
                  setTimeout("AutoComplete.Instances['" + thisObj.Id + "'].Reset();", 1000);
              },
              "blur"
        )
     }  
     connectBlur();
     
     var connectKeyPress = function(){ 
        JSUI.AddEventHandler(JSUI.GetElement(thisObj.textBoxId), //textBox,
            function(evt){
                var e = JSUI.GetEvent(evt);
                if( e.keyCode == 13 || e.keyCode == "13")
                    return false;
            },
            "keypress"
        )
        
     }
     
     connectKeyPress();
     
     this.ReAttach = function(){
        connectKeyDown();
        connectKeyUp();
        connectKeyPress();
        connectBlur();
     }
     
        this.AddSearchStartedListener = function(funcPointer){
            JSUI.IsFunctionOrDie(funcPointer);
            thisObj.SearchStartedListeners.push(funcPointer);
        } 
         
        this.AddSearchQueuedListener = function(funcPointer){
            JSUI.IsFunctionOrDie(funcPointer);
            thisObj.SearchQueuedListeners.push(funcPointer);
        }
          
        this.Clear = function(){
            var div = document.getElementById(id);
            if( div.hasChildNodes && div.removeChild)
            {
                while(div.hasChildNodes())
                {
                    div.removeChild(div.firstChild);		    
                }		    
            }
        }

        this.ShowList = function(searchResults){
            thisObj.IsSearching = false;
            if( searchResults.Status == "Success")            
            {
                thisObj.Reset();	        
                var first = true;

                for(var i = 0; i < searchResults.Data.length; i++)
                {
                    var result = searchResults.Data[i];
                    if(result.IsNonResult){
                        result.Input = searchResults.Input[0] ? searchResults.Input[0]: "";
                    }
                    
                    result.toString = function(){
                        return this.DisplayName;
                    }

                    thisObj.SearchResultsHash[result.toString()] = result; // place the result object into the searchresults hash
                    thisObj.SearchResultsIndexed[i] = result;
                    thisObj.SearchResultsIdHash[result.DataId] = result;

                    thisObj.addResult(result, i);//line, result.DataId, i);
                    if(first)
                        thisObj.Highlight(result.DivId);
                    first = false;
                }    	        
                iFrame.style.display = 'block';
                document.getElementById(thisObj.Id).style.display = 'block';                
            }
            
            thisObj.OnSearchCompleted(searchResults);	        
        }


    searcher.SearchCallback = this.ShowList;		    

    this.addResult = function(item, index){
        var div = document.getElementById(id);
        div.style.width = this.Width;
        var itemDiv = document.createElement("div");
        itemDiv.setAttribute("index", index.toString());
        itemDiv.style.width = "100%";//this.Width;
        itemDiv.style.height = this.OptionHeight + "px";
        itemDiv.style.padding = this.OptionPadding + "px";
        itemDiv.style.wordWrap = "break-word";
        itemDiv.style.color = this.FontColor;
        itemDiv.style.backgroundColor = this.UnhighlightedColor;
        itemDiv.style.fontFamily = this.FontFamily;
        itemDiv.style.fontWeight = this.FontWeight;
        itemDiv.style.fontSize = this.FontSize;
        
        JSUI.SetHandCursor(itemDiv);
        
        var divHeight = JSUI.GetElementHeight(id);

        if( this.SearchResultsIndexed.length <= this.ViewableOptionCount)
        {
//            div.style.height = (this.SearchResultsIndexed.length * this.OptionHeight) + "px";
            var newHeight = this.OptionHeight + this.OptionPadding;
            newHeight = (divHeight * 1) + newHeight;
            div.style.height = newHeight + "px";
            iFrame.style.height = newHeight + "px";
        }

        var divId = JSUI.GetRandomId(8);
        item.DivId = divId;
        itemDiv.setAttribute("id", divId);//item.DataId);
        if( index == 0 )
            this.Highlight(divId);//item.DataId);

        var text = document.createTextNode(item.toString());
        itemDiv.appendChild(text);
        div.appendChild(itemDiv);

        JSUI.AddEventHandler(itemDiv, 
            function(){ 
                thisObj.Highlight(divId);//item.DataId);//
            },
            "mouseover");

        JSUI.AddEventHandler(itemDiv,
            function(){
                thisObj.Unhighlight();//
            },
        "mouseout");

        //itemDiv.onclick = function(){thisObj.SelectHighlighted(item.DataId);};
        JSUI.AddEventHandler(itemDiv,
            function(){
                thisObj.SelectHighlighted(divId);//item.DataId);
            },
            "click");
    }
    
//    this.RemoveOption = function(strDataId){
//        var dataItem = thisObj.SearchResultsIdHash[strDataId];
//        var dropElement = JSUI.GetElement(thisObj.Id);
//        var itemDiv = JSUI.GetElement(dataItem.DivId);
//        dropElement.removeChild(itemDiv);
//        var newSearchResultsIndexed = [];
//        delete thisObj.SearchResultsHash[dataItem.DisplayName];
//        delete thisObj.SearchResultsIdHash[strDataId];
//        for(var i = 0; i < thisObj.SearchResultsIndexed.length; i++){
//            if( thisObj.SearchResultsIndexed[i].DataId != strDataId){
//                newSearchResultsIndexed.push(thisObj.SearchResultsIndexed[i]);
//                var itemDiv = JSUI.GetElement(thisObj.SearchResultsIndexed[i].DivId);
//                if(itemDiv)
//                    itemDiv.setAttribute("index", i.toString());
//            }
//        }
//        thisObj.SearchResultsIndexed = newSearchResultsIndexed;
//    }
}

window.AutoComplete = AutoComplete;
}

AutoComplete = window.AutoComplete;
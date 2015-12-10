if(JSUI == 'undefined'){
    alert("The core JSUI.js file was not loaded.");
}

var Scrollables = {};
Scrollables.makeScrollable = function(strElementId, dimensions){
    var scrollable = new ScrollableElement(strElementId, dimensions);
    Scrollables[strElementId] = scrollable;
    return scrollable;    
}

Scrollables.MakeScrollable = function(el, dims) {
    return Scrollables.makeScrollable(el, dims);
}

Scrollables.GetScrollable = function(strElementId){
    if(Scrollables[strElementId]){
        return Scrollables[strElementId];
    }
    return null;
}

function ScrollableElement(strElementId, dimensions){
    this.TargetElement = null; //set later placed here for visibility
    this.Width = null;   //set later placed here for visibility
    this.WidthString = "";
    this.Height = null;  //set later placed here for visibility
    this.HeightString = "";
    this.BorderWidth = 1;
    this.ScrollContainer = null;
    this.Head = null;
    this.Parent = null; // this is the outer div containing all the elements making the scrollable, may be null if target is not a table
    this.ItemTag = "tr"; // used to set the height of each "item".  The default is table row because this is intended to scrollify a table
                        //but if we were scrollifying a div this could be changed to the tagname of whatever contains the "items" in the div.
    this.ItemHeight = 15;
    
    var thisObj = this;
    
    var height = 200;
    var width = 200;
    var headHeight = 15;
    if(dimensions){
        height = dimensions.Height ? parseInt(dimensions.Height): height;
        width = dimensions.Width ? parseInt(dimensions.Width) : width;
    }
    
    this.Width = width;
    this.Height = height;
    
    var targetElement = JSUI.GetElement(strElementId);//document.getElementById(strElementId);
    targetElement.style.width = "100%";
    this.TargetElement = targetElement;
    
    var head = targetElement.getElementsByTagName("thead")[0];
    var newHeadTable = null;
    
    if(head){
        var headID = "scroll_head" + JSUI.RandomString(8);
        newHeadTable = document.createElement("table");
        newHeadTable.style.border = "1px";
        newHeadTable.setAttribute("id", headID);
        newHeadTable.setAttribute("cellspacing", "0");
        newHeadTable.setAttribute("cellpadding", "0");
        newHeadTable.style.width = "100%";
        
        targetElement.removeChild(head);
        
        newHeadTable.appendChild(head);
        targetElement.parentNode.insertBefore(newHeadTable, targetElement);
        this.Head = newHeadTable;
    }else{
        headHeight = 0;
    }
   
    function setColumnWidths(table, arrWidths, strTagName){
        var rows = table.getElementsByTagname("tr");
        if(rows != 'undefined'){
            var firstRow = rows[0];
            var cells = firstRow.getElementsByTagName(strTagName);
            for(var i = 0 ; i < arrWidths.length - 1; i++){
                cells[i].width = arrWidths[i] + "px"; // sets the width of all but the last column to prevent them from snapping back to original widths
            } 
        }
    }
    
    this.SetColumnWidths = function(arrWidths){
        if(newHeadTable){
            setColumnWidths(newHeadTable, arrWidths, "th");
        }                                 
        
        setColumnWidths(targetElement, arrWidths, "td");
        
        return thisObj;         
    }
     
    this.SetHeadHeight = function(intHeight){
        if(thisObj.Head){
            thisObj.Head.style.height = intHeight + "px";
            headHeight = intHeight;
            thisObj.ScrollContainer.style.height = (thisObj.Height - headHeight) + "px";
        }
        
        return thisObj;
    } 
    
    this.SetItemHeight = function(intHeight){
        var items = thisObj.ScrollContainer.getElementsByTagName(thisObj.ItemTag);
        for(var i = 0 ; i < items.length; i++){
            items[i].style.height = intHeight + "px";
        }
        
        return thisObj;
    }
    
    this.SetBorderStyle = function(strBorderStyle){
        var ele;
        if(thisObj.Parent){
            ele = JSUI.GetElement(thisObj.Parent.id);
        }
        else{
            ele = JSUI.GetElement(thisObj.ScrollContainer.id);
        }   
        ele.style.borderStyle = strBorderStyle;         
        return thisObj;
    }
    
    this.SetBorderWidth = function(intBorderWidth){
        if(thisObj.Parent){
            JSUI.GetElement(thisObj.Parent.id).style.borderWidth = intBorderWidth + "px";
        }else{
            JSUI.GetElement(thisObj.ScrollContainer.id).style.borderWidth = intBorderWidth + "px";
        }
        
        thisObj.BorderWidth = intBorderWidth;
        
        JSUI.GetElement(thisObj.ScrollContainer.id).style.height = (thisObj.Height - headHeight) - intBorderWidth + "px";
        
        return thisObj;
    }
    
    this.SetBorderColor = function(strBorderColor){
        if(thisObj.Parent)
            JSUI.GetElement(thisObj.Parent.id).style.borderColor = strBorderColor;
        else
            JSUI.GetElement(thisObj.ScrollContainer.id).style.borderColor = strBorderColor;
            
        return thisObj;        
    }
    
    this.SetWidth = function(width){
        if(!JSUI.IsNumber(width))
            return;

        thisObj.Width = parseInt(width);
        
        if(width.toString().trim().endsWith("%") || width.toString().trim().endsWith("px")){
            thisObj.WidthString = width;
        }else{
            thisObj.WidthString = width + "px";
        }

        if(thisObj.Parent){
            thisObj.Parent.style.width = thisObj.WidthString;
            thisObj.ScrollContainer.style.width = "100%";
        }else{
            thisObj.ScrollContainer.style.width = thisObj.Width - (thisObj.BorderWidth * 2) + "px";
        }
    }
    
    this.SetHeight = function(height){
        if(!JSUI.IsNumber(height))
            return;
        
        thisObj.Height = parseInt(height);
            
        if(height.toString().trim().endsWith("%") || height.toString().trim().endsWith("px")){
            thisObj.HeightString = height;
        }
        else{
            thisObj.HeightString = height + "px";            
        }
            
        if(thisObj.Parent)
            thisObj.Parent.style.height = thisObj.HeightString;
            
        thisObj.ScrollContainer.style.height = (thisObj.Height - headHeight) - thisObj.BorderWidth + "px";
        
        return thisObj;        
    }
                
    var scrollContainer;

    if(JSUI.ElementIs(targetElement, "div")){
        scrollContainer = targetElement;
    }else{
        scrollContainer = document.createElement("div");
        var scrollID = "scroll_container_" + JSUI.RandomString(8);
        scrollContainer.setAttribute("id", scrollID);
        
        targetElement.parentNode.insertBefore(scrollContainer, targetElement.nextSibling);
        var parent = document.createElement("div");
        parent.setAttribute("id", "scroll_outerdiv_" + JSUI.RandomString(8));
        
        targetElement.parentNode.insertBefore(parent, targetElement.nextSibling);    
        targetElement.parentNode.removeChild(targetElement);
        scrollContainer.appendChild(targetElement);
        if(this.Head){
            this.Head.parentNode.removeChild(this.Head);
            this.Head.style.width = "100%";
            parent.appendChild(this.Head);
        }    
        
        parent.appendChild(scrollContainer);
        parent.style.width = this.Width + "px";
        parent.style.height = this.Height + "px";
        this.Parent = parent;
    }
      
    scrollContainer.style.width = "100%";
    scrollContainer.style.height = (this.Height - headHeight) + "px";
    scrollContainer.style.overflow = "auto";
    
    this.ScrollContainer = scrollContainer;
    
    this.SetHeadHeight(headHeight);
}


JSUI.Assimilate(Scrollables);//, JSUI);
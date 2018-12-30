var Docker = {};
Docker.DockedElements = [];

Docker.DockedElementClass = function(strElementId, strDockToId, objDimensions){
    Docker.DockedElements.push(this);
    
    this.ElementId = strElementId;
    this.DockToId = strDockToId;
    this.Dimensions = objDimensions;
    
    var iFrame = document.createElement("iframe");
    iFrame.style.border = '0px';
    iFrame.style.position = 'absolute';
    iFrame.style.zIndex = -1;
    JSUI.GetDocumentBody().appendChild(iFrame);
    
    var thisObj = this;
    this.ReDock = function(){
        var contentElement = JSUI.GetElement(thisObj.ElementId);
        var targetPosition = JSUI.GetElementPosition(thisObj.DockToId);
        var targetHeight = JSUI.GetElementHeight(thisObj.DockToId);
        if( !targetHeight )
            targetHeight = 15;
            
        targetHeight += 6;
        var targetLeft = targetPosition.Left;
          
        var newTop = targetPosition.Top + targetHeight + "px";
        var newLeft = targetLeft + "px";
        
        contentElement.style.top = newTop;
        contentElement.style.left = newLeft;
        contentElement.style.position = "absolute";
    
    // this section is specifically for IE6 but breaks IE7 so it's been commented for now
//        iFrame.style.top = newTop;
//        iFrame.style.left = newLeft;
//        iFrame.style.height = JSUI.GetElementHeight(contentElement) + "px";
//        iFrame.style.width = JSUI.GetElementWidth(contentElement) + "px";
//        contentElement.parentNode.removeChild(contentElement);
//        iFrame.parentNode.removeChild(iFrame);
//        var docBody = JSUI.GetDocumentBody();
//        docBody.appendChild(iFrame); 
//        docBody.appendChild(contentElement);               
    }
    
    JSUI.addOnWindowResize(this.ReDock);
    JSUI.addOnWindowScroll(this.ReDock);
}

var Popper = {};

Popper.ToolTipify = function(targetElementOrId, contentElementOrId, dimensions, boolOrientLeft){
    JSUI.SetHandCursor(targetElementOrId);
    var tt = new Popper.PopperClass(targetElementOrId, contentElementOrId, dimensions, boolOrientLeft);
    tt.ShowContent = false;
    JSUI.AddEventHandler(JSUI.GetElement(targetElementOrId),
        tt.Poof,
        "mouseout")
        
    return tt;        
}

Popper.Popperize = function(targetElementOrId, contentElementOrId, boolDoPop, boolShowContent, dimensions){
    //JSUI.Require("Animator");
    var popper = new Popper.PopperClass(targetElementOrId, contentElementOrId, dimensions);
    popper.ShowContent = boolShowContent;
    if(boolDoPop)
        popper.Pop();
        
    return popper;
}

Docker.DockThisTo = function(strElementId, strDockToElementId, objDimensions){
    var docked = new Docker.DockedElementClass(strElementId, strDockToElementId, objDimensions);
    docked.ReDock();
    return docked;
}

Popper.PopperClass = function(targetElementOrId, contentElementOrId, popDimensions, orientLeft){ // orientLeft - quick hack this should be changed later
    var targetElement = JSUI.GetElement(targetElementOrId);
    var contentElement = JSUI.GetElement(contentElementOrId);
    contentElement.style.display = "none";

    var iFrame = document.createElement("iframe");
    iFrame.style.border = '0px';
    iFrame.style.position = 'absolute';
    iFrame.style.zIndex = -1;
    JSUI.GetDocumentBody().appendChild(iFrame);
        
//    var inserted = false;        
    this.Reposition = function(){
        var targetPosition = JSUI.GetElementPosition(targetElement);
        var targetHeight = JSUI.GetElementHeight(targetElement.id);
        if( !targetHeight )
            targetHeight = 15;
            
        targetHeight += 6;
        var targetLeft = targetPosition.Left;
        if(orientLeft)
            targetLeft = targetLeft - popDimensions.Width;            
        var newTop = targetPosition.Top + targetHeight + "px";
        var newLeft = targetLeft + "px";
        
        contentElement.style.top = newTop;
        contentElement.style.left = newLeft;
        contentElement.style.position = "absolute";
        
//        if(!inserted){
//            var docBody = JSUI.GetDocumentBody();
//            docBody.appendChild(iFrame, contentElement);
        
            iFrame.style.top = newTop;
            iFrame.style.left = newLeft;
            iFrame.style.height = JSUI.GetElementHeight(contentElement) + "px";
            iFrame.style.width = JSUI.GetElementWidth(contentElement) + "px";
            contentElement.parentNode.removeChild(contentElement);
            iFrame.parentNode.removeChild(iFrame);
            docBody.appendChild(iFrame);
            docBody.appendChild(contentElement);
//            inserted = true;
//        }
    }                                                      
    
    this.Animate = false;
    this.ShowContent = true;
    this.popCompleteListeners = [];
    this.poofCompleteListeners = [];
    
    var daObj = this;
    var dimensions = popDimensions;
    
    this.OnPoofComplete = function(){
        for(var i = 0; i < daObj.poofCompleteListeners.length; i++){
            daObj.poofCompleteListeners[i]();
        }
        JSUI.AddEventHandler(targetElement, daObj.Pop, "mouseover");
        JSUI.RemoveEventHandler(contentElement, daObj.Poof, "mouseout");
    }
    
    this.OnPopComplete = function(){
        for(var i = 0; i < daObj.popCompleteListeners.length; i++){
            daObj.popCompleteListeners[i]();
        }
        JSUI.AddEventHandler(contentElement, daObj.Poof, "mouseout");
        JSUI.RemoveEventHandler(targetElement, daObj.Pop, "mouseover");
    } 
    this.Pop = function(e){
        if( contentElement.style.display == 'none' ){
            if( daObj.Animate )
            {
                var anim = Animation.GrowIn(contentElement, 50, daObj.ShowContent, dimensions);
                anim.AddGrowCompleteListener(daObj.OnPopComplete);
                daObj.Reposition();//targetElement, contentElement);
                
            }else
            {
//                iFrame.style.dispaly = 'block';
                contentElement.style.display = 'block';
                
                daObj.Reposition();//targetElement, contentElement);
                daObj.OnPopComplete();
            } 
        }
    }
    
    this.AddPopCompleteListener = function(funcPointer){
        JSUI.IsFunction(funcPointer, true);
        
        if( JSUI.FunctionIsInArray(funcPointer, this.popCompleteListeners) )
            return;
            
        this.popCompleteListeners.push(funcPointer);
    }
    
    this.AddPoofCompleteListener = function(funcPointer){
        JSUI.IsFunction(funcPointer, true);
        
        if( JSUI.FunctionIsInArray(funcPointer, this.poofCompleteListeners) )
            return;
            
        this.poofCompleteListeners.push(funcPointer);
    }

    
    this.Poof = function(e){
        var event = window.event ? window.event: e;
        if( JSUI.MouseIsOverElement(contentElement, event) )
            return false;
        
        if(daObj.Animate)
        {
//            iFrame.style.display = "none";
            var anim = Animator.ShrinkOut(contentElement, 50, daObj.ShowContent);
            anim.AddShrinkCompleteListener(daObj.OnPoofComplete);
            daObj.Reposition(targetElement, contentElement);
        }else
        {
            contentElement.style.display = "none";
//            iFrame.style.display = "none";
             
            daObj.Reposition(targetElement, contentElement);
            daObj.OnPoofComplete();
        }
       
    }
    

    
    JSUI.AddEventHandler(targetElement, this.Pop, "mouseover");
    JSUI.SetHandCursor(contentElement);
    this.Reposition(targetElementOrId, contentElementOrId);
}

JSUI.Assimilate(Popper);//, JSUI);
JSUI.Assimilate(Docker);//, JSUI);
var Animation = {};

Animation.SizerClassInstances = {};

Animation.SizerClass = function (strElementId, dimensions, boolShowContent) {
    Animation.SizerClassInstances[strElementId] = this;
    this.TargetElement = JSUI.GetElement(strElementId);
    var defaultDims = { Width: 200, Height: 200 };
    var useDims = dimensions ? dimensions : defaultDims;

    var framesPerSecond = 10;

    this.MaxSize = useDims;

    this.ShowContent = boolShowContent;

    this.growStartedListeners = [];
    this.shrinkStartedListeners = [];

    this.growCompleteListeners = [];
    this.shrinkCompleteListeners = [];

    this.growDownStartedListeners = [];
    this.shrinkUpStartedListeners = [];

    this.growDownCompleteListeners = [];
    this.shrinkUpCompleteListeners = [];

    this.growRightStartedListeners = [];
    this.shrinkLeftStartedListeners = [];

    this.growRightCompleteListeners = [];
    this.shrinkLeftCompleteListeners = [];

    var animating = false;
    var daObj = this;

    var callFunctions = function (arrFunctions, arg) {
        for (var i = 0; i < arrFunctions.length; i++) {
            arrFunctions[i](arg);
        }
    };

    this.getFramesPerSecond = function () {
        return framesPerSecond;
    };

    this.setFramesPerSecond = function (val) {
        framesPerSecond = val;
    };

    this.OnGrowDownStarted = function () {
        callFunctions(daObj.growStartedListeners, daObj.TargetElement);
    };

    this.OnGrowDownComplete = function () {
        callFunctions(daObj.growDownCompleteListeners, daObj.TargetElement);
        //for (var i = 0; i <daObj.growDownCompleteListeners.length;i++) {
        //    daObj.growDownCompleteListeners[i](daObj.targetElement);
        //}
    };

    this.OnGrowRightStarted = function () {
        callFunctions(daObj.growStartedListeners, daObj.TargetElement);
    };

    this.OnGrowRightComplete = function () {
        callFunctions(daObj.growRightCompleteListeners, daObj.TargetElement);
        //        for (var i = 0; i <daObj.growRightCompleteListeners.length;i++) {
        //            daObj.growRightCompleteListeners[i](daObj.targetElement);
        //        }
    };

    this.OnShrinkLeftStarted = function () {
        callFunctions(daObj.shrinkStartedListeners, daObj.TargetElement);
    };

    this.OnShrinkLeftComplete = function () {
        callFunctions(daObj.shrinkLeftCompleteListeners, daObj.TargetElement);
        //        for (var i = 0; i < daObj.shrinkLeftCompleteListeners.length; i++) {
        //            daObj.shrinkLeftCompleteListeners[i](daObj.targetElement);
        //        }
    };

    this.OnShrinkUpStared = function () {
        callFunctions(daObj.shrinkStartedListeners, daObj.TargetElement);
    };

    this.OnShrinkUpComplete = function () {
        callFunctions(daObj.shrinkUpCompleteListeners, daObj.TargetElement);
        //        for (var i = 0; i < daObj.shrinkUpCompleteListeners.length; i++) {
        //            daObj.shrinkUpCompleteListeners[i](daObj.targetElement);
        //        }
    };

    this.OnGrowStarted = function () {
        callFunctions(daObj.growStartedListeners, daObj.TargetElement);
    };

    this.OnGrowComplete = function () {
        callFunctions(daObj.growCompleteListeners, daObj.TargetElement);
        //        for (var i = 0; i < daObj.growCompleteListeners.length; i++) {
        //            daObj.growCompleteListeners[i](daObj.targetElement);
        //        }        
    };

    this.OnShrinkStarted = function () {
        callFunctions(daObj.shrinkStartedListeners, daObj.TargetElement);
    };
    this.OnShrinkComplete = function () {
        callFunctions(daObj.shrinkCompleteListeners, daObj.TargetElement);
        //        for (var i = 0; i < daObj.shrinkCompleteListeners.length; i++) {
        //            daObj.shrinkCompleteListeners[i](daObj.targetElement);
        //        }
    };

    this.AddGrowStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growStartedListeners)) {
            return;
        }
        daObj.growStartedListeners.push(funcPointer);
    };

    this.AddGrowCompleteListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growCompleteListeners)) {
            return;
        }
        daObj.growCompleteListeners.push(funcPointer);
    };

    this.AddGrowDownStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growDownStartedListeners)) {
            return;
        }
        daObj.growDownStartedListeners.push(funcPointer);
    };

    this.AddGrowDownCompleteListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growDownCompleteListeners)) {
            return;
        }

        daObj.growDownCompleteListeners.push(funcPointer);
    };

    this.AddGrowRightStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growRightStartedListeners)) {
            return;
        }
        daObj.growRightStartedListeners.push(funcPointer);
    };

    this.AddGrowRightCompleteListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.growRightCompleteListeners)) {
            return;
        }

        daObj.growRightCompleteListeners.push(funcPointer);
    };

    this.AddShrinkUpStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.shrinkUpStartedListeners)) {
            return;
        }
        daObj.shrinkUpStartedListeners.push(funcPointer);
    };

    this.AddShrinkUpCompleteListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.shrinkUpCompleteListeners)) {
            return;
        }

        daObj.shrinkUpCompleteListeners.push(funcPointer);
    };
    this.AddShrinkLeftStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.shrinkLeftStartedListeners)) {
            return;
        }
        daObj.shrinkLeftStartedListeners.push(funcPointer);
    };

    this.AddShrinkLeftCompleteListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.shrinkLeftCompleteListeners)) {
            return;
        }

        daObj.shrinkLeftCompleteListeners.push(funcPointer);
    };

    this.AddShrinkStartedListener = function (funcPointer) {
        JSUI.IsFunctionOrDie(funcPointer);
        if (JSUI.FunctionIsInArray(funcPointer, daObj.shrinkStartedListeners)) {
            return;
        }
        daObj.shrinkStartedListeners.push(funcPointer);
    };

    this.AddShrinkCompleteListener = function (funcPointer) {
        if (typeof (funcPointer) !== 'function') {
            JSUI.ThrowException(funcPointer.toString() + " is not a function");
        }

        for (var i = 0; i < this.shrinkCompleteListeners.length; i++) {
            if (this.shrinkCompleteListeners[i] === funcPointer) {
                return;
            }
        }
        this.shrinkCompleteListeners.push(funcPointer);
    };

    this.Grow = function () {
        var curWidth = JSUI.GetElementWidth(daObj.targetElement.id);
        var curHeight = JSUI.GetElementHeight(daObj.targetElement.id);
        var newWidth = (curWidth * 1) + 10;
        var newHeight = (curHeight * 1) + 10;

        if (newHeight >= daObj.MaxSize.Height) {
            daObj.targetElement.style.height = daObj.MaxSize.Height + "px";
        } else {
            daObj.targetElement.style.height = newHeight + "px";
        }

        if (newWidth >= daObj.MaxSize.Width) {
            daObj.targetElement.style.width = daObj.MaxSize.Width + "px";
        } else {
            daObj.targetElement.style.width = newWidth + "px";
        }

        if (newHeight > daObj.MaxSize.Height && newWidth > daObj.MaxSize.Width) {
            daObj.OnGrowComplete();
            return;
        } else {
            setTimeout(daObj.Grow, 10);
        }
    };

    var growDown = function () {
        var curHeight = JSUI.GetElementHeight(daObj.TargetElement.id);
        var growPixels = daObj.MaxSize.Height / framesPerSecond;
        var timeout = 1000 / framesPerSecond;

        var newHeight = (curHeight * 1) + (growPixels * 1);

        if (newHeight >= daObj.MaxSize.Height) {
            daObj.TargetElement.style.height = daObj.MaxSize.Height + "px";
        } else {
            daObj.TargetElement.style.height = newHeight + "px";
        }
        if (newHeight > daObj.MaxSize.Height) {
            animating = false;
            daObj.OnGrowDownComplete();
        } else {
            setTimeout(growDown, timeout);
        }
    };

    this.GrowDown = function () {
        if (animating) {
            return;
        }
        animating = true;
        if (daObj.TargetElement.style.display === 'none') {
            daObj.TargetElement.style.display = 'block';
        }

        if (!daObj.ShowContent) {
            var content = daObj.TargetElement.innerHTML;
            daObj.TargetElement.innerHTML = "";
            daObj.AddGrowDownCompleteListener(function () {
                daObj.TargetElement.innerHTML = content;
            });
        }

        growDown();
    };

    // The majority of these functions have not been tested and I know for a fact that some do not work.  
    // Grow and Shrink are the only production animations currently 6/19/09
    this.GrowRight = function (elementOrId) {
        if (elementOrId) {
            var element = JSUI.GetElement(elementOrId);
            element.style.width = "0px";
            if (element.style.display === "none") {
                element.style.display = 'block';
            }
            daObj.TargetElement = element;
        }

        var curWidth = JSUI.GetElementWidth(daObj.TargetElement.id);
        var newWidth = (curWidth * 1) + 10;

        if (newWidth >= daObj.MaxSize.Width) {
            daObj.TargetElement.style.width = daObj.MaxSize.Width + "px";
        } else {
            daObj.TargetElement.style.width = newWidth + "px";
        }

        if (newWidth > daObj.MaxSize.Height) {
            daObj.OnGrowRightComplete();
        } else {
            setTimeout(daObj.GrowRight, 10);
        }
    };

    this.Shrink = function () {
        var curWidth = JSUI.GetElementWidth(daObj.TargetElement.id);
        var curHeight = JSUI.GetElementHeight(daObj.TargetElement.id);
        var newWidth = (curWidth * 1) - 10;
        var newHeight = (curHeight * 1) - 10;
        if (newHeight <= 0 || newWidth <= 0) {
            daObj.TargetElement.style.display = 'none';
            daObj.OnShrinkComplete();
            return;
        }
        else if (curWidth > curHeight) {
            daObj.TargetElement.style.width = newWidth + "px";
        } else if (curWidth < curHeight) {
            daObj.TargetElement.style.height = newHeight + "px";
        } else if (curWidth === curHeight) {
            daObj.TargetElement.style.height = newHeight + "px";
            daObj.TargetElement.style.width = newWidth + "px";
        }

        setTimeout(daObj.Shrink, 10);
    };

    var shrinkUp = function () {
        var curHeight = JSUI.GetElementHeight(daObj.TargetElement.id);
        var pixelStep = daObj.MaxSize.Height / framesPerSecond;
        var timeout = 1000 / framesPerSecond;

        var newHeight = (curHeight * 1) - (pixelStep * 1);
        if (newHeight <= 0) {
            daObj.TargetElement.style.display = 'none';
            daObj.TargetElement.style.height = "0px";
            animating = false;
            daObj.TargetElement.style.overflow = targetOverflow;
            daObj.OnShrinkUpComplete();
            return;
        } else {
            daObj.TargetElement.style.height = newHeight + "px";
        }

        setTimeout(shrinkUp, timeout);
    };
    this.ShrinkUp = function () {
        if (animating) {
            return;
        }

        animating = true;
        if (daObj.TargetElement.style.display === 'none') {
            daObj.TargetElement.style.display = 'block';
        }

        targetOverflow = daObj.TargetElement.style.overflow;
        daObj.TargetElement.style.overflow = "hidden";

        if (!daObj.ShowContent) {
            var content = daObj.TargetElement.innerHTML;
            daObj.TargetElement.innerHTML = "";
            daObj.AddShrinkUpCompleteListener(function () {
                daObj.TargetElement.innerHTML = content;
            })
        }
        shrinkUp();
    };

    this.ShrinkLeft = function (elementOrId) {
        var element = JSUI.GetElement(elementOrId);
        if (element.style.display == 'none')
            element.style.display = 'block';

        daObj.targetElement = element;
        var curWidth = JSUI.GetElementWidth(daObj.targetElement.id);
        var newWidth = (curWidth * 1) - 10;

        if (newWidth <= 0) {
            daObj.targetElement.style.display = 'none';
            daObj.OnShrinkLeftComplete();
            return;
        } else {
            daObj.targetElement.style.width = newWidth + "px";
        }

        setTimeout(daObj.ShrinkLeft, 10);
    };

    this.ShrinkOut = function (elementOrId) {
        var element = JSUI.GetElement(elementOrId);

        daObj.targetElement = element;
        daObj.Shrink();
    };

    this.GrowIn = function (elementOrId) {
        var element = JSUI.GetElement(elementOrId);
        element.style.width = "0px";
        element.style.height = "0px";
        if (element.style.display == 'none')
            element.style.display = 'block';

        daObj.targetElement = element;
        daObj.Grow();
    };
}

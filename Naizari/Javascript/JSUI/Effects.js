if (JSUI === undefined || JSUI === null)
    alert("The core JSUI.js file was not loaded");

JSUI.addConstructor("Color", function() {
    var hexNums = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"];
    var self = this;

    var toHex = function(rgOrb) {
        if (rgOrb < 0 || rgOrb > 255)
            throw new JSUI.Exception("Value must be from 0 to 255: " + rgOrb);

        var left = hexNums[Math.floor(rgOrb / 16)];
        var right = hexNums[rgOrb % 16];
        return left + right;
    }

    var toRGBInt = function(hexColor) {
        var hexColor = hexColor.toString();
        if (hexColor.length != 2 ||
            (hexColor.charAt(0).toUpperCase() > "F" || hexColor.charAt(1).toUpperCase() > "F"))
            throw new JSUI.Exception("Value must be a valid 2 character hex value 00 to FF");

        return parseInt(hexColor, 16);
    }

    var color = function(color) {
        this.Html = "";
        this.R = 255;
        this.G = 255;
        this.B = 255;

        var self = this;
        this.enact = function() {
            if (this.R > 255) this.R = 255;
            if (this.R < 0) this.R = 0;
            if (this.G > 255) this.G = 255;
            if (this.G < 0) this.G = 0;
            if (this.B > 255) this.B = 255;
            if (this.B < 0) this.B = 0;
            this.Html = "#" + toHex(this.R) + toHex(this.G) + toHex(this.B);
        }

        this.toString = function() {
            return this.Html;
        }

        this.toRGBString = function() {
            return 'rgb(' + this.R + ', ' + this.G + ', ' + this.B + ')';
        }

        this.moveToward = function(colorTwo, step) {
            return JSUI.Color.moveToward(self, colorTwo, step);
        }

        this.setColor = function(elementOrId, colorStyle) {
            var element = JSUI.GetElement(elementOrId);
            element.style[colorStyle] = this.toString();
        }

        this.setAnimationStyle = function(elementOrId, styleName) {
            self.setColor(elementOrId, styleName);
        }
        // init
        if (JSUI.isString(color) && color.startsWith("rgb")) {
            var values = color.toString().replace(/rgb\(/, '').replace(/\)/, '');
            var splitValues = values.split(",");
            var retVal = {};
            for (var i = 0; i < splitValues.length; i++) {
                JSUI.isNumberOrDie(splitValues[i]);
            }

            this.R = parseInt(splitValues[0]);
            this.G = parseInt(splitValues[1]);
            this.B = parseInt(splitValues[2]);
            this.enact();

        } else if (JSUI.isString(color) && (color.startsWith("#") || color.length == 6)) {
            // html color #000000
            if (color.startsWith("#"))
                color = color.substr(1, color.length - 1);
            this.R = toRGBInt(color.substr(0, 2));
            this.G = toRGBInt(color.substr(2, 2));
            this.B = toRGBInt(color.substr(4, 2));
            this.Html = "#" + color;
        } else if (JSUI.isObject(color)) {
            // color object
            JSUI.copyProperties(color, this);
            this.enact();
        }
    }

    this.from = function(c) {
        return new color(c);
    }

    this.moveToward = function(colorOne, colorTwo, step) {
        if (step === null || step === undefined)
            step = 10;

        var stepUp = step;
        var stepDown = -step;

        var clrOne = new color(colorOne);
        var clrTwo = new color(colorTwo);

        var rStep = clrTwo.R >= clrOne.R ? stepUp : -stepDown;
        var gStep = clrTwo.G >= clrOne.G ? stepUp : -stepDown;
        var bStep = clrTwo.B >= clrOne.B ? stepUp : -stepDown;

        clrOne.R = clrOne.R + rStep;
        if (rStep == stepUp && clrOne.R > clrTwo.R)
            clrOne.R = clrTwo.R;
        else if (rStep == stepDown && clrOne.R < clrTwo.R)
            clrOne.R = clrTwo.R;

        clrOne.G = clrOne.G + gStep;
        if (gStep == stepUp && clrOne.G > clrTwo.G)
            clrOne.G = clrTwo.G;
        else if (gStep == stepDown && clrOne.G < clrTwo.G)
            clrOne.G = clrTwo.G;

        clrOne.B = clrOne.B + bStep;
        if (bStep == stepUp && clrOne.B > clrTwo.B)
            clrOne.B = clrTwo.B;
        else if (bStep == stepDown && clrOne.B < clrTwo.B)
            clrOne.B = clrTwo.B;

        clrOne.enact();
        return clrOne;
    }
});

var Effects = {};
Effects.ImageSequenceClassInstances = {};

Effects.Color = JSUI.construct("Color");

Effects.setMouseCursor = function(elementOrId, strCursor) {
    var cursorName;
    if (strCursor == 'hand' || strCursor == 'pointer') {
        Effects.handCursor(elementOrId);
    } else {
        JSUI.GetElement(elementOrId).style.cursor = strCursor;
    }
    return JSUI;
}

Effects.SetMouseCursor = function(el, c) {
    Effects.setMouseCursor(el, c);
}

Effects.handCursor = function(elementOrId) {
    var pointer = "pointer";
    if (window.event)
        pointer = "hand";
    JSUI.GetElement(elementOrId).style.cursor = pointer;
    return JSUI;
}

Effects.SetHandCursor = function(el) {
    Effects.handCursor(el);
}

Effects.moveCursor = function(el) {
    return Effects.setMouseCursor(el, "move");
}

Effects.SetMoveCursor = function(elementOrId) {
    Effects.moveCursor(elementOrId);
}

Effects.defaultCursor = function(el) {
    return Effects.setMouseCursor(el, "default");
}

Effects.SetDefaultCursor = function(elementOrId){
    Effects.setMouseCursor(elementOrId, "default");
}

Effects.ImageSwapify = function(strFromImageElementOrId, imageName)
{
    if(!JSUI.Images[imageName] || !JSUI.Images[imageName + "over"]){
        throw new JSUI.Exception("The images for the requested image swap were not preloaded.  Use JSUI.PreLoadImage('" + imageName + "', <src path>) and JSUI.PreLoadImage('" + strToImageName + "over', <src path>)");
    }
                    
    var imageElement = JSUI.GetElement(strFromImageElementOrId);
    JSUI.AddEventHandler(imageElement, 
        function(){
            imageElement.src = JSUI.Images[imageName + "over"].src;
        },
        "mouseover"
    )
    
    JSUI.AddEventHandler(imageElement, 
        function(){
            imageElement.src = JSUI.Images[imageName].src;
        },
        "mouseout"
    )    
}

Effects.GetImageSequence = function(strDomId){
    return Effects.ImageSequenceClassInstances[strDomId];
}

/// This is intended to be an ImageSequence constructor
Effects.ImageSequenceClass = function(strDomId, boolClickThrough) {
    Effects.ImageSequenceClassInstances[strDomId] = this;
    var thisObj = this;
    var currentIndex = 0;
    var targetImg = JSUI.GetElement(strDomId);
    JSUI.handCursor(targetImg);

    this.Images = [];
    this.AddImage = function(strImageName, strSrc) {
        var imgObj = JSUI.PreLoadImage(strImageName, strSrc, false);
        thisObj.Images.push(imgObj);
    }

    this.Next = function() {
        currentIndex++;
        if (currentIndex == thisObj.Images.length)
            currentIndex = 0;
        return thisObj.Images[currentIndex];
    }

    this.ShowImage = function(strImageName) {
        var image = JSUI.Images[strImageName];
        targetImg.src = image.src;
    }

    this.CurrentIndex = function() {
        return currentIndex;
    }

    this.SetImageIndex = function(intIndex) {
        var image = thisObj.Images[intIndex];
        targetImg.src = image.src;
    }

    var showNext = function() {
        var nextImage = thisObj.Next();
        targetImg.src = nextImage.src;
    }

    this.EnableClick = function() {
        JSUI.AddEventHandler(targetImg,
            showNext,
            "click"
        )
    }

    this.DisableClick = function() {
        JSUI.removeEventHandler(targetImg, showNext, "click");
    }

    this.ShowNext = showNext;

    if (boolClickThrough) {
        this.EnableClick();
    }
}

Effects.setOpacity = function(elementOrId, oneToTen) {
    JSUI.isNumberOrDie(oneToTen);
    var value = parseInt(oneToTen);
    //    if (value < 1 || value > 10)
    //        throw new JSUI.Exception("Invalid opacity value specified (" + oneToTen + "), must be from 1 to 10.");
    var element = JSUI.GetElement(elementOrId);
    element.style.opacity = value / 10;
    element.style.filter = 'alpha(opacity=' + value * 10 + ')';
    return JSUI;
}

Effects.SetOpacity = function(elementOrId, oneToTen) {
    Effects.setOpacity(elementOrId, oneToTen);
}

JSUI.addConstructor("Fader", function() {
    // fader ctor
    var fader = function(options) {
        JSUI.throwIfNull(options.element, "options.element can't be null");
        targetElement = options.element;
        this.dir = 1;
        this.end = 10;
        this.fps = 12;
        this.speed = 1;
        this.timeout = 1000 / this.fps;
        this.callBack = null;
        this.start = 0;
        this.next = 0;

        JSUI.copyProperties(options, this);
        var self = this;
        this.enact = function() {
            JSUI.isNumberOrDie(self.end);
            JSUI.isNumberOrDie(self.fps);
            JSUI.isNumberOrDie(self.dir);
            JSUI.isNumberOrDie(self.start);
            JSUI.isNumberOrDie(self.next);
            if (self.end > 10 || self.end < 0)
                throw new JSUI.Exception("Invalid end opacity specified");

            if (self.dir != -1 && self.dir != 1)
                throw new JSUI.Exception("Invalid direction specified 1=in, -1=out");

            if (self.dir == -1) {
                self.start = 10;
                self.next = 10;
            }

            if (self.dir == 1) {
                self.start = 0;
                self.next = 0;
            }

            self.timeout = (1000 / self.fps) / self.speed;
        }

        var handle = null;
        this.doFade = function() {
            Effects.setOpacity(targetElement, self.next);
            if (handle !== null)
                clearTimeout(handle);

            if (!(self.next == self.end)) {
                self.next += self.dir;
                handle = setTimeout(self.doFade, self.timeout);
            } else {
                if (JSUI.isFunction(self.callBack))
                    self.callBack(targetElement);
            }
        }
    }
    // end fader ctor
    function getConfig(elementOrId, options) {
        var defaults = {
            element: JSUI.GetElement(elementOrId),
            fps: 12,
            end: 10,
            start: 0,
            speed: 1,
            callBack: function() { }
        }
        if (JSUI.isObject(options))
            JSUI.copyProperties(options, defaults);
        else if (JSUI.isNumber(options))
            defaults.fps = options;
        else if (JSUI.isFunction(options))
            defaults.callBack = options;

        return defaults;
    }

    this.fadeIn = function(elementOrId, options) {
        var defaults = getConfig(elementOrId, options);
        var f = new fader(defaults);
        f.enact();
        f.doFade();
        return JSUI;
    }

    this.fadeOut = function(elementOrId, options) {//fps, callBack) {
        var defaults = getConfig(elementOrId, options);
        defaults.dir = -1;
        defaults.start = 10;
        defaults.end = 0;
        var f = new fader(defaults);
        f.enact();
        f.doFade();
        return JSUI;
    }


});

Effects.Fader = JSUI.construct("Fader");
JSUI.Assimilate(Effects.Fader);//, JSUI);

Effects.transition = function(elementOrId, endStyles, options) {//fps, toggle) {
    var config = { fps: 12, toggle: false, callBack: function() { }, speed: 1 };
    if (JSUI.isNumber(options)) {
        config.fps = options;
    } else if (JSUI.isFunction(options)) {
        config.callBack = options;
    } else if (JSUI.isObject(options)) {
        JSUI.copyProperties(options, config);
    }

    var targetElement = JSUI.GetElement(elementOrId);
    targetElement.maxSize = {};
    var timeout = (1000 / config.fps) / config.speed;
    var callBack = config.callBack;

    if (config.toggle !== null && config.toggle !== undefined && config.toggle == true) {
        if (targetElement.toggleStart === null || targetElement.toggleStart === undefined) {
            targetElement.toggleStart = {};
            for (prop in endStyles) {
                if (prop.toString().toLowerCase().endsWith("color")) {
                    targetElement.toggleStart[prop] = JSUI.Color.from(targetElement.style[prop]);
                } else {
                    targetElement.toggleStart[prop] = JSUI.getStyleNum(targetElement, prop);
                }
            }
            targetElement.toggleEnd = endStyles;
        }
        endStyles = targetElement.toggleEnd;
    }

    var swapToggle = function(key) {
        var start = targetElement.toggleStart[key];
        var end = targetElement.toggleEnd[key];
        targetElement.toggleStart[key] = end;
        targetElement.toggleEnd[key] = start;
    }

    var isDone = function(doneKeys) {
        for (prop in doneKeys) {
            if (doneKeys[prop] == false)
                return false;
        }
        return true;
    }

    var doneKeys = {};
    JSUI.initProperties(endStyles, doneKeys, false, function(key, val) {
        targetElement.maxSize[key] = endStyles[key] > JSUI.getStyleNum(targetElement, key) ? endStyles[key] : JSUI.getStyleNum(targetElement, key);
    });
    var handle = null;
    var doTransition = function() {
        JSUI.forEach(endStyles, function(key, value) {
            key = key.pascalCase("-");
            if (doneKeys[key] != true) {
                if (key.toString().toLowerCase().endsWith("color")) {
                    var colorValue = JSUI.Color.from(value);
                    var newColor = JSUI.Color.from(targetElement.style[key]).moveToward(colorValue);
                    newColor.setColor(targetElement, key);
                    if (newColor.toString() == colorValue.toString()) {
                        if (config.toggle == true) {
                            swapToggle(key);
                        }
                        doneKeys[key] = true;
                    }
                } else {
                    var curVal = JSUI.getStyleNum(targetElement, key, value);
                    var endVal = value;
                    var changeNum = targetElement.maxSize[key] / config.fps;
                    var stepUp = changeNum;
                    var stepDown = -changeNum;
                    var step = curVal >= endVal ? stepDown : stepUp;

                    var newVal = (curVal * 1) + (step * 1);

                    if ((step == stepUp && newVal >= endVal) ||
                    step == stepDown && newVal <= endVal) {
                        JSUI.setStyleNum(targetElement, key, endVal);
                        if (config.toggle == true)
                            swapToggle(key);
                        doneKeys[key] = true;
                    } else {
                        JSUI.setStyleNum(targetElement, key, newVal);
                    }
                }
            }
        });

        if (handle !== null)
            clearTimeout(handle);

        if (!isDone(doneKeys)) {
            handle = setTimeout(doTransition, timeout);
        } else {
            if (config.callBack !== null) {
                config.callBack(targetElement);
            }
        }
    }

    doTransition();
}

JSUI.Assimilate(Effects);//, JSUI);


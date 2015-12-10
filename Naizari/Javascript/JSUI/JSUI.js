
if (window.JSUI === null ||
    window.JSUI === undefined) {

    window.JSUI = {};
    JSUI = window.JSUI;


    JSUI.HttpRequests = [];

    JSUI.NewHttpRequest = function() {
        var ret = false;
        if (typeof (XMLHttpRequest) != 'undefined') {
            ret = new XMLHttpRequest();
        }

        try {
            ret = new ActiveXObject("Msxml2.XMLHTTP");
        } catch (e) {
            try {
                ret = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (e) { }
        }
        JSUI.HttpRequests.push(ret);
        return ret;
    }

    JSUI.AsyncRequestClass = function(strUrl, funcCallback) {
        this.Url = strUrl;
        this.Callback = funcCallback;
        this.Verb = "GET";

        var thisObj = this;
        var request = null;
        this.Send = function(postData) {
            if (!thisObj.Url)
                throw new JSUI.Exception("Url was not specified.");

            JSUI.IsFunctionOrDie(thisObj.Callback);

            request = JSUI.NewHttpRequest();
            request.onreadystatechange = function() {
                if (request.readyState == 4 && request.status == 200) {
                    var responseText = request.responseText;
                    thisObj.Callback(responseText);
                }
            }
            request.open(thisObj.Verb, encodeURI(thisObj.Url), true);
            request.send(postData);
            return request;
        }

        this.Abort = function() {
            if (request)
                request.abort();
        }

        this.abort = function() {
            thisObj.Abort();
        }
    }

    JSUI.callFunctions = function(arrFunctions, arg, arrParams) {
        for (var i = 0; i < arrFunctions.length; i++) {
            if (JSUI.IsFunction(arrFunctions[i]))
                arrFunctions[i](arg, arrParams);
        }
    }

    JSUI.CallFunctions = function(arrFn, arg, arrParams) {
        JSUI.callFunctions(arrFn, arg, arrParams);
    } 
    
    // this is used primarily to allow "pre-invoke" functions on databoxes.
    JSUI.FireDelegate = function(arrFunctions) {
        for (var i = 0; i < arrFunctions.length; i++) {
            if (JSUI.IsFunction(arrFunctions[i])) {
                if (!arrFunctions[i]())
                    return false; // this will allow the processing of an invocation if a preinvoke returns false
            }
        }
    }
    
    JSUI.stackTrace = function () {
          var callstack = [];
          var isCallstackPopulated = false;
          try {
            i.dont.exist+=0; //doesn't exist- that's the point
          } catch(e) {
            if (e.stack) { //Firefox
              var lines = e.stack.split('\n');
              for (var i=0, len=lines.length; i<len; i++) {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                  callstack.push(lines[i]);
                }
              }
              //Remove call to stackTrace()
              callstack.shift();
              isCallstackPopulated = true;
            }
            else if (window.opera && e.message) { //Opera
              var lines = e.message.split('\n');
              for (var i=0, len=lines.length; i<len; i++) {
                if (lines[i].match(/^\s*[A-Za-z0-9\-_\$]+\(/)) {
                  var entry = lines[i];
                  //Append next line also since it has the file info
                  if (lines[i+1]) {
                    entry += " at " + lines[i+1];
                    i++;
                  }
                  callstack.push(entry);
                }
              }
              //Remove call to stackTrace()
              callstack.shift();
              isCallstackPopulated = true;
            }
          }
          if (!isCallstackPopulated) { //IE and Safari
            var currentFunction = arguments.callee.caller;
            while (currentFunction) {
              var fn = currentFunction.toString();
              var fname = fn.substring(fn.indexOf("function") + 8, fn.indexOf('')) || 'anonymous';
              callstack.push(fname);
              currentFunction = currentFunction.caller;
            }
          }
          return callstack;
    }


    JSUI.Exception = function(msg) {
        this.name = "JSUI.Exception";
        this.message = msg === null || msg === undefined || msg == "" ? "An Exception was thrown." : msg;
        this.message += "\r\n***** stack *****\r\n" + JSUI.stackTrace() + "\r\n***** end stack *****\r\n";
        this.toString = function() {
            return this.message;
        }
        if (jsLogger !== undefined && jsLogger !== null) {
            jsLogger.addErrorEntry({ msgSig: this.message });
        }
    }
    
    JSUI.throwException = function(msg) {
        throw new JSUI.Exception(msg);
    }

    JSUI.ThrowException = function(msg) {
        JSUI.throwException(msg);
    }
    
    JSUI.throwIfNull = function(o, msg) {
        if (typeof (o) == 'undefined')
            JSUI.ThrowException(msg);
    }

    JSUI.isNullOrUndef = function(o) {
        return (o === null || o === undefined);
    }

    JSUI.throwIfNullOrUndef = function(o, msg) {
        msg = JSUI.isNullOrUndef(msg) ? "The specified object was null or undefined" : msg;
        JSUI.isNullOrUndef(o) ? JSUI.throwException(msg) : "";
        return JSUI;
    }

    JSUI.ThrowIfNull = function(o, msg) {
        JSUI.throwIfNull(o, msg);
    }

    JSUI.GetDaysInMonth = function(month, year) {
        var dd = new Date(year, month, 0);
        return dd.getDate();
    }

    JSUI.getEvent = function(event) {
        var e = event;
        if (window.event) {
            e = window.event;
        }
        return e;
    }

    JSUI.GetEvent = function(e) {
        return JSUI.getEvent(e);
    }

    JSUI.getEventSource = function(event) {
        var e = JSUI.GetEvent(event);
        var returnElement;

        if (e.srcElement)
            returnElement = e.srcElement; // IE

        if (e.target)
            returnElement = e.target; // Firefox

        if (returnElement.nodeType != null && returnElement.nodeType != 'undefined' && returnElement.nodeType == 3)
            returnElement = returnElement.parentNode; //safari bug return the parent of the text node if text node was the srcElement/target

        return returnElement;
    }

    JSUI.GetEventSourceElement = function(evt) {
        return JSUI.getEventSource(evt);
    }

    JSUI.GetWebDirectory = function() {
        var path = document.location.href.toString().split("?")[0];
        return path.substr(0, path.lastIndexOf('/') + 1);
    }

    JSUI.GetFilePath = function() {
        return window.location.protocol + "//" + window.location.host + window.location.pathname;
    }

    JSUI.silarize = function(objectWithPowers, silar) {
        for (power in objectWithPowers) {
            if (JSUI.IsFunction(objectWithPowers[power])) {
                silar[power] = objectWithPowers[power];
            }
        }
    }

    JSUI.burglarize = function(objectWithProperties, burglar) {
        for (property in objectWithProperties) {
            if (!JSUI.IsFunction(objectWithProperties[property])) {
                burglar[property] = objectWithProperties[property];
            }
        }
    }

    JSUI.Silarize = function(o, s){
        JSUI.silarize(o, s);
    }

    JSUI.initProperties = function(src, target, val, transForm) {
        for (prop in src) {
            if (JSUI.isFunction(transForm)) {
                transForm(prop, src[prop]);
            }
            target[prop] = val;
        }
    }

    JSUI.Burglarize = function(o, b) {
        JSUI.burglarize(o, b);
    }

    JSUI.Assimilate = function(target) {
        JSUI.Silarize(target, JSUI);
        JSUI.Burglarize(target, JSUI);
    }

    JSUI.assimilate = function(src, dst) {
        if (dst === null || dst === undefined) {
            dst = JSUI;
        }
        JSUI.silarize(src, dst);
        JSUI.burglarize(src, dst);
    }

    JSUI.write = function(el, msg) {
        el = JSUI.getElement(el);
        el.appenChild(document.createTextNode(msg));
    }
    JSUI.FunctionIsInArray = function(funcPointer, arr) {
        // should probably do a JSUI.IsFunctionOrDie here.  Review further later.
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] === funcPointer) {
                return true;
            }
        }
        return false;
    }

    JSUI.isFunction = function(varToTest, boolThrowIfFalse) {
        if (JSUI.isNullOrUndef(varToTest))
            return false;
        if (typeof varToTest != 'function' && boolThrowIfFalse) {
            JSUI.ThrowException(varToTest.toString() + " is not a function");
        } else if (typeof varToTest != 'function') {
            return false;
        }

        return true;
    }

    JSUI.IsFunction = JSUI.isFunction;

    JSUI.classOf = function(o) {
        if (o === undefined) return "undefined";
        if (o === null) return "null";
        return {}.toString.call(o).slice(8, -1);
    }

    JSUI.isArray = function(o, boolThrowIfFalse) {
        var retVal = JSUI.classOf(o) == "Array";
        if (!retVal && boolThrowIfFalse) {
            JSUI.ThrowException(o.toString() + " is not an Array");
        } else if (!retVal) {
            return false;
        }

        return true;
    }

    JSUI.isArrayOrDie = function(o) {
        return JSUI.isArray(o, true);
    }

    JSUI.forEach = function(arr, callBack) {
        if (JSUI.isArray(arr) && arr.length == 0)
            return;
        for (prop in arr) {
            callBack(prop, arr[prop]);
        }
    }

    JSUI.isFunctionOrDie = function(funcPointer) {
        this.IsFunction(funcPointer, true);
    }

    JSUI.IsFunctionOrDie = function(funcPointer) {
        return JSUI.isFunctionOrDie(funcPointer);
    }

    JSUI.isIE6 = window.external && typeof window.XMLHttpRequest == "undefined";

    JSUI.isNumber = function (str) {
        if (JSUI.isFunction(str))
            return false;
        else if (JSUI.isObject(str))
            return false;

        var parsed = parseInt(str);
        var notNumber = parsed.toString() == "NaN";
        return (!notNumber);
    }

    JSUI.IsNumber = function(str) {
        return JSUI.isNumber(str);
    }

    JSUI.isObject = function(o) {
        return JSUI.classOf(o) == "Object";
    }

    JSUI.isObjectOrDie = function(o, msg) {
        if (msg === null || msg === undefined) {
            msg = o.constructor.toString() + " is not an Object";
        }
        if (!JSUI.isObject(o)) {
            throw new JSUI.Exception(msg);
        }
    }

    JSUI.isString = function(o) {
        return JSUI.classOf(o) == "String";
    }

    JSUI.valueUnlessNullOrUndef = function (val, valIfBlankNullOrUndef) {
        return JSUI.isNullOrUndef(val) ? valIfBlankNullOrUndef: val;
    }

    JSUI.isStringOrDie = function(o) {
        if (!JSUI.isString(o))
            throw new JSUI.Exception(o.toString() + " is not a string.");
    }

    JSUI.isNumberOrDie = function(num, msg) {
        var ret = JSUI.isNumber(num);
        if (!ret)
            JSUI.ThrowException(num.toString() + " is not a number" + msg !== null ? ":" + msg: "");
        return ret;
    }

    JSUI.copyProperties = function(src, dst) {
        for (prop in src) {
            if (dst[prop] !== undefined && dst[prop] !== null) {
                dst[prop] = src[prop];
            }
        }
    }

    JSUI.copyUnsetProperties = function(src, dst) {
        for (prop in src) {
            if (dst[prop] == undefined || dst[prop] == null) {
                dst[prop] = src[prop];
            }
        }
    }

    JSUI.isTrueOrDie = function(test) {
        if (!test)
            JSUI.throwException("Value was not true");
    }

    JSUI.toDictionary = function(keyProp, objArr) {
        JSUI.isArrayOrDie(objArr);
        var retVal = {};
        JSUI.forEach(objArr, function(i, value) {
            retVal[value[keyProp].toString()] = value;
        });
        return retVal;
    }

    JSUI.countKeys = function(o) {
        var retval = 0;
        for (prop in o) {
            retval++;
        }

        return retval;
    }
    
    String.prototype.trim = function() {
        return this.replace(/^\s+|\s+$/g, "");
    }

    String.prototype.ltrim = function() {
        return this.replace(/^\s+/, "");
    }

    String.prototype.rtrim = function() {
        return this.replace(/\s+$/, "");
    }

    String.prototype.startsWith = function(str) {
        return (this.match("^" + str) == str);
    }

    String.prototype.endsWith = function(str) {
        return (this.match(str + "$") == str);
    }

    String.prototype.checkIfBlank = function() {
        if (this.trim() == "") {
            return "&nbsp;";
        }
        return this;
    }

    String.prototype.isBlank = function() {
        return this.checkIfBlank() == "&nbsp;";
    }

    String.prototype.pascalCase = function(dlmtr) {
        var split = this.split(dlmtr);
        var retVal = "";
        for (var i = 0; i < split.length; i++) {
            var word = split[i];
            for (var ii = 0; ii < word.length; ii++) {
                if (i != 0 && ii == 0) {
                    retVal += word.charAt(ii).toString().toUpperCase();
                } else {
                    retVal += word.charAt(ii);
                }
            }
        }

        return retVal.toString();
    }

    String.prototype.keyValuesToObject = function(entrySep, kvSep) {
        var entries = this.split(entrySep);
        var retVal = {};
        JSUI.forEach(entries, function(k, v) {
            var keyValue = v.split(kvSep);
            JSUI.isTrueOrDie(keyValue.length == 2);
            retVal[keyValue[0]] = keyValue[1];
        });
        return retVal;
    }



    JSUI.arrayContains = function(arrayToCheck, value) {
        for (var i = 0; i < arrayToCheck.length; i++) {
            if (arrayToCheck[i] == value)
                return true;
        }

        return false;
    }

    JSUI.ArrayContains = function(arrayToCheck, value) {
        return JSUI.arrayContains(arrayToCheck, value);
    }

    JSUI.RandomLetter = function() {
        var chars = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];
        return chars[Math.floor(Math.random() * 26)]
    }

    JSUI.RandomNumber = function() {
        var nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];
        return nums[Math.floor(Math.random() * 10)]; 
    }

    JSUI.RandomBool = function() {
        return Math.random() > .5;
    }

    JSUI.randomString = function(intLength) {
        var ret = "";
        for (var i = 0; i < intLength; i++) {
            if (JSUI.RandomBool()) {
                ret += JSUI.RandomLetter();
            } else {
                ret += JSUI.RandomLetter().toUpperCase();
            }
        }
        return ret;
    }

    JSUI.GetRandomId = function(ln) {
        return JSUI.randomString(ln);
    }

    JSUI.RandomString = function(intLength) {
        return JSUI.GetRandomId(intLength);
    }
    
    
    var addStack = function(str) {
        return str + "\r\n***** stack *****\r\n" + JSUI.stackTrace() + "\r\n***** end stack *****\r\n";
    }
    JSUI.logInfo = function(msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addInfoEntry("JSUI.logInfo:: " + msg);
        }
    }

    JSUI.logWarn = function(msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addWarnEntry("JSUI.logWarn:: " + msg);
        }
    }

    JSUI.logError = function(msg, stack) {
        if (JSUI.isObject(jsLogger)) {
            msg = msg.toString(); // in case it's an object
            if (stack == true) {
                msg = addStack(msg);
            }
            jsLogger.addErrorEntry("JSUI.logError:: " + msg);
        }
    }
}

JSUI = window.JSUI;


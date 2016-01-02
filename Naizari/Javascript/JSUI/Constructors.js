if(!JSUI)
    alert("The core JSUI.js file was not loaded");
    
var Constructors = {};
Constructors.Ctors = {}; // ideally keyed by class name

Constructors.addConstructor = function(strClassName, ctor){
    Constructors.Ctors[strClassName] = ctor;
};

Constructors.construct = function(strClassName, arrParams){
    return new Constructors.Ctors[strClassName](arrParams);
};

JSUI.Assimilate(Constructors, JSUI);
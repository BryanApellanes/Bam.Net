/*
	Copyright © Bryan Apellanes 2015  
*/
var exampleViewModel = (function($, b, app){
    "use strict";

    function exampleViewModelCtor(element, application){
        var the = this,
            a = application;

        this.view = { // this object will be sent to the dust renderer
            items: [], // this is the typical pattern, the name "items" must match the dust section tag {#items}
            init: function(){
                // return initialization promise here
                // this can be used to initialize items using an ajax call; this must return a promise (deferred) or an exception will be thrown
            }
        };

        this.model = {
            init: function(){
                // initialization here
            },
            activate: function(scopeElement){
                // activation here
            }/*
            ,
            // other sdo model properties here will get set if the scope element has an itemscope attribute
            propOne: value,
            propTwo: value2
            // other functions will be attached to data-action="methodName"
            actionOne: function(){}
            */
        }
    }

    return exampleViewModelCtor;
})(jQuery, bam, _, bam.app("stickerize"));
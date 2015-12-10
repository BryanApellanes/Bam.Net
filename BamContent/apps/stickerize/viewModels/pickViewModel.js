/*
	Copyright © Bryan Apellanes 2015  
*/
var pickViewModel = (function($, b, app){
    "use strict";

    function pickViewModelCtor(element, application){
        var the = this,
            a = application;

        this.view = { // this object will be sent to the dust renderer
            items: [], // this is the typical pattern the name "items" must match the dust section tag {#items}
            init: function(){
                // return initialization promise here
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

    return pickViewModelCtor;
})(jQuery, bam, _, bam.app("stickerize"));
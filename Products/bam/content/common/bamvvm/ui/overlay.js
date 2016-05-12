var overlay = (function(){
    "use strict";

    function show(message){
        html("div", {style: 'position: absolute; width: 100%: height: 100%; background-color: #FF0000'}, message).renderIn($("body"));
    }

    return {
        show: show
    }
})();

module.exports = overlay;
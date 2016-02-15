/*
	Copyright © Bryan Apellanes 2015  
*/
var deferredView = (function($, _, d, b){
    "use strict";

    function activateAll(){
        $("[data-plugin=deferredView]").dataSet().dataSetPlugins();
    }

    function _deferredView(el, options){
        var viewName = $(el).dataSet().data("dataset").viewName,
            conf = {
                pollInterval: 2750 // two and three quarters seconds
            };

        $.extend(conf, options);

        function _render(d){
            if(d.Success){
                $(el).html(d.Data);
                if(!d.ContentReady){
                    _.delay(_deferredView, conf.pollInterval, el, options);
                }
            }
        }

        _.act("deferredView", "getContent", {name: viewName}, options)
            .done(_render)
            .fail(_render);
    }

    $.fn.deferredView = function(opts){
        return $(this).each(function () {
            var the = $(this);
            _deferredView(the, opts);
        });
    };

    $(function(){
        deferredView.activateAll();
    });

    return{
        activateAll: activateAll
    }
})(jQuery, _, dao, bam);

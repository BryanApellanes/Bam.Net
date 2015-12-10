var methodForms = (function($, _, b, w){
    "use strict";

    function activateAll(){
        $("[data-plugin=methodForm]").dataSet().dataSetPlugins();
    }

    function _methodForms(el){
        var conf = {
                className: "",
                methodName: "",
                layout: "Default"
            };

        $.extend(conf, $.dataSetOptions(el));

        function _render(d){
            $(el).html(d);
        }

        _.act("bam", "methodForm", conf, {dataType: "html"})
            .done(_render)
            .fail(_render);
    }

    $(function(){
        methodForms.activateAll();
    });

    $.fn.methodForm = function(){
        return $(this).each(function () {
            var the = $(this);
            _methodForms(the);
        });
    };
    return {
        activateAll: activateAll
    }
})(jQuery, _, bam, window || {});

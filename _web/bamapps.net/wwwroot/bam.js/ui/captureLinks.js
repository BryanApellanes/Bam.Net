(function(b, $, _){
    "use strict";

    /**
     * Capture all anchor tags that link to other pages within
     * the specified application and use in app navigation
     * rather than browser navigation to follow the links
     * @param appName
     */
    b.captureLinks = function(appName){
        var app = b(appName);
        $('a').off("click").on("click", function(ev){
            var $link = $(ev.target),
                path = $link.attr("href"),
                pageName = _.withoutExtension(path);

            if(app.pages[pageName]){
                app.navigateTo(pageName);
                _.preventDefault(ev);
                return false;
            }
        })
    };

    $(function(){
        if(_ !== undefined && _.mixin !== undefined){
            _.mixin(b);
        }
    })
})(bam, jQuery, _);
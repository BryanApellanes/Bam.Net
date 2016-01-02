/*
	Copyright © Bryan Apellanes 2015  
*/
var stickerizeeViewModel = (function ($, b, s) {
    "use strict";

    function stickerizeeViewModel(el, app) {
        var the = this,
            a = app;

        this.view = {
            items: [],
            init: function () {
                var the = this;
                return $.Deferred(function () {
                    var prom = this;
                    s.getStickerizees().then(function (r) {
                        the.items = r;
                        prom.resolve();
                    })
                }).promise();
            }
        };

        this.model = {
            stickerize: function(n, o, ev){
                var val = n.raw;
                a.pages["stickerizables"].stickerizee = val;
                a.transitionToPage("stickerizables", val);
            },
            remove: function (n, o, ev) {
                var val = n.raw;
                s.removeStickerizee(val.Id).then(function(){
                    a.refresh("#stickerizees");
                })
            }
        }
    }

    return stickerizeeViewModel;

})(jQuery, bam, stickerize);



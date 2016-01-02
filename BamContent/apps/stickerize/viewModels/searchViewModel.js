/*
	Copyright © Bryan Apellanes 2015  
*/
var searchViewModel = (function ($, b, s, _) {
    "use strict";

    function searchViewModel(el, app) {
        var the = this,
            scope = $(el),
            theApp = app;

//        this.view = {
//            stickerizers: [], // results of the search for stickerizers
//            stickerizees: [], // results of the search for stickerizees
//            init: function () {
//                var prom = $.Deferred(function () {
//
//                });
//
//                return prom.promise();
//            }
//        };

        this.model = _.observe({
            stickerizeeQuery: "Find stickerizees",
            stickerizerQuery: "Find stickerizers",
            // when actions are attached then executed the framework will
            // send the new item and the old item along with the original event.
            // Since in this implementation this.model is defined as an
            // observable (see line 9 above; _.observe) the old value
            // is an observable and will have the new values
            searchStickerizers: function(sdoItem, obs, ev){
                var query = obs.stickerizerQuery();
                if(query.length > 2){
                    s.searchStickerizers(query).done(function(arr){
                        //a.refresh("#stickerizers");
                    });
                }
            },
            searchStickerizees: function(sdoItem, obs, ev){
                var query = obs.stickerizeeQuery();
                if(query.length > 2){
                    s.searchStickerizees(query).done(function(arr){
                        var tmp = document.createElement("div");
                        theApp.view("claimStickerizeeSearchResults", {items: arr}, tmp).done(function(){
                            $("#stickerizees").html($(tmp).html());
                        })
                    });
                }
            }
        });
    }

    return searchViewModel;
})(jQuery, bam, stickerize, _);
/*
	Copyright © Bryan Apellanes 2015  
*/
var stickerizableListsViewModel = (function ($, b, sys, izables) {
    "use strict";

    function stickerizableListsViewModel(el) {
        this.view = {
            items: [],
            init: function () {
                var the = this;
                return $.Deferred(function () {
                    var prom = this;
                    sys.getStickerizableList(izables.currentListId).then(function(r){
                        the.items = r;
                        prom.resolve();
                    });
                }).promise();
            }
        };

        this.model = {
            init: function(){

            },
            stickerize: function(o, n, ev){
                var ds = ev.currentTarget.dataset,
                    app = b.app("stickerize"),
                    page = app.pages[app.currentPage],
                    date = page.getSelectedDate();

                sys.stickerize(date, ds.id, page.stickerizee.Id).done(function(r){
                    if(!r.Success){
                        izables.setMessage(r.Message || r, "danger");
                    }

                    izables.setStickerizations(page);
                });
            },
            unstickerize: function(o, n, ev){
                var ds = ev.currentTarget.dataset,
                    app = b.app("stickerize"),
                    page = app.pages[app.currentPage],
                    date = page.getSelectedDate();

                sys.unstickerize(date, ds.id, page.stickerizee.Id).done(function(r){
                    if(!r.Success){
                        izables.setMessage()
                    }

                    izables.setStickerizations(page);
                });
            }
        }
    }

    return stickerizableListsViewModel;
})(jQuery, bam, stickerize, stickerizables); // stickerizables is defined in stickerizables.js
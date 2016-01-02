/*
	Copyright © Bryan Apellanes 2015  
*/
var addListViewModel = (function ($, b, s) {
    "use strict";
    /**
     */
    function addListViewModelCtor(el, app) {
        var the = this,
            a = app;
        this.view = {

            //init: function () {

            //}
        };

        this.model = _.observe({
            name: "",
            add: function () {
                s.addList(the.model.name()).then(function(){
                    a.refresh($("#pickList"));
                });
            }
        });
    }

    return addListViewModelCtor;
})(jQuery, bam, stickerize);
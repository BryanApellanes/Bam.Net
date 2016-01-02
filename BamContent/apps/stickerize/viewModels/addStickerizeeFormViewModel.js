/*
	Copyright © Bryan Apellanes 2015  
*/
var addStickerizeeFormViewModel = (function ($, b, s) {
    "use strict";
    /**
    */
    function addStickerizeeFormViewModelCtor(el, app) {
        var the = this,
            a = app;
        this.view = {

            //init: function () {

            //}
        };

        this.model = _.observe({
            name: "",
            gender: "Male",
            add: function () {
                var name = the.model.name(),
                    gender = the.model.gender();

                s.addStickerizee(name, gender).then(function(){
                    a.refresh($("#stickerizees"));
                });
            }
        });
    }

    return addStickerizeeFormViewModelCtor;
})(jQuery, bam, stickerize);

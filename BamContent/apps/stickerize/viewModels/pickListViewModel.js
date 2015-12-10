/*
	Copyright © Bryan Apellanes 2015  
*/
var pickListViewModel = (function($, b, _, app, s, izables){
    "use strict";

    function pickListViewModelCtor(element, application){
        var the = this,
            a = application;

        this.view = { // this object will be sent to the dust renderer
            items: [], // this is the typical pattern; the name "items" must match the dust section tag {#items}
            init: function(){
                // return initialization promise here
                var the = this;
                return $.Deferred(function(){
                    var prom = this; // the deferred object
                    s.getStickerizableLists().then(function (r) {
                        the.items = r;
                        prom.resolve();
                    });
                }).promise();
            }
        };

        function getDataSet(event){
            var element = event.currentTarget;
            return $(element).data("dataset");
        }

        this.model = {
            init: function(){
                // initialization here
            },
            activate: function(scopeElement){
                // activation here
            },
            selectList: function(newModel, oldModel, event){
                var dataSet = getDataSet(event);
                izables.currentListId = dataSet.id;
                app.navigateTo("stickerizables");
            },
            addSubSection: function(newModel, oldModel, event){
                var dataSet = getDataSet(event),
                    listId = dataSet.listId,
                    subSectionName = $("#addSubSectionTo" + listId).val();
                s.addSubsection(listId, subSectionName).then(function(){
                    a.refresh($("#pickList"));
                })
            },
            addStickerizable: function(newModel, oldModel, event){
                var dataSet = getDataSet(event),
                    subSectionId = dataSet.subSectionId,
                    stickerizableName = $("#addStickerizableTo" + subSectionId).val();
                s.addStickerizable(subSectionId, stickerizableName).then(function(){
                    a.refresh($("#pickList"));
                })
            }
            /*
             ,
             // other sdo model properties here will get set if the scope element has an itemscope attribute
             propOne: value,
             propTwo: value2
             // other functions will be attached to data-action="methodName"
             actionOne: function(){}
             */
        }
    }

    return pickListViewModelCtor;
})(jQuery, bam, _, bam.app("stickerize"), stickerize, stickerizables);
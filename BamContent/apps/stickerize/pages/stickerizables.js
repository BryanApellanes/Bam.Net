/*
	Copyright © Bryan Apellanes 2015  
*/
var stickerizables=(function(app, sys){
    "use strict";

    function setMessage(msg, type){
        if(_.isUndefined(type)){
            type = "success";
        }
        var $msg = $("#message"),
            classes = ["success", "info", "warning", "danger"];
        _.each(classes, function(v){
            $msg.removeClass("panel-" + v);
        });
        $msg.addClass("panel-" + type);
        $("h3", $msg).text(msg);
    }

    function getFormattedDate(date){
        return (date.getMonth() + 1).toString() + "/" + date.getDate() + "/" + date.getFullYear();
    }
    function setDateVisibility(page){
        if(page.name != "stickerizables"){
            $("#stickerizeDate").hide();
        }else{
            $("#stickerizeDate").show();
        }
    }

    function showStickerize(id){
        $("button[data-id=" + id +"][data-action=stickerize]").removeClass("hidden").show();
        $("button[data-id=" + id +"][data-action=unstickerize]").addClass("hidden").hide();
    }

    function showUnstickerize(id){
        $("button[data-id=" + id +"][data-action=stickerize]").addClass("hidden").hide();
        $("button[data-id=" + id +"][data-action=unstickerize]").removeClass("hidden").show();
    }

    function resetStickerizeButtons(){
        $("input[type=hidden][name=stickerizableId]").each(function(i, el){
            showStickerize($(el).val());
        })
    }

    function setStickerizations(page){
        return $.Deferred(function(){
            var prom = this; // we're in the deferred
            if(!_.isUndefined(page.stickerizee) && !_.isNull(page.stickerizee)){
                var date = page.getSelectedDate(),
                    stickerizeeId = page.stickerizee.Id;

                sys.getStickerizations(date, stickerizeeId).done(function(r){
                    var $msg = $("#message");
                    if(!r.Success){
                        setMessage(r.Message, "danger");
                    }
                    resetStickerizeButtons();
                    page.stickerizations = {};
                    _.each(r.Result, function(v){
                        page.stickerizations[v.StickerizableId] = v;
                        if(v.IsUndone){
                            showStickerize(v.StickerizableId);
                        }else{
                            showUnstickerize(v.StickerizableId);
                        }
                    });
                    prom.resolve(page.stickerizations);
                });
            }else{
                prom.resolve({});
            }
        }).promise();
    }

    function setSelectedDate(date){ // gets attached to the stickerizables page
        this.selectedDate = date;
        this.selectedDateString = getFormattedDate(this.selectedDate);
        $("#stickerizeDate").html("&nbsp;&nbsp;&nbsp;" + new moment(date).format('MMMM Do YYYY'));
    }

    function getSelectedDate(){
        if(_.isUndefined(this.selectedDate)){
            this.setSelectedDate(new Date());
        }

        return this.selectedDate;
    }

    function getSelectedDateString(){
        if(_.isUndefined(this.selectedDateString)){
            this.setSelectedDate(new Date());
        }

        return this.selectedDateString;
    }

    $(document).ready(function(){
        app.pageActivated("stickerizables", function(page, stickerizee){
            page.stickerizee = stickerizee || page.stickerizee;
            if(_.isUndefined(page.setSelectedDate)){
                page.setSelectedDate = setSelectedDate;
            }

            if(_.isUndefined(page.getSelectedDate)){
                page.getSelectedDate = getSelectedDate;
            }

            if(_.isUndefined(page.selectedDate)){
                page.setSelectedDate(new Date());
            }

            if(_.isUndefined(page.getSelectedDateString)){
                page.getSelectedDateString = getSelectedDateString;
            }

            if(!_.isUndefined(page.stickerizee) && !_.isNull(page.stickerizee)){
                $("#toolBarTitle").text(page.stickerizee.Name);
            }else{
                app.navigateTo("stickerizees");
            }

        })
        .anyPageActivated(function(page, data){
            setDateVisibility(page);
        });

        var originalOnModelsAttached = app.onModelsAttached;
        app.onModelsAttached = function(){
            setStickerizations(this.pages[this.currentPage]);
            if(_.isFunction(originalOnModelsAttached)){
                originalOnModelsAttached();
            }
        }
    });

    return {
        setMessage: setMessage,
        setStickerizations: setStickerizations,
        setSelectedDate: function(date){
            var page = app.pages[app.currentPage];
            page.setSelectedDate(date);
            bam.promise(function(){
                app.renderViews(document, "stickerize");
            });
        },
        currentListId: 1
    }
})(bam.app("stickerize"), stickerize || {});

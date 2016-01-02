var templates = (function($,_,b){
    "use strict";

    var shared = {
        attachHandlers: function(eventName){
            var the = this;
            if(!_.isString(eventName)){
                eventName = "click";
            }
            if(!_.isUndefined(the.items) && !_.isUndefined(the.handlers)){
                _.each(this.items, function(v, i){
                    $("#" + v.id)[eventName](the.handlers[i]);
                })
            }
        },
        addItem: function(text, clickHandler){
            var the = this,
                id = _.randomString(6),
                items = the.items || [],
                handlers = the.handlers || [];

            items.push({id: id, text: text});
            handlers.push(clickHandler);
            the.items = items;
            the.handlers = handlers;
        },
        renderIn: function(selector){
            var the = this;
            return this.render().then(function(h){
                $(selector).html(h);
                the.attachHandlers();
            });
        }
    };
    /**
     * @constructor
     */
    function accordionModel(){
        this.id = _.randomString(6);
        this.sections = [];
        var the = this;

        $.extend(this, shared);

        this.addSection = function(title, content){
            the.sections.push({id: _.randomString(6), title: title, content: content});
        };

        this.render = function(opts){
            return _.render("accordion", the, opts);
        };

        this.getSection = function(i){
            var section = the.sections[i];
            return $("#" + section.id + "Content");
        }
    }

    /**
     * @constructor
     * @param title
     * @param content
     */
    function collapsibleModel(title, content){
        this.id = _.randomString(6);
        this.title = title;
        this.content = content;
        var the = this;

        $.extend(this, shared);

        this.render = function(opts){
            return _.render("collapsible", the, opts);
        };

        this.content = function(html){
            $("#" + the.id + "Content").html(html);
        };
    }

    /**
     * @constructor
     * @param name
     */
    function dropdownModel(name){
        this.name = name;

        var the = this;
        $.extend(this, shared);

        this.render = function(opts){
            return _.render("dropdown", the, opts);
        };
    }

    /**
     * @constructor
     */
    function buttongroupModel(){
        var the = this;

        $.extend(this, shared);

        this.render = function(opts){
            return _.render("buttonGroup", the, opts);
        };
    }

    /**
     * @constructor
     * @param date
     */
    function datepickerModel(date){
        var the = this,
            val = date || new Date(),
            changeHandlers = [];
        this.id = _.randomString(6);
        this.format = "mm-dd-yyyy";
        this.date = _.format("{0}-{1}-{2}", val.getMonth(), val.getDate(), val.getFullYear());

        this.render = function(opts){
            return _.render("datepicker", the, opts);
        };

        this.renderIn = shared.renderIn;

        this.attachHandlers = function(){
            var dp = $("#" + the.id).datepicker();
            _.each(changeHandlers, function(v){
                dp.on("changeDate", v);
            })
        };

        this.changeDate = function(handler){
            changeHandlers.push(handler);
        };

        this.show = function(){
            $("#" + the.id).datepicker("show");
        };

        this.hide = function(){
            $("#" + the.id).datepicker("hide");
        }
    }

    return {
        /**
         * @constructor
         */
        accordionModel: accordionModel,
        /**
         * @constructor
         */
        collapsibleModel: collapsibleModel,
        /**
         * @constructor
         */
        dropdownModel: dropdownModel,
        buttongroupModel: buttongroupModel,
        datepickerModel: datepickerModel
    }
})(jQuery, _, bam);
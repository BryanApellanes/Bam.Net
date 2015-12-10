var templates = (function($,_,b){
    "use strict";

    var shared = {
        attachHandlers: function(eventName){
            var the = this;
            if(!_.isString(eventName)){
                eventName = "click";
            }
            if(!_.isUndefined(the.items) && !_.isUndefined(the.handlers)){
                _.each(the.items, function(v, i){
                    $("#" + v.id).off(eventName).on(eventName, the.handlers[i]);
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
    //function modalModel() {
    //    this.id = _.randomString(6);
    //    this.body = function () {
    //        return "monkey";
    //    }
    //}
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
            changeHandlers = [],
            rendered = false;
        this.id = _.randomString(6);
        this.format = "mm-dd-yyyy";
        this.date = _.format("{0}-{1}-{2}", val.getMonth(), val.getDate(), val.getFullYear());

        function throwIfNotRendered(){
            if(!rendered){
                throw new Error(_.format("datepicker {0} hasn't been rendered yet", the.id));
            }
        }

        this.render = function(opts){
            rendered = true;
            return _.render("datepicker", the, opts);
        };

        this.renderIn = shared.renderIn;

        this.attachHandlers = function(){
            var dp = $("#" + the.id).datepicker();
            _.each(changeHandlers, function(v){
                dp.off("changeDate").on("changeDate", v);
            })
        };

        this.changeDate = function(handler){
            if(!rendered){
                changeHandlers.push(handler);
            }else{
                the.attachHandlers();
            }
        };

        this.show = function(){
            throwIfNotRendered();
            $("#" + the.id).datepicker("show");
        };

        this.hide = function(){
            throwIfNotRendered();
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
        /**
         *  @constructor
         */
        buttongroupModel: buttongroupModel,
        /**
         *  @constructor
         */
        datepickerModel: datepickerModel
    }
})(jQuery, _, bam);
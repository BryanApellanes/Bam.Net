/*
	Copyright © Bryan Apellanes 2015  
*/
var validatable = (function($, _, b, s){
    "use strict";


    function validatable(el){
        var element = el,
            theMessages = _.clone(messages),
            theRules = _.clone(rules),
            the = this;

        $(el).data("validatable", the);

        this.results = {};

        this.validateObject = function(data) {
            var results = {
                messages: {},
                messageElements: {},
                success: true
            };
            _.each(data, function (value, propertyName) { // value, propertyName
                var valueElement = $("[itemprop=" + propertyName + "]", element)[0],
                    elementId = $(valueElement).attr("id"),
                    opts = $.dataSetOptions(valueElement),
                    labelElement = $("[for=" + elementId + "]"),
                    label = labelElement.text() || propertyName;

                results.messageElements[propertyName]= $("[data-validation-message-for=" + elementId + "]");
                opts.required = opts.required || !_.isUndefined($(valueElement).attr("required"));

                _.each(opts, function (ruleParam, ruleName) {
                    if (!_.isUndefined(theRules[ruleName])) {
                        var valid = theRules[ruleName](value, ruleParam),
                            msg = opts[ruleName + "Msg"],
                            failed = false;

                        if(value == "" || value == null || value == undefined){
                            if(opts.required){
                                if (!valid) {
                                    failed = true;
                                }
                            }
                        }else if(value.length > 0){
                            if(!valid){
                                failed = true;
                            }
                        }

                        if(_.isUndefined(msg) && _.isString(ruleParam) &&
                            (opts.required || opts.email || opts.url || opts.creditCard)){
                            msg = ruleParam;
                        }

                        if(failed){
                            msg = msg ? _.format(msg, label) : false;
                            if(_.isUndefined(results.messages[propertyName])){
                                results.messages[propertyName] = msg || theMessages[ruleName](label, ruleParam);
                            }
                            results.success = false;
                        }
                    }
                })
            });

            the.results = results;
            return results;
        };

        this.hideMessages = function(){
            if(the.results && the.results.messageElements){
                _.each(the.results.messageElements, function(message, propertyName) {
                    the.results.messageElements[propertyName].hide();
                });
            }
        };
        this.showMessages = function(effect){
            if(the.results && the.results.messages){
                the.hideMessages();
                _.each(the.results.messages, function(message, propertyName){
                    var msgElement = the.results.messageElements[propertyName];
                    if(!_.isUndefined(effect)){
                        msgElement.text(message).show(effect);
                    }else{
                        msgElement.text(message).show();
                    }
                });
            }
        };
        
        this.validate = function(){
            var data = s.getItem(element);
            the.results = the.validateObject(data.raw);
            return the.results;
        };

        this.isValid = function(){
            return the.validate().success;
        };

        this.setRule = function(rn, msg, fn){
            if(_.isFunction(msg)){
                fn = msg;
                msg = "{0} failed validation";
            }
            theRules[rn] = fn;
            theMessages[rn] = function(label){
                return _.format(msg, label);
            }
        }
    }

    function enableValidation(el){
        return new validatable(el);
    }

    function getMinMax(ruleValue){
        var split = ruleValue.split(","),
            min = Number(split[0]),
            max = Number(split[1]);

        return {min: min, max: max};
    }

    var rules = {
        rangeLength: function(value, ruleParam){
            var minMax = getMinMax(ruleParam),
                minLength = minMax.min,
                maxLength = minMax.max;

            if(_.isNaN(minLength)){
                throw { message: "Invalid minLength specified" };
            }
            if(_.isNaN(maxLength)){
                throw { message: "Invalid maxLength specified" };
            }

            return value.length < maxLength && value.length > minLength;
        },
        minLength: function(value, ruleParam){
            var min = Number(ruleParam);
            if(_,isNaN(min)){
                throw { message: "Invalid minLength specified: " + ruleParam };
            }
            return value.length >= min;
        },
        maxLength: function(value, ruleParam){
            var max = Number(ruleParam);
            if(_.isNaN(max)){
                throw { message: "Invalid maxLength specified: " + ruleParam };
            }
            return value.length <= max;
        },
        email: function(value){
            return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/i.test(value);
        },
        url: function(value){
            return /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(value);
        },
        creditCard: function(value){
            // accept only spaces, digits and dashes
            if (/[^0-9 -]+/.test(value))
                return false;
            var nCheck = 0,
                nDigit = 0,
                bEven = false;

            value = value.replace(/\D/g, "");

            for (var n = value.length - 1; n >= 0; n--) {
                var cDigit = value.charAt(n),
                    nDigit = parseInt(cDigit, 10);
                if (bEven) {
                    if ((nDigit *= 2) > 9)
                        nDigit -= 9;
                }
                nCheck += nDigit;
                bEven = !bEven;
            }

            return (nCheck % 10) == 0;
        },
        required: function(value) {
            if(_.isUndefined(value) || _.isNull(value)){
                return false;
            }
            return $.trim(value).length > 0;
        }
    };

    var messages = {
        rangeLength: function(label, ruleParam){
            var minMax = getMinMax(ruleParam);
            return label + " must be between " + minMax.min + " and " + minMax.max;
        },
        minLength: function(label, ruleParam){
            return label + " must be at least " + ruleParam + " characters long";
        },
        email: function(label){
            return "Please enter a valid email address for " + label;
        },
        url: function(label){
            return "Please enter a valid url for " + label;
        },
        creditCard: function(label){
            return "Please enter a valid credit card number for " + label;
        },
        required: function(label){
            return label + " is required";
        }
    };

    $.fn.validatable = function(){
        return $(this).each(function(){
            enableValidation($(this)[0]);
        })
    };

    return {
        enableValidation: enableValidation
    }
})(jQuery, _, bam, sdo);
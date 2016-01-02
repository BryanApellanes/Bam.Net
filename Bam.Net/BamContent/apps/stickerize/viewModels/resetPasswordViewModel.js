/*
	Copyright © Bryan Apellanes 2015  
*/
var resetPasswordViewModel = (function($, b, app){
    "use strict";

    function resetPasswordViewModelCtor(element, application){
        var the = this,
            a = application;


        this.model = {
            init: function(){
                // initialization here
            },
            activate: function(scopeElement){
                // activation here
            },
            resetPassword: function(newModel, oldModel, event){
                var inputData = {},
                    validator = $("#passwordResetPageElement").data("validatable"),
                    validation = validator.validate();

                _.each(["#passwordHelp", "#confirmPasswordHelp"], function(val,i){
                    $(val).text("");
                });
                if(!validation.success){
                    _.each(validation.messages, function(val, name){
                        $("#" + name + "Help").text(val);
                    })
                }else{
                    if(newModel.password !== newModel.confirmPassword){
                        $("#resetPasswordMessage").text("Passwords don't match");
                    }else{
                        var passHash = _.sha1(newModel.password),
                            token = _.getQueryString("token");

                        user.resetPassword(passHash, token)
                            .done(function(r){
                                if(r.Success){
                                    a.navigateTo("message",
                                        {
                                            Message: "Your password was successfully reset",
                                            Header: "Password Reset Successful",
                                            Buttons: [
                                                {
                                                    text: "feed",
                                                    action: function(){
                                                        a.navigateTo("home");
                                                    }
                                                },
                                                {
                                                    text: "stickerizables",
                                                    action: function(){
                                                        a.navigateTo("stickerizables");
                                                    }
                                                },
                                                {
                                                    text: "stickerizees",
                                                    action: function(){
                                                        a.navigateTo("stickerizees");
                                                    }
                                                }
                                            ]
                                        });
                                }else{
                                    $("resetPasswordMessage").text(r.Message);
                                }
                            })
                            .fail(function(){
                                log.addEntry("CLIENT::An error occurred resetting password", 2);
                                $("#resetPasswordMessage").text("Zoinks!  Something went wrong.  Our engineers have been notified, please try again later")
                            })
                    }
                }

                _.preventDefault(event);
            }/*
             ,
             // other sdo model properties here will get set if the scope element has an itemscope attribute
             propOne: value,
             propTwo: value2
             // other functions will be attached to data-action="methodName"
             actionOne: function(){}
             */
        }
    }

    return resetPasswordViewModelCtor;
})(jQuery, bam, _, bam.app("stickerize"));
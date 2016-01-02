/*
	Copyright © Bryan Apellanes 2015  
*/
var forgotPasswordViewModel = (function($, b, app){
    "use strict";

    function forgotPasswordViewModelCtor(element, application){
        var the = this,
            a = application;

        this.model = {
            // these get called synchronously
            init: function(){
                // initialization here
            },
            activate: function(scopeElement){
                // activation here
            },
            // --

            forgotPassword: function(newModel, oldModel, event){
                var emailAddress = newModel.raw.emailAddress,
                    $msg = $("#forgotPasswordMessage");
                if(emailAddress == null || emailAddress == ""){
                    $msg.addClass("error").text("Please enter your email address");
                }else{
                    user.forgotPassword(newModel.raw.emailAddress)
                        .done(function(r){
                            if(r.Success){
                                a.navigateTo("message", {Message:"A password reset email has been sent to (" + emailAddress + ")" });
                            }else{
                                $msg.addClass("error").text(r.Message);
                            }
                        })
                        .fail(function(){
                            log.addEntry("CLIENT::An error occurred requesting password reset email", 2);
                            $msg.addClass("error").text("Zoinks!  Something went wrong.  Our engineers have been notified, please try again later");
                        });
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

    return forgotPasswordViewModelCtor;
})(jQuery, bam, _, bam.app("stickerize"));
/*
	Copyright © Bryan Apellanes 2015  
*/
var loginViewModel = (function($, b, app){
    "use strict";

    return function loginViewModelCtor(element, application){
        var the = this,
            a = application;

        this.model = {
            // bam framework will call these if they're defined
            init: function(){
                // initialization here
            },
            activate: function(scopeElement){
                // activation here
            },
            // -- end bam framework will call these if they're defined

            login: function(newModel, oldModel, event){
                var inputData = {};
                _.extend(inputData, newModel.raw);
                var passHash = _.sha1(inputData.password);

                user.login(inputData.emailAddress, passHash)
                    .done(function(r){
                        if(r.Success){
                            _.setItem("#loginElement", {emailAddress: "", userName: "", password: ""});
                            $("#mainLoginLinks").addClass("hidden").hide();
                            $("#mainSignOutLink").removeClass("hidden").show();

                            a.navigateTo("stickerizees");
                        }else{
                            $("#loginMessage").text(r.Message);
                        }
                    })
                    .fail(function(){
                        $("#loginMessage").text("login failed");
                    });

                _.preventDefault(event);
            },
            signUp: function(newModel, oldModel, event){
                var inputData = {},
                    validator = $("#loginElement").data("validatable"),
                    validation = validator.validate();

                _.each(["#emailAddressHelp", "#userNameHelp", "#passwordHelp"], function(val, i){
                    $(val).text("");
                });

                if(validation.success){
                    _.extend(inputData, newModel.raw);
                    var passHash = _.sha1(inputData.password);
                    // user is a proxy to the UserManager on the server
                    user.signUp(inputData.emailAddress, inputData.userName, passHash, true)
                        .done(function(r){
                            if(r.Success){
                                a.navigateTo("message", {
                                    Header: "Sign Up Successful",
                                    Message: "You should receive an email shortly to activate your account.  Check your email and click the link.  You can close this window."
                                });
                            }else{
                                $("#loginMessage").text(r.Message);
                            }
                        })
                        .fail(function(r){
                            $("#message").text(r.Message);
                        });
                }else{
                    _.each(validation.messages, function(val, name){
                        $("#" + name + "Help").text(val);
                    });
                }

                _.preventDefault(event);
            },
            forgotPassword: function(newModel, oldModel, event){
                a.navigateTo("auth/forgotPassword");
                _.preventDefault(event);
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
})(jQuery, bam, _, bam.app("stickerize"));
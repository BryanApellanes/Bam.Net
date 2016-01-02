/*
	Copyright © Bryan Apellanes 2015  
*/
var init = (function($, b){
    "use strict";
    var app;

    function setUser(){
        user.getUser().done(function(u){
            var $userNameSpan = $("#mainUserName");

            $userNameSpan.text("");
            if(u.isAuthenticated){
                $userNameSpan.text(u.userName);

                $("#mainLoginLinks").addClass("hidden").hide();
                $("#mainLoggedInLinks").removeClass("hidden").show();
            }
        })
    }

    function signOut(){
        user.signOut().then(function(r){
            cookies.clear("SESS");
            var msg = r.Message;
            if(r.Success){
                msg = r.Data;
            }

            app.navigateTo("message",
                {
                    Header: msg,
                    then: function(){
                        setTimeout(function(){
                            $("#messageHeader").text("Redirecting...");
                            window.location = "/";
                        }, 2500)
                    }
                });
            $("#mainUserName").text("");
            $("#mainLoginLinks").removeClass("hidden").show();
            $("#mainLoggedInLinks").addClass("hidden").hide();
            $("#stickerizees").empty();
        });
    }

    function commonActivationHandler(app){
        $("#mainLoginLink").off("click").on("click", function(ev){
            b.app("stickerize").navigateTo("auth/login", {state: "initial"});
            _.preventDefault(ev);
        });

        $("#mainSignUpLink").off("click").on("click", function(ev){
            b.app("stickerize").navigateTo("auth/login", {state: "signUp"});
            _.preventDefault(ev);
        });

        $("#mainSignOutLink").off("click").on("click", function(ev){
            signOut(app);
            _.preventDefault(ev);
        });

        $("#stickerizableNav").hide();
        setUser();
    }

    $(document).ready(function () {
        app = bam.app("stickerize");

        app.pageCreated = function(page){
//            if(page.name == "home"){
//                page.helloEffect = null;
//                page.goodByeEffect = null;
//            }
        };

        app.pageLoading = function(page){

        };

        app.pagesLoaded = function(){
//            var page = app.pages["home"];
//                page.helloEffect = "fade";
//                page.goodByeEffect = "fade";
        };

        app.setPageTransitionFilter("current", "next", function (tx, d) {
            // tx is the transitionHandler which looks like this
            // {
            //      name: <string>,
            //      from: <string>, // the name of the page the transition is from
            //      to: <string>, // the name of the page the transition is to
            //      play: function(data), // plays the transition passing in optional data
            //      also triggers start and end events before and after play
            // }
            // analyze the data d to determine if the transition will be allowed or
            // directly analyze the state of the dom.
            // return false to stop the transition from current to next page
        })
        .pageActivated("home", function (page, data) {
            commonActivationHandler(app);
            $("#jumbotronLoginLink").off("click").on("click", function(ev){
                b.app("stickerize").navigateTo("auth/login", {state: "initial"});
                _.preventDefault(ev);
            });

            $("#jumbotronSignUpLink").off("click").on("click", function(ev){
                b.app("stickerize").navigateTo("auth/login", {state: "signUp"});
                _.preventDefault(ev);
            });
        })
        .pageActivated("auth/login", function(page, data){
            commonActivationHandler(app);
            $(document).scrollTop(0);
            $("#loginPage").removeClass("hidden").show(page.getDefault);
            _.setItem("#loginElement", {emailAddress: "", userName: "", password: ""});
            if(!_.isUndefined(data) && data.state){
                if(page.currentState != data.state){
                    page.transitionTo(data.state);
                }
            }
        })
        .pageActivated("stickerizables", function(page, data){
            commonActivationHandler(app);
            $("#toolBar").show().removeClass("hidden").activate();
            $("#stickerizeDate").html("&nbsp;&nbsp;&nbsp;" + moment().format("LL"));
        })
        .pageActivated("tests", function(page, data){
            commonActivationHandler(app);
        })
        .pageActivated("auth/confirmAccount", function(page, data){
            commonActivationHandler(app);
            var token = _.getQueryString("token");
            if(!token){
                app.navigateTo("home");
            }else{
                user.confirmAccount(token)
                    .done(function(r){
                        var data = {Header: "Account Confirmed"};
                        if(!r.Success){
                            data = {Header: "Error", Message: r.Message};
                        }
                        data.Buttons =[
                            {
                                text: "login",
                                action: function(){
                                    app.navigateTo("auth/login");
                                }
                            }
                        ];
                        app.navigateTo("message", data);
                    })
                    .fail(function(){
                        app.navigateTo("message", {Header: "Zoinks!", Message: "An error occurred confirming your account"});
                    })
            }
        })
        .pageActivated("message", function(page, data){
                commonActivationHandler(app);
                if(data.Message){
                    $("#messageText").text(data.Message);
                }
                if(data.Header){
                    $("#messageHeader").text(data.Header);
                }
                if(data.Buttons){
                    if(_.isArray(data.Buttons)){
                        var buttons = new templates.buttongroupModel();
                        _.each(data.Buttons, function(buttonDef, i){
                            buttons.addItem(buttonDef.text, buttonDef.action);
                        });
                        $("#messageButtons").text("");
                        buttons.renderIn("#messageButtons");
                    }
                }else{
                    $("#messageButtons").empty();
                }

                if(_.isFunction(data.then)){
                    data.then();
                }
        });
    });

    return {

    }
})(jQuery, bam);



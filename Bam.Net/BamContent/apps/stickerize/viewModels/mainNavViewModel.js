/*
	Copyright © Bryan Apellanes 2015  
*/
function mainNavViewModel(el, app) {
    var model = new templates.navLeftModel();

    user.getUser().done(function (u) {
        model.addItem("feed", function () {
            app.navigateTo("feed");
        });

        if (u.isAuthenticated) {
            model.addItem("stickerize", function () {
                app.navigateTo("stickerizees");
            });
            model.addItem("stickerizables", function(){
                app.navigateTo("pickList");
            });

            $("#mainLoginLinks").addClass("hidden").hide();
            $("#mainSignOutLink").removeClass("hidden").show();
        }

        model.renderIn(el);
    });
}
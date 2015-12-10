/*
	Copyright © Bryan Apellanes 2015  
*/
function navButtonsViewModel(el, app) {
    var buttons = new templates.buttongroupModel();
    buttons.addItem("home", function(){
        app.navigateTo("home");
    });

    user.get().done(function (u) {
        if (u.isAuthenticated) {
            buttons.addItem("stickerizables", function () {
                app.navigateTo("stickerizables");
            });
            buttons.addItem("stickerizees", function () {
                app.navigateTo("stickerizees");
            });
        }

        buttons.renderIn(el);
    });
}